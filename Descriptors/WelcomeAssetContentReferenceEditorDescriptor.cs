using System;
using System.Collections.Generic;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Cms.Shell.UI.ObjectEditing.Internal;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web;

namespace WelcomeDAM.Descriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = UIHint.Image, EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = WelcomeDAMUIHints.WelcomeImage, EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    public class WelcomeAssetContentReferenceEditorDescriptor : ImageReferenceEditorDescriptor
    {
        public WelcomeAssetContentReferenceEditorDescriptor(FileExtensionsResolver fileExtensionsResolver)
            : base(fileExtensionsResolver)
        {
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ClientEditingClass = "welcomedam/editors/welcomedamassetselector";
            EditorDescriptorExtender.ModifyMetadata(metadata, attributes);
            base.ModifyMetadata(metadata, attributes);
        }
    }
}