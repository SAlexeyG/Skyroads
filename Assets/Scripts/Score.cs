using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Score
{
    int value = 0;

    public Score(string str)
    {
        int value;
        this.value = int.TryParse(str, out value) ? value : 0;
    }

    public static Score operator +(Score a, int b)
    {
        Score temp = new Score("0");
        temp.value = a.value + b;
        return temp;
    }

    public static Score operator -(Score a, int b)
    {
        Score temp = new Score("0");
        temp.value = a.value - b;
        return temp;
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder("My Score: ");
        str.Append(value);
        return str.ToString();
    }

    public static implicit operator Score(int a) => new Score("0") { value = a };
    public static explicit operator int(Score a) => a.value;

    public static bool operator <(Score a, int b) => a.value < b;

    public static bool operator >(Score a, int b) => a.value > b;

    public static bool operator ==(Score a, int b) => a.value == b;

    public static bool operator !=(Score a, int b) => a.value != b;
}
