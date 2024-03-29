using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class DiveSiteUrl : Entity {
    public virtual int DiveSiteUrlId {
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
    public virtual DiveSite DiveSite {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiveSiteUrl);
    }

    public virtual bool Equals(DiveSiteUrl obj) {
      if (obj == null) return false;

      if (Equals(DiveSiteUrlId, obj.DiveSiteUrlId) == false)
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

      result = (result * 397) ^ DiveSiteUrlId.GetHashCode();
      result = (result * 397) ^ IsImage.GetHashCode();
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      result = (result * 397) ^ (Url != null ? Url.GetHashCode() : 0);
      return result;
    }
  }
}