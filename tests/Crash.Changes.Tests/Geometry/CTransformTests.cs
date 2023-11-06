using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash.Changes.Tests.Geometry
{
	public sealed class CTransformTests
	{
		[TestCase(1,0,0)]
		[TestCase(1, 1, 0)]
		[TestCase(1, 1, 1)]
		[TestCase(1, 0, 1)]
		[TestCase(0, 0, 1)]
		[TestCase(0, 1, 1)]
		[TestCase(0, 1, 0)]
		[TestCase(0, 0, 0)]
		public void TransformTranslation(double x, double y, double z)
		{
			CTransform transform = new CTransform();
			CTransform secondTransform = new CTransform(m00:1,m11:1,m22:1,m33:1 ,m03:x,m13:y,m23:z);

			CTransform combined = CTransform.Combine(transform, secondTransform);

			double[,] expected = new double[4, 4]
			{
				{ 1, 0, 0, x },
				{ 0, 1, 0, y },
				{ 0, 0, 1, z },
				{ 0, 0, 0, 1 }
			};

			Assert.That(combined, Is.EqualTo(expected));
		}

		public void TransformRotation() { }	

		public void TransformScale() { }


	}
}
