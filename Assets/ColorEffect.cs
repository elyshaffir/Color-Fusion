using UnityEngine;
using System.Collections.Generic;

enum ColorEffect
{
    NoEffect,
    Heal // todo this is temporary
}

class ColorEffectMapping
{
    public static bool TryGetValue(Color color, out ColorEffect outColorEffect)
    {
        if (color.r == 1.0f && color.g == 0.0f && color.b == 0.0f)
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