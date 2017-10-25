using Newtonsoft.Json;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{

    public class GridTileMap
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alpha-2")]
        public string Alpha2 { get; set; }

        [JsonProperty("alpha-3")]
        public string Alpha3 { get; set; }

        [JsonProperty("country-code")]
        public string CountryCode { get; set; }

        [JsonProperty("iso_3166-2")]
        public string Iso31662 { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("sub-region")]
        public string SubRegion { get; set; }

        [JsonProperty("region-code")]
        public string RegionCode { get; set; }

        [JsonProperty("sub-region-code")]
        public string SubRegionCode { get; set; }

        [JsonProperty("coordinates")]
        public int[] Coordinates { get; set; }
    }

}
