using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarketPlaceService.API.DataTransfer
{
    public class MarketPlaceDTO
    {
        /// <summary>
        /// Market Place Id
        /// </summary>
        [JsonProperty("marketplace_id")]
        public long MarketPlaceId { get; set; }

        /// <summary>
        /// Market Place Name
        /// </summary>
        [JsonProperty("name")]
        public string MarketPlaceName { get; set; }
        
        /// <summary>
        /// Image Id
        /// </summary>
        [JsonProperty("image_id")]
        public long MarketPlaceImage { get; set; }

        /// <summary>
        /// Products Ids
        /// </summary>
        [JsonProperty("products_id")]
        public List<long> MarketPlaceProducts { get; set; }
    }
}
