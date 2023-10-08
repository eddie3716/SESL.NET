using System;
using System.Data;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using SESL.NET.Function.Commands;

namespace SESL.NET;

public class Variant
{
    private readonly bool _bool;
    private readonly decimal _number;

    private readonly string _string;

    private readonly VariantType _variantType;

    public bool IsVoid => _variantType == VariantType.Void;

    public static Variant Void => new();

    public static Variant Delta => new(0.000000001m);

    public Variant()
    {
        _variantType = VariantType.Void;
        _number = 0;
        _bool = false;
        _string = "Void";
    }
    public Variant(bool val)
    {
        _variantType = VariantType.Bool;
        _bool = val;    
        _number = val ? 1 : 0;
        _string = val.ToString();
    }
    
    public Variant(decimal val)
    {
        _variantType = VariantType.Numeric;
        _bool = val != 0;
        _number = val;
        _string = val.ToString();
    }

    public Variant(string val)
    {
        _string = val;
        _bool = bool.TryParse(val, out bool boolResult) && boolResult;
        _variantType = VariantType.String;
        _number = decimal.TryParse(val, out decimal decimalResult) ? decimalResult : default;
    }

    public bool ToBoolean()
    {
        return _bool;
    }

    public decimal ToNumeric()
    {
        return _number;
    }

