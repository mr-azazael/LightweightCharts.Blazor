using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Converters
{
	/// <summary>
	/// Base class for enum to javascript converters.
	/// </summary>
	/// <typeparam name="T">An enum type.</typeparam>
	internal abstract class BaseEnumJsonConverter<T> : JsonConverter<T> where T : Enum
	{
		Dictionary<T, string> _Mapping;

		public BaseEnumJsonConverter()
		{
			_Mapping = GetEnumMapping();
		}

		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var nextToken = reader.GetString();
			string enumName;

			if (int.TryParse(nextToken, out int intValue))
				enumName = intValue.ToString();
			else
				enumName = nextToken;

			if (_Mapping.ContainsValue(enumName))
				return _Mapping.First(x => x.Value == enumName).Key;

			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(_Mapping[value]);
		}

		protected abstract Dictionary<T, string> GetEnumMapping();
	}
}
