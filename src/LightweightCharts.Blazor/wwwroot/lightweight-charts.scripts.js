/*download 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.production.js' into lightweight-charts.standalone.js for release builds*/
/*use 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.development.js' for debugging*/

window.lightweightChartsBlazor = {

	createChartLayout: function (containerId, chartConfig) {
		var container = document.getElementById(containerId);
		return LightweightCharts.createChart(container, chartConfig);
	},
	lightweightChartsInvoke: async function (target, method, replaceDelegates /*args*/) {
		var args = Array.prototype.slice.call(arguments, 3);
		if (replaceDelegates) {
			for (let arg in args)
				lightweightChartsBlazor.replaceJsDelegates(args[arg]);
		}

		var result = target[method].apply(target, args);
		if (typeof result === 'object' && typeof result.then === 'function')
			result = await result;

		if (result != undefined && result.uniqueJavascriptId == undefined)
			result.uniqueJavascriptId = this.generateJavascriptId();

		return result;
	},
	subscribeToEvent: function (target, dotNetHandler, subscriptionMethod, methodName) {
		let callbackWrapper = {
			eventCallback: async function (eventArgs) {
				if (eventArgs.seriesData != undefined) {
					var replacement = new Array();
					for (let [key, value] of eventArgs.seriesData) {
						replacement.push({
							seriesId: key.uniqueJavascriptId,
							seriesType: key.seriesType(),
							dataItem: value
						});
					}

					eventArgs.seriesData = replacement;
				}

				if (eventArgs.hoveredSeries != undefined) {
					eventArgs.hoveredSeries = eventArgs.hoveredSeries.uniqueJavascriptId;
				}

				//source event might contain a ref to the window object, which causes a circular exception in the json serialization process
				if (eventArgs.sourceEvent != undefined)
					eventArgs.sourceEvent = {
						clientX: eventArgs.sourceEvent.clientX ?? 0,
						clientY: eventArgs.sourceEvent.clientY ?? 0,
						pageX: eventArgs.sourceEvent.pageX ?? 0,
						pageY: eventArgs.sourceEvent.pageY ?? 0,
						screenX: eventArgs.sourceEvent.screenX ?? 0,
						screenY: eventArgs.sourceEvent.screenY ?? 0,
						localX: eventArgs.sourceEvent.localX ?? 0,
						localY: eventArgs.sourceEvent.localY ?? 0,
						ctrlKey: eventArgs.sourceEvent.ctrlKey ?? false,
						altKey: eventArgs.sourceEvent.altKey ?? false,
						shiftKey: eventArgs.sourceEvent.shiftKey ?? false,
						metaKey: eventArgs.sourceEvent.metaKey ?? false
					};

				await dotNetHandler.invokeMethodAsync(methodName, eventArgs);
			}
		};

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
		return target.uniqueJavascriptId;
	},
	generateJavascriptId: function () {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
		}

		return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
	},
	replaceJsDelegates: function (target) {
		if (!target)
			return;

		for (let property in target) {
			let value = target[property];
			if (!value)
				continue;

			let source = window;
			if (value?.objectType === 'JsDelegate') {
				const path = value.delegateName.split('.');
				for (let token of path) {
					//error if source is undefined
					source = source[token];
				}
				target[property] = source;
			}
			else if (typeof value === 'object')
				lightweightChartsBlazor.replaceJsDelegates(value);
		}
	}
};
