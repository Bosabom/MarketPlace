using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarketPlaceService.API.DataTransfer
{
    public class MarketPlaceCreateDTO
    {
        [JsonProperty("name")]
        public string MarketPlaceName { get; set; }

        [JsonProperty("image_id")]
        public long MarketPlaceImage { get; set; }//image id

        [JsonProperty("products_id")]
        public List<long> MarketPlaceProducts { get; set; }//product's id
    }
}
