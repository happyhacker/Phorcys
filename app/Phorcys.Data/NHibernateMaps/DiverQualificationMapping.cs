using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiverQualificationMapping : ClassMap<DiverQualification>
	{
		public DiverQualificationMapping()
		{
			Table("DiverQualifications");
			Schema("dbo");			CompositeId()
				.KeyProperty(x => x.DiverId, "DiverId")
				.KeyProperty(x => x.QualificationId, "QualificationId");

			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Qualified);
			References(x => x.Diver)
				.Class(typeof(Diver))
				.Not.Nullable()	
				.Column("DiverId")
				.Insert()
				.Update();
			References(x => x.Qualification)
				.Class(typeof(Qualification))
				.Not.Nullable()	
				.Column("QualificationId")
				.Insert()
				.Update();
		}
	}
}