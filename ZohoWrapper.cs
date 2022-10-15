using Zoho.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Zoho
{
    public sealed class ZohoWrapper
    {
        public ZohoWrapper(IBookService bookService, ICampaignService campaignService, ISubscriptionService subscriptionService)
        {
            Book = bookService;
            Campaign = campaignService;
            Subscription = subscriptionService;
        }

        public IBookService Book { get; }

        public ICampaignService Campaign { get; }

        public ISubscriptionService Subscription { get; }
    }
}
