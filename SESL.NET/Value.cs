using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET
{
	public class Value
	{
		private IConvertible _inner;

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
			else if (inner is Double)
			{
				_inner = Math.Round(inner.ToDouble(null), RoundFloatingPointDigits);
			}
			else
			{
				_inner = inner;
			}
		}

		public Boolean IsZero
		{
			get
			{
				return _inner is Zero;
			}
		}

		public Boolean IsOne
		{
			get
			{
				return _inner is One;
			}
		}

		private Boolean? _isIntegerCompatible;
		public Boolean IsIntegerCompatible
		{
			get
			{
				if (!_isIntegerCompatible.HasValue)
				{
					_isIntegerCompatible = _inner is Byte || _inner is SByte || _inner is char || _inner is Int32 || _inner is Int16 || _inner is Int64 || _inner is UInt32 || _inner is UInt16 || _inner is UInt64
										||
										(_inner is Decimal && (Math.Truncate((Decimal)(_inner)) == (Decimal)(_inner)))
										||
										(_inner is Double && (Math.Truncate((Double)(_inner)) == (Double)(_inner)));
				}
				return _isIntegerCompatible.Value;
			}
		}

		private Boolean? _isPositive;
		public Boolean IsPositive
		{
			get
			{
				if (!_isPositive.HasValue)
				{
					_isPositive = (!(_inner is Void) && !(_inner is String)) && (_inner.ToDouble(null) > 0);
				}
				return _isPositive.Value;
			}
		}

		private Boolean? _isNegative;
		public Boolean IsNegative
		{
			get
			{
				if (!_isNegative.HasValue)
				{
					_isNegative = (!(_inner is Void) && !(_inner is String)) && (_inner.ToDouble(null) < 0);
				}
				return _isNegative.Value;
			}
		}

		public Boolean IsVoid
		{
			get
			{
				return _inner is Void;
			}
		}

		public static Value Delta
		{
			get
			{
				return new Value(0.000000001);
			}
		}

		public static Value Void
		{
			get
			{
				return new Value();
			}
		}

		public static Value Zero
		{
			get
			{
				return new Value(new Zero());
			}
		}

		public static Value One
		{
			get
			{
				return new Value(new One());
			}
		}

		public Type InnerType
		{
			get
			{
				return _inner.GetType();
			}
		}

		public Boolean ToBoolean()
		{
			return _inner.ToBoolean(null);
		}

		public Decimal ToDecimal()
		{
			return _inner.ToDecimal(null);
		}

		public double ToDouble()
		{
			return Math.Round(_inner.ToDouble(null), RoundFloatingPointDigits);
		}

		public Int32 ToInt32()
		{
			return _inner.ToInt32(null);
		}

		public Int64 ToInt64()
		{
			return _inner.ToInt64(null);
		}

		public object ToObject()
		{
			return _inner;
		}

		public Value Negate()
		{
			if (this.IsZero) return this;
			if (this.IsOne) return new Value(-1);
			if (_inner is Decimal) return new Value(-_inner.ToDecimal(null));
			if (_inner is Double) return new Value(-_inner.ToDouble(null));
			if (_inner is Int64) return new Value(-_inner.ToInt64(null));
			if (_inner is Int32) return new Value(-_inner.ToInt32(null));
			if (_inner is Boolean) return new Value(-_inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process type {0} for 'NEGATE' operation", _inner.GetType()));
		}

		public override string ToString()
		{
			return _inner.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj is Value)
			{
				var other = (Value)obj;
				if (this.IsZero && other.IsZero) return true;
				if (this.IsOne && other.IsOne) return true;
				if (this._inner is Decimal || other._inner is Decimal) return this._inner.ToDecimal(null).Equals(other._inner.ToDecimal(null));
				if (this._inner is Double || other._inner is Double) return this._inner.ToDouble(null).Equals(other._inner.ToDouble(null));
				if (this._inner is Int64 || other._inner is Int64) return this._inner.ToInt64(null).Equals(other._inner.ToInt64(null));
				if (this._inner is Int32 || other._inner is Int32) return this._inner.ToInt32(null).Equals(other._inner.ToInt32(null));
				if (this._inner is Boolean || other._inner is Boolean) return this._inner.ToInt32(null).Equals(other._inner.ToInt32(null));
				
				return this._inner.Equals(other._inner);
			}
			return false;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + this._inner.GetHashCode();

				return hash;
			}
		}

		public T ToType<T>()
		{
			return (T)Convert.ChangeType(_inner, typeof(T));
		}

		public static Value operator +(Value left, Value right)
		{
			if (left._inner is String || right._inner is String) return new Value(left.IsZero.ToString() + right._inner.ToString());
			if (left.IsZero) return right;
			if (right.IsZero) return left;
			if (left.IsOne && right.IsOne) return new Value(2);
			if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) + right._inner.ToDecimal(null));
			if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) + right._inner.ToDouble(null));
			if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) + right._inner.ToInt64(null));
			if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) + right._inner.ToInt32(null));
			if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) + right._inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process types {0} and {1} for '+' operation", left._inner.GetType(), left._inner.GetType()));
		}

		public static Value operator -(Value left, Value right)
		{
			if (left.IsZero) return right.Negate();
			if (right.IsZero) return left;
			if (left.IsOne && right.IsOne) return Value.Zero;
			if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) - right._inner.ToDecimal(null));
			if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) - right._inner.ToDouble(null));
			if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) - right._inner.ToInt64(null));
			if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) - right._inner.ToInt32(null));
			if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) - right._inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process types {0} and {1} for '-' operation", left._inner.GetType(), left._inner.GetType()));
		}

		public static Value operator -(Value left)
		{
			return left.Negate();
		}

		public static Value operator %(Value left, Value right)
		{
			if (left.IsZero && right.IsZero) return new Value(Double.NaN);
			if (left.IsZero) return new Value(right.IsPositive ? Double.PositiveInfinity : Double.NegativeInfinity);
			if (right.IsZero) return right;
			if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) % right._inner.ToDecimal(null));
			if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) % right._inner.ToDouble(null));
			if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) % right._inner.ToInt64(null));
			if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) % right._inner.ToInt32(null));
			if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) % right._inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for '%' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator *(Value left, Value right)
		{
			if (left.IsZero) return left;
			if (right.IsZero) return right;
			if (left.IsOne) return right;
			if (right.IsOne) return left;
			if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) * right._inner.ToDecimal(null));
			if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) * right._inner.ToDouble(null));
			if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) * right._inner.ToInt64(null));
			if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) * right._inner.ToInt32(null));
			if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) * right._inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for '*' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator /(Value left, Value right)
		{
			if (left.IsZero && right.IsZero) return new Value(Double.NaN);
			if (left.IsZero) return left;
			if (right.IsZero) return new Value(left.IsPositive ? Double.PositiveInfinity : Double.NegativeInfinity);
			if (right.IsOne) return left;
			if (left._inner is Decimal || right._inner is Decimal || left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToDecimal(null) / right._inner.ToDecimal(null));
			if (left._inner is Double || right._inner is Double || left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToDouble(null) / right._inner.ToDouble(null));
			if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) / right._inner.ToInt32(null));
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for '/' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Int64 RecursivePow(ref Int64 baseInt, Int64 exponentInt)
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
					Int64 y = RecursivePow(ref baseInt, exponentInt / 2);
					return y * y;
				}
				//Odd values of (n)  
				else
				{
					Int64 y = RecursivePow(ref baseInt, exponentInt - 1);
					return baseInt * y;
				}  
			}
		}

		public static Decimal RecursivePow(ref Decimal baseDecimal, Int64 exponentInt)
		{
			if (exponentInt == 1)
			{
				return baseDecimal;
			}
			else
			if (exponentInt % 2 == 0)
			{
				Decimal y = RecursivePow(ref baseDecimal, exponentInt / 2);
				return y * y;
			}
			//Odd values of (n)  
			else
			{
				Decimal y = RecursivePow(ref baseDecimal, exponentInt - 1);
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
					Decimal decimalBase = baseValue.ToDecimal();
					return new Value(Value.RecursivePow(ref decimalBase, longExponent));
				}
			}

			return new Value(Math.Pow(baseValue.ToDouble(), exponent.ToDouble()));
		}

		public static Value Abs(ref Value left)
		{
			if (!left.IsNegative) return left;
			if (left.IsNegative) return left.Negate();
			throw new InvalidOperationException(String.Format("Unable to process type {0} for 'ABS' operation", left._inner.GetType()));
		}

		public static Value Max(ref Value left, ref Value right)
		{
			if (!left.IsPositive && !right.IsNegative || left.Equals(right)) return right;
			if (!left.IsNegative && !right.IsPositive) return left;
			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is Decimal || right._inner is Decimal) return new Value(System.Math.Max(left._inner.ToDecimal(null), right._inner.ToDecimal(null)));
				if (left._inner is Double || right._inner is Double) return new Value(System.Math.Max(left._inner.ToDouble(null), right._inner.ToDouble(null)));
				if (left._inner is Int64 || right._inner is Int64) return new Value(System.Math.Max(left._inner.ToInt64(null), right._inner.ToInt64(null)));
				if (left._inner is Int32 || right._inner is Int32) return new Value(System.Math.Max(left._inner.ToInt32(null), right._inner.ToInt32(null)));
				if (left._inner is Boolean || right._inner is Boolean) return new Value(System.Math.Max(left._inner.ToInt32(null), right._inner.ToInt32(null)));
			}
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for 'MAX' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value Min(ref Value left, ref Value right)
		{
			if (!left.IsNegative && !right.IsPositive|| left.Equals(right)) return right;
			if (!left.IsPositive && !right.IsNegative) return left;
			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is Decimal || right._inner is Decimal) return new Value(System.Math.Min(left._inner.ToDecimal(null), right._inner.ToDecimal(null)));
				if (left._inner is Double || right._inner is Double) return new Value(System.Math.Min(left._inner.ToDouble(null), right._inner.ToDouble(null)));
				if (left._inner is Int64 || right._inner is Int64) return new Value(System.Math.Min(left._inner.ToInt64(null), right._inner.ToInt64(null)));
				if (left._inner is Int32 || right._inner is Int32) return new Value(System.Math.Min(left._inner.ToInt32(null), right._inner.ToInt32(null)));
				if (left._inner is Boolean || right._inner is Boolean) return new Value(System.Math.Min(left._inner.ToInt32(null), right._inner.ToInt32(null)));
			}
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for 'MIN' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator >(Value left, Value right)
		{
			if ((left.IsNegative && !right.IsNegative) || (!left.IsPositive && right.IsPositive) || left.Equals(right)) return new Value(false);
			if ((left.IsPositive && !right.IsPositive) || (!left.IsNegative && right.IsNegative)) return new Value(true);

			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) > right._inner.ToDecimal(null));
				if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) > right._inner.ToDouble(null));
				if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) > right._inner.ToInt64(null));
				if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) > right._inner.ToInt32(null));
				if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) > right._inner.ToInt32(null));
			}
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for '>' operation", left._inner.GetType(), right._inner.GetType()));
		}

		public static Value operator <(Value left, Value right)
		{
			if ((!left.IsNegative && right.IsNegative) || (left.IsPositive && !right.IsPositive) || left.Equals(right)) return new Value(false);
			if ((!left.IsPositive && right.IsPositive) || (left.IsNegative && !right.IsNegative)) return new Value(true);

			if ((left.IsPositive && right.IsPositive) || (left.IsNegative && right.IsNegative))
			{
				if (left._inner is Decimal || right._inner is Decimal) return new Value(left._inner.ToDecimal(null) < right._inner.ToDecimal(null));
				if (left._inner is Double || right._inner is Double) return new Value(left._inner.ToDouble(null) < right._inner.ToDouble(null));
				if (left._inner is Int64 || right._inner is Int64) return new Value(left._inner.ToInt64(null) < right._inner.ToInt64(null));
				if (left._inner is Int32 || right._inner is Int32) return new Value(left._inner.ToInt32(null) < right._inner.ToInt32(null));
				if (left._inner is Boolean || right._inner is Boolean) return new Value(left._inner.ToInt32(null) < right._inner.ToInt32(null));
			}
			throw new InvalidOperationException(String.Format("Unable to process type {0} and {1} for '<' operation", left._inner.GetType(), right._inner.GetType()));
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