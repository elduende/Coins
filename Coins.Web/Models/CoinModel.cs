using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Coins.Web.Models
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

    public static class CoinModelExtensions
    {
        public static CoinModel ToCoinModel(this Coin source)
        {
            var model = new CoinModel
            {
                Name = source.Name,
                Symbol = source.Symbol,
                Image = source.Image,
                ImageSmall = source.ImageSmall,
                Status = source.Status,
                Cotizacion = source.Cotizacion
            };
            return model;
        }
    }
}