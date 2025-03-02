using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarketPlaceService.API.DataTransfer
{
    public class MarketPlaceCreateDTO
    {
        /// <summary>
        /// Market Place Name
        /// </summary>
        [JsonProperty("name")]
        public string MarketPlaceName { get; set; }

        /// <summary>
        /// Image ID
        /// </summary>
        [JsonProperty("image_id")]
        public long MarketPlaceImage { get; set; }

        /// <summary>
        /// Product IDs
        /// </summary>
        [JsonProperty("products_id")]
        public List<long> MarketPlaceProducts { get; set; }
    }
}
