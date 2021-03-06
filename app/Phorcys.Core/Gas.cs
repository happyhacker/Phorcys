using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Gas : Entity {
    public virtual int GasId {
      get;
      set;
    }
    public virtual string Name {
      get;
      set;
    }
    public virtual ISet<GasPrice> GasPrices {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Gas);
    }

    public virtual bool Equals(Gas obj) {
      if (obj == null) return false;

      if (Equals(GasId, obj.GasId) == false)
        return false;

      if (Equals(Name, obj.Name) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ GasId.GetHashCode();
      result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
      return result;
    }
  }
}