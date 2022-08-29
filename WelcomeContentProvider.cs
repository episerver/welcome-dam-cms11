using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Construction;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Logging.Compatibility;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Optimizely.Labs.WelcomeDAM.Models;
using Optimizely.Labs.WelcomeDAM.REST;
using Optimizely.Labs.WelcomeDAM.REST.Media;

namespace Optimizely.Labs.WelcomeDAM
{
    public class WelcomeContentProvider : ContentProvider
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IdentityMappingService identityMappingService;

        private readonly IWelcomeClient welcomeClient;

        private readonly IContentTypeRepository contentTypeRepository;

        private readonly IContentLoader contentLoader;

        private readonly IContentFactory contentFactory;

        private readonly IUrlSegmentGenerator urlSegmentGenerator;

        public override string ProviderKey => WelcomeConstants.ProviderKey;

        public WelcomeContentProvider(
            IWelcomeClient welcomeClient,
            IdentityMappingService identityMappingService,
            IContentTypeRepository contentTypeRepository,
            IContentFactory contentFactory,
            IContentLoader contentLoader,
            IUrlSegmentGenerator urlSegmentGenerator)
        {
            this.identityMappingService = identityMappingService;
            this.contentTypeRepository = contentTypeRepository;
            this.welcomeClient = welcomeClient;
            this.contentFactory = contentFactory;
            this.contentLoader = contentLoader;
            this.urlSegmentGenerator = urlSegmentGenerator;
        }

        protected override IContent LoadContent(ContentReference contentLink, ILanguageSelector languageSelector)
        {
            MappedIdentity mappedIdentity = identityMappingService.Get(contentLink);
            IContent currentContent = null;

            if (mappedIdentity == null)
            {
                return null;
            }

            try
            {
                var welcomeResourceId = (WelcomeResourceId)mappedIdentity.ExternalIdentifier;

                IContent assetData = null;
                switch (welcomeResourceId.Type)
                {
                    case WelcomeConstants.ImageIdentifier:
                        var imageItem = this.welcomeClient.GetAsset(welcomeResourceId.Id.ToString("N"));
                        assetData = CreateWelcomeImageFile(mappedIdentity, imageItem);
                        break;

                    case WelcomeConstants.ArticleIdentifier:
                        var articleItem = this.welcomeClient.GetArticle(welcomeResourceId.Id.ToString("N"));
                        assetData = CreateWelcomeArticleFile(mappedIdentity, articleItem);
                        break;

                    case WelcomeConstants.VideoIdentifier:
                        var videoItem = this.welcomeClient.GetVideo(welcomeResourceId.Id.ToString("N"));
                        assetData = CreateWelcomeVideoFile(mappedIdentity, videoItem);
                        break;

                    case WelcomeConstants.RawIdentifier:
                        var rawItem = this.welcomeClient.GetRawItem(welcomeResourceId.Id.ToString("N"));
                        assetData = CreateWelcomeRawFile(mappedIdentity, rawItem);
                        break;

                    case WelcomeConstants.FolderIdentifier:
                        var folderItem = this.welcomeClient.GetFolder(welcomeResourceId.Id.ToString("N"));
                        assetData = CreateWelcomeFolder(mappedIdentity, folderItem);
                        break;
                }

                if (assetData == null)
                {
                    throw new NullReferenceException(
                        $"welcomeResourceId {welcomeResourceId.Type} could not be determined");
                }

                this.AddContentToCache(assetData);

                return assetData;
            }
            catch (Exception ex)
            {
                this.log.Error("Error locating asset", ex);
            }
            return currentContent;
        }

