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
var series = await Chart.AddLineSeriesAsync();
```

The Github repository contains a demo project with several examples (all available series, custom options, multiple series in a chart, multiple charts, etc.).

## Limitations
Synchronous methods can't be forwarded to c# code. 
Because of this, some options can be set only from javascript.
The demo project contains an example that offers a workaround.
Known options not available in c# wrappers are:
- TimeScale options tickMarkFormatter;
- Series options autoscaleInfoProvider;

## Debugging
Clone the LightweightCharts.Blazor project and add a project reference to it in your blazor client.<br/>
The contents of 'lightweight-charts.standalone.js' can be replaced with the development version to debug the javascript library.
