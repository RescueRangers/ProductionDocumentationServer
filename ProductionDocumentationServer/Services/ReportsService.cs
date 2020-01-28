using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProductionDocumentationServer.Data;
using ProductionDocumentationServer.Data.Repositories;

namespace ProductionDocumentationServer.Services
{
    public class ReportsService : IReportsService
    {
        private IReportSectionsRepository _sectionsRepo;
        private IProductionReportsRepository _reportsRepo;
        private IItemNamesRepository _itemNamesRepo;
        private IItemNumbersRepository _itemNumbersRepo;
        private IOrdersRepository _ordersRepo;

        public ReportsService(IOrdersRepository ordersRepo, IItemNumbersRepository itemNumbersRepo, IItemNamesRepository itemNamesRepo, IProductionReportsRepository reportsRepo, IReportSectionsRepository sectionsRepo)
        {
            _ordersRepo = ordersRepo;
            _itemNumbersRepo = itemNumbersRepo;
            _itemNamesRepo = itemNamesRepo;
            _reportsRepo = reportsRepo;
            _sectionsRepo = sectionsRepo;
        }

        public async Task<List<string>> GetItemNumbers()
        {
            return await _itemNumbersRepo.Get().ConfigureAwait(false);
        }

        public async Task PostItemNumber(string itemNumber)
        {
            await _itemNumbersRepo.Post(itemNumber).ConfigureAwait(false);
        }

        public async Task<List<string>> GetItemNames()
        {
            return await _itemNamesRepo.Get().ConfigureAwait(false);
        }

        public async Task PostItemName(string itemName)
        {
            await _itemNamesRepo.Post(itemName).ConfigureAwait(false);
        }

        public async Task<ReportSections> GetReportSections()
        {
            return await _sectionsRepo.Get().ConfigureAwait(false);
        }

        public async Task PostSections(List<string> sections)
        {
            await _sectionsRepo.Post(sections).ConfigureAwait(false);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _ordersRepo.Get().ConfigureAwait(false);
        }

        public async Task<int> GetOrderId(string orderNumber)
        {
            return await _ordersRepo.GetOrderId(orderNumber).ConfigureAwait(false);
        }

        public async Task<bool> PostReport(ProductionReport productionReport)
        {
            return await _reportsRepo.Post(productionReport).ConfigureAwait(false);
        }
    }
}
