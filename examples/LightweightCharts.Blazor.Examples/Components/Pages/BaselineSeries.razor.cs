﻿using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class BaselineSeries
{
	const string TwEncodedImage = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTg5IiBoZWlnaHQ9IjMyIiB2aWV3Qm94PSIwIDAgMTg5IDMyIiBmaWxsPSJub25lIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPgo8cGF0aCBkPSJNNDcuNTk3MyAyNS4wMjM4SDUxLjI5OTVWMTAuNTE5M0g1Ni4xNDI4VjcuMDE5OTZINDIuODA0N1YxMC41MTkzSDQ3LjU5NzNWMjUuMDIzOFoiIGZpbGw9IiMwMDAwMDAiLz4KPHBhdGggZD0iTTU2LjE0ODEgMjUuMDIzOEg1OS42MjIxVjE4Ljk2MzRDNTkuNjIyMSAxNi44NTg3IDYwLjcxMjUgMTUuNTkwOCA2Mi4zNjA3IDE1LjU5MDhDNjIuOTQzOSAxNS41OTA4IDYzLjQwMDQgMTUuNjkyMyA2My45MzI5IDE1Ljg2OThWMTIuMzQ1MUM2My41MDE4IDEyLjIxODMgNjMuMTQ2OCAxMi4xOTI5IDYyLjc2NjQgMTIuMTkyOUM2MS4zNDY0IDEyLjE5MjkgNjAuMDUzMiAxMy4xMzExIDU5LjYyMjEgMTQuMzczN1YxMi4zNDUxSDU2LjE0ODFWMjUuMDIzOFoiIGZpbGw9IiMwMDAwMDAiLz4KPHBhdGggZD0iTTcwLjYwMDcgMjUuMzAyOEM3Mi4zNTA0IDI1LjMwMjggNzMuNjY5IDI0LjQxNTMgNzQuMjUyMiAyMy42MDM4VjI1LjAyMzhINzcuNzI2MlYxMi4zNDUxSDc0LjI1MjJWMTMuNzY1MUM3My42NjkgMTIuOTUzNiA3Mi4zNTA0IDEyLjA2NjEgNzAuNjAwNyAxMi4wNjYxQzY3LjIyODIgMTIuMDY2MSA2NC42NDE3IDE1LjEwOSA2NC42NDE3IDE4LjY4NDRDNjQuNjQxNyAyMi4yNTk5IDY3LjIyODIgMjUuMzAyOCA3MC42MDA3IDI1LjMwMjhaTTcxLjMzNjEgMjIuMTMzMUM2OS40MDg5IDIyLjEzMzEgNjguMTQxMSAyMC42ODc3IDY4LjE0MTEgMTguNjg0NEM2OC4xNDExIDE2LjY4MTIgNjkuNDA4OSAxNS4yMzU4IDcxLjMzNjEgMTUuMjM1OEM3My4yNjMzIDE1LjIzNTggNzQuNTMxMiAxNi42ODEyIDc0LjUzMTIgMTguNjg0NEM3NC41MzEyIDIwLjY4NzcgNzMuMjYzMyAyMi4xMzMxIDcxLjMzNjEgMjIuMTMzMVoiIGZpbGw9IiMwMDAwMDAiLz4KPHBhdGggZD0iTTg1Ljk5NzUgMjUuMzAyOEM4Ny43NDcyIDI1LjMwMjggODkuMDY1OCAyNC40MTUzIDg5LjY0OSAyMy42MDM4VjI1LjAyMzhIOTMuMTIzVjYuMDA1NjZIODkuNjQ5VjEzLjc2NTFDODkuMDY1OCAxMi45NTM2IDg3Ljc0NzIgMTIuMDY2MSA4NS45OTc1IDEyLjA2NjFDODIuNjI1IDEyLjA2NjEgODAuMDM4NSAxNS4xMDkgODAuMDM4NSAxOC42ODQ0QzgwLjAzODUgMjIuMjU5OSA4Mi42MjUgMjUuMzAyOCA4NS45OTc1IDI1LjMwMjhaTTg2LjczMjkgMjIuMTMzMUM4NC44MDU3IDIyLjEzMzEgODMuNTM3OSAyMC42ODc3IDgzLjUzNzkgMTguNjg0NEM4My41Mzc5IDE2LjY4MTIgODQuODA1NyAxNS4yMzU4IDg2LjczMjkgMTUuMjM1OEM4OC42NjAxIDE1LjIzNTggODkuOTI4IDE2LjY4MTIgODkuOTI4IDE4LjY4NDRDODkuOTI4IDIwLjY4NzcgODguNjYwMSAyMi4xMzMxIDg2LjczMjkgMjIuMTMzMVoiIGZpbGw9IiMwMDAwMDAiLz4KPHBhdGggZD0iTTk3Ljk3MTEgMTAuMjY1N0M5OS4yMTM2IDEwLjI2NTcgMTAwLjIwMyA5LjI3Njc5IDEwMC4yMDMgOC4wNTk2MkMxMDAuMjAzIDYuODQyNDYgOTkuMjEzNiA1Ljg1MzUyIDk3Ljk3MTEgNS44NTM1MkM5Ni43NzkzIDUuODUzNTIgOTUuNzY1IDYuODQyNDYgOTUuNzY1IDguMDU5NjJDOTUuNzY1IDkuMjc2NzkgOTYuNzc5MyAxMC4yNjU3IDk3Ljk3MTEgMTAuMjY1N1pNOTYuMjQ2NyAyNS4wMjM4SDk5LjcyMDdWMTIuMzQ1MUg5Ni4yNDY3VjI1LjAyMzhaIiBmaWxsPSIjMDAwMDAwIi8+CjxwYXRoIGQ9Ik0xMDIuODAzIDI1LjAyMzhIMTA2LjI3N1YxOC44MzY2QzEwNi4yNzcgMTYuNDAyMyAxMDcuNDY5IDE1LjIzNTggMTA5LjA5MiAxNS4yMzU4QzExMC41MzcgMTUuMjM1OCAxMTEuMzIzIDE2LjM1MTUgMTExLjMyMyAxOC4xNzczVjI1LjAyMzhIMTE0Ljc5N1YxNy41OTQxQzExNC43OTcgMTQuMjk3NiAxMTIuOTcxIDEyLjA2NjEgMTA5LjkwMyAxMi4wNjYxQzEwOC4yNTUgMTIuMDY2MSAxMDYuOTYyIDEyLjc3NjEgMTA2LjI3NyAxMy43OTA0VjEyLjM0NTFIMTAyLjgwM1YyNS4wMjM4WiIgZmlsbD0iIzAwMDAwMCIvPgo8cGF0aCBkPSJNMTIzLjYzOCAyMS43Mjc0QzEyMS44ODggMjEuNzI3NCAxMjAuNDE3IDIwLjQwODggMTIwLjQxNyAxOC40ODE2QzEyMC40MTcgMTYuNTI5MSAxMjEuODg4IDE1LjIzNTggMTIzLjYzOCAxNS4yMzU4QzEyNS4zODcgMTUuMjM1OCAxMjYuODU4IDE2LjUyOTEgMTI2Ljg1OCAxOC40ODE2QzEyNi44NTggMjAuNDA4OCAxMjUuMzg3IDIxLjcyNzQgMTIzLjYzOCAyMS43Mjc0Wk0xMjMuMjMyIDMwLjU3NzFDMTI3LjExMiAzMC41NzcxIDEzMC4wNTMgMjguNTk5MyAxMzAuMDUzIDI0LjAzNDlWMTIuMzQ1MUgxMjYuNTc5VjEzLjc2NTFDMTI1Ljg5NSAxMi43NzYxIDEyNC41MjUgMTIuMDY2MSAxMjIuOTAyIDEyLjA2NjFDMTE5LjU4IDEyLjA2NjEgMTE2LjkxOCAxNC45MDYyIDExNi45MTggMTguNDgxNkMxMTYuOTE4IDIyLjAzMTYgMTE5LjU4IDI0Ljg3MTcgMTIyLjkwMiAyNC44NzE3QzEyNC41MjUgMjQuODcxNyAxMjUuODk1IDI0LjEzNjMgMTI2LjU3OSAyMy4xOTgxVjI0LjA2MDJDMTI2LjU3OSAyNi4wODg5IDEyNS4yMzUgMjcuNTM0MiAxMjMuMTgxIDI3LjUzNDJDMTIxLjc2MSAyNy41MzQyIDEyMC4zNDEgMjcuMDUyNCAxMTkuMjI1IDI1Ljk2MjFMMTE3LjMyNCAyOC41NDg1QzExOC43NjkgMjkuOTQzMiAxMjEuMDI2IDMwLjU3NzEgMTIzLjIzMiAzMC41NzcxWiIgZmlsbD0iIzAwMDAwMCIvPgo8cGF0aCBkPSJNMTM3LjM5OCAyNS4wMjM4SDE0MC42NjlMMTQ4LjEyNSA3LjAxOTk2SDE0NC4wNjdMMTM5LjA0NyAxOS41MjEyTDEzMy45NSA3LjAxOTk2SDEyOS45NjlMMTM3LjM5OCAyNS4wMjM4WiIgZmlsbD0iIzAwMDAwMCIvPgo8cGF0aCBkPSJNMTUxLjMxMiAxMC4yNjU3QzE1Mi41NTUgMTAuMjY1NyAxNTMuNTQ0IDkuMjc2NzkgMTUzLjU0NCA4LjA1OTYyQzE1My41NDQgNi44NDI0NiAxNTIuNTU1IDUuODUzNTIgMTUxLjMxMiA1Ljg1MzUyQzE1MC4xMiA1Ljg1MzUyIDE0OS4xMDYgNi44NDI0NiAxNDkuMTA2IDguMDU5NjJDMTQ5LjEwNiA5LjI3Njc5IDE1MC4xMiAxMC4yNjU3IDE1MS4zMTIgMTAuMjY1N1pNMTQ5LjU4OCAyNS4wMjM4SDE1My4wNjJWMTIuMzQ1MUgxNDkuNTg4VjI1LjAyMzhaIiBmaWxsPSIjMDAwMDAwIi8+CjxwYXRoIGQ9Ik0xNjIuMDUyIDI1LjMwMjhDMTY0LjcxNSAyNS4zMDI4IDE2Ni43NDQgMjQuMTg3IDE2Ny45MzUgMjIuNDYyN0wxNjUuMzQ5IDIwLjUzNTVDMTY0Ljc2NiAyMS40MjMxIDE2My42NzUgMjIuMTMzMSAxNjIuMDc4IDIyLjEzMzFDMTYwLjQwNCAyMi4xMzMxIDE1OC45MDggMjEuMTk0OCAxNTguNzA1IDE5LjQ5NTlIMTY4LjE4OUMxNjguMjY1IDE4Ljk2MzQgMTY4LjI0IDE4LjYwODQgMTY4LjI0IDE4LjMwNDFDMTY4LjI0IDE0LjE3MDggMTY1LjMyNCAxMi4wNjYxIDE2Mi4wMDIgMTIuMDY2MUMxNTguMTQ3IDEyLjA2NjEgMTU1LjMzMyAxNC45MzE1IDE1NS4zMzMgMTguNjg0NEMxNTUuMzMzIDIyLjY5MDkgMTU4LjI3NCAyNS4zMDI4IDE2Mi4wNTIgMjUuMzAyOFpNMTU4LjgzMiAxNy4xMzc2QzE1OS4xNjIgMTUuNTkwOCAxNjAuNjA3IDE0LjkzMTUgMTYxLjkgMTQuOTMxNUMxNjMuMTkzIDE0LjkzMTUgMTY0LjQ2MSAxNS42MTYyIDE2NC43NjYgMTcuMTM3NkgxNTguODMyWiIgZmlsbD0iIzAwMDAwMCIvPgo8cGF0aCBkPSJNMTgxLjI1NSAyNS4wMjM4SDE4NC43MDNMMTg4Ljg4NyAxMi4zNDUxSDE4NS4xNkwxODIuNzc2IDIwLjQ1OTVMMTgwLjE5IDEyLjM0NTFIMTc3LjUyN0wxNzQuOTY2IDIwLjQ1OTVMMTcyLjU1NyAxMi4zNDUxSDE2OC44NTVMMTczLjA2NCAyNS4wMjM4SDE3Ni41MTNMMTc4Ljg3MSAxNy44MjIzTDE4MS4yNTUgMjUuMDIzOFoiIGZpbGw9IiMwMDAwMDAiLz4KPHBhdGggZmlsbC1ydWxlPSJldmVub2RkIiBjbGlwLXJ1bGU9ImV2ZW5vZGQiIGQ9Ik0xNCAyNUg3VjE0SDBWN0gxNFYyNVpNMzUuNSA3TDI4IDI1SDIwTDI3LjUgN0gzNS41Wk0yMCAxNUMyMi4yMDkxIDE1IDI0IDEzLjIwOTEgMjQgMTFDMjQgOC43OTA4NiAyMi4yMDkxIDcgMjAgN0MxNy43OTA5IDcgMTYgOC43OTA4NiAxNiAxMUMxNiAxMy4yMDkxIDE3Ljc5MDkgMTUgMjAgMTVaIiBmaWxsPSIjMDAwMDAwIi8+Cjwvc3ZnPgo=";

	bool _InitChartComponent;
	ChartComponent _ChartComponent;

	[Inject]
	IJSRuntime JsRuntime { get; set; }

	ChartComponent ChartComponent
	{
		set
		{
			if (_ChartComponent == value)
				return;

			_ChartComponent = value;
			_InitChartComponent = true;
			StateHasChanged();
		}
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_InitChartComponent)
		{
			_InitChartComponent = false;
			await InitializeChartComponent();
		}
	}

	async Task InitializeChartComponent()
	{
		if (_ChartComponent == null)
			return;

		await _ChartComponent.InitializationCompleted;
		await _ChartComponent.ApplyOptions(new ChartOptions
		{
			LeftPriceScale = new PriceScaleOptions
			{
				Visible = false
			},
			RightPriceScale = new PriceScaleOptions
			{
				ScaleMargins = new Customization.PriceScaleMargins
				{
					Bottom = 0.1d,
					Top = 0.1d
				}
			}
		});

		var series = await _ChartComponent.AddSeries(Customization.Enums.SeriesType.Baseline, new BaselineStyleOptions
		{
			BaseValue = new BaseValuePrice { Price = BtcUsdDataPoints.OneWeek.Average(x => x.ClosePrice) }
		});
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new BaselineData<long>
		{
			Time = x.OpenTime.ToUnix(),
			Value = x.ClosePrice
		}));
		var pane = await series.GetPane();
		var imageWatermark = await pane.CreateImageWatermark(TwEncodedImage, new ImageWatermarkOptions
		{
			//does not seem to have alignment properties like the text watermark has
			Alpha = 0.75f,
			MaxWidth = 200
		});

		await JsRuntime.InvokeVoidAsync("javascriptHelpers.setAutoscaleInfoProvider", series.JsObjectReference);

		var timeScale = await _ChartComponent.TimeScale();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
