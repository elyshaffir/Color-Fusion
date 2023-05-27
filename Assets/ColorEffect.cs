using UnityEngine;
// todo change the name of this file

class Colors
{
    public static Color red = new Color(1.0f, 0.0f, 0.0f);
    public static Color orange = new Color(1.0f, 0.7f, 0.0f);
    public static Color yellow = new Color(1.0f, 1.0f, 0.0f);
    public static Color green = new Color(0.0f, 1.0f, 0.0f);
    public static Color blue = new Color(0.0f, 0.0f, 1.0f);
    public static Color purple = new Color(1.0f, 0.0f, 1.0f);

    public static Color[] colors = {
        red,
        orange,
        yellow,
        green,
        blue,
        purple
    };

    public static bool Compare(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }

    public static bool TryGetIndexOfColor(Color c, out int outIndex)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (Compare(colors[i], c))
            {
                outIndex = i;
                return true;
            }
        }
        outIndex = -1;
        return false;
    }
}

enum ColorEffect
{
    NoEffect,
    Heal // todo this is temporary
}

class ColorEffectMapping
{
    public static bool TryGetValue(Color color, out ColorEffect outColorEffect)
    {
        if (Colors.Compare(color, Colors.red))
        {
            outColorEffect = ColorEffect.Heal;
            return true;
        }
        else
        {
            outColorEffect = ColorEffect.NoEffect;
            return false;
        }
    }
}