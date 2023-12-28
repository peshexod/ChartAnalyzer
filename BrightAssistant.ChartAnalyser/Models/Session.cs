using System;
using Newtonsoft.Json;

namespace BrightAssistant.ChartAnalyser.Models
{
    public class Session
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string userid { get; set; }
        [JsonIgnore]
        public TimeSpan? Duration => EndTime - BeginTime;
        //public Track SessionTrack { get; set; }
        public string SessionTrackUrl { get; set; }
        public string DeviceType { get; set; } //Value "None" is session without device
        public string DeviceId { get; set; }
        public DateTime? BeginTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public double Score { get; set; }
        public string BasicModality { get; set; }
        public string Note { get; set; }
        public bool HasFeedback { get; set; } = true;
        //[JsonIgnore]
        //public List<GroupDataPoint> GroupData { get; set; } = new List<GroupDataPoint>();
        //[JsonIgnore]
        //public List<SingleDataPoint> SingleData { get; set; } = new List<SingleDataPoint>();

    }
}

