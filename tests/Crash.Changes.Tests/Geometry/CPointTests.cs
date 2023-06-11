namespace Crash.Changes.Tests.Geometry
{

	public sealed class CPointTests
	{

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void Equality(int count)
		{
			var random = TestContext.CurrentContext.Random;

			for (int i = 0; i < count; i++)
			{
				double x = random.NextDouble(short.MinValue, short.MaxValue);
				double y = random.NextDouble(short.MinValue, short.MaxValue);
				double z = random.NextDouble(short.MinValue, short.MaxValue);

				CPoint p1 = new(x, y, z);
				CPoint p2 = new(x, y, z);

				Assert.Multiple(() =>
				{
					Assert.That(p1.X, Is.EqualTo(p2.X));
					Assert.That(p1.Y, Is.EqualTo(p2.Y));
					Assert.That(p1.Z, Is.EqualTo(p2.Z));

					Assert.That(p1, Is.EqualTo(p2));
				});
			}
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void Inequality(int count)
		{
			var random = TestContext.CurrentContext.Random;

			for (int i = 0; i < count; i++)
			{
				double x1 = random.NextDouble(short.MinValue, short.MaxValue);
				double y1 = random.NextDouble(short.MinValue, short.MaxValue);
				double z1 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p1 = new(x1, y1, z1);

				double x2 = random.NextDouble(short.MinValue, short.MaxValue);
				double y2 = random.NextDouble(short.MinValue, short.MaxValue);
				double z2 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p2 = new(x2, y2, z2);

				Assert.Multiple(() =>
				{
					Assert.That(x1, Is.Not.EqualTo(x2));
					Assert.That(y1, Is.Not.EqualTo(y2));
					Assert.That(z1, Is.Not.EqualTo(z2));

					Assert.That(p1, Is.Not.EqualTo(p2));
				});
			}
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void Subtract(int count)
		{
			var random = TestContext.CurrentContext.Random;

			for (int i = 0; i < count; i++)
			{
				double x1 = random.NextDouble(short.MinValue, short.MaxValue);
				double y1 = random.NextDouble(short.MinValue, short.MaxValue);
				double z1 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p1 = new(x1, y1, z1);

				double x2 = random.NextDouble(short.MinValue, short.MaxValue);
				double y2 = random.NextDouble(short.MinValue, short.MaxValue);
				double z2 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p2 = new(x2, y2, z2);

				Assert.Multiple(() =>
				{
					Assert.That(x1, Is.Not.EqualTo(x2));
					Assert.That(y1, Is.Not.EqualTo(y2));
					Assert.That(z1, Is.Not.EqualTo(z2));
				});

				CPoint p3 = p2 - p1;

				Assert.Multiple(() =>
				{
					Assert.That(p3.X, Is.EqualTo(p2.X - p1.X));
					Assert.That(p3.Y, Is.EqualTo(p2.Y - p1.Y));
					Assert.That(p3.Z, Is.EqualTo(p2.Z - p1.Z));

					Assert.That(p1, Is.Not.EqualTo(p3));
					Assert.That(p2, Is.Not.EqualTo(p3));
					Assert.That(p2, Is.Not.EqualTo(p1));
				});

				CPoint p4 = p1 - p2;

				Assert.Multiple(() =>
				{
					Assert.That(p4.X, Is.EqualTo(p1.X - p2.X));
					Assert.That(p4.Y, Is.EqualTo(p1.Y - p2.Y));
					Assert.That(p4.Z, Is.EqualTo(p1.Z - p2.Z));

					Assert.That(p2, Is.Not.EqualTo(p4));
					Assert.That(p1, Is.Not.EqualTo(p4));
					Assert.That(p1, Is.Not.EqualTo(p2));
				});
			}
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(100)]
		public void Addition(int count)
		{
			var random = TestContext.CurrentContext.Random;

			for (int i = 0; i < count; i++)
			{
				double x1 = random.NextDouble(short.MinValue, short.MaxValue);
				double y1 = random.NextDouble(short.MinValue, short.MaxValue);
				double z1 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p1 = new(x1, y1, z1);

				double x2 = random.NextDouble(short.MinValue, short.MaxValue);
				double y2 = random.NextDouble(short.MinValue, short.MaxValue);
				double z2 = random.NextDouble(short.MinValue, short.MaxValue);
				CPoint p2 = new(x2, y2, z2);

				Assert.Multiple(() =>
				{
					Assert.That(x1, Is.Not.EqualTo(x2));
					Assert.That(y1, Is.Not.EqualTo(y2));
					Assert.That(z1, Is.Not.EqualTo(z2));
				});
				CPoint p3 = p2 + p1;

				Assert.Multiple(() =>
				{
					Assert.That(p3.X, Is.EqualTo(p2.X + p1.X));
					Assert.That(p3.Y, Is.EqualTo(p2.Y + p1.Y));
					Assert.That(p3.Z, Is.EqualTo(p2.Z + p1.Z));

					Assert.That(p1, Is.Not.EqualTo(p3));
					Assert.That(p2, Is.Not.EqualTo(p3));
					Assert.That(p2, Is.Not.EqualTo(p1));
				});
			}
		}
	}
}
