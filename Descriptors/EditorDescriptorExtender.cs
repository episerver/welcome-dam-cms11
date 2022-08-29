using System;
using System.Collections.Generic;
using EPiServer.Cms.Shell.UI.ObjectEditing.Internal;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;

namespace Optimizely.Labs.WelcomeDAM.Descriptors
{
    public static class EditorDescriptorExtender
    {
        private static readonly Injected<FileExtensionsResolver> fileExtensionsResolver;

        public static void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            metadata.EditorConfiguration["welcomeSelectContentUrl"] = $"http://app.welcomesoftware.com/cloud/library-picker";
            metadata.EditorConfiguration["welcomeiconpath"] = $"/EPiServer/{WelcomeConstants.ModuleName}/1.0.0/clientresources/scripts/editors/images/welcome-logo.png";
            metadata.EditorConfiguration["allowedExtensions"] = fileExtensionsResolver.Service.GetAllowedExtensions(typeof(IContentImage), metadata.Attributes);
            metadata.EditorConfiguration["welcomemodulepath"] = Paths.ToResource(WelcomeConstants.ModuleName, string.Empty);
        }
    }
}