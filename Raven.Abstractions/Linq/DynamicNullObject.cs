﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace Raven.Abstractions.Linq
{
	public class DynamicNullObject : DynamicObject, IEnumerable<object>
	{
		public override string ToString()
		{
			return String.Empty;
		}

		public bool IsExplicitNull { get; set; }

		public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			switch (binder.Operation)
			{
				case ExpressionType.Equal:
					result = arg == null || arg is DynamicNullObject;
					break;
				case ExpressionType.NotEqual:
					result = arg != null && arg is DynamicNullObject == false;
					break;
				default:
					result = this;
					break;
			}
			return true;
		}

		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			result = new DynamicNullObject
			{
				IsExplicitNull = false
			};
			return true;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = new DynamicNullObject
			{
				IsExplicitNull = false
			};
			return true;
		}

		public IEnumerator<object> GetEnumerator()
		{
			yield break;
		}

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			switch (binder.Name)
			{
				case "Count":
					result = 0;
					return true;
				case "DefaultIfEmpty":
					result = new[]
					{
						new DynamicNullObject
						{
							IsExplicitNull = false
						}
					};
					return true;
				default:
					return base.TryInvokeMember(binder, args, out result);
			}
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			result = new DynamicNullObject
			{
				IsExplicitNull = false
			};
			return true;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		// null is false or 0 by default
		public static implicit operator bool(DynamicNullObject o) { return false; }
		public static implicit operator bool?(DynamicNullObject o) { return null; }
		public static implicit operator decimal(DynamicNullObject o) { return 0; }
		public static implicit operator decimal?(DynamicNullObject o) { return null; }
		public static implicit operator double(DynamicNullObject o) { return 0; }
		public static implicit operator double?(DynamicNullObject o) { return null; }
		public static implicit operator float(DynamicNullObject o) { return 0; }
		public static implicit operator float?(DynamicNullObject o) { return null; }
		public static implicit operator long(DynamicNullObject o) { return 0; }
		public static implicit operator long?(DynamicNullObject o) { return null; }
		public static implicit operator int(DynamicNullObject o) { return 0; }
		public static implicit operator int?(DynamicNullObject o) { return null; }
		public static implicit operator short(DynamicNullObject o) { return 0; }
		public static implicit operator short?(DynamicNullObject o) { return null; }
		public static implicit operator byte(DynamicNullObject o) { return 0; }
		public static implicit operator byte?(DynamicNullObject o) { return null; }
		public static implicit operator string(DynamicNullObject o) { return null; }
		public static implicit operator char(DynamicNullObject o) { return Char.MinValue; }
		public static implicit operator char?(DynamicNullObject o) { return null; }
		public static implicit operator DateTime(DynamicNullObject o) { return DateTime.MinValue; }
		public static implicit operator DateTime?(DynamicNullObject o) { return null; }
		public static implicit operator DateTimeOffset(DynamicNullObject o) { return DateTimeOffset.MinValue; }
		public static implicit operator DateTimeOffset?(DynamicNullObject o) { return null; }
		public static implicit operator TimeSpan(DynamicNullObject o) { return TimeSpan.Zero; }
		public static implicit operator TimeSpan?(DynamicNullObject o) { return null; }
		public static implicit operator Guid(DynamicNullObject o) { return Guid.Empty; }
		public static implicit operator Guid?(DynamicNullObject o) { return null; }

		public static bool operator ==(DynamicNullObject left, object right)
		{
			return right == null || right is DynamicNullObject;
		}

		public static bool operator !=(DynamicNullObject left, object right)
		{
			return right != null && (right is DynamicNullObject) == false;
		}

		public override bool Equals(object obj)
		{
			return obj is DynamicNullObject;
		}

		public override int GetHashCode()
		{
			return 0;
		}
	}
}