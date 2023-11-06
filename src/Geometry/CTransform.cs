﻿namespace Crash.Geometry
{
	/// <summary>A Transformation Matrix.</summary>
	[JsonConverter(typeof(CTransformConverter))]
	public struct CTransform : IEnumerable<double>
	{
		/// <summary>the transform matrix</summary>
		private readonly double[,] _transforms;

		/// <summary>An Empty Transform</summary>
		public static CTransform Unset = new();

		/// <summary>Defaults to the identity matrix</summary>
		public CTransform()
		{
			_transforms = GetIdentityMatrix();
		}

		/// <summary>Creates a new CTransform</summary>
		public CTransform(double m00 = 0, double m01 = 0, double m02 = 0, double m03 = 0,
			double m10 = 0, double m11 = 0, double m12 = 0, double m13 = 0,
			double m20 = 0, double m21 = 0, double m22 = 0, double m23 = 0,
			double m30 = 0, double m31 = 0, double m32 = 0, double m33 = 0)
			: this()
		{
			_transforms[0,0] = m00;
			_transforms[0,1] = m01;
			_transforms[0, 2] = m02;
			_transforms[0, 3] = m03;
			_transforms[1, 0] = m10;
			_transforms[1, 1] = m11;
			_transforms[1, 2] = m12;
			_transforms[1, 3] = m13;
			_transforms[2, 0] = m20;
			_transforms[2, 1] = m21;
			_transforms[2, 2] = m22;
			_transforms[2, 3] = m23;
			_transforms[3, 0] = m30;
			_transforms[3, 1] = m31;
			_transforms[3, 2] = m32;
			_transforms[3, 3] = m33;
		}

		/// <summary>Creates a new CTransform from an array of arrays</summary>
		public CTransform(params double[] mValues)
			: this()
		{
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 4; col++)
				{
					int colValue = (row + 1) * (col + 1);
					if (col == 0 && row == col)
					{
						colValue = 0;
					}

					if (colValue >= mValues.Length)
					{
						return;
					}

					_transforms[row, col] = mValues[colValue];
				}
			}
		}

		/// <summary>Returns a Matrix with uniform values</summary>
		private static double[][] GetUniformMatrix(double value)
		{
			double[][] matrix =
			{
				new double[4] { value, value, value, value }, new double[4] { value, value, value, value },
				new double[4] { value, value, value, value }, new double[4] { value, value, value, value }
			};
			return matrix;
		}

		/// <summary>Returns a Identity Matrix</summary>
		private static double[,] GetIdentityMatrix()
		{
			double[,] matrix =
			{
				{ 1, 0, 0, 0 }, { 0, 1, 0, 0 },
				{ 0, 0, 1, 0 }, { 0, 0, 0, 1 }
			};
			return matrix;
		}

		/// <summary>Returns the value at the given coordinate</summary>
		public double this[int row, int column]
		{
			get => _transforms[row,column];
			set => _transforms[row, column] = value;
		}

		public static double[] Flatten(double[,] twoDimensionalArray)
		{
			int numRows = twoDimensionalArray.GetLength(0);
			int numCols = twoDimensionalArray.GetLength(1);
			double[] oneDimensionalArray = new double[numRows * numCols];
			int index = 0;

			for (int row = 0; row < numRows; row++)
			{
				for (int col = 0; col < numCols; col++)
				{
					oneDimensionalArray[index] = twoDimensionalArray[row, col];
					index++;
				}
			}

			return oneDimensionalArray;
		}

		/// <summary>Tests the Matrix for any NaN's or infinity numbers</summary>
		public bool IsValid()
		{
			double[] values = Flatten(_transforms);

			// Check that the matrix is all zeros
			if (values.All(v => v == 0))
				return true;

			return values.Length == 16 &&
			       !values.Any(v =>
			       {
				       return double.IsNaN(v) ||
				              double.IsInfinity(v) ||
				              double.MaxValue == v ||
				              double.MinValue == v;
			       });
		}

		/// <summary>Returns an Enumerator of all the values</summary>
		public IEnumerator<double> GetEnumerator()
		{
			return Flatten(_transforms).ToList().GetEnumerator();
		}

		/// <summary>Returns an Enumerator of all the values</summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Combines two transforms and returns the displacement product
		///     perform matrix multiplication (dot product)
		/// </summary>
		/// <param name="initialTransform"></param>
		/// <param name="newTransform"></param>
		/// <returns>The combined Transform</returns>
		public static CTransform Combine(CTransform initialTransform, CTransform newTransform)
		{
			int rows1 = 4;
			int cols1 = 4;
			int cols2 = 4;

			CTransform result = new CTransform();

			for (int i = 0; i < rows1; i++)
			{
				for (int j = 0; j < cols2; j++)
				{
					double sum = 0;
					for (int k = 0; k < cols1; k++)
					{
						sum += initialTransform[i, k] * newTransform[k, j];
					}
					result[i, j] = sum;
				}
			}

			return result;
		}
	}
}
