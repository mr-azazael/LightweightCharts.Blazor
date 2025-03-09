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
	/// The main interface of a single yield curve chart.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IYieldCurveChartApi"/>
	/// </summary>
	public interface IYieldCurveChart : IChartApiBase<long>, ICustomizableObject<YieldCurveChartOptions>
	{

	}

	/// <summary>
	/// implementation for <see cref="IYieldCurveChart"/>
	/// </summary>
	public class YieldCurveChartComponent : ChartComponentBase<long, YieldCurveChartOptions>, IYieldCurveChart, IAsyncDisposable
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		protected override ValueTask<IJSObjectReference> CreateChart()
			=> JsModule.CreateYieldCurveChart(JsRuntime, Id, new YieldCurveChartOptions());

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
						return typeof(LineData<long>);
					}
				case SeriesType.Area:
					{
						ThrowIfOptionsTypeDoesntMatch<O, AreaStyleOptions>(type);
						return typeof(AreaData<long>);
					}
				default:
					throw new NotImplementedException("chart type not handled");
			};
		}
	}
}
