window.javascriptHelpers = {
	setAutoscaleInfoProvider: function (series) {
		series.applyOptions({
			autoscaleInfoProvider: () => ({
				priceRange: {
					minValue: -75000,
					maxValue: 75000
				},
				marging: {
					above: 10,
					bellow: 10
				}
			})
		})
	},
	customTimeFormatter: function (time) {
		var date = new Date(time * 1000);
		return 'cross at: ' + date.toLocaleTimeString(navigator.language, { hour: 'numeric', minute: 'numeric' });
	}
};
