using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveUrlMapping : ClassMap<DiveDetailUrl>
	{
		public DiveUrlMapping()
		{
			Table("DiveUrls");
			Schema("dbo");
			Id(x => x.DiveUrlId);
			Map(x => x.IsImage);
			Map(x => x.Title);
			Map(x => x.Url, "URL");
			References(x => x.Dive)
				.Class(typeof(Dive))
				.Not.Nullable()	
				.Column("DiveId")
				.Insert()
				.Update();
		}
	}
}