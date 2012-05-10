using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Manufacturer : Entity {
    public virtual int ManufacturerId {
      get;
      set;
    }
    public virtual Contact Contact {
      get;
      set;
    }
    public virtual ISet<Gear> Gears {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Manufacturer);
    }

    public virtual bool Equals(Manufacturer obj) {
      if (obj == null) return false;

      if (Equals(ManufacturerId, obj.ManufacturerId) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ ManufacturerId.GetHashCode();
      return result;
    }
  }
}