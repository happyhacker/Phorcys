using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Attribute : Entity {
    public virtual int AttributeId {
      get;
      set;
    }
    public virtual DateTime Created {
      get;
      set;
    }
    public virtual DateTime LastModified {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual string TableToAssociate {
      get;
      set;
    }
    public virtual string Title {
      get;
      set;
    }
    public virtual ISet<AttributeAssociation> AttributeAssociations {
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

      return Equals(obj as Attribute);
    }

    public virtual bool Equals(Attribute obj) {
      if (obj == null) return false;

      if (Equals(AttributeId, obj.AttributeId) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(TableToAssociate, obj.TableToAssociate) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (AttributeId != null ? AttributeId.GetHashCode() : 0);
      result = (result * 397) ^ (Created != null ? Created.GetHashCode() : 0);
      result = (result * 397) ^ (LastModified != null ? LastModified.GetHashCode() : 0);
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (TableToAssociate != null ? TableToAssociate.GetHashCode() : 0);
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}