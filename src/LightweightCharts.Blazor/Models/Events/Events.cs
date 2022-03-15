namespace LightweightCharts.Blazor.Models.Events
{
	internal static class Events
	{
		public static readonly EventDescriptor Click = new EventDescriptor
		{
			SubscribeMethod = "subscribeClick",
			UnsubscribeMethod = "unsubscribeClick"
		};

		public static readonly EventDescriptor CrosshairMove = new EventDescriptor
		{
			SubscribeMethod = "subscribeCrosshairMove",
			UnsubscribeMethod = "unsubscribeCrosshairMove"
		};

		public static readonly EventDescriptor VisibleTimeRangeChange = new EventDescriptor
		{
			SubscribeMethod = "subscribeVisibleTimeRangeChange",
			UnsubscribeMethod = "unsubscribeVisibleTimeRangeChange"
		};

		public static readonly EventDescriptor VisibleLogicalRangeChange = new EventDescriptor
		{
			SubscribeMethod = "subscribeVisibleLogicalRangeChange",
			UnsubscribeMethod = "unsubscribeVisibleLogicalRangeChange"
		};

		public static readonly EventDescriptor SizeChanged = new EventDescriptor
		{
			SubscribeMethod = "subscribeSizeChange",
			UnsubscribeMethod = "unsubscribeSizeChange"
		};
	}
}
