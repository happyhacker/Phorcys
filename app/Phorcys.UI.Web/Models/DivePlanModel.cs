using Phorcys.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phorcys.UI.Web.Models
{
    public class DivePlanModel
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public IList<SelectListItem> DiveSiteList { get; set; }
        public int DiveSiteId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public DateTime DescentTime { get; set; }
        public int MaxDepth { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}