using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Crash.Geometry;

namespace Crash.Changes.Serialization
{
	/// <summary>Converts a CTransform into json efficiently</summary>
	public sealed class CTransformConverter : JsonConverter<CTransform>
	{
		/// <inheritdoc />
		public override CTransform Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string[][]? values = JsonSerializer.Deserialize<string[][]>(ref reader, options);
			return new CTransform(
				FloatingDoubleConverter.FromString(values[0][0], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[0][1], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[0][2], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[0][3], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[1][0], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[1][1], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[1][2], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[1][3], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[2][0], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[2][1], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[2][2], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[2][3], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[3][0], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[3][1], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[3][2], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(values[3][3], CultureInfo.InvariantCulture)
			);
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, CTransform value, JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer,
				new[]
				{
					new[]
					{
						FloatingDoubleConverter.ToString(value[0, 0], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[0, 1], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[0, 2], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[0, 3], CultureInfo.InvariantCulture)
					},
					new[]
					{
						FloatingDoubleConverter.ToString(value[1, 0], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[1, 1], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[1, 2], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[1, 3], CultureInfo.InvariantCulture)
					},
					new[]
					{
						FloatingDoubleConverter.ToString(value[2, 0], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[2, 1], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[2, 2], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[2, 3], CultureInfo.InvariantCulture)
					},
					new[]
					{
						FloatingDoubleConverter.ToString(value[3, 0], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[3, 1], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[3, 2], CultureInfo.InvariantCulture),
						FloatingDoubleConverter.ToString(value[3, 3], CultureInfo.InvariantCulture)
					}
				}, options);
		}
	}
}
