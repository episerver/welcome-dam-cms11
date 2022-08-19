using System;
using System.Linq;

namespace WelcomeDAM
{
    public class WelcomeResourceId
    {
        public WelcomeResourceId(string type, Guid id)
        {
            Type = type;
            Id = id;
        }

        public WelcomeResourceId(string type, string id)
        {
            Type = type;
            if (Guid.TryParse(id, out Guid guidOut))
            {
                Id = guidOut;
            }
        }

        public string Type { get; private set; }

        public Guid Id { get; private set; }

        public static implicit operator string(WelcomeResourceId id) =>
            id.ToString();

        public static explicit operator WelcomeResourceId(Uri id)
        {
            if (id == null)
            {
                return null;
            }

            var resourceType = RemoveTrailingSlash(id.Segments[1]);
            var resourceId = id.Segments.LastOrDefault().Trim();
            var welcomeResourceId = new WelcomeResourceId(resourceType, resourceId);

            return welcomeResourceId;
        }

        public override string ToString() =>
            $"{this.Type }/{this.Id.ToString("N") }";

        private static string RemoveTrailingSlash(string virtualPath) =>
            !string.IsNullOrEmpty(virtualPath) && virtualPath[virtualPath.Length - 1] == '/' ? virtualPath.Substring(0, virtualPath.Length - 1) : virtualPath;
    }
}