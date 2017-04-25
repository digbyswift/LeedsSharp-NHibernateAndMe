using System;
using System.Linq;
using System.Reflection;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace LeedsSharp.NHib.Domain.NHib.Events
{
	[Serializable]
	public class DateAuditEventListener : IPreUpdateEventListener
	{
		private const string PropertyName = "LastUpdated";

		public bool OnPreUpdate(PreUpdateEvent @event)
		{
			var property = @event.Entity
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.FirstOrDefault(x => x.Name == PropertyName);

			if (property == null)
				return false;

			var time = DateTime.UtcNow;

			Set(@event.Persister, @event.State, PropertyName, time);

			property.SetValue(@event.Entity, time);

			return false;
		}

		private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
		{
			var index = Array.IndexOf(persister.PropertyNames, propertyName);
			if (index == -1)
				return;
				
			state[index] = value;
		}

	}
}
