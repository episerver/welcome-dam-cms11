using System;
using System.Collections.Generic;
using EPiServer.Shell;
using EPiServer.Shell.ObjectEditing;

namespace WelcomeDAM.Descriptors
{
    public static class EditorDescriptorExtender
    {

        public static void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            metadata.EditorConfiguration["welcomeSelectContentUrl"] = $"http://app.welcomesoftware.com/cloud/library-picker";
            metadata.EditorConfiguration["welcomeiconpath"] = $"/EPiServer/{WelcomeConstants.ModuleName}/1.0.0/clientresources/scripts/editors/images/welcome-logo.png";
            metadata.EditorConfiguration["allowedExtensions"] = "TODO";
            metadata.EditorConfiguration["welcomemodulepath"] = Paths.ToResource(WelcomeConstants.ModuleName, string.Empty);
        }
    }
}