using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data
{
    public class ProductionReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ReportPicture> ReportPictures { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
    }
}
