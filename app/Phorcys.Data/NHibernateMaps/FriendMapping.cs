using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class FriendMapping : ClassMap<Friend>
	{
		public FriendMapping()
		{
			Table("Friends");
			Schema("dbo");			CompositeId()
				.KeyProperty(x => x.RequestorUserId, "RequestorUserId")
				.KeyProperty(x => x.RecipientUserId, "RecipientUserId")
				.KeyProperty(x => x.DateRequested, "DateRequested");

			Map(x => x.LastUpdated);
			Map(x => x.Status);
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("RequestorUserId")
				.Insert()
				.Update();
			References(x => x.User1)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("RecipientUserId")
				.Insert()
				.Update();
		}
	}
}