using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveDetailUrlMapping : ClassMap<DiveDetailUrl>
	{
		public DiveDetailUrlMapping()
		{
			Table("DiveDetailUrls");
			Schema("dbo");
			Id(x => x.DiveDetailUrlId);
			Map(x => x.IsImage);
			Map(x => x.Title);
			Map(x => x.Url, "URL");
			References(x => x.DiveDetail)
				.Class(typeof(DiveDetail))
				.Not.Nullable()	
				.Column("DiveDetailsId")
				.Insert()
				.Update();
		}
	}
}