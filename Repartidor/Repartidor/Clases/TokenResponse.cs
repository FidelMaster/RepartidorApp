using Newtonsoft.Json;

namespace Repartidor.Clases
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }


    }
}
