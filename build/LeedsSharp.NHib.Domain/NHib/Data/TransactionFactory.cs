using System;
using System.Data;
using NHibernate;

namespace LeedsSharp.NHib.Domain.NHib.Data
{
	public class TransactionFactory
	{
		private readonly ISession _session;

		public TransactionFactory(ISession session)
		{
			_session = session;
		}

		public TransactionWrapper Create(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			if (_session.Transaction.IsActive)
			{
				throw new Exception("Active transaction already exists!");
			}

			return new TransactionWrapper(_session.BeginTransaction(isolationLevel));
		}
	}

}