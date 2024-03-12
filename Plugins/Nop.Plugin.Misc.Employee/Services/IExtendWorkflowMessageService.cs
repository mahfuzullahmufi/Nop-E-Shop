using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Misc.Employee.Services
{
    public interface IExtendWorkflowMessageService
    {
        Task SendNotificationForFirstOrderAsync(Order order, int languageId,
            string attachmentFilePath = null, string attachmentFileName = null);
    }
}
