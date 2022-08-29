using EPiServer.DataAnnotations;

namespace Optimizely.Labs.WelcomeDAM.Models
{
    [ContentType(DisplayName = "Welcome Video", GroupName = "Welcome", GUID = "6E32C31C-CF1F-4947-9986-235352CC01D6", AvailableInEditMode = false)]
    public class WelcomeVideoFile : WelcomeGenericFile
    {
    }
}