using EPiServer;
using EPiServer.Core;
using EPiServer.Shell.Services.Rest;
using EPiServer.Web;
using System.Web.Mvc;
using WelcomeDAM.Models;

namespace WelcomeDAM.Stores
{
    [RestStore("welcomeassetstore")]
    public class WelcomeAssetStore : RestControllerBase
    {
        private readonly IContentLoader contentLoader;

        private readonly WelcomeContentModelStore _welcomeContentModelStore;

        public WelcomeAssetStore(IContentLoader contentLoader, WelcomeContentModelStore welcomeContentModelStore)
        {
            this.contentLoader = contentLoader;
            this._welcomeContentModelStore = welcomeContentModelStore;
        }

        [HttpGet]
        public ActionResult Get(string id)
        {
            var contentLink = new ContentReference(id);
            if (!string.IsNullOrWhiteSpace(contentLink.ProviderName) && contentLink.ProviderName.Equals(WelcomeConstants.ProviderKey))
            {
                var content = this.contentLoader.Get<WelcomeAssetData>(contentLink);
                var item = this._welcomeContentModelStore.Create(content);
                if (content is WelcomeImageFile welcomeImage)
                {
                    var url = welcomeImage.Url;
                    url = UriUtil.AddQueryString(url, "width", "150");
                    url = UriUtil.AddQueryString(url, "height", "80");
                    item.Properties.Add("thumbnailUrl", url);
                }
                return Rest(item);
            }

            return Rest(this.contentLoader.Get<IContent>(contentLink));
        }
    }
}