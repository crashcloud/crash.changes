using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Crash.Geometry;

namespace Crash.Changes.Serialization
{
	/// <summary>Converts a CPoint into json efficiently</summary>
	public sealed class CPointConverter : JsonConverter<CPoint>
	{
		/// <inheritdoc />
		public override CPoint Read(ref Utf8JsonReader reader, Type typeToConvert,
			JsonSerializerOptions options)
		{
			string[]? array = JsonSerializer.Deserialize<string[]>(ref reader, options);

			if (array is null || array.Length < 3)
			{
				return CPoint.None;
			}

			return new CPoint(
				FloatingDoubleConverter.FromString(array[0], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(array[1], CultureInfo.InvariantCulture),
				FloatingDoubleConverter.FromString(array[2], CultureInfo.InvariantCulture)
			);
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, CPoint value,
			JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer,
				new[]
				{
					FloatingDoubleConverter.ToString(value.X, CultureInfo.InvariantCulture),
					FloatingDoubleConverter.ToString(value.Y, CultureInfo.InvariantCulture),
					FloatingDoubleConverter.ToString(value.Z, CultureInfo.InvariantCulture)
				}, options);
		}
	}
}
