using System.Collections.Generic;
using EPiServer.Cms.Shell.UI.Rest.Models;
using EPiServer.Cms.Shell.UI.Rest.Models.Transforms;
using EPiServer.Cms.Shell.UI.Rest.Models.Transforms.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Globalization;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM
{
    public class ThumbnailWelcomeTransform : StructureStoreModelTransform
    {
        public ThumbnailWelcomeTransform(IContentProviderManager contentProviderManager,
            ILanguageBranchRepository languageBranchRepository,
            IContentLanguageSettingsHandler contentLanguageSettingsHandler,
            ISiteDefinitionRepository siteDefinitionRepository,
            LanguageResolver languageResolver,
            IEnumerable<IHasChildrenEvaluator> hasChildrenEvaluator,
            TemplateResolver templateResolver,
            UrlResolver urlResolver) :
            base(contentProviderManager, languageBranchRepository, contentLanguageSettingsHandler, siteDefinitionRepository, hasChildrenEvaluator, languageResolver, urlResolver, templateResolver)
        {
        }

        public override void TransformInstance(IContent source, StructureStoreContentDataModel target, IModelTransformContext context)
        {
            base.TransformInstance(source, target, context);
            if (source is IWelcomeThumbnail welcomeThumbnail)
            {
                target.ThumbnailUrl = welcomeThumbnail.Thumbnail;
            }
        }
    }
}