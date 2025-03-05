using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	file interface IJsonPropertyConverter
	{
		object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);

		void Write(Utf8JsonWriter writer, object instance, JsonSerializerOptions options);
	}

	file class JsonPropertyConverter<T>(JsonConverter<T> converter) : IJsonPropertyConverter
	{
		public object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> converter.Read(ref reader, typeToConvert, options);

		public void Write(Utf8JsonWriter writer, object instance, JsonSerializerOptions options)
			=> converter.Write(writer, (T)instance, options);
	}

	/// <summary>
	/// mapping between runtime name, json name
	/// </summary>
	/// <param name="PropertyName">runtime property name</param>
	/// <param name="JsonName">json member name</param>
	/// <param name="PropertyType">runtime property type</param>
	/// <param name="PropertyConverter">wrapper for the converter decorated on the property</param>
	file record PropertyMetadata(string PropertyName, string JsonName, Type PropertyType, IJsonPropertyConverter PropertyConverter);

	/// <summary>
	/// helper class that builds a list of <see cref="PropertyMetadata"/> using the public properties of the argument type
	/// </summary>
	file class TypeMetadata
	{
		public TypeMetadata(Type type)
		{
			foreach (var property in type.GetProperties())
			{
				var converterAttribute = property.GetCustomAttribute<JsonConverterAttribute>();
				var jsonNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
				IJsonPropertyConverter converter = null;
				if (converterAttribute != null)
				{
					var instance = (JsonConverter)Activator.CreateInstance(converterAttribute.ConverterType);
					var wrapperType = typeof(JsonPropertyConverter<>).MakeGenericType(property.PropertyType);
					converter = (IJsonPropertyConverter)Activator.CreateInstance(wrapperType, instance);
				}

				var jsonName = jsonNameAttribute?.Name ?? property.Name;
				_RuntimeProperties[property.Name] = new(property.Name, jsonName, property.PropertyType, converter);
				_JsonProperties[jsonName] = new(property.Name, jsonName, property.PropertyType, converter);
			}
		}

		Dictionary<string, PropertyMetadata> _RuntimeProperties = [];
		Dictionary<string, PropertyMetadata> _JsonProperties = [];

		/// <summary>
		/// find property using the runtime name
		/// </summary>
		public PropertyMetadata GetPropertyByRuntimeName(string name)
		{
			if (_RuntimeProperties.TryGetValue(name, out var metadata))
				return metadata;

			return null;
		}

		/// <summary>
		/// find property using the json member name
		/// </summary>
		public PropertyMetadata GetPropertyByJsonName(string name)
		{
			if (_JsonProperties.TryGetValue(name, out var metadata))
				return metadata;

			return null;
		}
	}

	/// <summary>
	/// <see cref="TypeMetadata"/> cache
	/// </summary>
	file static class TypeMetadataCache
	{
		static Dictionary<Type, TypeMetadata> _Cache = [];

		/// <summary>
		/// returns the cached instance or creates a new one if not cached yet
		/// </summary>
		/// <typeparam name="T">class type</typeparam>
		public static TypeMetadata Get<T>()
			where T : class
		{
			if (_Cache.TryGetValue(typeof(T), out var value))
				return value;

			return _Cache[typeof(T)] = new TypeMetadata(typeof(T));
		}
	}

	/// <summary>
	/// base class for all options objects<br/>
	/// only properties that have been set are written to the json object
	/// </summary>
	public abstract class JsonOptionsObject
	{
		/// <summary>
		/// converter that writes only the properties that have been set<br/>
		/// reads every property that has a mapping (<see cref="JsonPropertyNameAttribute"/>)
		/// </summary>
		/// <typeparam name="T">converted class type</typeparam>
		protected class JsonOptionsObjectConverter<T> : JsonConverter<T> where T : JsonOptionsObject
		{
			/// <summary>
			/// <inheritdoc/>
			/// </summary>
			/// <param name="reader"><inheritdoc/></param>
			/// <param name="typeToConvert"><inheritdoc/></param>
			/// <param name="options"><inheritdoc/></param>
			/// <returns><inheritdoc/></returns>
			public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				System.Diagnostics.Debug.Assert(reader.TokenType == JsonTokenType.StartObject);

				var metadata = TypeMetadataCache.Get<T>();
				var instance = Activator.CreateInstance<T>();

				while (reader.Read())
				{
					if (reader.TokenType == JsonTokenType.EndObject)
						break;

					var propertyName = reader.GetString();
					var property = metadata.GetPropertyByJsonName(propertyName);
					if (property == null)
					{
						reader.Read();
						continue;
					}

					object value;
					if (property.PropertyConverter == null)
						value = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);
					else
					{
						reader.Read();
						value = property.PropertyConverter.Read(ref reader, property.PropertyType, options);
					}

					instance._Properties[property.PropertyName] = value;
				}

				return instance;
			}

			/// <summary>
			/// <inheritdoc/>
			/// </summary>
			/// <param name="writer"><inheritdoc/></param>
			/// <param name="instance"><inheritdoc/></param>
			/// <param name="options"><inheritdoc/></param>
			public override void Write(Utf8JsonWriter writer, T instance, JsonSerializerOptions options)
			{
				if (instance == null)
				{
					writer.WriteNullValue();
					return;
				}

				var metadata = TypeMetadataCache.Get<T>();
				writer.WriteStartObject();
				foreach (var (name, value) in instance._Properties)
				{
#if DEBUG
					if (value is JsonOptionsObject jsonOptions)
						ValidateConverterAttribute(jsonOptions);
#endif
					var property = metadata.GetPropertyByRuntimeName(name);
					writer.WritePropertyName(property.JsonName);
					if (property.PropertyConverter == null)
						JsonSerializer.Serialize(writer, value, property.PropertyType, options);
					else
						property.PropertyConverter.Write(writer, value, options);
				}
				writer.WriteEndObject();
			}
		}

		readonly Dictionary<string, object> _Properties = [];

		/// <summary>
		/// returns true if the property is set (present in the internal dictionary)
		/// </summary>
		/// <param name="propertyName">runtime property name</param>
		protected bool IsSet(string propertyName)
			=> _Properties.ContainsKey(propertyName);

		/// <summary>
		/// returns the current value if set, otherwise the value provided by <paramref name="defaultValue"/> or the default for the type <typeparamref name="T"/><br/>
		/// if the default value is a <see cref="JsonOptionsObject"/>, the value is also stored in the internal properties
		/// </summary>
		/// <typeparam name="T">property type</typeparam>
		/// <param name="defaultValue">default property value</param>
		/// <param name="propertyName">runtime property name</param>
		protected T GetValue<T>(Func<T> defaultValue = null, [CallerMemberName] string propertyName = null)
		{
			if (_Properties.TryGetValue(propertyName, out var value))
				return (T)value;

			if (defaultValue != null)
			{
				var defValue = defaultValue();
				if (defValue is JsonOptionsObject)
					_Properties[propertyName] = defValue;

				return defValue;
			}

			return default;
		}

		/// <summary>
		/// adds the property+value to the internal dictionary<br/>
		/// (no equality checks are performed, meaning that even if default is set, property will be added to the json object)
		/// </summary>
		/// <typeparam name="T">property type</typeparam>
		/// <param name="value">property value</param>
		/// <param name="propertyName">runtime property name</param>
		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
			=> _Properties[propertyName] = value;

		/// <summary>
		/// removes the property from the internal value
		/// </summary>
		/// <param name="propertyName">runtime property name</param>
		/// <param name="value">stored value</param>
		/// <returns>return true if the value was present and removed</returns>
		protected bool ClearValue(string propertyName, out object value)
		{
			ArgumentException.ThrowIfNullOrEmpty(propertyName);
			return _Properties.Remove(propertyName, out value);
		}

		internal static void ValidateConverterAttribute(JsonOptionsObject target)
		{
			var type = target.GetType();
			var converter = type.GetCustomAttribute<JsonConverterAttribute>();
			if (converter == null)
				throw new InvalidOperationException("Custom converter expected");

			if (converter.ConverterType.GetGenericTypeDefinition() != typeof(JsonOptionsObjectConverter<>))
				throw new InvalidOperationException("Custom converter expected");

			if (converter.ConverterType.GetGenericArguments()[0] != target.GetType())
				throw new InvalidOperationException("Custom converter for the given type expected");
		}
	}
}
