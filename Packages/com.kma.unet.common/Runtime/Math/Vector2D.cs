using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace UNet
{
    public struct Vector2D
	{
		public float X { get; set; }
		public float Y { get; set; }

        public Vector2D(float x = 0, float y = 0)
		{
			X = x;
			Y = y;
		}

        public float Magnitude
        {
            readonly get
            {
                return Mathf.Sqrt(X * X + Y * Y);
            }
			set
			{
				var scale = value / Magnitude;
				X *= scale;
				Y *= scale;
			}
        }

        public float MagnitudeSqr
        {
            readonly get
            {
                return X * X + Y * Y;
            }
            set
            {
                var scale = value / MagnitudeSqr;
                X *= scale;
                Y *= scale;
            }
        }

        public Vector2D Normalized
		{
            readonly get
			{
				var mag = Magnitude;
				return mag == 0 ? Zero : this / Magnitude;
			}
			set
			{
				var mag = Magnitude;
				X = value.X * mag;
				Y = value.Y * mag;
			}
		}

        public static Vector2D Zero => new(0, 0);
		public static Vector2D Left => new(-1, 0);
		public static Vector2D Right => new(1, 0);
		public static Vector2D Up => new(0, 1);
		public static Vector2D Down => new(0, -1);
		public static Vector2D One => new(1, 1);

        public static Vector2D operator +(Vector2D a) => new(a.X, a.Y);
        public static Vector2D operator -(Vector2D a) => new(-a.X, -a.Y);
        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, Vector2D b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector2D operator /(Vector2D a, Vector2D b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2D operator *(Vector2D a, float b) => new(a.X * b, a.Y * b);
        public static Vector2D operator /(Vector2D a, float b) => new(a.X / b, a.Y / b);
        public static Vector2D operator *(float b, Vector2D a) => new(a.X * b, a.Y * b);
        public static Vector2D operator /(float b, Vector2D a) => new(a.X / b, a.Y / b);

        public static implicit operator Vector2(Vector2D param)
		{
			return new Vector2(param.X, param.Y);
		}

		public static implicit operator Vector2D(Vector2 param)
		{
			return new Vector2D(param.x, param.y);
		}

        public static implicit operator Vector3(Vector2D param)
        {
            return new Vector3(param.X, param.Y, 0);
        }

        public static implicit operator Vector2D(Vector3 param)
        {
            return new Vector2D(param.x, param.y);
        }

        public override string ToString()
        {
			return $"({X:f2}, {Y:f2})";
        }
    }
}