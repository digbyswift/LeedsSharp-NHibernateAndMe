using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Type;

namespace LeedsSharp.NHib.Domain.NHib.Mapping
{

	public class CustomForeignKeyConvention : ForeignKeyConvention
	{
		protected override string GetKeyName(Member property, Type type)
		{
			return $"{(property == null ? type.Name : property.Name)}Id";
		}
	}

	public class DefaultStringLengthConvention : IPropertyConvention
	{
		public void Apply(IPropertyInstance instance)
		{
			instance.Length(250);
		}
	}

	public class EnumConvention : IPropertyConvention, IPropertyConventionAcceptance
	{
		public void Apply(IPropertyInstance instance)
		{
			instance.CustomType(instance.Property.PropertyType);
		}

		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
		{
			criteria.Expect(x =>
				x.Property.PropertyType.IsEnum || (
					x.Property.PropertyType.IsGenericType &&
					x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
					x.Property.PropertyType.GetGenericArguments()[0].IsEnum)
				);
		}
	}

	public class DateTimeConvention : IPropertyConvention, IPropertyConventionAcceptance
	{
		public void Apply(IPropertyInstance instance)
		{
			instance.CustomType(typeof(UtcDateTimeType));
		}

		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
		{
			criteria.Expect(x =>
				x.Property.PropertyType == typeof(DateTime) || (
					x.Property.PropertyType.IsGenericType &&
					x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
					x.Property.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
				);
		}
	}

}
