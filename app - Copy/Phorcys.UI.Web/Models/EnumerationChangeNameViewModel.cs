﻿using System.ComponentModel.DataAnnotations;

namespace Phorcys.UI.Web.Models
{
    public class EnumerationChangeNameViewModel
    {
        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(40, ErrorMessage = "Must enter a name of no more than 40 characters.", MinimumLength = 1)]
        public string Name { get; set; }

        public int EnumerationId { get; set; }
    }
}