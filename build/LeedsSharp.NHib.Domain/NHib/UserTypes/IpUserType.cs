using System;
using System.Data;
using System.Data.Common;
using System.Net;

namespace LeedsSharp.NHib.Domain.NHib.UserTypes
{

	[Serializable]
	public class IpUserType : BaseUserType<IPAddress>
	{
		public override bool Equals(object x, object y)
		{
			if (x == null && y == null)
				return true;

			if (x == null || y == null)
				return false;

			return AsString(x) == AsString(y);
		}

		public override object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			if (names.Length != 1)
				throw new InvalidOperationException("Only expecting one column ...");

			var rawValue = rs[names[0]] as string;
			if (rawValue == null)
				return null;

			return IPAddress.Parse(rawValue);
		}

		public override void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			var parameter = (DbParameter)cmd.Parameters[index];

			if (value == null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = AsString(value);
			}
		}

		public override object Assemble(object cached, object owner)
		{
			return cached;
		}

		public override object Disassemble(object value)
		{
			return AsString(value);
		}

		private string AsString(object value)
		{
			return (value as IPAddress)?.ToString();
		}
	}
}