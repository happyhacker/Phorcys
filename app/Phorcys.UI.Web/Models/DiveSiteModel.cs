using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models {
  public class DiveSiteModel : DiveSite {

    public virtual string Url4Map() {
      var retVal = new StringBuilder("");

      if (GeoCode != null && GeoCode.Trim().Length > 0) {
        retVal.Append("<a href=\"http://maps.google.com/maps?q=");
        retVal.Append(GeoCode.Trim());
        //arrow is centered
        retVal.Append("&ll=");
        retVal.Append(GeoCode.Trim());
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