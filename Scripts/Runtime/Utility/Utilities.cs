using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static string ToCommasString(this int number)
    {
        return $"{number:n0}";
    }

    public static string ToCommasString(this long number)
    {
        return $"{number:n0}";
    }
}
