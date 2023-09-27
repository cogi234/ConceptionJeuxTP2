using System;
using System.Numerics;

internal class OnChangedCallAttribute : Attribute
{
    private string v;

    public OnChangedCallAttribute(string v)
    {
        this.v = v;
    }
}