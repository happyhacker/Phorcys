using System.Collections.Generic;
using System.Text;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models {
  public class DiveSitesIndexModel {
    public IList<DiveSite> DiveSiteList;

    public virtual string Url4Map(string geoCode) {
      var retVal = new StringBuilder("");

      if (geoCode != null && geoCode.Trim().Length > 0) {
        retVal.Append("<a href=\"http://maps.google.com/maps?q=");
        retVal.Append(geoCode.Trim());
        //arrow is centered
        retVal.Append("&ll=");
        retVal.Append(geoCode.Trim());
        //zoom level
        retVal.Append("&z=14");
        retVal.Append("\"");
        retVal.Append(" target=\"_blank\" ");
        //retVal.Append("onclick='return ! window.open(this.href);'");
        retVal.Append(">Map</a>");
      }
      return retVal.ToString();
    }
  }
}