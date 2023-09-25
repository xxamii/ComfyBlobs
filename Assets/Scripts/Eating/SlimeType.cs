using System;
using UnityEngine;

public class SlimeType
{
    public enum Type
    {
        Green,
        Blue
    }

    public static int GetIndexOfElement(Type type)
    {
        return (int)type;
    }

    public static Type GetElementOfIndex(int index)
    {
        Type[] enumArray = GetArrayOfTypeValues();
        return enumArray[index];
    }

    public static Type GetFirstElement()
    {
        Type[] enumArray = GetArrayOfTypeValues();
        return enumArray[0];
    }

    public static Type GetLastElement()
    {
        Type[] enumArray = GetArrayOfTypeValues();
        return enumArray[enumArray.Length - 1];
    }

    public static Color GetTypeColor(Type type)
    {
        switch(type)
        {
            case Type.Green:
                return new Color(0.20f, 0.90f, 0.3366f);
            case Type.Blue:
                return Color.white;
            default:
                Debug.LogError("Trying to get a color of slime type that does not exist " + type);
                return Color.green;
        }
    }

    public static Type GetNextSlimeType(Type type)
    {
        Type nextType = type + 1;

        if (nextType > GetLastElement())
        {
            nextType = GetFirstElement();
        }

        return nextType;
    }

    private static Type[] GetArrayOfTypeValues()
    {
        return (Type[])Enum.GetValues(typeof(Type));
    }
}
