/*download 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.production.js' into lightweight-charts.standalone.js for release builds*/
/*use 'https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.development.js' for debugging*/

window.lightweightChartsBlazor = {
	//interop
	createChart: function (containerId, chartConfig) {
		var container = document.getElementById(containerId);
		return LightweightCharts.createChart(container, chartConfig);
	},
	//interop
	createYieldCurveChart: function (containerId, chartConfig) {
		var container = document.getElementById(containerId);
		return LightweightCharts.createYieldCurveChart(container, chartConfig);
	},
	//interop
	createOptionsChart: function (containerId, chartConfig) {
		var container = document.getElementById(containerId);
		return LightweightCharts.createOptionsChart(container, chartConfig);
	},
	//interop
	addSeries: function (chart, type, options, paneIndex) {
		chart = lightweightChartsBlazor.getStoredReference(chart);
		lightweightChartsBlazor.replaceJsDelegates(options);
		let descriptor = null;
		switch (type) {
			case 'Line':
				descriptor = LightweightCharts.LineSeries;
				break;
			case 'Area':
				descriptor = LightweightCharts.AreaSeries;
				break;
			case 'Bar':
				descriptor = LightweightCharts.BarSeries;
				break;
			case 'Candlestick':
				descriptor = LightweightCharts.CandlestickSeries;
				break;
			case 'Histogram':
				descriptor = LightweightCharts.HistogramSeries;
				break;
			case 'Baseline':
				descriptor = LightweightCharts.BaselineSeries;
				break;
		}

		let series = chart.addSeries(descriptor, options, paneIndex);
		if (series != undefined && series.uniqueJavascriptId == undefined)
			series.uniqueJavascriptId = this.generateJavascriptId();

		return series;
	},
	//interop
	lightweightChartsInvoke: async function (target, method, replaceDelegates /*args*/) {
		var args = Array.prototype.slice.call(arguments, 3);
		if (replaceDelegates) {
			for (let arg in args)
				lightweightChartsBlazor.replaceJsDelegates(args[arg]);
		}

		target = lightweightChartsBlazor.getStoredReference(target);
		for (let i = 0; i < args.length; i++)
			args[i] = lightweightChartsBlazor.getStoredReference(args[i]);

		var result = target[method].apply(target, args);
		if (typeof result === 'object' && typeof result.then === 'function')
			result = await result;

		lightweightChartsBlazor.ensureUniqueJavascriptId(result);
		if (!result || lightweightChartsBlazor.isValueSerializable(result))
			return result;

		if (!Array.isArray(result)) {
			return lightweightChartsBlazor.createStoredReference(result);
		}

		let referencesArray = [];
		for (const item of result) {
			const stored = lightweightChartsBlazor.createStoredReference(item);
			const ref = DotNet.createJSObjectReference(stored);
			referencesArray.push(ref);
		}

		return referencesArray;
	},
	//interop
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

		target = lightweightChartsBlazor.getStoredReference(target);
		target[subscriptionMethod](callbackWrapper.eventCallback);
		return callbackWrapper;
	},
	//interop
	unsubscribeFromEvent: function (target, callbackWrapper, unsubscriptionMethod) {
		target = lightweightChartsBlazor.getStoredReference(target);
		target[unsubscriptionMethod](callbackWrapper.eventCallback);
	},
	//interop
	takeScreenshot: function (target) {
		target = lightweightChartsBlazor.getStoredReference(target);
		var canvas = target['takeScreenshot'].apply(target);
		var context = canvas.getContext('2d');
		var data = context.getImageData(0, 0, canvas.width, canvas.height);
		return new Uint8Array(data.data);
	},
	//local
	getUniqueJavascriptId: function (target) {
		return target.uniqueJavascriptId;
	},
	//local
	generateJavascriptId: function () {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
		}

		return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
	},
	//local
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
	},
	//interop
	createImageWatermark: function (pane, imageUrl, options) {
		pane = lightweightChartsBlazor.getStoredReference(pane);
		lightweightChartsBlazor.replaceJsDelegates(options);
		return LightweightCharts.createImageWatermark(pane, imageUrl, options);
	},
	//interop
	createTextWatermark: function (pane, options) {
		pane = lightweightChartsBlazor.getStoredReference(pane);
		lightweightChartsBlazor.replaceJsDelegates(options);
		return LightweightCharts.createTextWatermark(pane, options);
	},
	//interop
	createSeriesMarkers: function (series, markers) {
		series = lightweightChartsBlazor.getStoredReference(series);
		return LightweightCharts.createSeriesMarkers(series, markers);
	},
	//interop
	createUpDownMarkers: function (series, options) {
		series = lightweightChartsBlazor.getStoredReference(series);
		return LightweightCharts.createUpDownMarkers(series, options);
	},
	version: function () {
		return LightweightCharts.version();
	},
	//local
	ensureUniqueJavascriptId: function (target) {
		if (!target)
			return;

		if (!Array.isArray(target)) {
			if (target.uniqueJavascriptId == undefined)
				target.uniqueJavascriptId = lightweightChartsBlazor.generateJavascriptId();
			return;
		}

		for (const item of target)
			lightweightChartsBlazor.ensureUniqueJavascriptId(item);
	},
	//local
	isValueSerializable: function (value) {
		if (value === null)
			return true;

		switch (typeof value) {
			case 'string':
			case 'boolean':
			case 'number':
			case 'bigint':
				return true;
		}

		if (Array.isArray(value))
			return value.every(lightweightChartsBlazor.isValueSerializable);

		if (typeof value === 'object') {
			if (Object.getPrototypeOf(value) !== Object.prototype)
				return false;

			return Object.values(value).every(lightweightChartsBlazor.isValueSerializable);
		}

		return false;
	},
	//local
	createStoredReference: function (reference) {
		if (Array.isArray(reference))
			throw new Error('unexpected array reference');

		lightweightChartsBlazor.ensureUniqueJavascriptId(reference);
		if (!lightweightChartsBlazor.storedReferencesMap)
			lightweightChartsBlazor.storedReferencesMap = new Map();

		ref = { __uniqueJavascriptId: reference.uniqueJavascriptId };
		lightweightChartsBlazor.storedReferencesMap.set(reference.uniqueJavascriptId, reference);
		return ref;
	},
	//local
	getStoredReference: function (reference) {
		if (reference.__uniqueJavascriptId === undefined)
			return reference;

		if (!lightweightChartsBlazor.storedReferencesMap)
			throw new Error('unknown reference');

		const object = lightweightChartsBlazor.storedReferencesMap.get(reference.__uniqueJavascriptId);
		if (!object)
			throw new Error('unknown reference');

		return object;
	}
};
