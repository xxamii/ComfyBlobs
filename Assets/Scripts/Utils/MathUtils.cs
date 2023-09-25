using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static int FindClosestPowerOfTwoOrOne(int n)
    {
        for (int i = n; i >= 2; i--)
        {
            if (Mathf.IsPowerOfTwo(i))
            {
                return i;
            }
        }

        return 1;
    }
}
