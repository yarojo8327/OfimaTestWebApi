using Newtonsoft.Json;

using System.Text.Json;

namespace Ofima.TechnicalTest.WebApi.Models
{
    public class RegisterPlayer
    {
        [JsonProperty("Jugador 1")]
        public string PlayerOne { get; set; }

        [JsonProperty("Jugador 2")]
        public string PlayerTwo { get; set; }
    }
}
