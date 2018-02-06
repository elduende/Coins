using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Coins.Models
{
    public class CoinModel
    {
        [JsonProperty("Name")]
        [DisplayName("Moneda")]
        public string Name { get; set; }
        [DisplayName("Símbolo")]
        public string Symbol { get; set; }
        [DisplayName("Imagen")]
        public string Image { get; set; }
        [DisplayName("Ícono")]
        public string ImageSmall { get; set; }
        [DisplayName("Estado")]
        public string Status { get; set; }
        [DisplayName("Cotización (BTC)")]
        [DisplayFormat(DataFormatString = "{0:####0.00000000}", ApplyFormatInEditMode = true)]
        public float Cotizacion { get; set; }
    }
}