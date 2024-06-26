using System;
using FluentNHibernate.Mapping;
using Phorcys.Core;

namespace Phorcys.Data.NHibernateMaps
{
	public class CertificationMapping : ClassMap<Certification>
	{
		public CertificationMapping()
		{
			Table("Certifications");
      Id(x => x.Id, "CertificationId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);

			References(x => x.DiveAgency)
				.Class(typeof(DiveAgency))	
				.Column("DiveAgencyId")
				.Insert()
				.Update();

			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
		}
	}
}