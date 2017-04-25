using System;
using System.Data;
using FluentNHibernate.Utils;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace LeedsSharp.NHib.Domain.NHib.UserTypes
{

	[Serializable]
	public abstract class BaseUserType<T> : IUserType
	{
		public new abstract bool Equals(object x, object y);
		public abstract object NullSafeGet(IDataReader rs, string[] names, object owner);
		public abstract void NullSafeSet(IDbCommand cmd, object value, int index);
		public abstract object Disassemble(object value);

		public int GetHashCode(object x)
		{
			if (x == null)
				return 0;

			return x.GetHashCode();
		}

		public object DeepCopy(object value)
		{
			return value?.DeepClone();
		}

		public virtual object Replace(object original, object target, object owner)
		{
			return original;
		}

		public virtual object Assemble(object cached, object owner)
		{
			return cached;
		}

		public virtual SqlType[] SqlTypes => new SqlType[] { new StringSqlType() };

		public Type ReturnedType => typeof(T);

		public virtual bool IsMutable => true;
	}
}