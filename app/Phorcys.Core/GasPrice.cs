using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class GasPrice : Entity {
    public virtual DateTime? FillDate {
      get;
      set;
    }
    public virtual int GasPriceId {
      get;
      set;
    }
    public virtual decimal UnitPrice {
      get;
      set;
    }
    public virtual int VolumeAdded {
      get;
      set;
    }
    public virtual Gas Gase {
      get;
      set;
    }
    public virtual ISet<TanksOnDive> TanksOnDives {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as GasPrice);
    }

    public virtual bool Equals(GasPrice obj) {
      if (obj == null) return false;

      if (Equals(FillDate, obj.FillDate) == false)
        return false;

      if (Equals(GasPriceId, obj.GasPriceId) == false)
        return false;

      if (Equals(UnitPrice, obj.UnitPrice) == false)
        return false;

      if (Equals(VolumeAdded, obj.VolumeAdded) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (FillDate != null ? FillDate.GetHashCode() : 0);
      result = (result * 397) ^ GasPriceId.GetHashCode();
      result = (result * 397) ^ UnitPrice.GetHashCode();
      result = (result * 397) ^ VolumeAdded.GetHashCode();
      return result;
    }
  }
}