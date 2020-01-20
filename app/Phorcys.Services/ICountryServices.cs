using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface ICountryServices
  {
    Country GetCountry(string id);
  }
}
