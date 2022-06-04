using Zoho.Interfaces;

// ReSharper disable once CheckNamespace
namespace Zoho
{
    public sealed class ZohoWrapper
    {
        private readonly IBookService _bookService;
        private readonly ICampaignService _campaignService;
        private readonly ISubscriptionService _subscriptionService;

        public ZohoWrapper(IBookService bookService, ICampaignService campaignService, ISubscriptionService subscriptionService)
        {
            _bookService = bookService;
            _campaignService = campaignService;
            _subscriptionService = subscriptionService;
        }
        public IBookService Book => _bookService;
        public ICampaignService Campaign => _campaignService;
        public ISubscriptionService Subscription => _subscriptionService;
    }
}