        protected override IList<GetChildrenReferenceResult> LoadChildrenReferencesAndTypes(ContentReference contentLink, string languageID, out bool languageSpecific)
        {
            languageSpecific = false;
            var childrenList = new List<GetChildrenReferenceResult>();

            var mappedIdentity = this.identityMappingService.Get(contentLink);
            WelcomeResourceId contentResourceId = null;
            if (mappedIdentity != null)
            {
                contentResourceId = (WelcomeResourceId)mappedIdentity.ExternalIdentifier;
            }
            if (EntryPoint.CompareToIgnoreWorkID(contentLink) || (mappedIdentity != null && contentResourceId != null && contentResourceId.Type.Equals(WelcomeConstants.FolderIdentifier)))
            {
                FolderPageList folderPageList;
                if (EntryPoint.CompareToIgnoreWorkID(contentLink))
                {
                    // Top level folder, i.e. the entry point
                    folderPageList = this.welcomeClient.GetFolders();
                }
                else
                {
                    // Sub folders
                    folderPageList = this.welcomeClient.GetFolders(0, 100, new KeyValuePair<string, string>("parent_folder_id", contentResourceId.Id.ToString().Replace("-", string.Empty)));
                }

                if (folderPageList.Folders.Any())
                {
                    foreach (var folder in folderPageList.Folders)
                    {
                        var childAsset = new GetChildrenReferenceResult()
                        {
                            ContentLink = ConvertToMappedIdentity(new WelcomeResourceId(WelcomeConstants.FolderIdentifier, folder.Id)).ContentLink,
                            IsLeafNode = false,
                            ModelType = typeof(ContentAssetFolder)
                        };
                        childrenList.Add(childAsset);
                    }
                }

                MediaPageList assetPageList;
                if (contentResourceId == null)
                {
                    // Top level folder, i.e. the entry point so list everything
                    assetPageList = this.welcomeClient.GetAssets(0, 100, new KeyValuePair<string, string>("include_subfolder_assets", "false"));
                }
                else
                {
                    assetPageList = this.welcomeClient.GetAssets(0, 100, new KeyValuePair<string, string>("folder_id", contentResourceId.Id.ToString("N")));
                }

                if (assetPageList != null && assetPageList.Assets.Any())
                {
                    foreach (var asset in assetPageList.Assets)
                    {
                        var assetIdentity = ConvertToMappedIdentity(new WelcomeResourceId(asset.Type, asset.Id));
                        var childAsset = new GetChildrenReferenceResult()
                        {
                            ContentLink = assetIdentity.ContentLink,
                            IsLeafNode = true,
                            ModelType = GetMediaItemType(asset.Type)
                        };
                        childrenList.Add(childAsset);
                    }
                }
            }

            return childrenList;
        }

        protected override ContentResolveResult ResolveContent(ContentReference contentLink)
        {
            // Check to see fi this is our content
            if (contentLink.ProviderName != this.ProviderKey)
            {
                return null;
            }

            ContentResolveResult contentResolvedResult = new ContentResolveResult
            {
                ContentLink = contentLink
            };
            var content = LoadContent(contentLink, null);
            contentResolvedResult.UniqueID = content.ContentGuid;
            contentResolvedResult.ContentUri = ConstructContentUri(content.ContentTypeID, contentResolvedResult.ContentLink, contentResolvedResult.UniqueID);
            return contentResolvedResult;
        }

        protected override ContentResolveResult ResolveContent(Guid contentGuid)
        {
            var contentItem = this.identityMappingService.Get(contentGuid);
            if (contentItem == null)
            {
                return null;
            }

            ContentResolveResult contentResolvedType = new ContentResolveResult
            {
                ContentLink = contentItem.ContentLink
            };
            var content = LoadContent(contentResolvedType.ContentLink, null);
            contentResolvedType.UniqueID = contentGuid;
            contentResolvedType.ContentUri = ConstructContentUri(content.ContentTypeID, contentResolvedType.ContentLink, contentResolvedType.UniqueID);
            return contentResolvedType;
        }

        private IContent CreateWelcomeImageFile(MappedIdentity mappedIdentity, ImageItem imageItem)
        {
            var content = CreateAndAssignIdentity(mappedIdentity, typeof(WelcomeImageFile), imageItem);
            if (content is WelcomeImageFile welcomeImage)
            {
                welcomeImage.MimeType = imageItem.MimeType;
                welcomeImage.Description = imageItem.Description;
                welcomeImage.FileSize = imageItem.FileSize;
                if (imageItem.ImageResolution != null)
                {
                    welcomeImage.Width = imageItem.ImageResolution.Width;
                    welcomeImage.Height = imageItem.ImageResolution.Height;
                }
            }
            return content;
        }

