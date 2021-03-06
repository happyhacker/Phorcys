using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class DiveUrl : Entity {
    public virtual int DiveUrlId {
      get;
      set;
    }
    public virtual bool IsImage {
      get;
      set;
    }
    public virtual string Title {
      get;
      set;
    }
    public virtual string Url {
      get;
      set;
    }
    public virtual Dive Dive {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiveUrl);
    }

    public virtual bool Equals(DiveUrl obj) {
      if (obj == null) return false;

      if (Equals(DiveUrlId, obj.DiveUrlId) == false)
        return false;

      if (Equals(IsImage, obj.IsImage) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      if (Equals(Url, obj.Url) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ DiveUrlId.GetHashCode();
      result = (result * 397) ^ IsImage.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      result = (result * 397) ^ (Url != null ? Url.GetHashCode() : 0);
      return result;
    }
  }
}