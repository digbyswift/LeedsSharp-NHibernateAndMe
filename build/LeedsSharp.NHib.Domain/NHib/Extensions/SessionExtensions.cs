using NHibernate;

namespace LeedsSharp.NHib.Domain.NHib.Extensions
{
	public static class SessionExtensions
	{
		public static void Delete<T>(this ISession session, object id)
		{
			session.Delete(session.Load<T>(id));
		}
	}
}