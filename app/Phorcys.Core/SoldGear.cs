using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class SoldGear : Entity {
    public virtual decimal? Amount {
      get;
      set;
    }
    public virtual int GearId {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual DateTime SoldOn {
      get;
      set;
    }
    public virtual Contact Contact {
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

      return Equals(obj as SoldGear);
    }

    public virtual bool Equals(SoldGear obj) {
      if (obj == null) return false;

      if (Equals(Amount, obj.Amount) == false)
        return false;

      if (Equals(GearId, obj.GearId) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(SoldOn, obj.SoldOn) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Amount != null ? Amount.GetHashCode() : 0);
      result = (result * 397) ^ GearId.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ SoldOn.GetHashCode();
      return result;
    }
  }
}