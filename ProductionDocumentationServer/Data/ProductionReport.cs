using System;
using System.Collections.Generic;
using ProductionDocumentationServer.Services;
using Serilog;

namespace ProductionDocumentationServer.Data
{
    public class ProductionReport
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public List<ReportPicture> ReportPictures { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string TimeCode { get; set; }

        public DateTime? MaterialDate { get {
                if (string.IsNullOrWhiteSpace(TimeCode)) { return null; }
                try
                {
                    return (DateTime?)new DateTime(Base36.Base36ToNumber(TimeCode));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Error when converting time code to date for {ItemNumber}");
                    return null;
                }
            } }
    }
}