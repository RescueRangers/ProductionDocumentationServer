using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductionDocumentationServer.Data.Repositories;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace ProductionDocumentationServer.Data.DataAdaptors
{
    public class OrdersAdaptor : DataAdaptor
    {
        private IOrdersRepository _ordersRepository;

        public OrdersAdaptor(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            var orders = await _ordersRepository.Get().ConfigureAwait(false);
            if (dm.Search?.Count > 0)
            {
                orders = DataOperations.PerformSearching(orders, dm.Search);
            }
            if (dm.Sorted?.Count > 0)
            {
                orders = DataOperations.PerformSorting(orders, dm.Sorted);
            }
            if (dm.Where?.Count > 0)
            {
                orders = DataOperations.PerformFiltering(orders, dm.Where, dm.Where[0].Operator);
            }

            var count = orders.Count();
            if (dm.Skip != 0)
            {
                orders = DataOperations.PerformSkip(orders, dm.Skip);
            }
            if (dm.Take != 0)
            {
                orders = DataOperations.PerformTake(orders, dm.Take);
            }

            return dm.RequiresCounts ? new DataResult { Result = orders, Count = count } : (object)orders;
        }
    }
}
