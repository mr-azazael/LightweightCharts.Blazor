using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.Series;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts;

/// <summary>
/// AddSeries extensions
/// </summary>
public static class ChartComponentBaseExtensions
{
	extension<H, O>(ChartComponentBase<H, O> chart)
		where H : struct
		where O : class, new()
	{
		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for line series
		/// </summary>
		public async Task<ISeriesApi<H, LineStyleOptions>> AddLineSeries(LineStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Line, options, paneIndex).ConfigureAwait(false);

		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for area series
		/// </summary>
		public async Task<ISeriesApi<H, AreaStyleOptions>> AddAreaSeries(AreaStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Area, options, paneIndex).ConfigureAwait(false);

		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for bar series
		/// </summary>
		public async Task<ISeriesApi<H, BarStyleOptions>> AddBarSeries(BarStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Bar, options, paneIndex).ConfigureAwait(false);

		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for candlestick series
		/// </summary>
		public async Task<ISeriesApi<H, CandlestickStyleOptions>> AddCandlestickSeries(CandlestickStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Candlestick, options, paneIndex).ConfigureAwait(false);

		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for histogram series
		/// </summary>
		public async Task<ISeriesApi<H, HistogramStyleOptions>> AddHistogramSeries(HistogramStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Histogram, options, paneIndex).ConfigureAwait(false);

		/// <summary>
		/// calls <see cref="ChartComponentBase{H, O}.AddSeries{S}(Customization.Enums.SeriesType, S, int)"/> configured for baseline series
		/// </summary>
		public async Task<ISeriesApi<H, BaselineStyleOptions>> AddBaselineSeries(BaselineStyleOptions options = null, int paneIndex = 0)
			=> await chart.AddSeries(Customization.Enums.SeriesType.Baseline, options, paneIndex).ConfigureAwait(false);
	}
}
