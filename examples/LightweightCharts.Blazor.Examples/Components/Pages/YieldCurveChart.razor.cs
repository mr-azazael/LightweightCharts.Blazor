using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;
using System.Drawing;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class YieldCurveChart
{
	YieldCurveChartComponent _YieldCurve;
	YieldCurveChartComponent YieldCurve
	{
		set
		{
			if (_YieldCurve == value)
				return;

			_YieldCurve = value;
			InvokeAsync(InitializeYieldCurve);
		}
	}

	async Task InitializeYieldCurve()
	{
		if (_YieldCurve == null)
			return;

		await _YieldCurve.InitializationCompleted;
		await AddSeries([
			new LineData<long>() { Time = 12 * 1, Value = 3.851 },
 			new LineData<long>() { Time = 12 * 2, Value = 3.789 },
 			new LineData<long>() { Time = 12 * 3, Value = 3.787 },
 			new LineData<long>() { Time = 12 * 4, Value = 3.843 },
 			new LineData<long>() { Time = 12 * 5, Value = 3.896 },
 			new LineData<long>() { Time = 12 * 6, Value = 4.038 },
 			new LineData<long>() { Time = 12 * 7, Value = 4.158 },
 			new LineData<long>() { Time = 12 * 8, Value = 4.287 },
 			new LineData<long>() { Time = 12 * 9, Value = 4.347 },
 			new LineData<long>() { Time = 12 * 10, Value = 4.411 },
 			new LineData<long>() { Time = 12 * 12, Value = 4.519 },
 			new LineData<long>() { Time = 12 * 15, Value = 4.625 },
 			new LineData<long>() { Time = 12 * 20, Value = 4.908 },
 			new LineData<long>() { Time = 12 * 30, Value = 4.966 }
		], Color.Red);
		await AddSeries([
			new LineData<long>() { Time = 12 * 1, Value = 2.294 },
			new LineData<long>() { Time = 12 * 2, Value = 2.375 },
			new LineData<long>() { Time = 12 * 3, Value = 2.455 },
			new LineData<long>() { Time = 12 * 4, Value = 2.568 },
			new LineData<long>() { Time = 12 * 5, Value = 2.716 },
			new LineData<long>() { Time = 12 * 6, Value = 2.840 },
			new LineData<long>() { Time = 12 * 7, Value = 2.939 },
			new LineData<long>() { Time = 12 * 8, Value = 3.035 },
			new LineData<long>() { Time = 12 * 9, Value = 3.123 },
			new LineData<long>() { Time = 12 * 10, Value = 3.192 },
			new LineData<long>() { Time = 12 * 15, Value = 3.413 },
			new LineData<long>() { Time = 12 * 20, Value = 3.491 },
			new LineData<long>() { Time = 12 * 25, Value = 3.463 },
			new LineData<long>() { Time = 12 * 30, Value = 3.550 }
		], Color.Orange);

		var timeScale = await _YieldCurve.TimeScale();
		await timeScale.SetVisibleLogicalRange(new Models.LogicalRange
		{
			//logical range is relative to BaseResolution
			From = 0,
			To = 12 * 31
		});
	}

	async Task<ISeriesApi<long, LineStyleOptions>> AddSeries(LineData<long>[] data, Color lineColor)
	{
		var series = await _YieldCurve.AddSeries(Customization.Enums.SeriesType.Line, new LineStyleOptions
		{
			Color = lineColor
		});
		await series.SetData(data);
		return series;
	}
}
