using System.Text.Json.Serialization;

namespace Blog.Models.Enums
{

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PostSorting
        {
        CreateAsc,
        CreatDesc,
        LikeAsc,
        LikeDesc,
       
    }
    }
