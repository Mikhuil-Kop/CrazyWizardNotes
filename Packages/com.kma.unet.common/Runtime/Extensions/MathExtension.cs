using System;

public static class MathExtension
{
    public static int Limit(this int value, int min, int max)
    {
        if (value < min)
        {
            return value;
        }
        else if(value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

    public static int Limit(this short value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

    public static bool AlmostEquals(this float value1, float value2, float difference = 0.01f)
    {
        return Math.Abs(Math.Abs(value1) - Math.Abs(value2)) <= difference;
    }
}
