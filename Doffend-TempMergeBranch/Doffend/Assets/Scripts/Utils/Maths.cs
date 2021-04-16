using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{

    public static float roundToArbitraryPrecision(float n, float precision = 1.0f)
    {
        return Mathf.Floor((n + (precision / 2.0f)) / precision) * precision;
    }

    public static float roundPixelPerfectPosition(float p, float pixelsPerMeter)
    {
        return Mathf.Round(p * pixelsPerMeter) / pixelsPerMeter;
    }
}