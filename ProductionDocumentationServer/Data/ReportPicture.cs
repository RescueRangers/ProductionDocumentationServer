using System;

namespace ProductionDocumentationServer.Data
{
    public class ReportPicture
    {
        public string SectionName { get; set; }
        public string PictureUrl { get; set; }

        public string GuId { get; set; }

        public ReportPicture()
        {
            GuId = Guid.NewGuid().ToString();
        }
    }
}