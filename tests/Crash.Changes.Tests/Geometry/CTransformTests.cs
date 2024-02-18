namespace Crash.Changes.Tests.Geometry
{
	public sealed class CTransformTests
	{
		private const double MIN = -50.99;
		private const double MID = 0.0;
		private const double MAX = 5000.123;

		[TestCase(1, 0, 0)]
		[TestCase(1, 1, 0)]
		[TestCase(1, 1, 1)]
		[TestCase(1, 0, 1)]
		[TestCase(0, 0, 1)]
		[TestCase(0, 1, 1)]
		[TestCase(0, 1, 0)]
		[TestCase(0, 0, 0)]
		public void TransformTranslation(double x, double y, double z)
		{
			CTransform transform = new();
			CTransform secondTransform = new(1, m11: 1, m22: 1, m33: 1, m03: x, m13: y, m23: z);

			CTransform combined = CTransform.Combine(transform, secondTransform);

			double[,] expected = new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };

			Assert.That(combined, Is.EqualTo(expected));
		}

		[TestCase(1, 0, 0)]
		[TestCase(1, 1, 0)]
		[TestCase(1, 1, 1)]
		[TestCase(1, 0, 1)]
		[TestCase(0, 0, 1)]
		[TestCase(0, 1, 1)]
		[TestCase(0, 1, 0)]
		[TestCase(0, 0, 0)]
		public void TransformRotation(double rx, double ry, double rz)
		{
			CTransform transform = new();
			CTransform secondTransform = new(
				Math.Cos(ry) + Math.Cos(rz),
				-Math.Sin(rz),
				Math.Sin(ry),
				m10: Math.Sin(rz),
				m11: Math.Cos(rx) + Math.Cos(rz),
				m12: -Math.Sin(rx),
				m20: -Math.Sin(ry),
				m21: Math.Sin(rx),
				m22: Math.Cos(rx) + Math.Cos(ry));

			CTransform combined = CTransform.Combine(transform, secondTransform);

			double[,] expected = new double[4, 4]
			{
				{ Math.Cos(ry) + Math.Cos(rz), -Math.Sin(rz), Math.Sin(ry), 0 },
				{ Math.Sin(rz), Math.Cos(rx) + Math.Cos(rz), -Math.Sin(rx), 0 },
				{ -Math.Sin(ry), Math.Sin(rx), Math.Cos(rx) + Math.Cos(ry), 0 }, { 0, 0, 0, 1 }
			};
		}

		[TestCase(1, 0, 0)]
		[TestCase(1, 1, 0)]
		[TestCase(1, 1, 1)]
		[TestCase(1, 0, 1)]
		[TestCase(0, 0, 1)]
		[TestCase(0, 1, 1)]
		[TestCase(0, 1, 0)]
		[TestCase(0, 0, 0)]
		public void TransformScale(double sx, double sy, double sz)
		{
			CTransform transform = new();
			CTransform secondTransform = new(sx, m11: sy, m22: sz);

			CTransform combined = CTransform.Combine(transform, secondTransform);

			double[,] expected = new double[4, 4] { { sx, 0, 0, 0 }, { 0, sy, 0, 0 }, { 0, 0, sz, 0 }, { 0, 0, 0, 1 } };
		}

		/// <summary>
		///     Checks every single location in the matrix for equality failures
		/// </summary>
		[Test]
		public void Transform_Equality()
		{
			for (double i = 0; i < 16; i++)
			{
				IEnumerable<double> zeroArray = Enumerable.Range(0, 16).Select(v => i);
				CTransform left = new(zeroArray.ToArray());
				CTransform right = new(zeroArray.ToArray());

				Assert.That(left, Is.EqualTo(right));
			}
		}
	}
}
