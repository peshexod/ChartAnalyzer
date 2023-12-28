using System;
namespace BrightAssistant.ChartAnalyser.Chart
{
	public class ChartGroup
	{
		public string Title { get; set; }
		public List<ChartVM> Charts { get; set; } = new List<ChartVM>();
	}
}

