using System;

public struct ElementsValues
{
    private const int ELEMENTS_COUNT = 8;
    private float[] Values { get; set; }

    public float Phisical { get { return Values[0]; } set { Values[0] = value; } }
    public float Fire { get { return Values[1]; } set { Values[1] = value; } }
    public float Cold { get { return Values[2]; } set { Values[2] = value; } }
    public float Toxic { get { return Values[3]; } set { Values[3] = value; } }
    public float Electric { get { return Values[4]; } set { Values[4] = value; } }
    public float Radiant { get { return Values[5]; } set { Values[5] = value; } }
    public float Necrotic { get { return Values[6]; } set { Values[6] = value; } }
    public float Magic { get { return Values[7]; } set { Values[7] = value; } }

    public float this[ElementsTypes i]
    {
        get { return Values[(byte)i]; }
        set { Values[(byte)i] = value; }
    }

    public float Sum
    {
        get => Phisical + Fire + Cold + Toxic + Electric + Radiant + Necrotic + Magic;
    }

    public static ElementsValues operator +(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = ElementsValues.Zero();

        for (int i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue.Values[i] = val1.Values[i] + val2.Values[i];
        }

        return returnValue;
    }

    public static ElementsValues operator -(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = ElementsValues.Zero();

        for (int i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue.Values[i] = val1.Values[i] - val2.Values[i];
        }

        return returnValue;
    }

    public static ElementsValues operator *(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = ElementsValues.Zero();

        for (int i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue.Values[i] = val1.Values[i] * val2.Values[i];
        }

        return returnValue;
    }

    public static ElementsValues operator /(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = ElementsValues.Zero();

        for (int i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue.Values[i] = val1.Values[i] / val2.Values[i];
        }

        return returnValue;
    }


    public static ElementsValues Zero()
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT]
        };
    }

    public static ElementsValues GetPhisical(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Phisical = value
        };
    }

    public static ElementsValues GetFire(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Fire = value
        };
    }

    public static ElementsValues GetCold(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Cold = value
        };
    }

    public static ElementsValues GetToxic(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Toxic = value
        };
    }

    public static ElementsValues GetElectric(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Electric = value
        };
    }

    public static ElementsValues GetRadiant(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Radiant = value
        };
    }

    public static ElementsValues GetNecrotic(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Necrotic = value
        };
    }

    public static ElementsValues GetMagic(float value = 0)
    {
        return new ElementsValues()
        {
            Values = new float[ELEMENTS_COUNT],
            Magic = value
        };
    }
}

public enum ElementsTypes : byte
{
    Phisical = 0,
    Fire = 1,
    Cold = 2,
    Toxic = 3,
    Electric = 4,
    Radiant = 5,
    Necrotic = 6,
    Magic = 7
}
