public static class ParseExtension
{
    public static int? ParseToInt(this string str)
    {
        if (int.TryParse(str, out var result))
        {
            return result;
        }
        else
        {
            return null;
        }
    }

    public static float? ParseToFloat(this string str)
    {
        if (float.TryParse(str, out var result))
        {
            return result;
        }
        else
        {
            return null;
        }
    }

}
