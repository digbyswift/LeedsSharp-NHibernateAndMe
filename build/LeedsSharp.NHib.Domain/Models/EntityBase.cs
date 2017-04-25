using System;
using System.Xml.Serialization;

namespace LeedsSharp.NHib.Domain.Models
{
	public interface IEntity<TId>
	{
		TId Id { get; set; }
	}

	[Serializable]
	public abstract class EntityBase<TId, TDomain> : IEntity<TId>
	{
		/// <summary>
		/// To help ensure hashcode uniqueness, a carefully selected random number multiplier
		/// is used within the calculation.  Goodrich and Tamassia's Data Structures and
		/// Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
		/// of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// for more information.
		/// </summary>
		private const int HashMultiplier = 31;

		private int? _cachedHashcode;

		/// <summary>
		/// Id may be of type string, int, custom type, etc.
		/// Setter is protected to allow unit tests to set this property via reflection and to allow
		/// domain objects more flexibility in setting this for those objects with assigned Ids.
		/// It's virtual to allow NHibernate-backed objects to be lazily loaded.
		/// This is ignored for XML serialization because it does not have a public setter (which is very much by design).
		/// See the FAQ within the documentation if you'd like to have the Id XML serialized.
		/// </summary>
		[XmlIgnore]
		public virtual TId Id { get; set; }


		public override int GetHashCode()
		{
			if (_cachedHashcode.HasValue)
			{
				return _cachedHashcode.Value;
			}

			if (IsTransient())
			{
				_cachedHashcode = base.GetHashCode();
			}
			else
			{
				unchecked
				{
					// It's possible for two objects to return the same hash code based on 
					// identically valued properties, even if they're of two different types, 
					// so we include the object's type in the hash calculation
					var hashCode = GetType().GetHashCode();
					_cachedHashcode = (hashCode * HashMultiplier) ^ Id.GetHashCode();
				}
			}

			return _cachedHashcode.Value;
		}

		public override bool Equals(object obj)
		{
			var compareTo = obj as EntityBase<TId, TDomain>;

			return (compareTo != null) &&
			       (HasSameNonDefaultIdAs(compareTo) ||
			        (((IsTransient()) || compareTo.IsTransient()) &&
			         HasSameBusinessSignatureAs(compareTo)));
		}

		protected virtual bool IsTransient()
		{
			return Id == null || Id.Equals(default(TId));
		}

		private bool HasSameBusinessSignatureAs(EntityBase<TId, TDomain> compareTo)
		{
			if (compareTo == null)
				throw new ArgumentNullException(nameof(compareTo));

			return GetHashCode().Equals(compareTo.GetHashCode());
		}

		private bool HasSameNonDefaultIdAs(EntityBase<TId, TDomain> compareTo)
		{
			if (compareTo == null)
				throw new ArgumentNullException(nameof(compareTo));

			return
				Id != null &&
				!Id.Equals(default(TId)) &&
			    compareTo.Id != null &&
				!compareTo.Id.Equals(default(TId)) &&
			    Id.Equals(compareTo.Id);
		}
	}
}