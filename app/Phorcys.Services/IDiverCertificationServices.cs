using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface IDiverCertificationServices {
    void Delete(DiverCertification diverCertification);
    DiverCertification Get(int id);
    IList<DiverCertification> GetAll(int diverId);
    void Save(DiverCertification diverCertification);
  }
}
