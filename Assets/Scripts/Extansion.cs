using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extansion 
{
    public static string ToGUIString(this int score)
    {
        var str = score.ToString();
        return (score < 1000) ? str : str.Insert(str.Length - 3, "\'");
    }

    public static string ToGUIString(this float score)
    {
        var str = score.ToString();
        return (score < 1000) ? str : str.Insert(str.Length - 3, "\'");
    }

    public static string ToGUIString(this double score)
    {
        var str = score.ToString();
        return (score < 1000) ? str : str.Insert(str.Length - 3, "\'");
    }
}
