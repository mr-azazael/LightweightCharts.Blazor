using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Models.Events
{
	internal class EventsHelper : IAsyncDisposable
	{
		interface IEventData : IList
		{
			IJSObjectReference JavascriptCallback { get; set; }
		}

		//holds the invocation list for a specific event
		class EventData<E> : List<EventHandler<E>>, IEventData
		{
			public EventData(EventsHelper helper)
			{
				Helper = helper;
			}

			public IJSObjectReference JavascriptCallback { get; set; }
			public EventsHelper Helper { get; set; }

			[JSInvokable(nameof(RaiseEvent))]
			public Task RaiseEvent(E args)
			{
				//raise the event
				foreach (var eventHandler in this)
					eventHandler(Helper._Target, args);

				return Task.CompletedTask;
			}
		}

		public EventsHelper(IJsObjectWrapper target, IJSRuntime jsRuntime)
		{
			_Target = target;
			_JsRuntime = jsRuntime;
			_Events = new Dictionary<EventDescriptor, IEventData>();
		}

		Dictionary<EventDescriptor, IEventData> _Events;
		IJsObjectWrapper _Target;
		IJSRuntime _JsRuntime;

		public async void AddEvent<T>(EventHandler<T> handler, EventDescriptor descriptor)
		{
			if (!_Events.ContainsKey(descriptor))
			{
				//create event wrapper and subscribe it to event
				var eventHandler = new EventData<T>(this);
				var dotnetRef = DotNetObjectReference.Create(eventHandler);
				eventHandler.JavascriptCallback = await JsModule.SubscribeToEvent(_JsRuntime, _Target.JsObjectReference, dotnetRef, descriptor, "RaiseEvent");
				_Events[descriptor] = eventHandler;
			}

			//add the handler to the invocation list
			_Events[descriptor].Add(handler);
		}

		public async void RemoveEvent<T>(EventHandler<T> handler, EventDescriptor descriptor)
		{
			//not subscribed
			if (!_Events.ContainsKey(descriptor))
				return;

			//remove the handler from invocation list
			if (_Events[descriptor].Contains(handler))
				_Events[descriptor].Remove(handler);

			if (_Events[descriptor].Count == 0)
			{
				//unsubscribe if the invocation list is empty
				await JsModule.UnsubscribeFromEvent(_JsRuntime, _Target.JsObjectReference, _Events[descriptor].JavascriptCallback, descriptor);
				_Events.Remove(descriptor);
			}
		}

		public async ValueTask DisposeAsync()
		{
			await Task.WhenAll(_Events.Select(async descriptor =>
			{
				//unregister all events
				await JsModule.UnsubscribeFromEvent(_JsRuntime, _Target.JsObjectReference, descriptor.Value.JavascriptCallback, descriptor.Key);
			}));

			_Events.Clear();
		}
	}
}
