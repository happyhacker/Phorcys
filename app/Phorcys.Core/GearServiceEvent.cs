using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class GearServiceEvent : Entity {
    public virtual decimal? Cost {
      get;
      set;
    }
    public virtual int GearServiceEventsId {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual DateTime ServicedDate {
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

      return Equals(obj as GearServiceEvent);
    }

    public virtual bool Equals(GearServiceEvent obj) {
      if (obj == null) return false;

      if (Equals(Cost, obj.Cost) == false)
        return false;

      if (Equals(GearServiceEventsId, obj.GearServiceEventsId) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(ServicedDate, obj.ServicedDate) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Cost != null ? Cost.GetHashCode() : 0);
      result = (result * 397) ^ GearServiceEventsId.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ ServicedDate.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}