using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarketPlaceService.API.Models
{
    public class MarketPlace
    {
        [JsonProperty("marketplace_id")]
        public long MarketPlaceId { get; set; }
        [JsonProperty("name")]
        public string MarketPlaceName { get; set; }
        [JsonProperty("image_id")]
        public long MarketPlaceImage { get; set; }//image id
        [JsonProperty("products_id")]
        public List<long> MarketPlaceProducts { get; set; }//product's id
    }
}
