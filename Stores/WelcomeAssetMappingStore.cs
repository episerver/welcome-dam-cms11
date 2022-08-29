using System;
using System.Web.Mvc;
using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.Shell.Services.Rest;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.Stores
{
    [RestStore("welcomeassetmappingstore")]
    public class WelcomeAssetMappingStore : RestControllerBase
    {
        private readonly IdentityMappingService identityMappingService;

        private readonly IContentLoader contentLoader;

        private readonly IContentCacheRemover contentCacheRemover;

        private readonly WelcomeContentModelStore _welcomeContentModelStore;

        public WelcomeAssetMappingStore(
            IdentityMappingService identityMappingService,
            IContentLoader contentLoader,
            IContentCacheRemover contentCacheRemover,
            WelcomeContentModelStore welcomeContentModelStore)
        {
            this.identityMappingService = identityMappingService;
            this.contentLoader = contentLoader;
            this.contentCacheRemover = contentCacheRemover;
            this._welcomeContentModelStore = welcomeContentModelStore;
        }

        [HttpGet]
        public ActionResult Get(string id, string type, string publicUrl, string title, string ownerContentItemId)
        {
            Uri externalIdentifier = MappedIdentity.ConstructExternalIdentifier(WelcomeConstants.ProviderKey, $"{type}/{id}");
            MappedIdentity mappedIdentity = this.identityMappingService.Get(externalIdentifier, true);
            var assetData = this.contentLoader.Get<WelcomeAssetData>(mappedIdentity.ContentLink);
            var assetContentModel = this._welcomeContentModelStore.Create(assetData);
            return Rest(assetContentModel);
        }
    }
}