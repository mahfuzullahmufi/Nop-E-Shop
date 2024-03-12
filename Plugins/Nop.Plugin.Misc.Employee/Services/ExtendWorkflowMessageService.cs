using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.Employee.Services
{
    public class ExtendWorkflowMessageService : WorkflowMessageService, IExtendWorkflowMessageService
    {
        #region Fields
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMessageTokenProvider _messageTokenProvider;
        private readonly IAddressService _addressService;
        #endregion

        #region Ctor
        public ExtendWorkflowMessageService(CommonSettings commonSettings, 
            EmailAccountSettings emailAccountSettings, 
            IAddressService addressService, 
            IAffiliateService affiliateService, 
            ICustomerService customerService, 
            IEmailAccountService emailAccountService, 
            IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            IMessageTemplateService messageTemplateService, 
            IMessageTokenProvider messageTokenProvider, 
            IOrderService orderService, 
            IProductService productService, 
            IQueuedEmailService queuedEmailService, 
            IStoreContext storeContext, 
            IStoreService storeService, 
            ITokenizer tokenizer, 
            MessagesSettings messagesSettings) 
            : base(commonSettings, emailAccountSettings, addressService, affiliateService, customerService, emailAccountService, eventPublisher, languageService, localizationService, messageTemplateService, messageTokenProvider, orderService, productService, queuedEmailService, storeContext, storeService, tokenizer, messagesSettings)
        {
            _storeService = storeService;
            _storeContext = storeContext;
            _eventPublisher = eventPublisher;
            _messageTokenProvider = messageTokenProvider;
            _addressService = addressService;
        }
        #endregion

        #region Methods
        public async Task SendNotificationForFirstOrderAsync(Order order, int languageId,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var store = await _storeService.GetStoreByIdAsync(order.StoreId) ?? await _storeContext.GetCurrentStoreAsync();

            var messageTemplate = await GetActiveMessageTemplatesAsync(EmployeeDefaults.CustomerFirstOrderNotification, store.Id);

            languageId = await EnsureLanguageIsActiveAsync(languageId, store.Id);

            //tokens
            var commonTokens = new List<Token>();
            await _messageTokenProvider.AddOrderTokensAsync(commonTokens, order, languageId);
            await _messageTokenProvider.AddCustomerTokensAsync(commonTokens, order.CustomerId);

            //email account
            var emailAccount = await GetEmailAccountOfMessageTemplateAsync(messageTemplate.FirstOrDefault(), languageId);

            var tokens = new List<Token>(commonTokens);
            tokens.Add(new Token("Coupon.Code", $"KaccKhor{order.CustomerId}", true));
            await _messageTokenProvider.AddStoreTokensAsync(tokens, store, emailAccount);

            //event notification
            await _eventPublisher.MessageTokensAddedAsync(messageTemplate.FirstOrDefault(), tokens);

            var billingAddress = await _addressService.GetAddressByIdAsync(order.BillingAddressId);

            var toEmail = billingAddress.Email;
            var toName = $"{billingAddress.FirstName} {billingAddress.LastName}";

            await SendNotificationAsync(messageTemplate.FirstOrDefault(), emailAccount, languageId, tokens, toEmail, toName,
                    attachmentFilePath, attachmentFileName);
        }
        #endregion
    }
}

