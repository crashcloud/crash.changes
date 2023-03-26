using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Crash.Geometry;

namespace Crash.Changes.Serialization
{

	/// <summary>
	/// Converts a CVector into json efficiently
	/// </summary>
	public sealed class CVectorConverter : JsonConverter<CVector>
	{

		/// <inheritdoc/>
		public override CVector Read(ref Utf8JsonReader reader, Type typeToConvert,
									JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
			{
				throw new JsonException();
			}

			string? x, y, z;

			if (!reader.Read()) throw new JsonException("Couldn't find x!");
			x = reader.GetString();

			if (!reader.Read()) throw new JsonException("Couldn't find y!");
			y = reader.GetString();

			if (!reader.Read()) throw new JsonException("Couldn't find z!");
			z = reader.GetString();

			if (reader.Read() && reader.TokenType == JsonTokenType.EndArray)
			{
				double xNum = FloatingDoubleConverter.FromString(x, CultureInfo.InvariantCulture);
				double yNum = FloatingDoubleConverter.FromString(y, CultureInfo.InvariantCulture);
				double zNum = FloatingDoubleConverter.FromString(z, CultureInfo.InvariantCulture);

				return new CVector(xNum, yNum, zNum);
			}
			else
			{
				throw new JsonException();
			}
		}

		/// <inheritdoc/>
		public override void Write(Utf8JsonWriter writer, CVector value,
									JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			writer.WriteStringValue(FloatingDoubleConverter.ToString(value.X, CultureInfo.InvariantCulture));
			writer.WriteStringValue(FloatingDoubleConverter.ToString(value.Y, CultureInfo.InvariantCulture));
			writer.WriteStringValue(FloatingDoubleConverter.ToString(value.Z, CultureInfo.InvariantCulture));
			writer.WriteEndArray();
		}

	}

}
