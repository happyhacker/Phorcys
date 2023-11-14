using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface IDiveServices {
    void Delete(Dive dive);
    Dive Get(int id);
    IList<Dive> GetAllForUser(int userId);
    void Save(Dive dive);
  }
}
