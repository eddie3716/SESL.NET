using System;
using System.Runtime.CompilerServices;

namespace SESL.NET;

public class Variant
{
    public bool BoolValue;
    public decimal DecimalValue { get; }

    public string StringValue { get; }

    public VariantType VariantType { get; }
    
    public bool IsVoid => VariantType == VariantType.Void;

    public static Variant Void => new();

    public static Variant Delta => new(0.000000001m);


    public Variant()
    {
        VariantType = VariantType.Void;
        DecimalValue = 0;
        BoolValue = false;
        StringValue = "Void";
    }
    public Variant(bool val)
    {
        VariantType = VariantType.Bool;
        BoolValue = val;    
        DecimalValue = val ? 1 : 0;
        StringValue = val.ToString();
    }

    public Variant(decimal val)
    {
        VariantType = VariantType.Numeric;
        BoolValue = val != 0;
        DecimalValue = val;
        StringValue = val.ToString();
    }

    public Variant(string val)
    {
        StringValue = val;
        BoolValue = bool.TryParse(val, out bool boolResult) && boolResult;
        VariantType = VariantType.String;
        DecimalValue = decimal.TryParse(val, out decimal decimalResult) ? decimalResult : default;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BoolValue, DecimalValue, StringValue, VariantType);
    }

    public bool Equals(Variant other)
    {
        return this.VariantType == other.VariantType && this.DecimalValue == other.DecimalValue && this.StringValue == other.StringValue;
    }

    public override string ToString()
    {
        return StringValue;
    }

    public static Variant Parse(string s)
    {
        if (s == null)
        {
            return new Variant();
        }
        else if (bool.TryParse(s, out bool boolResult))
        {
            return new Variant(boolResult);
        }
        else if (decimal.TryParse(s, out decimal decimalResult))
        {
            return new Variant(decimalResult);
        }
        else
        {
            return new Variant(s);
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Variant Value && Equals(Value);
    }

    public static bool operator ==(Variant left, Variant right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Variant left, Variant right)
    {
        return !(left == right);
    }
}