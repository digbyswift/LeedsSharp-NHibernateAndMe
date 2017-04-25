using System;
using System.Data;
using System.Web.Mvc;
using LeedsSharp.NHib.Domain.NHib.Data;
using NHibernate;

namespace LeedsSharp.NHib.UI.Attributes
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public class AsTransactionAttribute : ActionFilterAttribute
	{
		private readonly IsolationLevel _isolationLevel;
		private TransactionWrapper _transaction;
		public TransactionFactory TransactionFactory { get; set; }

		public AsTransactionAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			_isolationLevel = isolationLevel;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_transaction = TransactionFactory.Create(_isolationLevel);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (!_transaction.IsActive)
				return;

			if (filterContext.Exception == null)
			{
				try
				{
					filterContext.Controller.TempData["StatusAlert"] = "Karate chop!!";

					_transaction.Commit();
				}
				catch (StaleObjectStateException)
				{
					// Handle stale update here
					filterContext.Controller.TempData["StatusAlert"] = "The changes could not be applied since this record has been updated by another user.";

					// Redirect back to self. This is required in case the
					// action is attempting to redirect to another action.
					filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.RawUrl);

					// Allow rollback
					_transaction.Dispose();
				}
			}
			else
			{
				_transaction.Dispose();
			}
		}
	}
}