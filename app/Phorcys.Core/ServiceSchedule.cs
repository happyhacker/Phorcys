using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class ServiceSchedule : Entity {
    public virtual DateTime? NextServiceDate {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual int ServiceScheduleId {
      get;
      set;
    }
    public virtual string Title {
      get;
      set;
    }
    public virtual Gear Gear {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as ServiceSchedule);
    }

    public virtual bool Equals(ServiceSchedule obj) {
      if (obj == null) return false;

      if (Equals(NextServiceDate, obj.NextServiceDate) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(ServiceScheduleId, obj.ServiceScheduleId) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (NextServiceDate != null ? NextServiceDate.GetHashCode() : 0);
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ ServiceScheduleId.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}