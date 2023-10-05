using System;

namespace SESL.NET
{
    public class Value
	{
		private readonly IConvertible _inner;

		private const int RoundFloatingPointDigits = 15;

		protected Value()
		{
			_inner = new Void();
		}

		public Value(IConvertible inner)
		{
			if (SESL.NET.Zero.IsZeroEquivalent(inner))
			{
				_inner = new Zero();
			}
			else if (SESL.NET.One.IsOneEquivalent(inner))
			{
				_inner = new One();
			}
			else if (inner is double)
			{
				_inner = Math.Round(inner.ToDouble(null), RoundFloatingPointDigits);
			}
			else
			{
				_inner = inner;
			}
		}

        public bool IsZero => _inner is Zero;

        public bool IsOne => _inner is One;

        private bool? _isIntegerCompatible;
		public bool IsIntegerCompatible
		{
			get
			{
				if (!_isIntegerCompatible.HasValue)
				{
					_isIntegerCompatible = _inner is byte || _inner is sbyte || _inner is char || _inner is int || _inner is short || _inner is long || _inner is uint || _inner is ushort || _inner is ulong
                                        ||
										(_inner is decimal && (Math.Truncate((decimal)_inner) == (decimal)_inner))
										||
										(_inner is double && (Math.Truncate((double)_inner) == (double)_inner));
				}
				return _isIntegerCompatible.Value;
			}
		}

		private bool? _isPositive;
		public bool IsPositive
		{
			get
			{
				if (!_isPositive.HasValue)
				{
					_isPositive = !(_inner is Void) && !(_inner is string) && (_inner.ToDouble(null) > 0);
				}
				return _isPositive.Value;
			}
		}

		private bool? _isNegative;
		public bool IsNegative
		{
			get
			{
				if (!_isNegative.HasValue)
				{
					_isNegative = !(_inner is Void) && !(_inner is string) && (_inner.ToDouble(null) < 0);
				}
				return _isNegative.Value;
			}
		}

        public bool IsVoid => _inner is Void;

        public static Value Delta => new(0.000000001);

        public static Value Void => new();

        public static Value Zero => new(new Zero());

        public static Value One => new(new One());

        public Type InnerType => _inner.GetType();

        public bool ToBoolean()
		{
			return _inner.ToBoolean(null);
		}

		public decimal ToDecimal()
		{
			return _inner.ToDecimal(null);
		}

		public double ToDouble()
		{
			return Math.Round(_inner.ToDouble(null), RoundFloatingPointDigits);
		}

		public int ToInt32()
		{
			return _inner.ToInt32(null);
		}

		public long ToInt64()
		{
			return _inner.ToInt64(null);
		}

		public object ToObject()
		{
			return _inner;
		}

