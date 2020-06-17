using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductionDocumentationServer.Data.Repositories;
using Serilog;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace ProductionDocumentationServer.Data.DataAdaptors
{
    public class ReportsAdaptor : DataAdaptor
    {
        private IProductionReportsRepository _productionReportsRepository;

        public ReportsAdaptor(IProductionReportsRepository productionReportsRepository)
        {
            _productionReportsRepository = productionReportsRepository;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            IEnumerable<ProductionReport> reports = null;
            if (dm?.Params == null)
            {
                reports = await _productionReportsRepository.Get().ConfigureAwait(false);
            }
            else
            {
                var hasKey = dm.Params.TryGetValue("OrderId", out var orderId);

                if (!hasKey)
                {
                    return null;
                }
                else
                {
                    var isValid = int.TryParse(orderId.ToString(), out var id);
                    if (!isValid) return null;

                    reports = await _productionReportsRepository.GetByOrder(id).ConfigureAwait(false);
                }
            }
            if (dm.Search?.Count > 0)
            {
                try
                {
                    reports = DataOperations.PerformSearching(reports, dm.Search);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, nameof(this.ReadAsync));
                }
            }
            if (dm.Sorted?.Count > 0)
            {
                reports = DataOperations.PerformSorting(reports, dm.Sorted);
            }
            if (dm.Where?.Count > 0)
            {
                reports = DataOperations.PerformFiltering(reports, dm.Where, dm.Where[0].Operator);
            }

            var count = reports.Count();
            if (dm.Skip != 0)
            {
                reports = DataOperations.PerformSkip(reports, dm.Skip);
            }
            if (dm.Take != 0)
            {
                reports = DataOperations.PerformTake(reports, dm.Take);
            }

            return dm.RequiresCounts ? new DataResult { Result = reports, Count = count } : (object)reports;
        }

        public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
        {
            if (data is ProductionReport report)
            {
                await _productionReportsRepository.UpdateReport(report).ConfigureAwait(false);
                return report;
            }
            return null;
        }
    }
}
