using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data
{
    public class ReportSections
    {
        public List<string> Sections { get; set; }

        public ReportSections()
        {
            Sections = new List<string> { "Root", "Tip", "Middle"};
        }
    }
}
