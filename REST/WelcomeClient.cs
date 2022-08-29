using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Cache;
using EPiServer.Web;
using Optimizely.Labs.WelcomeDAM.REST.Authorization;
using Optimizely.Labs.WelcomeDAM.REST.Media;

namespace Optimizely.Labs.WelcomeDAM.REST
{
    public class WelcomeClient : IWelcomeClient
    {
        private readonly Uri AuthorizationUrl = new Uri(WelcomeConstants.AccountsBaseUrl);

        private readonly ISynchronizedObjectInstanceCache cache;

        private SynchronousWebClient httpClient;

        public WelcomeClient(ISynchronizedObjectInstanceCache cache)
        {
            this.cache = cache;
            if (httpClient == null)
            {
                httpClient = new SynchronousWebClient();
            }
        }

        public MediaPageList GetAssets(int offset = 0, int pageSize = 100, params KeyValuePair<string, string>[] pairs)
        {
            var url = FormatRequestUrl(WelcomeConstants.AssetListPath);
            url = UriUtil.AddQueryString(url, "page_size", pageSize.ToString());
            if (offset > 0)
            {
                url = UriUtil.AddQueryString(url, "offset", offset.ToString());
            }
            if (pairs != null && pairs.Any())
            {
                foreach (var kv in pairs)
                {
                    url = UriUtil.AddQueryString(url, kv.Key, kv.Value);
                }
            }

            httpClient.SetAccessToken(GetToken());
            var assetList = httpClient.Get<MediaPageList>(url);
            return assetList;
        }

        public ImageItem GetAsset(string id)
        {
            var cacheKey = string.Format(WelcomeConstants.GetImagesPath, id);
            var asset = this.cache.Get<ImageItem>(cacheKey, ReadStrategy.Immediate);
            if (asset == null)
            {
                httpClient.SetAccessToken(GetToken());
                asset = httpClient.Get<ImageItem>(FormatRequestUrl(string.Format(WelcomeConstants.GetImagesPath, id)));
                if (asset != null)
                {
                    cache.Insert(cacheKey, asset, new CacheEvictionPolicy(TimeSpan.FromMinutes(5), CacheTimeoutType.Absolute));
                }
            }
            return asset;
        }

        public VideoItem GetVideo(string id)
        {
            httpClient.SetAccessToken(GetToken());
            return httpClient.Get<VideoItem>(FormatRequestUrl(string.Format(WelcomeConstants.GetVideoPath, id)));
        }

        public RawItem GetRawItem(string id)
        {
            httpClient.SetAccessToken(GetToken());
            return httpClient.Get<RawItem>(FormatRequestUrl(string.Format(WelcomeConstants.GetRawItemPath, id)));
        }

        public ArticleItem GetArticle(string id)
        {
            httpClient.SetAccessToken(GetToken());
            return httpClient.Get<ArticleItem>(FormatRequestUrl(string.Format(WelcomeConstants.GetArticlePath, id)));
        }

        public FolderPageList GetFolders(int offset = 0, int pageSize = 100, params KeyValuePair<string, string>[] pairs)
        {
            var cacheKey = $"{WelcomeConstants.FolderListPath}-{offset}-{pageSize}-{string.Join("|", pairs.Select(kvp => $"@{kvp.Key}={kvp.Value}"))}";
            var folders = this.cache.Get<FolderPageList>(cacheKey, ReadStrategy.Immediate);
            if (folders == null)
            {
                var url = FormatRequestUrl(WelcomeConstants.FolderListPath);
                url = UriUtil.AddQueryString(url, "page_size", pageSize.ToString());
                if (offset > 0)
                {
                    url = UriUtil.AddQueryString(url, "offset", offset.ToString());
                }
                if (pairs != null && pairs.Any())
                {
                    foreach (var kv in pairs)
                    {
                        url = UriUtil.AddQueryString(url, kv.Key, kv.Value);
                    }
                }

                httpClient.SetAccessToken(GetToken());
                folders = httpClient.Get<FolderPageList>(url);

                if (folders != null)
                {
                    cache.Insert(cacheKey, folders, new CacheEvictionPolicy(TimeSpan.FromMinutes(5), CacheTimeoutType.Absolute));
                }
            }
            return folders;
        }

        public Folder GetFolder(string id)
        {
            httpClient.SetAccessToken(GetToken());
            return httpClient.Get<Folder>(FormatRequestUrl(string.Format(WelcomeConstants.GetFolderPath, id)));
        }

        private string GetToken()
        {
            var responseToken = this.cache.Get<AuthorizationResponse>(WelcomeConstants.TokenCacheKey, ReadStrategy.Immediate);
            if (responseToken != null)
            {
                return responseToken.AccessToken;
            }

            var authorizationRequest = new AuthorizationRequest();
            responseToken = httpClient.Post<AuthorizationRequest, AuthorizationResponse>(FormatRequestUrl("o/oauth2/v1/token", WelcomeConstants.AccountsBaseUrl), authorizationRequest);
            if (responseToken != null)
            {
                cache.Insert(WelcomeConstants.TokenCacheKey, responseToken, new CacheEvictionPolicy(TimeSpan.FromMinutes(50), CacheTimeoutType.Absolute));
                return responseToken.AccessToken;
            }

            return null;
        }

        private string FormatRequestUrl(string url, string baseUrl = WelcomeConstants.APIBaseUrl) =>
            string.Concat(baseUrl, url);
    }
}