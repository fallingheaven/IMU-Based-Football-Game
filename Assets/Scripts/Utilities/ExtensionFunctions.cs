using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionFunctions
{
    public static void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }
}
