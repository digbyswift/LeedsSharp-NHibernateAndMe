using System;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;

namespace LeedsSharp.NHib.Domain.NHib.UserTypes
{

	[Serializable]
	public class JsonUserType<T> : BaseUserType<T> where T : class
	{
		public override bool Equals(object x, object y)
		{
			if (x == null && y == null)
				return true;

			if (x == null || y == null)
				return false;

			return ToJson(x) == ToJson(y);
		}

		public override object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			if (names.Length != 1)
				throw new InvalidOperationException("Only expecting one column ...");

			return FromJson(rs[names[0]] as string);
		}

		public override void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			var parameter = (DbParameter)cmd.Parameters[index];

			parameter.Value = value == null
				? DBNull.Value
				: (object)ToJson(value);
		}

		public override object Assemble(object cached, object owner)
		{
			var str = cached as string;

			return FromJson(str);
		}

		public override object Disassemble(object value)
		{
			return ToJson(value);
		}

		#region Private

		private string ToJson(object value)
		{
			if (value == null)
				return null;

			return JsonConvert.SerializeObject(value);
		}

		private T FromJson(string serializedValue)
		{
			if (String.IsNullOrWhiteSpace(serializedValue))
				return null;

			return JsonConvert.DeserializeObject<T>(serializedValue);
		}

		#endregion

	}
}