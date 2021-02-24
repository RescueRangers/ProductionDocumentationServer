using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data
{
    public class ReportViewModel
    {
        [Required]
        public string ItemNumber { get; set; }
        [Required]
        public string ItemName { get; set; }
    }
}
