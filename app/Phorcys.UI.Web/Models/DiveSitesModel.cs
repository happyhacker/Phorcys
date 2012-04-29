using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models
{
    public class DiveSitesModel
    {
        public DiveSitesModel() {
            this.DiveSite = new DiveSite();
        }

        public int DiveLocationId { get; set; }

        [Required]
        public string Title {
            get { return DiveSite.Title == null ? "" : DiveSite.Title; }
            set { DiveSite.Title = value; } 
        }

        [Required]
        [DisplayName("Water Type")]
        public bool IsFreshWater {
            get { return DiveSite.IsFreshWater; }
            set { DiveSite.IsFreshWater = value; } 
        }

        public string WaterType { get { return IsFreshWater ? "Fresh" : "Salt"; } }

        [DisplayName("Geocode")]
        public string GeoCode {
            get { return GeoCodeSafe; }
            set { DiveSite.GeoCode = value; } 
        }

        public string GeoCodeSafe { get { return DiveSite.GeoCode == null ? "" : DiveSite.GeoCode; } }

        [DisplayName("Notes")]
        public  string Notes {
            get { return DiveSite.Notes; }
            set { DiveSite.Notes = value; } 
        }

        
        [Range(0f,36198f)]
        [DisplayName("Max Depth")]
        public float MaxDepth
        {
            get { return DiveSite.MaxDepth; }
            set { DiveSite.MaxDepth = value; }
        }

        public  int UserId {
            get { return DiveSite.UserId; }
            set { DiveSite.UserId = value; } 
        }

        //public IList<DiveLocation> DiveLocationsList { get; set; }
        
        public IList<SelectListItem> DiveLocationsListItems { get; set; }

        public DiveSite DiveSite { get; internal set;}


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