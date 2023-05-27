using UnityEngine;
// todo change the name of this file

class Colors
{
    public static Color red = new Color(1.0f, 0.0f, 0.0f);
    public static Color yellow = new Color(1.0f, 1.0f, 0.0f);
    public static Color blue = new Color(0.0f, 0.0f, 1.0f);

    public static bool Compare(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
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