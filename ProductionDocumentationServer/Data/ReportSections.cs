using System.Collections.Generic;

namespace ProductionDocumentationServer.Data
{
    public class ReportSections
    {
        public List<string> Sections { get; set; }

        public ReportSections()
        {
            Sections = new List<string> { "Root", "Tip", "Middle" };
        }
    }
}