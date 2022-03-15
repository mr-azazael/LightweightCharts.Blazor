/*download 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.production.js' into lightweight-charts.standalone.js for release builds*/
/*use 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.development.js' for debugging*/

window.LightweightChartsBlazor = {

	createChartLayout: function (containerId, chartConfig) {
		var container = document.getElementById(containerId);
		return LightweightCharts.createChart(container, chartConfig);
	},

	lightweightChartsInvoke: async function (target, method /*args*/) {
		var args = Array.prototype.slice.call(arguments, 2);
		var result = target[method].apply(target, args);

		if (result != undefined && result.UniqueJavascriptId == undefined)
			result.UniqueJavascriptId = await DotNet.invokeMethodAsync('LightweightCharts.Blazor', 'GenerateGuidForJavascript');

		return result;
	},

	registerSizeChangedEvent: function (elementId, dotNetHandler, methodName) {
		var element = document.getElementById(elementId);
		var resizeObserver = new ResizeObserver(entry => {
			var boundingRect = entry[0].contentRect;
			dotNetHandler.invokeMethodAsync(methodName, boundingRect.width, boundingRect.height);
		});

		resizeObserver.observe(element);
	},

	subscribeToEvent: function (target, dotNetHandler, subscriptionMethod, methodName) {

		var callbackWrapper = new Object();

		callbackWrapper.eventCallback = function (eventArgs) {

			if (eventArgs.seriesPrices != undefined) {

				var replacement = new Array();
				for (let [key, value] of eventArgs.seriesPrices) {
					replacement.push({
						seriesId: key.UniqueJavascriptId,
						seriesType: key.seriesType(),
						dataItem: value
					});
				}

				eventArgs.seriesPrices = replacement;
			}

			if (eventArgs.hoveredSeries != undefined) {
				eventArgs.hoveredSeries = eventArgs.hoveredSeries.UniqueJavascriptId;
			}

			dotNetHandler.invokeMethodAsync(methodName, eventArgs);
		}

		target[subscriptionMethod](callbackWrapper.eventCallback);
		return callbackWrapper;
	},

	unsubscribeFromEvent: function (target, callbackWrapper, unsubscriptionMethod) {
		target[unsubscriptionMethod](callbackWrapper.eventCallback);
	},

	takeScreenshot: function (target) {
		var canvas = target['takeScreenshot'].apply(target);
		var context = canvas.getContext('2d');
		var data = context.getImageData(0, 0, canvas.width, canvas.height);
		return new Uint8Array(data.data);
	},
	getUniqueJavascriptId: function (target) {
		return target.UniqueJavascriptId;
	}
};
