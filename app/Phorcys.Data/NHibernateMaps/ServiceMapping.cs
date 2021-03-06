using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class ServiceMapping : ClassMap<Service>
	{
		public ServiceMapping()
		{
			Table("Services");
			Schema("dbo");
			Id(x => x.ServiceId);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);
			HasManyToMany(x => x.DiveShops)
				.ChildKeyColumn("ServiceId")
				.ParentKeyColumn("DiveShopId")
				.Table("DiveShopServices")
				.Fetch.Select()
				.AsSet();
		}
	}
}