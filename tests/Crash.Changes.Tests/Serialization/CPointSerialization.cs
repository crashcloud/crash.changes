using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crash.Changes.Tests.Serialization
{
	[TestFixture]
	public sealed class CPointSerializationTests
	{
		internal static readonly JsonSerializerOptions TestOptions;

		static CPointSerializationTests()
		{
			TestOptions = new JsonSerializerOptions
			{
				IgnoreReadOnlyFields = true,
				IgnoreReadOnlyProperties = true,
				IncludeFields = true,
				NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
				ReadCommentHandling = JsonCommentHandling.Skip,
				WriteIndented = true // TODO : Should this be avoided? Does it add extra memory?
			};
		}

		[TestCase(double.NaN, double.NaN, double.NaN)]
		[TestCase(double.MaxValue, double.MinValue, double.NaN)]
		[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		public void TestCPointSerializationMaximums(double x, double y, double z)
		{
			TestCPointSerializtion(new CPoint(x, y, z));
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void TestCPointSerializationRandom(int count)
		{
			for (int i = 0; i < count; i++)
			{
				double x = TestContext.CurrentContext.Random.NextDouble(short.MinValue, short.MaxValue);
				double y = TestContext.CurrentContext.Random.NextDouble(short.MinValue, short.MaxValue);
				double z = TestContext.CurrentContext.Random.NextDouble(short.MinValue, short.MaxValue);
				TestCPointSerializtion(new CPoint(x, y, z));
			}
		}

		[Theory]
		// [TestCase("")]
		// [TestCase(null)]
		[TestCase("[]")]
		[TestCase("[\"[]\")]")]
		public void TestCPointSerializationInValid(string? value)
		{
			Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<CPoint>(value, TestOptions));
		}

		private static void TestCPointSerializtion(CPoint cpoint)
		{
			string json = JsonSerializer.Serialize(cpoint, TestOptions);
			CPoint cPointOut = JsonSerializer.Deserialize<CPoint>(json, TestOptions);
			Assert.Multiple(() =>
			{
				Assert.That(cpoint.X, Is.EqualTo(cPointOut.X));
				Assert.That(cpoint.Y, Is.EqualTo(cPointOut.Y));
				Assert.That(cpoint.Z, Is.EqualTo(cPointOut.Z));
			});
		}
	}
}
