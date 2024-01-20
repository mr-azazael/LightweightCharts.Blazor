window.javascriptHelpers = {
	setTimeScaleTickMarkFormatter: function (timeScale) {
		timeScale.applyOptions({
			tickMarkFormatter: (time, type, locale) => {
				var date = new Date(time * 1000);
				return 'D/M: ' + date.toLocaleString(locale, { day: 'numeric', month: 'short' });
			}
		})
	},
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
	}
};
