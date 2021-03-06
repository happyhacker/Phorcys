using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class InsurancePolicy : Entity {
    public virtual decimal? Deductible {
      get;
      set;
    }
    public virtual DateTime? EndOfTerm {
      get;
      set;
    }
    public virtual int InsurancePolicyId {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual DateTime? StartOfTerm {
      get;
      set;
    }
    public virtual decimal? ValueAmount {
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

      return Equals(obj as InsurancePolicy);
    }

    public virtual bool Equals(InsurancePolicy obj) {
      if (obj == null) return false;

      if (Equals(Deductible, obj.Deductible) == false)
        return false;

      if (Equals(EndOfTerm, obj.EndOfTerm) == false)
        return false;

      if (Equals(InsurancePolicyId, obj.InsurancePolicyId) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(StartOfTerm, obj.StartOfTerm) == false)
        return false;

      if (Equals(ValueAmount, obj.ValueAmount) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Deductible != null ? Deductible.GetHashCode() : 0);
      result = (result * 397) ^ (EndOfTerm != null ? EndOfTerm.GetHashCode() : 0);
      result = (result * 397) ^ InsurancePolicyId.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (StartOfTerm != null ? StartOfTerm.GetHashCode() : 0);
      result = (result * 397) ^ (ValueAmount != null ? ValueAmount.GetHashCode() : 0);
      return result;
    }
  }
}