using Mathf = UnityEngine.Mathf;
using Vector4 = UnityEngine.Vector4;

namespace UNet
{
    public struct Vector4D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector4D(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float Magnitude
        {
            get
            {
                return Mathf.Sqrt(X * X + Y * Y + Z * Z + W * W);
            }
            set
            {
                var scale = value / Magnitude;
                X *= scale;
                Y *= scale;
                Z *= scale;
                W *= scale;
            }
        }

        public Vector4D Normalized
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
                W = value.W * mag;
            }
        }

        public static Vector4D Zero => new(0, 0, 0, 0);
        public static Vector4D One => new(1, 1, 1, 1);
        public static Vector4D Left => new(-1, 0, 0, 0);
        public static Vector4D Right => new(1, 0, 0, 0);
        public static Vector4D Up => new(0, 1, 0, 0);
        public static Vector4D Down => new(0, -1, 0, 0);
        public static Vector4D Forward => new(0, 0, 1, 0);
        public static Vector4D Back => new(0, 0, -1, 0);
        public static Vector4D Ana => new(0, 0, 0, 1); // 4D "ana" direction
        public static Vector4D Kata => new(0, 0, 0, -1); // 4D "kata" direction

        public static Vector4D operator +(Vector4D a) => new(a.X, a.Y, a.Z, a.W);
        public static Vector4D operator -(Vector4D a) => new(-a.X, -a.Y, -a.Z, -a.W);
        public static Vector4D operator +(Vector4D a, Vector4D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        public static Vector4D operator -(Vector4D a, Vector4D b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        public static Vector4D operator *(Vector4D a, Vector4D b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        public static Vector4D operator /(Vector4D a, Vector4D b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        public static Vector4D operator *(Vector4D a, float b) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);
        public static Vector4D operator /(Vector4D a, float b) => new(a.X / b, a.Y / b, a.Z / b, a.W / b);
        public static Vector4D operator *(float b, Vector4D a) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);
        public static Vector4D operator /(float b, Vector4D a) => new(a.X / b, a.Y / b, a.Z / b, a.W / b);

        // Explicit conversion from 2D to 4D (Z = 0, W = 0)
        public static explicit operator Vector4D(Vector2D v)
        {
            return new Vector4D(v.X, v.Y, 0, 0);
        }

        // Explicit conversion from 3D to 4D (W = 0)
        public static explicit operator Vector4D(Vector3D v)
        {
            return new Vector4D(v.X, v.Y, v.Z, 0);
        }

        // Explicit conversion from 4D to 2D (Z and W are dropped)
        public static explicit operator Vector2D(Vector4D v)
        {
            return new Vector2D(v.X, v.Y);
        }

        // Explicit conversion from 4D to 3D (W is dropped)
        public static explicit operator Vector3D(Vector4D v)
        {
            return new Vector3D(v.X, v.Y, v.Z);
        }

        // Implicit conversion to Unity's Vector4
        public static implicit operator Vector4(Vector4D param)
        {
            return new Vector4(param.X, param.Y, param.Z, param.W);
        }

        // Implicit conversion from Unity's Vector4
        public static implicit operator Vector4D(Vector4 param)
        {
            return new Vector4D(param.x, param.y, param.z, param.w);
        }
    }
}