    public static Variant operator >(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number > right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '>' operation"),
        };
    }

    public static Variant operator <(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number < right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '<' operation"),
        };
    }

    public static Variant operator >=(Variant left, Variant right)
	{
		return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number >= right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '>' operation"),
        };
	}

	public static Variant operator <=(Variant left, Variant right)
	{
		return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number <= right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '<' operation"),
        };
	}

    public static Variant And(ref Variant left, ref Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Bool or VariantType.Numeric => new Variant(left._bool && right._bool),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for 'AND' operation"),
        };
    }

    public static Variant Or(ref Variant left, ref Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Bool or VariantType.Numeric => new Variant(left._bool || right._bool),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for 'OR' operation"),
        };
    }

    public static Variant operator %(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number % right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '%' operation"),
        };
    }

    public static Variant operator /(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number / right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '/' operation"),
        };
    }

    public static Variant operator ==(Variant left, Variant right)
    {
        return new Variant(left.Equals(right));
    }

    public static Variant operator !=(Variant left, Variant right)
    {
        return new Variant(!(left == right).ToBoolean());
    }

    public static Variant operator *(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number * right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '*' operation"),
        };
    }

    public static Variant operator -(Variant value) => value._variantType switch
    {
        VariantType.Numeric or VariantType.Bool => 
            new Variant(-value._number),
        _ => throw new InvalidOperationException($"Unable to process type {value.GetValueType()} for '-' operation"),
    };

    public static Variant operator -(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(left._number - right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '-' operation"),
        };
    }

    public static Variant operator +(Variant left, Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool=>
                new Variant(left._number + right._number),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for '+' operation"),
        };
    }

    public static Variant Pow(ref Variant baseValue, ref Variant exponent)
    {
        if ((baseValue._variantType == VariantType.Numeric || baseValue._variantType == VariantType.Bool) && (exponent._variantType == VariantType.Numeric || exponent._variantType == VariantType.Bool)) 
            return new Variant(DecimalMath.DecimalEx.Pow(baseValue._number, exponent._number));
        throw new InvalidOperationException($"Unable to process type {baseValue.GetValueType()} and {exponent.GetValueType()} for 'POW' operation");
    }

    public static Variant LogBase(ref Variant baseValue, ref Variant value)
    {
        if ((baseValue._variantType == VariantType.Numeric || baseValue._variantType == VariantType.Bool) && (value._variantType == VariantType.Numeric || value._variantType == VariantType.Bool)) 
            return new Variant(DecimalMath.DecimalEx.Log(value._number, baseValue._number));
        throw new InvalidOperationException($"Unable to process type {baseValue.GetValueType()} and {value.GetValueType()} for 'LOG' operation");
    }

    public static Variant Log10(ref Variant value)
    {
        if (value._variantType == VariantType.Numeric || value._variantType == VariantType.Bool) 
            return new Variant(DecimalMath.DecimalEx.Log10(value._number));
        throw new InvalidOperationException($"Unable to process type {value.GetValueType()} for 'LOG10' operation");
    }

    public static Variant NLog(ref Variant value)
    {
        if (value._variantType == VariantType.Numeric || value._variantType == VariantType.Bool) 
            return new Variant(DecimalMath.DecimalEx.Log(value._number));
        throw new InvalidOperationException($"Unable to process type {value.GetValueType()} for 'NLOG' operation");
    }

    public static Variant Exp(ref Variant value)
    {
        if (value._variantType == VariantType.Numeric || value._variantType == VariantType.Bool) 
            return new Variant(DecimalMath.DecimalEx.Exp(value._number));
        throw new InvalidOperationException($"Unable to process type {value.GetValueType()} for 'EXP' operation");
    }

    public static Variant Sqrt(ref Variant value)
    {
        if (value._variantType == VariantType.Numeric || value._variantType == VariantType.Bool) 
            return new Variant(DecimalMath.DecimalEx.Sqrt(value._number));
        throw new InvalidOperationException($"Unable to process type {value.GetValueType()} for 'SQRT' operation");
    }

    public static Variant Sin(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Sin(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'SIN' operation");
    }

    public static Variant Cos(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Cos(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'COS' operation");
    }

    public static Variant Tan(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.Tan(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'TAN' operation");
    }

    public static Variant ASin(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ASin(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'ASIN' operation");
    }

    public static Variant ACos(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ACos(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'ACOS' operation");
    }

    public static Variant ATan(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = DecimalMath.DecimalEx.ATan(left._number);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'ATAN' operation");
    }
    
    public static Variant Sinh(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(left._number) - DecimalMath.DecimalEx.Exp(-left._number))/2;
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'SINH' operation");
    }
    public static Variant Cosh(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(left._number) + DecimalMath.DecimalEx.Exp(-left._number))/2;
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'COSH' operation");
    }

    public static Variant Tanh(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool)
        {
            var result = (DecimalMath.DecimalEx.Exp(2*left._number) - 1)/(DecimalMath.DecimalEx.Exp(2*left._number) + 1);
            return new Variant(result);
        } 
        throw new InvalidOperationException($"Unable to process type {left.GetValueType()} for 'TANH' operation");
    }

    public static Variant Abs(ref Variant left)
    {
        if (left._variantType == VariantType.Numeric || left._variantType == VariantType.Bool) 
            return new Variant(Math.Abs(left._number));
        throw new InvalidOperationException(string.Format("Unable to process type {0} for 'ABS' operation", left.GetValueType()));
    }

    public static Variant Max(ref Variant left, ref Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(Math.Max(left._number, right._number)),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for 'MAX' operation"),
        };
    }

    public static Variant Min(ref Variant left, ref Variant right)
    {
        return DetermineBestType(ref left, ref right) switch
        {
            VariantType.Numeric or VariantType.Bool =>
                new Variant(Math.Min(left._number, right._number)),
            _ => throw new InvalidOperationException($"Unable to process type {left.GetValueType()} and {right.GetValueType()} for 'MIN' operation"),
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_bool, _number, _string, _variantType);
    }

    public VariantType GetValueType()
    {
        return _variantType;
    }

    public bool Equals(Variant other)
    {
        return this._variantType == other._variantType && this._number == other._number && this._string == other._string;
    }

    public override string ToString()
    {
        return _string;
    }


    static VariantType DetermineBestType(ref Variant left, ref Variant right)
    {
        return (left._variantType, right._variantType) switch
        {
            (VariantType.Bool, VariantType.Bool) => VariantType.Bool,
            (VariantType.Numeric, VariantType.Numeric) => VariantType.Numeric,
            (VariantType.String, VariantType.String) => VariantType.String,
            (VariantType.Numeric, _) => VariantType.Numeric,
            (_, VariantType.Numeric) => VariantType.Numeric,
            (VariantType.String, _) => VariantType.Numeric,
            (_, VariantType.String) => VariantType.String,
            _=> throw new InvalidOperationException($"Unable to determine best type for {left.GetValueType()} and {right.GetValueType()}")
        };
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
}