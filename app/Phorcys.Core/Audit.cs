using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Audit : Entity {
    public virtual string ActionPerformed {
      get;
      set;
    }
    public virtual int AuditsId {
      get;
      set;
    }
    public virtual DateTime Created {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual int? RowId {
      get;
      set;
    }
    public virtual string TableName {
      get;
      set;
    }
    public virtual string UserName {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Audit);
    }

    public virtual bool Equals(Audit obj) {
      if (obj == null) return false;

      if (Equals(ActionPerformed, obj.ActionPerformed) == false)
        return false;

      if (Equals(AuditsId, obj.AuditsId) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(RowId, obj.RowId) == false)
        return false;

      if (Equals(TableName, obj.TableName) == false)
        return false;

      if (Equals(UserName, obj.UserName) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (ActionPerformed != null ? ActionPerformed.GetHashCode() : 0);
      result = (result * 397) ^ AuditsId.GetHashCode();
      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (RowId != null ? RowId.GetHashCode() : 0);
      result = (result * 397) ^ (TableName != null ? TableName.GetHashCode() : 0);
      result = (result * 397) ^ (UserName != null ? UserName.GetHashCode() : 0);
      return result;
    }
  }
}