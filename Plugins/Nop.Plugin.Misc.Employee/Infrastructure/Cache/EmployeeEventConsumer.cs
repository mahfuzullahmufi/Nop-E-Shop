using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Events;
using Nop.Plugin.Misc.Employee.Domain;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.Employee.Infrastructure.Cache
{
    public class EmployeeEventConsumer :
        IConsumer<EntityInsertedEvent<EmployeeDetails>>,
        IConsumer<EntityUpdatedEvent<EmployeeDetails>>,
        IConsumer<EntityDeletedEvent<EmployeeDetails>>
    {
        #region Fields

        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public EmployeeEventConsumer(
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }

        #endregion

        #region Methods
        public async Task HandleEventAsync(EntityInsertedEvent<EmployeeDetails> eventMessage)
        {
            var prefix = string.Format(EmployeeDefaults.EmployeePluginPrefix, (await _workContext.GetCurrentCustomerAsync()).Id);
            await _staticCacheManager.RemoveByPrefixAsync(prefix);
        }
        public async Task HandleEventAsync(EntityUpdatedEvent<EmployeeDetails> eventMessage)
        {
            var prefix = string.Format(EmployeeDefaults.EmployeePluginPrefix, (await _workContext.GetCurrentCustomerAsync()).Id);
            await _staticCacheManager.RemoveByPrefixAsync(prefix);
        }

        public async Task HandleEventAsync(EntityDeletedEvent<EmployeeDetails> eventMessage)
        {
            var prefix = string.Format(EmployeeDefaults.EmployeePluginPrefix, (await _workContext.GetCurrentCustomerAsync()).Id);
            await _staticCacheManager.RemoveByPrefixAsync(prefix);
        }
       
        #endregion
    }
}
