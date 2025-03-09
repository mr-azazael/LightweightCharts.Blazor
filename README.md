<div align="center">
  <h1>LightweightCharts for Blazor</h1>
  A blazor wrapper for TradingView's Lightweight Charts javascript library.
</div>

## Installing
Install the package via the NuGet package manager.<br/>
Add the script imports to your root html file (index.html or _Layout.cshtml).

```html
<script src="_content/LightweightCharts.Blazor/lightweight-charts.scripts.js"></script>
<script src="_content/LightweightCharts.Blazor/lightweight-charts.standalone.js"></script>
```

## How to use it

Add the chart component to your razor file.

```html
@using LightweightCharts.Blazor.Charts
<ChartComponent @ref="Chart" />
```

Wait for the chart component to finish its initialization, then use the exposed api methods to add data series to it.

```c#
await Chart.InitializationCompleted;
var series = await Chart.AddSeries<LineStyleOptions>(SeriesType.Line);
```
or
```c#
await Chart.InitializationCompleted;
var series = await Chart.AddSeries(SeriesType.Line, new LineStyleOptions
{
	...
});
```

The Github repository contains a demo project with several examples (all available series, custom options, multiple series in a chart, multiple charts, etc.).

## Limitations
Synchronous methods can't be forwarded to c# code. 
Because of this, sync functions can be defined only in javascript, the demo project contains examples (JsDelegate).
Custom series are not implemented.

## Available examples
- Area Series: text watermark, up/down markers, timescale customization, data points update after initialization
- Bar Series: customTimeFormatter, customTimeScaleTickMarkFormatter functions (JsDelegate), 'live' data update
- Baseline Series: image watermark, another example on how to customize a sync js function (autoscaleInfoProvider), custom chart options
- Candlestick Series: histogram on its own pane, chart events, custom chart options
- Histogram Series: timescale customization
- Line Series: sync zoom, timeline position, crosshair between two charts
- Yield curve: basic usage
- Options: basic usage

## Debugging
Clone the LightweightCharts.Blazor project and add a project reference to it in your blazor project.<br/>
The contents of 'lightweight-charts.standalone.js' can be replaced with the development version to debug the javascript library.

## Changes in 5.0
- removed <>Async from method names
- added time value type as a generic parameter, this requires changing data point types
	from LineData to LineData<long> (example for a chart component)
- WhitespaceData and derrived classes now have a generic 'Time' (UnixTime in previous version) property that must match the time value type of the chart;
	'Time' of type DateTime is no longer available (LightweightCharts.Blazor.Utilities.Extensions has extension methods to convert/set the 'Time' property of a ISeriesData<long> item).