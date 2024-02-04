using System;
using System.Collections.Generic;

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
}