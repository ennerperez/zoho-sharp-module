using Zoho.Interfaces;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Zoho
{
    public sealed class ZohoWrapper
    {
        public ZohoWrapper(IBookService bookService, ICampaignService campaignService, ISubscriptionService subscriptionService, ICrmService crm, IProjectService project)
        {
            Book = bookService;
            Campaign = campaignService;
            Subscription = subscriptionService;
            CRM = crm;
            Project = project;
        }

        public IBookService Book { get; }

        public ICampaignService Campaign { get; }

        public ISubscriptionService Subscription { get; }
        
        public ICrmService CRM { get; }
        
        public IProjectService Project { get; }
    }
}