        private IContent CreateWelcomeArticleFile(MappedIdentity mappedIdentity, ArticleItem articleItem)
        {
            var content = CreateAndAssignIdentity(mappedIdentity, typeof(WelcomeArticleFile), articleItem);
            if (content is WelcomeArticleFile welcomeArticle)
            {
                welcomeArticle.MetaTitle = articleItem.MetaTitle;
                welcomeArticle.MetaDescription = articleItem.MetaDescription;
                welcomeArticle.MetaKeywords = articleItem.MetaKeywords;
                welcomeArticle.ArticleSource = articleItem.SourceArticle;
                if (articleItem.Authors.Any())
                {
                    //welcomeArticle.Authors = articleItem.Authors.Select(x => x.Name).ToList();
                }
                if (articleItem.HtmlBody != null)
                {
                    welcomeArticle.HtmlBody = new XhtmlString(articleItem.HtmlBody);
                }
                if (!string.IsNullOrWhiteSpace(articleItem.MetaUrl))
                {
                    welcomeArticle.MetaUrl = new Url(articleItem.MetaUrl);
                }

                if (welcomeArticle is ILocalizable localizable)
                {
                    localizable.Language = new System.Globalization.CultureInfo(articleItem.LangCode);
                }
            }
            return content;
        }

        private IContent CreateWelcomeVideoFile(MappedIdentity mappedIdentity, VideoItem videoItem)
        {
            var content = CreateAndAssignIdentity(mappedIdentity, typeof(WelcomeVideoFile), videoItem);
            if (content is WelcomeVideoFile welcomeVideo)
            {
                welcomeVideo.MimeType = videoItem.MimeType;
                welcomeVideo.Description = videoItem.Description;
                welcomeVideo.FileSize = videoItem.FileSize;
            }
            return content;
        }

        private IContent CreateWelcomeRawFile(MappedIdentity mappedIdentity, RawItem rawItem)
        {
            var content = CreateAndAssignIdentity(mappedIdentity, typeof(WelcomeRawFile), rawItem);
            if (content is WelcomeRawFile welcomeRaw)
            {
                welcomeRaw.MimeType = rawItem.MimeType;
                welcomeRaw.Description = rawItem.Description;
                welcomeRaw.FileSize = rawItem.FileSize;
            }
            return content;
        }

        private IContent CreateWelcomeFolder(MappedIdentity mappedIdentity, Folder folderItem)
        {
            var content = CreateAndAssignIdentity(mappedIdentity, typeof(ContentAssetFolder), folderItem);
            return content;
        }

        private IContent CreateAndAssignIdentity(MappedIdentity mappedIdentity, Type contentItemType, BaseRestItem restItem)
        {
            // Find parent
            var parentLink = EntryPoint;

            if (restItem.FolderId.HasValue)
            {
                var parentFolderIdentity = ConvertToMappedIdentity(new WelcomeResourceId(WelcomeConstants.FolderIdentifier, restItem.FolderId.Value));
                if (parentFolderIdentity != null)
                {
                    parentLink = parentFolderIdentity.ContentLink;
                }
            }

            // Set content type and content type Id.
            var contentType = contentTypeRepository.Load(contentItemType);
            var content = contentFactory.CreateContent(contentType);
            content.ContentTypeID = contentType.ID;
            content.ParentLink = parentLink;
            content.ContentGuid = mappedIdentity.ContentGuid;
            content.ContentLink = mappedIdentity.ContentLink;
            content.Name = restItem.Title;
            if (content is WelcomeAssetData assetData)
            {
                assetData.AssetId = restItem.Id;
                assetData.FileLocation = restItem.FileLocation;

                if (restItem is MediaItem mediaItem)
                {
                    assetData.AssetType = mediaItem.Type;
                }

                if (restItem is IPublicWelcomeUrl welcomeUrl)
                {
                    assetData.Url = welcomeUrl.Url;
                }
            }
            if (content is IRoutable routable)
            {
                routable.RouteSegment = this.urlSegmentGenerator.Create(content.Name);
            }

            if (content is IContentSecurable securable)
            {
                securable.GetContentSecurityDescriptor().AddEntry(new AccessControlEntry(EveryoneRole.RoleName, AccessLevel.Read));
            }

            if (content is IVersionable versionable)
            {
                versionable.Status = VersionStatus.Published;
            }

            if (content is IChangeTrackable changeTrackable)
            {
                changeTrackable.Changed = DateTime.Now;
            }

            return content;
        }

