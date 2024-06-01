using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ElementsValues : IEnumerable<float>
{
    private const int ELEMENTS_COUNT = 8;

    public float Temperature;
    public float Charge;
    public float Structure;
    public float Form;
    public float Time;
    public float Life;
    public float Mind;
    public float Magnet;

    public float this[ElementsTypes i]
    {
        get { return this[(byte)i]; }
        set { this[(byte)i] = value; }
    }

    public float this[int i]
    {
        get { return this[(byte)i]; }
        set { this[(byte)i] = value; }
    }

    public float this[byte i]
    {
        get
        {
            switch (i)
            {
                case 0: return Temperature;
                case 1: return Charge;
                case 2: return Structure;
                case 3: return Form;
                case 4: return Time;
                case 5: return Life;
                case 6: return Mind;
                case 7: return Magnet;
                default: throw new IndexOutOfRangeException();
            }
        }
        set
        {
            switch (i)
            {
                case 0: Temperature = value; break;
                case 1: Charge = value; break;
                case 2: Structure = value; break;
                case 3: Form = value; break;
                case 4: Time = value; break;
                case 5: Life = value; break;
                case 6: Mind = value; break;
                case 7: Magnet = value; break;
                default: throw new IndexOutOfRangeException();
            }
        }
    }

    public IEnumerator<float> GetEnumerator()
    {
        yield return Temperature;
        yield return Charge;
        yield return Structure;
        yield return Form;
        yield return Time;
        yield return Life;
        yield return Mind;
        yield return Magnet;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #region Операторы

    // TODO: Оптимизировать.

    public static ElementsValues operator +(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] + val2[i];
        }

        return returnValue;
    }

    public static ElementsValues operator -(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = new ElementsValues();

        for (int i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] - val2[i];
        }

        return returnValue;
    }

    public static ElementsValues operator *(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] * val2[i];
        }

        return returnValue;
    }

    public static ElementsValues operator /(ElementsValues val1, ElementsValues val2)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] / val2[i];
        }

        return returnValue;
    }

    public static ElementsValues operator *(ElementsValues val1, float val2)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] * val2;
        }

        return returnValue;
    }

    public static ElementsValues operator *(float val2, ElementsValues val1)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] * val2;
        }

        return returnValue;
    }

    public static ElementsValues operator /(ElementsValues val1, float val2)
    {
        var returnValue = new ElementsValues();

        for (byte i = 0; i < ELEMENTS_COUNT; i++)
        {
            returnValue[i] = val1[i] / val2;
        }

        return returnValue;
    }

    #endregion

    #region Функции


    #endregion
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
