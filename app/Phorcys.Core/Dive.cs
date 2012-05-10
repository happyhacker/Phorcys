using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Dive : Entity {
    public virtual DateTime Created {
      get;
      set;
    }
    public virtual int DiveId {
      get;
      set;
    }
    public virtual DateTime LastModified {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual DateTime ScheduledTime {
      get;
      set;
    }
    public virtual string Title {
      get;
      set;
    }
    public virtual ISet<DiveDetail> DiveDetails {
      get;
      set;
    }
    public virtual ISet<Diver> Divers {
      get;
      set;
    }
    public virtual DiveSite DiveSite {
      get;
      set;
    }
    public virtual ISet<DiveType> DiveTypes {
      get;
      set;
    }
    public virtual User User {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Dive);
    }

    public virtual bool Equals(Dive obj) {
      if (obj == null) return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(DiveId, obj.DiveId) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(ScheduledTime, obj.ScheduledTime) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ DiveId.GetHashCode();
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ ScheduledTime.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}