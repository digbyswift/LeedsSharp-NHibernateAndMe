using System;
using NHibernate;

namespace LeedsSharp.NHib.Domain.NHib.Data
{
	public class TransactionWrapper : IDisposable
	{
		private readonly ITransaction _transaction;
		private bool _isDisposed;

		public TransactionWrapper(ITransaction transaction)
		{
			_transaction = transaction;
		}

		public bool IsActive => _transaction != null && _transaction.IsActive;

		public void Commit()
		{
			_transaction.Commit();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && !_isDisposed)
			{
				_transaction.Dispose();
				_isDisposed = true;
			}
		}
	}
}