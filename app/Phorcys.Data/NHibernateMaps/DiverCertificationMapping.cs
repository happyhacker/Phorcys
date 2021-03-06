using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiverCertificationMapping : ClassMap<DiverCertification>
	{
		public DiverCertificationMapping()
		{
			Table("DiverCertifications");
      Id(x => x.Id, "DiverCertificationId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.CertificationNum);
			Map(x => x.Certified);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			References(x => x.Certification)
				.Class(typeof(Certification))
				.Not.Nullable()	
				.Column("CertificationId")
				.Insert()
				.Update();
			References(x => x.Instructor)
				.Class(typeof(Instructor))	
				.Column("InstructorId")
				.Insert()
				.Update();
			References(x => x.Diver)
				.Class(typeof(Diver))
				.Not.Nullable()	
				.Column("DiverId")
				.Insert()
				.Update();
		}
	}
}