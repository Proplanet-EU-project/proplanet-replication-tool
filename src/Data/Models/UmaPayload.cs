using System.Text.Json.Serialization;

namespace ProplanetReplicationTool.Data.Models
{
    public class CalculationPayload
    {
        [JsonPropertyName("substances")]
        public List<Material> Substances { get; set; }

        [JsonPropertyName("environmental_media")]
        public string EnvironmentalMedia { get; set; }

        [JsonPropertyName("scenario")]
        public string Scenario { get; set; }

        [JsonPropertyName("low_mols")]
        public List<int> LowMols { get; set; }

        [JsonPropertyName("upp_mols")]
        public List<int> UppMols { get; set; }

        [JsonPropertyName("upperbound")]
        public List<int> Upperbound { get; set; }

        [JsonPropertyName("lung_model")]
        public string LungModel { get; set; }

        [JsonPropertyName("lung_respiratory_volume_rate")]
        public string LungRespiratoryVolumeRate { get; set; }

        [JsonPropertyName("lung_exposure_duration")]
        public int LungExposureDuration { get; set; }

        [JsonPropertyName("wca_surface")]
        public int WcaSurface { get; set; }

        [JsonPropertyName("hca_surface")]
        public int Hca { get; set; }

        [JsonPropertyName("sfe_surface")]
        public int Sfe { get; set; }

        [JsonPropertyName("lung_deposition_flags")]
        public Dictionary<string, bool> LungDepositionFlags { get; set; }

        [JsonPropertyName("performance_flags")]
        public Dictionary<string, bool> PerformanceFlags { get; set; }

        [JsonPropertyName("base_concentration")]
        public float BaseConcentration { get; set; }
    }

    public class ApiResponse
    {
        [JsonPropertyName("html_content")]
        public HtmlContent Plots { get; set; }

        [JsonPropertyName("csv_data")]
        public string CsvData { get; set; }
    }

    public class HtmlContent
    {
        [JsonPropertyName("optimization_plot")]
        public string OptimizationPlot { get; set; }

        [JsonPropertyName("proportions_plot")]
        public string ProportionsPlot { get; set; }

        [JsonPropertyName("properties_plot")]
        public string PropertiesPlot { get; set; }

        [JsonPropertyName("concentration_plot")]
        public string ConcentrationPlot { get; set; }

        [JsonPropertyName("lung_plot")]
        public string LungPlot { get; set; }
    }
}