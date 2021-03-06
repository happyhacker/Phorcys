using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Role : Entity {
    public virtual string Notes {
      get;
      set;
    }
    public virtual int RoleId {
      get;
      set;
    }
    public virtual string Title {
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

      return Equals(obj as Role);
    }

    public virtual bool Equals(Role obj) {
      if (obj == null) return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(RoleId, obj.RoleId) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ RoleId.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}