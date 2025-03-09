using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// An instance of IChartApiBase configured for price-based horizontal scaling.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/functions/createOptionsChart"/>
	/// </summary>
	public interface IOptionsChart : IChartApiBase<double>, ICustomizableObject<PriceChartOptions>
	{

	}

	/// <summary>
	/// implementation for <see cref="IOptionsChart"/>
	/// </summary>
	public class OptionsChartComponent : ChartComponentBase<double, PriceChartOptions>, IOptionsChart, IAsyncDisposable
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		protected override ValueTask<IJSObjectReference> CreateChart()
			=> JsModule.CreateOptionsChart(JsRuntime, Id, new PriceChartOptions());

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <typeparam name="O"><inheritdoc/></typeparam>
		/// <param name="type"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		/// <exception cref="NotImplementedException"></exception>
		protected override Type ValidateAndResolveSeries<O>(SeriesType type)
		{
			switch (type)
			{
				case SeriesType.Line:
					{
						ThrowIfOptionsTypeDoesntMatch<O, LineStyleOptions>(type);
						return typeof(LineData<double>);
					}
				case SeriesType.Area:
					{
						ThrowIfOptionsTypeDoesntMatch<O, AreaStyleOptions>(type);
						return typeof(AreaData<double>);
					}
				case SeriesType.Bar:
					{
						ThrowIfOptionsTypeDoesntMatch<O, BarStyleOptions>(type);
						return typeof(BarData<double>);
					}
				case SeriesType.Candlestick:
					{
						ThrowIfOptionsTypeDoesntMatch<O, CandlestickStyleOptions>(type);
						return typeof(CandlestickData<double>);
					}
				case SeriesType.Histogram:
					{
						ThrowIfOptionsTypeDoesntMatch<O, HistogramStyleOptions>(type);
						return typeof(HistogramData<double>);
					}
				case SeriesType.Baseline:
					{
						ThrowIfOptionsTypeDoesntMatch<O, BaselineStyleOptions>(type);
						return typeof(BaselineData<double>);
					}
				default:
					throw new NotImplementedException("chart type not handled");
			};
		}
	}
}
