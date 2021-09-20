using Zoho.Interfaces;

namespace Zoho
{
    public class Wrapper
    {
        private readonly IBookService _bookService;
        private readonly ICampaignService _campaignService;
        private readonly ISubscriptionService _subscriptionService;

        public Wrapper(IBookService bookService, ICampaignService campaignService, ISubscriptionService subscriptionService)
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