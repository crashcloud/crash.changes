using System.Globalization;

namespace Crash.Changes.Serialization
{

	/// <summary>Utility for converting strings to numbers</summary>
	internal static class FloatingDoubleConverter
	{

		const string NaN = "NaN";
		const string PositiveInfinity = "+∞";
		const string NegativeInfinity = "-∞";

		/// <summary>Converts the number to string</summary>
		internal static string ToString(double number, IFormatProvider provider)
		{
			if (double.IsNaN(number))
				return NaN;

			if (double.IsPositiveInfinity(number))
				return PositiveInfinity;

			if (double.IsNegativeInfinity(number))
				return NegativeInfinity;

			return number.ToString(provider);
		}

		/// <summary>Converts the number from string</summary>
		internal static double FromString(string? number, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(number))
				return 0;

			if (number.Equals(NaN, StringComparison.Ordinal))
				return double.NaN;

			if (number.Equals(PositiveInfinity, StringComparison.Ordinal))
				return double.PositiveInfinity;

			if (number.Equals(NegativeInfinity, StringComparison.Ordinal))
				return double.NegativeInfinity;

			if (double.TryParse(number, NumberStyles.Float, provider, out var result))
				return result;

			return 0;
		}

	}
}
