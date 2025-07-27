using System.Text.Json.Serialization;

namespace DomainModels.Enums
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
