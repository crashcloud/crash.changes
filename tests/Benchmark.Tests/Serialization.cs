using System.Text.Json;

using Crash.Geometry;

namespace Benchmark.Tests
{
	[HtmlExporter]
	[MemoryDiagnoser]
	public class Serialization
	{

		[Params(1, 100)]
		public int Nloops { get; set; }

		readonly CPoint point;
		readonly CVector vector;
		readonly CTransform transform;
		readonly JsonSerializerOptions options;

		readonly string cPoint_json;
		readonly string cVector_json;
		readonly string cTransform_json;

		static readonly Random RAND = new();

		public Serialization()
		{
			point = new CPoint(
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue));

			vector = new CVector(
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue));

			transform = new CTransform(
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),

				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),

				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),

				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue),
				RAND.Next(short.MinValue, short.MaxValue));

			options = new();

			cPoint_json = JsonSerializer.Serialize(point, options);
			cVector_json = JsonSerializer.Serialize(point, options);
			cTransform_json = JsonSerializer.Serialize(point, options);
		}

		[Benchmark]
		public void Serialize_CPoint()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Serialize(point, options);
			}
		}

		[Benchmark]
		public void DeSerialize_CPoint()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Deserialize<CPoint>(cPoint_json, options);
			}
		}

		[Benchmark]
		public void Serialize_CVector()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Serialize(vector, options);
			}
		}

		[Benchmark]
		public void DeSerialize_CVector()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Deserialize<CVector>(cVector_json, options);
			}
		}

		[Benchmark]
		public void Serialize_CTransform()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Serialize(transform, options);
			}
		}

		[Benchmark]
		public void DeSerialize_CTransform()
		{
			for (int i = 0; i < Nloops; i++)
			{
				JsonSerializer.Deserialize<CTransform>(cTransform_json, options);
			}
		}
	}
}
