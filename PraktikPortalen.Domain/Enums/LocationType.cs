using System.Text.Json.Serialization;

namespace PraktikPortalen.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LocationType
    {

        Unassigned = 0,
        OnSite = 1,
        Remote = 2,
        Hybrid = 3
        
    }
}
