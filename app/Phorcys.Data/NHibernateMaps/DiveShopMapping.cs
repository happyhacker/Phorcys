using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveShopMapping : ClassMap<DiveShop>
	{
		public DiveShopMapping()
		{
			Table("DiveShops");
			Schema("dbo");
			Id(x => x.DiveShopId);
			Map(x => x.Notes);
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();
			HasManyToMany(x => x.Services)
				.ChildKeyColumn("DiveShopId")
				.ParentKeyColumn("ServiceId")
				.Table("DiveShopServices")
				.Fetch.Select()
				.AsSet();
		}
	}
}