using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static int CountEven(this IEnumerable<int> collection)
    {
        int sum = 0;
        foreach (var num in collection)
            sum += (num % 2 == 0) ? num : 0;
        return sum;
    }
}
