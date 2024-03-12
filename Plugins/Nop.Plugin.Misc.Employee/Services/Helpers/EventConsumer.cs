using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.Employee.Services.Helpers
{
    public class EventConsumer : IConsumer<OrderPlacedEvent>
    {
        #region Fields
        private readonly IExtendWorkflowMessageService _extendWorkflowMessageService;
        private readonly IRepository<Order> _orderRepository;

        #endregion

        #region Ctor

        public EventConsumer(IExtendWorkflowMessageService extendWorkflowMessageService, IRepository<Order> orderRepository)
        {
            _extendWorkflowMessageService = extendWorkflowMessageService;
            _orderRepository = orderRepository;
        }

        #endregion

        #region Methods

        public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
        {
            var order = eventMessage.Order;
            var customerOder = await _orderRepository.Table.Where(x => x.CustomerId == order.CustomerId).ToListAsync();
            if (customerOder.Count() == 3)
                await _extendWorkflowMessageService.SendNotificationForFirstOrderAsync(order, order.CustomerLanguageId);
        }

        #endregion
    }
}

