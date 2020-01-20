using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IDiveShopServices
  {
    DiveShop Save(DiveShop diveShop);
    void Delete(DiveShop diveShop);
    DiveShop GetDiveShop(int id);
    IList<DiveShop> GetAllForUser(int userId);
  }
}
