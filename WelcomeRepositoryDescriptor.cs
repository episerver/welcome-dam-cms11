using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using WelcomeDAM.Models;

namespace WelcomeDAM
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class WelcomeRepositoryDescriptor : ContentRepositoryDescriptorBase
    {
        public override IEnumerable<ContentReference> Roots => new ContentReference[] {
                    WelcomeContentProvider.GetRoot()};

        public static string RepositoryKey => WelcomeConstants.ProviderKey;

        public override string Key => WelcomeConstants.ProviderKey;

        public override string Name => WelcomeConstants.ProviderName;

        public override IEnumerable<Type> ContainedTypes => new[] {
            typeof(WelcomeGenericFile),
            typeof(WelcomeArticleFile),
            typeof(WelcomeVideoFile),
            typeof(WelcomeRawFile),
            typeof(WelcomeImageFile)
        };

        public override IEnumerable<Type> CreatableTypes => new[] {
            typeof(WelcomeGenericFile),
            typeof(WelcomeArticleFile),
            typeof(WelcomeVideoFile),
            typeof(WelcomeRawFile),
            typeof(WelcomeImageFile)
        };

        public override IEnumerable<Type> MainNavigationTypes =>
            new[] { typeof(ContentFolder) };
    }
}