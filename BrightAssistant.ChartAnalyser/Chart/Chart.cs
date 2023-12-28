using System;
namespace BrightAssistant.ChartAnalyser.Chart
{
	public class ChartVM
	{
		public string Title { get; set; }
		public List<List<SplineAreaChartData>> Series { get; set; } = new List<List<SplineAreaChartData>>();
	}
}
