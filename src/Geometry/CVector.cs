using System.Text.Json.Serialization;

namespace Crash.Geometry
{
	/// <summary>A 3 dimensional representation of a direction.</summary>
	[JsonConverter(typeof(CVectorConverter))]
	public struct CVector
	{
		/// <summary>The X Direction</summary>
		public double X { get; set; } = 0;

		/// <summary>The Y Direction</summary>
		public double Y { get; set; } = 0;

		/// <summary>The Z Direction</summary>
		public double Z { get; set; } = 0;


		/// <summary>Returns a non existant drection.</summary>
		public static CVector None => new(double.NaN, double.NaN, double.NaN);

		/// <summary>Returns a CVector at 0,0,0.</summary>
		public static CVector Origin => new(0);

		/// <summary>Empty Constructor</summary>
		public CVector() { }

		/// <summary>Creates a new CVector</summary>
		public CVector(double x = 0, double y = 0, double z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is not CVector cPoint)
			{
				return false;
			}

			return this == cPoint;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{X},{Y},{Z}";
		}

		/// <summary>Returns a Rounded CVector</summary>
		/// <param name="digits">the number</param>
		/// <returns>The number of fractional digits in the return value</returns>
		public CVector Round(int digits = 0)
		{
			return new CVector(Math.Round(X, digits),
				Math.Round(Y, digits),
				Math.Round(Z, digits));
		}

		/// <summary>Conperts a CPoint to a CVector</summary>
		public static implicit operator CVector(CPoint p)
		{
			return new CVector(p.X, p.Y, p.Z);
		}

		/// <summary>Tests for mathmatic equality</summary>
		public static bool operator ==(CVector v1, CVector v2)
		{
			return v1.X == v2.X &&
			       v1.Y == v2.Y &&
			       v1.Z == v2.Z;
		}

		/// <summary>Tests for mathmatic inequality</summary>
		public static bool operator !=(CVector v1, CVector v2)
		{
			return !(v1 == v2);
		}

		/// <summary>Subtracts p2 from p1</summary>
		public static CVector operator -(CVector v1, CVector v2)
		{
			return new CVector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		/// <summary>Adds p2 and p1</summary>
		public static CVector operator +(CVector v1, CVector v2)
		{
			return new CVector(v2.X + v1.X, v2.Y + v1.Y, v2.Z + v1.Z);
		}
	}
}
