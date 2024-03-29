using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Friend : Entity {
    public virtual DateTime DateRequested {
      get;
      set;
    }
    public virtual DateTime LastUpdated {
      get;
      set;
    }
    public virtual int RecipientUserId {
      get;
      set;
    }
    public virtual int RequestorUserId {
      get;
      set;
    }
    public virtual string Status {
      get;
      set;
    }
    public virtual User User {
      get;
      set;
    }
    public virtual User User1 {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Friend);
    }

    public virtual bool Equals(Friend obj) {
      if (obj == null) return false;

      if (Equals(DateRequested, obj.DateRequested) == false)
        return false;

      if (Equals(LastUpdated, obj.LastUpdated) == false)
        return false;

      if (Equals(RecipientUserId, obj.RecipientUserId) == false)
        return false;

      if (Equals(RequestorUserId, obj.RequestorUserId) == false)
        return false;

      if (Equals(Status, obj.Status) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ DateRequested.GetHashCode();
      result = (result * 397) ^ LastUpdated.GetHashCode();
      result = (result * 397) ^ RecipientUserId.GetHashCode();
      result = (result * 397) ^ RequestorUserId.GetHashCode();
      result = (result * 397) ^ (Status != null ? Status.GetHashCode() : 0);
      return result;
    }
  }
}