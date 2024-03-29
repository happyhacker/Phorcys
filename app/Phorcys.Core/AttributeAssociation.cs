using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class AttributeAssociation : Entity {
    public virtual int AttributeId {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual int TableRowId {
      get;
      set;
    }
    public virtual string Title {
      get;
      set;
    }
    public virtual Attribute Attribute {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as AttributeAssociation);
    }

    public virtual bool Equals(AttributeAssociation obj) {
      if (obj == null) return false;

      if (Equals(AttributeId, obj.AttributeId) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(TableRowId, obj.TableRowId) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ AttributeId.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ TableRowId.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}