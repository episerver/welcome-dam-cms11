using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM
{
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = "WelcomeImage")]
    public class BlockReferenceEditorDescriptor : ContentReferenceEditorDescriptor<WelcomeImageFile>
    {
        public override IEnumerable<ContentReference> Roots
        {
            get
            {
                //Sample to override the default root for the repository.
                //Take the reference from configuration or site initialization and do not hard-code it.
                return new ContentReference[] { new ContentReference(137 ) };
            }
        }
    }
}