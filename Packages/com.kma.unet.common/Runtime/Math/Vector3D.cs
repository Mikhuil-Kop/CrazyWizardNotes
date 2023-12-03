using Mathf = UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

namespace UNet
{
    public struct Vector3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3D(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Magnitude
        {
            get
            {
                return Mathf.Sqrt(X * X + Y * Y + Z * Z);
            }
            set
            {
                var scale = value / Magnitude;
                X *= scale;
                Y *= scale;
                Z *= scale;
            }
        }

        public Vector3D Normalized
        {
            get
            {
                return this / Magnitude;
            }
            set
            {
                var mag = Magnitude;
                X = value.X * mag;
                Y = value.Y * mag;
                Z = value.Z * mag;
            }
        }

        public static Vector3D Zero => new(0, 0, 0);
        public static Vector3D Left => new(-1, 0, 0);
        public static Vector3D Right => new(1, 0, 0);
        public static Vector3D Up => new(0, 1, 0);
        public static Vector3D Down => new(0, -1, 0);
        public static Vector3D Forward => new(0, 0, 1);
        public static Vector3D Back => new(0, 0, -1);
        public static Vector3D One => new(1, 1, 1);

        public static Vector3D operator +(Vector3D a) => new(a.X, a.Y, a.Z);
        public static Vector3D operator -(Vector3D a) => new(-a.X, -a.Y, -a.Z);
        public static Vector3D operator +(Vector3D a, Vector3D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3D operator -(Vector3D a, Vector3D b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3D operator *(Vector3D a, Vector3D b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Vector3D operator /(Vector3D a, Vector3D b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static Vector3D operator *(Vector3D a, float b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Vector3D operator /(Vector3D a, float b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Vector3D operator *(float b, Vector3D a) => new(a.X * b, a.Y * b, a.Z * b);
        public static Vector3D operator /(float b, Vector3D a) => new(a.X / b, a.Y / b, a.Z / b);

        // Explicit conversion from 2D to 3D (Z = 0)
        public static explicit operator Vector3D(Vector2D v)
        {
            return new Vector3D(v.X, v.Y, 0);
        }

        // Explicit conversion from 3D to 2D (Z is dropped)
        public static explicit operator Vector2D(Vector3D v)
        {
            return new Vector2D(v.X, v.Y);
        }

        // Implicit conversion to Unity's Vector3
        public static implicit operator Vector3(Vector3D param)
        {
            return new Vector3(param.X, param.Y, param.Z);
        }

        // Implicit conversion from Unity's Vector3
        public static implicit operator Vector3D(Vector3 param)
        {
            return new Vector3D(param.x, param.y, param.z);
        }
    }
}