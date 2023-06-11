using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crash.Changes.Tests.Serialization
{
	[TestFixture]
	public sealed class CTransformSerializationTests
	{

		internal readonly static JsonSerializerOptions TestOptions;

		static CTransformSerializationTests()
		{
			TestOptions = new JsonSerializerOptions()
			{
				IgnoreReadOnlyFields = true,
				IgnoreReadOnlyProperties = true,
				IncludeFields = true,
				NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
				ReadCommentHandling = JsonCommentHandling.Skip,
				WriteIndented = true, // TODO : Should this be avoided? Does it add extra memory?
			};
		}

		[TestCase(double.NaN, double.NaN, double.NaN)]
		[TestCase(double.MaxValue, double.MinValue, double.NaN)]
		[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		public void TestCTransformSerializationMaximums(double x, double y, double z)
		{
			TestCTransformSerializtion(new CTransform(x, y, z));
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void TestCTransformSerializationRandom(int count)
		{
			for (var i = 0; i < count; i++)
			{
				double[] values = new double[16];
				for (int j = 0; j < 16; j++)
				{
					values[j] = TestContext.CurrentContext.Random.NextDouble(short.MinValue, short.MaxValue);
				}

				TestCTransformSerializtion(new CTransform(values));
			}
		}

		private static void TestCTransformSerializtion(CTransform CTransform)
		{
			var json = JsonSerializer.Serialize(CTransform, TestOptions);
			var cTransformOut = JsonSerializer.Deserialize<CTransform>(json, TestOptions);
			Assert.Multiple(() =>
			{
				for (int x = 0; x < 4; x++)
				{
					for (int y = 0; x < 4; x++)
					{
						Assert.That(CTransform[x, y], Is.EqualTo(cTransformOut[x, y]));
					}
				}
			});
		}
	}
}
