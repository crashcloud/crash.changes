using System.Text.Json.Serialization;

namespace Crash.Geometry
{
	/// <summary>A 3 dimensional representation of a point.</summary>
	[JsonConverter(typeof(CPointConverter))]
	public struct CPoint
	{
		/// <summary>The X Coordinate</summary>
		public double X { get; set; } = 0;

		/// <summary>The Y Coordinate</summary>
		public double Y { get; set; } = 0;

		/// <summary>The Z Coordinate</summary>
		public double Z { get; set; } = 0;


		/// <summary>Returns a non existant point.</summary>
		public static CPoint None => new(double.NaN, double.NaN, double.NaN);

		/// <summary>Returns a Point at 0,0,0.</summary>
		public static CPoint Origin => new(0);

		/// <summary>Empty Constructor</summary>
		public CPoint() { }

		/// <summary>Creates a new CPoint</summary>
		public CPoint(double x = 0, double y = 0, double z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is not CPoint cPoint)
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

		/// <summary>Returns a Rounded CPoint</summary>
		/// <param name="digits">the number</param>
		/// <returns>The number of fractional digits in the return value</returns>
		public CPoint Round(int digits = 0)
		{
			return new CPoint(Math.Round(X, digits),
				Math.Round(Y, digits),
				Math.Round(Z, digits));
		}

		/// <summary>Conperts a CVector to a Cpoint</summary>
		public static implicit operator CPoint(CVector p)
		{
			return new CPoint(p.X, p.Y, p.Z);
		}

		/// <summary>Tests for mathmatic equality</summary>
		public static bool operator ==(CPoint p1, CPoint p2)
		{
			return p1.X == p2.X &&
			       p1.Y == p2.Y &&
			       p1.Z == p2.Z;
		}

		/// <summary>Tests for mathmatic inequality</summary>
		public static bool operator !=(CPoint p1, CPoint p2)
		{
			return !(p1 == p2);
		}

		/// <summary>Subtracts p2 from p1</summary>
		public static CPoint operator -(CPoint p1, CPoint p2)
		{
			return new CPoint(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
		}

		/// <summary>Adds p2 and p1</summary>
		public static CPoint operator +(CPoint p1, CPoint p2)
		{
			return new CPoint(p2.X + p1.X, p2.Y + p1.Y, p2.Z + p1.Z);
		}
	}
}
