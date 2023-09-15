using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IManufacturerServices
  {
    Manufacturer Save(Manufacturer manufacturer);
    void Delete(Manufacturer manufacturer);
    Manufacturer GetManufacturer(int id);
    IList<Manufacturer> GetAllForUser(int userId);
  }
}
