using System.Text.Json.Serialization;

namespace ProplanetReplicationTool.Data.Models.DTOs
{    /// <summary>
     /// Represents the scenario entity
     /// </summary>
    public class ScenarioDTO
    {
        /// <summary>
        /// scenario name
        /// </summary>
        [JsonPropertyName("scenario")]
        public string ScenarioName { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}