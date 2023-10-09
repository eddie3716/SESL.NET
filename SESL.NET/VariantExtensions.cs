using System;

namespace SESL.NET;

public static class VariantExtensions
{
    public static Variant IsGreaterThan(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue > other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '>' operation"),
        };
    }

    public static Variant IsLessThan(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue < other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '<' operation"),
        };
    }

    public static Variant IsGreaterThanOrEqualTo(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue >= other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '>' operation"),
        };
    }

    public static Variant IsLessThanOrEqualTo(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue <= other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '<' operation"),
        };
    }

    public static Variant And(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Bool or VariantType.Numeric => new Variant(@this.BoolValue && other.BoolValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for 'AND' operation"),
        };
    }

    public static Variant Or(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Bool or VariantType.Numeric => new Variant(@this.BoolValue || other.BoolValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for 'OR' operation"),
        };
    }

    public static Variant Mod(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue % other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '%' operation"),
        };
    }

    public static Variant DividedBy(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue / other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '/' operation"),
        };
    }

    public static Variant IsEqualTo(this Variant @this, Variant other)
    {
        return new Variant(@this.Equals(other));
    }

    public static Variant DoesNotEqual(this Variant @this, Variant other)
    {
        return new Variant(!Equals(@this, other));
    }

    public static Variant Times(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue * other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '*' operation"),
        };
    }

    public static Variant Negate(this Variant @this) => @this.VariantType switch
    {
        VariantType.Numeric or VariantType.Bool =>
        new Variant(-@this.DecimalValue),
        _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} for '-' operation"),
    };

    public static Variant Minus(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue - other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '-' operation"),
        };
    }

    public static Variant Plus(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(@this.DecimalValue + other.DecimalValue),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for '+' operation"),
        };
    }

    public static Variant ToPowerOf(this Variant @this, Variant other)
    {
        if ((@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool) && (other.VariantType == VariantType.Numeric || other.VariantType == VariantType.Bool))
            return new Variant(DecimalMath.DecimalEx.Pow(@this.DecimalValue, other.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for 'POW' operation");
    }

    public static Variant LogBase(this Variant @this, Variant other)
    {
        if ((@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool) && (other.VariantType == VariantType.Numeric || other.VariantType == VariantType.Bool))
            return new Variant(DecimalMath.DecimalEx.Log(@this.DecimalValue, @other.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {@this.VariantType} for 'LOG' operation");
    }

    public static Variant Log10(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
            return new Variant(DecimalMath.DecimalEx.Log10(@this.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'LOG10' operation");
    }

    public static Variant NaturalLog(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
            return new Variant(DecimalMath.DecimalEx.Log(@this.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'NLOG' operation");
    }

    public static Variant EToPowerOf(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
            return new Variant(DecimalMath.DecimalEx.Exp(@this.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'EXP' operation");
    }

    public static Variant Sqrt(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
            return new Variant(DecimalMath.DecimalEx.Sqrt(@this.DecimalValue));
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'SQRT' operation");
    }

    public static Variant Sin(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Sin(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'SIN' operation");
    }

    public static Variant Cos(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Cos(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'COS' operation");
    }

    public static Variant Tan(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Tan(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'TAN' operation");
    }

    public static Variant ASin(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ASin(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'ASIN' operation");
    }

    public static Variant ACos(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ACos(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'ACOS' operation");
    }

    public static Variant ATan(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ATan(@this.DecimalValue);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'ATAN' operation");
    }

    public static Variant Sinh(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(@this.DecimalValue) - DecimalMath.DecimalEx.Exp(-@this.DecimalValue)) / 2;
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'SINH' operation");
    }
    public static Variant Cosh(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(@this.DecimalValue) + DecimalMath.DecimalEx.Exp(-@this.DecimalValue)) / 2;
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'COSH' operation");
    }

    public static Variant Tanh(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(2 * @this.DecimalValue) - 1) / (DecimalMath.DecimalEx.Exp(2 * @this.DecimalValue) + 1);
            return new Variant(result);
        }
        throw new InvalidOperationException($"Unable to process type {@this.VariantType} for 'TANH' operation");
    }

    public static Variant Abs(this Variant @this)
    {
        if (@this.VariantType == VariantType.Numeric || @this.VariantType == VariantType.Bool)
            return new Variant(Math.Abs(@this.DecimalValue));
        throw new InvalidOperationException(string.Format("Unable to process type {0} for 'ABS' operation", @this.VariantType));
    }

    public static Variant Max(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(Math.Max(@this.DecimalValue, other.DecimalValue)),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for 'MAX' operation"),
        };
    }

    public static Variant Min(this Variant @this, Variant other)
    {
        return DetermineBestType(@this, other) switch
        {
            VariantType.Numeric or VariantType.Bool =>
            new Variant(Math.Min(@this.DecimalValue, other.DecimalValue)),
            _ => throw new InvalidOperationException($"Unable to process type {@this.VariantType} and {other.VariantType} for 'MIN' operation"),
        };
    }

    private static VariantType DetermineBestType(Variant @this, Variant other)
    {
        return (@this.VariantType, other.VariantType) switch
        {
            (VariantType.Bool, VariantType.Bool) => VariantType.Bool,
            (VariantType.Numeric, VariantType.Numeric) => VariantType.Numeric,
            (VariantType.String, VariantType.String) => VariantType.String,
            (VariantType.Numeric, _) => VariantType.Numeric,
            (_, VariantType.Numeric) => VariantType.Numeric,
            (VariantType.String, _) => VariantType.Numeric,
            (_, VariantType.String) => VariantType.String,
            _ => throw new InvalidOperationException($"Unable to determine best type for {@this.VariantType} and {other.VariantType}")
        };
    }
}