        protected override IList<MatchingSegmentResult> ListMatchingSegments(ContentReference parentLink, string urlSegment)
        {
            var list = new List<MatchingSegmentResult>();

            bool languageSpecific = false;

            var children = LoadChildrenReferencesAndTypes(parentLink, null, out languageSpecific);

            foreach (var child in children)
            {
                var content = LoadContent(child.ContentLink, null);

                if (content is IRoutable && (content as IRoutable).RouteSegment.Equals(urlSegment, StringComparison.InvariantCultureIgnoreCase))
                {
                    list.Add(new MatchingSegmentResult() { ContentLink = content.ContentLink });
                }
            }

            return list;
        }

        protected override void SetCacheSettings(ContentReference contentReference, IEnumerable<GetChildrenReferenceResult> children, CacheSettings cacheSettings)
        {
            // Set a low cache setting so new items are fetched from data source, but keep the
            // items already fetched for a long time in the cache.
            //cacheSettings.CancelCaching = true;
            cacheSettings.SlidingExpiration = TimeSpan.Zero; //System.Web.Caching.Cache.NoSlidingExpiration;
            cacheSettings.AbsoluteExpiration = DateTime.Now.AddMinutes(2);

            base.SetCacheSettings(contentReference, children, cacheSettings);
        }

        protected override void SetCacheSettings(IContent content, CacheSettings cacheSettings)
        {
            cacheSettings.SlidingExpiration = TimeSpan.Zero; //System.Web.Caching.Cache.NoSlidingExpiration;
            cacheSettings.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            base.SetCacheSettings(content, cacheSettings);
        }

        private Type GetMediaItemType(string type)
        {
            switch (type)
            {
                case "image":
                    return typeof(WelcomeImageFile);

                case "article":
                    return typeof(WelcomeArticleFile);

                case "video":
                    return typeof(WelcomeVideoFile);

                case "raw":
                    return typeof(WelcomeRawFile);

                default:
                    return typeof(WelcomeGenericFile);
            }
        }

        private MappedIdentity ConvertToMappedIdentity(WelcomeResourceId resourceId)
        {
            var resourceUri = MappedIdentity.ConstructExternalIdentifier(ProviderKey, resourceId);
            var resourceIdentity = this.identityMappingService.Get(resourceUri, true);
            return resourceIdentity;
        }

        private string RemoveStartingSlash(string virtualPath) =>
            !string.IsNullOrWhiteSpace(virtualPath) && virtualPath[0] == '/' ? virtualPath.Substring(1) : virtualPath;

        private string RemoveEndingSlash(string virtualPath) =>
            !string.IsNullOrWhiteSpace(virtualPath) && virtualPath[virtualPath.Length - 1] == '/' ? virtualPath.Substring(0, virtualPath.Length - 1) : virtualPath;

        public static ContentReference GetRoot()
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var root = contentRepository.GetBySegment(ContentReference.RootPage, WelcomeConstants.ProviderKey, LanguageSelector.AutoDetect(true));
            if (root == null)
            {
                root = contentRepository.GetDefault<ContentFolder>(ContentReference.RootPage);
                root.Name = WelcomeConstants.ProviderName;
                ((IRoutable)root).RouteSegment = WelcomeConstants.ProviderKey;
                return contentRepository.Save(root, SaveAction.Publish, AccessLevel.NoAccess);
            }
            return root.ContentLink;
        }
    }
}