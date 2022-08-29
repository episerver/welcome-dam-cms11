using System.Collections.Specialized;
using System.Linq;
using EPiServer.Cms.Shell.UI.Rest;
using EPiServer.Cms.Shell.UI.Rest.Models;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.Stores
{
    //[ServiceConfiguration(ServiceType = typeof(WelcomeContentModelStore))]
    public class WelcomeContentModelStore
    {
        private readonly IContentStoreModelCreator contentStoreModelCreator;

        public WelcomeContentModelStore(IContentStoreModelCreator contentStoreModelCreator)
        {
            this.contentStoreModelCreator = contentStoreModelCreator;
        }

        public ContentDataStoreModel Create(WelcomeAssetData assetData)
        {
            DefaultQueryParameters queryParameters = new DefaultQueryParameters
            {
                TypeIdentifiers = Enumerable.Empty<string>(),
                AllParameters = new NameValueCollection(0)
            };
            return this.contentStoreModelCreator.CreateContentDataStoreModel<ContentDataStoreModel>(assetData, queryParameters);
        }
    }
}