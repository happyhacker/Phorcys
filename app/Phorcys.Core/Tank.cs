using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Tank : Entity {
    //public virtual int GearId {get; set;}
    public virtual int Volume {get; set;}
    public virtual int WorkingPressure {get; set;}
    public virtual int ManufacturedMonth {get; set;}
    public virtual int ManufacturedYear {get; set;}
    public virtual Gear Gear {get; set;}
    public virtual ISet<TanksOnDive> TanksOnDives {get; set;}

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Tank);
    }

    public virtual bool Equals(Tank obj) {
      if (obj == null) return false;

      if (Equals(Volume, obj.Volume) == false)
        return false;

      if (Equals(WorkingPressure, obj.WorkingPressure) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Volume != null ? Volume.GetHashCode() : 0);
      result = (result * 397) ^ (WorkingPressure != null ? WorkingPressure.GetHashCode() : 0);
      return result;
    }
  }
}