		public Value Negate()
		{
			if (IsZero) return this;
			if (IsOne) return new Value(-1);
			if (_inner is decimal) return new Value(-_inner.ToDecimal(null));
			if (_inner is double) return new Value(-_inner.ToDouble(null));
			if (_inner is long) return new Value(-_inner.ToInt64(null));
			if (_inner is int) return new Value(-_inner.ToInt32(null));
			if (_inner is bool) return new Value(-_inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process type {0} for 'NEGATE' operation", _inner.GetType()));
		}

		public override string ToString()
		{
			return _inner.ToString();
		}

		public override bool Equals(object obj)
		{
            if (obj is Value other)
            {
                if (IsZero && other.IsZero) return true;
                if (IsOne && other.IsOne) return true;
                if (_inner is decimal || other._inner is decimal) return _inner.ToDecimal(null).Equals(other._inner.ToDecimal(null));
                if (_inner is double || other._inner is double) return _inner.ToDouble(null).Equals(other._inner.ToDouble(null));
                if (_inner is long || other._inner is long) return _inner.ToInt64(null).Equals(other._inner.ToInt64(null));
                if (_inner is int || other._inner is int) return _inner.ToInt32(null).Equals(other._inner.ToInt32(null));
                if (_inner is bool || other._inner is bool) return _inner.ToInt32(null).Equals(other._inner.ToInt32(null));

                return _inner.Equals(other._inner);
            }
            return false;
		}

		public override int GetHashCode()
        {
            return HashCode.Combine(_inner);
        }

        public T ToType<T>()
		{
			return (T)Convert.ChangeType(_inner, typeof(T));
		}

		public static Value operator +(Value left, Value right)
		{
			if (left._inner is string || right._inner is string) return new Value(left.IsZero.ToString() + right._inner.ToString());
			if (left.IsZero) return right;
			if (right.IsZero) return left;
			if (left.IsOne && right.IsOne) return new Value(2);
			if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) + right._inner.ToDecimal(null));
			if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) + right._inner.ToDouble(null));
			if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) + right._inner.ToInt64(null));
			if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) + right._inner.ToInt32(null));
			if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) + right._inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process types {0} and {1} for '+' operation", left._inner.GetType(), left._inner.GetType()));
		}

		public static Value operator -(Value left, Value right)
		{
			if (left.IsZero) return right.Negate();
			if (right.IsZero) return left;
			if (left.IsOne && right.IsOne) return Value.Zero;
			if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) - right._inner.ToDecimal(null));
			if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) - right._inner.ToDouble(null));
			if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) - right._inner.ToInt64(null));
			if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) - right._inner.ToInt32(null));
			if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) - right._inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process types {0} and {1} for '-' operation", left._inner.GetType(), left._inner.GetType()));
		}

		public static Value operator -(Value left)
		{
			return left.Negate();
		}

		public static Value operator %(Value left, Value right)
		{
			if (left.IsZero && right.IsZero) return new Value(double.NaN);
			if (left.IsZero) return new Value(right.IsPositive ? double.PositiveInfinity : double.NegativeInfinity);
			if (right.IsZero) return right;
			if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) % right._inner.ToDecimal(null));
			if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) % right._inner.ToDouble(null));
			if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) % right._inner.ToInt64(null));
			if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) % right._inner.ToInt32(null));
			if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) % right._inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for '%' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator *(Value left, Value right)
		{
			if (left.IsZero) return left;
			if (right.IsZero) return right;
			if (left.IsOne) return right;
			if (right.IsOne) return left;
			if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) * right._inner.ToDecimal(null));
			if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) * right._inner.ToDouble(null));
			if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) * right._inner.ToInt64(null));
			if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) * right._inner.ToInt32(null));
			if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) * right._inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for '*' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator /(Value left, Value right)
		{
			if (left.IsZero && right.IsZero) return new Value(double.NaN);
			if (left.IsZero) return left;
			if (right.IsZero) return new Value(left.IsPositive ? double.PositiveInfinity : double.NegativeInfinity);
			if (right.IsOne) return left;
			if (left._inner is decimal || right._inner is decimal || left._inner is long || right._inner is long) return new Value(left._inner.ToDecimal(null) / right._inner.ToDecimal(null));
			if (left._inner is double || right._inner is double || left._inner is int || right._inner is int) return new Value(left._inner.ToDouble(null) / right._inner.ToDouble(null));
			if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) / right._inner.ToInt32(null));
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for '/' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static long RecursivePow(ref long baseInt, long exponentInt)
		{
			unchecked
			{
				if (exponentInt == 1)
				{
					return baseInt;
				}
				else
				if (exponentInt % 2 == 0)
				{
                    long y = RecursivePow(ref baseInt, exponentInt / 2);
					return y * y;
				}
				//Odd values of (n)  
				else
				{
                    long y = RecursivePow(ref baseInt, exponentInt - 1);
					return baseInt * y;
				}  
			}
		}

		public static decimal RecursivePow(ref decimal baseDecimal, long exponentInt)
		{
			if (exponentInt == 1)
			{
				return baseDecimal;
			}
			else
			if (exponentInt % 2 == 0)
			{
                decimal y = RecursivePow(ref baseDecimal, exponentInt / 2);
				return y * y;
			}
			//Odd values of (n)  
			else
			{
                decimal y = RecursivePow(ref baseDecimal, exponentInt - 1);
				return baseDecimal * y;
			}  
		}

		public static Value Pow(ref Value baseValue, ref Value exponent)
		{
			if (baseValue.IsZero)
			{
				return Value.Zero;
			}

			if (exponent.IsZero)
			{
				return Value.One;
			}

			if (exponent.IsOne)
			{
				return baseValue;
			}

			if (exponent.IsIntegerCompatible && exponent.IsPositive)
			{
				long longExponent = exponent.ToInt64();
				if (baseValue.IsIntegerCompatible)
				{
					long longBase = baseValue.ToInt64();
					return new Value(Value.RecursivePow(ref longBase, longExponent));
				}
				else
				{
                    decimal decimalBase = baseValue.ToDecimal();
					return new Value(Value.RecursivePow(ref decimalBase, longExponent));
				}
			}

			return new Value(Math.Pow(baseValue.ToDouble(), exponent.ToDouble()));
		}

		public static Value Abs(ref Value left)
		{
			if (!left.IsNegative) return left;
			if (left.IsNegative) return left.Negate();
			throw new InvalidOperationException(string.Format("Unable to process type {0} for 'ABS' operation", left._inner.GetType()));
		}

		public static Value Max(ref Value left, ref Value right)
		{
			if (!left.IsPositive && !right.IsNegative || left.Equals(right)) return right;
			if (!left.IsNegative && !right.IsPositive) return left;
			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is decimal || right._inner is decimal) return new Value(System.Math.Max(left._inner.ToDecimal(null), right._inner.ToDecimal(null)));
				if (left._inner is double || right._inner is double) return new Value(System.Math.Max(left._inner.ToDouble(null), right._inner.ToDouble(null)));
				if (left._inner is long || right._inner is long) return new Value(System.Math.Max(left._inner.ToInt64(null), right._inner.ToInt64(null)));
				if (left._inner is int || right._inner is int) return new Value(System.Math.Max(left._inner.ToInt32(null), right._inner.ToInt32(null)));
				if (left._inner is bool || right._inner is bool) return new Value(System.Math.Max(left._inner.ToInt32(null), right._inner.ToInt32(null)));
			}
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for 'MAX' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value Min(ref Value left, ref Value right)
		{
			if (!left.IsNegative && !right.IsPositive|| left.Equals(right)) return right;
			if (!left.IsPositive && !right.IsNegative) return left;
			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is decimal || right._inner is decimal) return new Value(System.Math.Min(left._inner.ToDecimal(null), right._inner.ToDecimal(null)));
				if (left._inner is double || right._inner is double) return new Value(System.Math.Min(left._inner.ToDouble(null), right._inner.ToDouble(null)));
				if (left._inner is long || right._inner is long) return new Value(System.Math.Min(left._inner.ToInt64(null), right._inner.ToInt64(null)));
				if (left._inner is int || right._inner is int) return new Value(System.Math.Min(left._inner.ToInt32(null), right._inner.ToInt32(null)));
				if (left._inner is bool || right._inner is bool) return new Value(System.Math.Min(left._inner.ToInt32(null), right._inner.ToInt32(null)));
			}
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for 'MIN' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator >(Value left, Value right)
		{
			if ((left.IsNegative && !right.IsNegative) || (!left.IsPositive && right.IsPositive) || left.Equals(right)) return new Value(false);
			if ((left.IsPositive && !right.IsPositive) || (!left.IsNegative && right.IsNegative)) return new Value(true);

			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) > right._inner.ToDecimal(null));
				if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) > right._inner.ToDouble(null));
				if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) > right._inner.ToInt64(null));
				if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) > right._inner.ToInt32(null));
				if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) > right._inner.ToInt32(null));
			}
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for '>' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator <(Value left, Value right)
		{
			if ((!left.IsNegative && right.IsNegative) || (left.IsPositive && !right.IsPositive) || left.Equals(right)) return new Value(false);
			if ((!left.IsPositive && right.IsPositive) || (left.IsNegative && !right.IsNegative)) return new Value(true);

			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is decimal || right._inner is decimal) return new Value(left._inner.ToDecimal(null) < right._inner.ToDecimal(null));
				if (left._inner is double || right._inner is double) return new Value(left._inner.ToDouble(null) < right._inner.ToDouble(null));
				if (left._inner is long || right._inner is long) return new Value(left._inner.ToInt64(null) < right._inner.ToInt64(null));
				if (left._inner is int || right._inner is int) return new Value(left._inner.ToInt32(null) < right._inner.ToInt32(null));
				if (left._inner is bool || right._inner is bool) return new Value(left._inner.ToInt32(null) < right._inner.ToInt32(null));
			}
			throw new InvalidOperationException(string.Format("Unable to process type {0} and {1} for '<' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator >=(Value left, Value right)
		{
			if (left.Equals(right)) return new Value(true);

			return left > right;
		}

		public static Value operator <=(Value left, Value right)
		{
			if (left.Equals(right)) return new Value(true);

			return left < right;
		}

		public static Value operator !=(Value left, Value right)
		{
			return new Value(!left.Equals(right));
		}

		public static Value operator ==(Value left, Value right)
		{
			return new Value(left.Equals(right));
		}

		public static Value And(ref Value left, ref Value right)
		{
			return new Value(left.ToBoolean() && right.ToBoolean());
		}

		public static Value Or(ref Value left, ref Value right)
		{
			return new Value(left.ToBoolean() || right.ToBoolean());
		}
	}
}