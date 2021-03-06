using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class TanksOnDive : Entity {
    public virtual int DiveDetailsId {
      get;
      set;
    }
    public virtual int? EndingPressure {
      get;
      set;
    }
    public virtual int GearId {
      get;
      set;
    }
    public virtual int? StartingPressure {
      get;
      set;
    }

    public virtual ISet<GasPrice> GasPrices {
      get;
      set;
    }
    public virtual Tank Tank {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as TanksOnDive);
    }

    public virtual bool Equals(TanksOnDive obj) {
      if (obj == null) return false;

      if (Equals(DiveDetailsId, obj.DiveDetailsId) == false)
        return false;

      if (Equals(EndingPressure, obj.EndingPressure) == false)
        return false;

      if (Equals(GearId, obj.GearId) == false)
        return false;

      if (Equals(StartingPressure, obj.StartingPressure) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ DiveDetailsId.GetHashCode();
      result = (result * 397) ^ (EndingPressure != null ? EndingPressure.GetHashCode() : 0);
      result = (result * 397) ^ GearId.GetHashCode();
      result = (result * 397) ^ (StartingPressure != null ? StartingPressure.GetHashCode() : 0);
      return result;
    }
  }
}