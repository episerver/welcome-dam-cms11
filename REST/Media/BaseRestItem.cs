using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WelcomeDAM.REST.Media
{
    public abstract class BaseRestItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; set; }

        [JsonProperty("labels")]
        public List<Label> Labels { get; set; }

        [JsonProperty("folder_id")]
        public Guid? FolderId { get; set; }

        [JsonProperty("file_location")]
        public string FileLocation { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("owner_organization_id")]
        public string OwnerOrganizationId { get; set; }
    }
}