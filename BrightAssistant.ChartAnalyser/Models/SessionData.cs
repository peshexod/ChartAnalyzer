using System;
using Newtonsoft.Json;
using System.Reflection;

namespace BrightAssistant.ChartAnalyser.Models
{
    public class SessionData //Separate data class for each session for CosmosDB model
    {
        public string id { get; set; }
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "sessionId")]
        public string SessionId { get; set; }
        //public List<GroupDataPoint> GroupData { get; set; } = new List<GroupDataPoint>();
        public SingleSessionData SingleData { get; set; }
    }

    public class SingleSessionData
    {
        public List<DateTime> TimeStamps { get; set; } = new List<DateTime>();
        public List<ModalityData> ModalitiesData { get; set; } = new List<ModalityData>();
    }

    public class ModalityData
    {
        public string ModalityName { get; set; }
        public Dictionary<string, List<double>> ModalityValues { get; set; }
        public ModalityData()
        {
            ModalityValues = new Dictionary<string, List<double>>();
            var propertyInfos = typeof(ModalityDataValues).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in propertyInfos)
            {
                ModalityValues.Add(prop.Name, new List<double>());
            }
        }
    }

    public class ModalityDataValues
    {
        public double Value { get; set; }
        public double Contrib { get; set; }
        public double Threshold { get; set; }
        public double Average { get; set; }
        public double AverageThreshold { get; set; }
        public double ValueAboveAverageThreshold { get; set; }
        public double ThresholdAboveAverageThreshold { get; set; }
        public double ThresholdToValueAboveAverageThreshold { get; set; }
        public double AverageThresholdContribution { get; internal set; }
    }
}

