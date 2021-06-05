namespace Zoho.Subscriptions.Types
{
    // public enum SubscriptionStatus : short
    // {
    //     Live,
    //     Trial,
    //     Dunning,
    //     Unpaid,
    //     NonRenewing,
    //     Cancelled,
    //     CreationFailed,
    //     CancelledFromDunning,
    //     Expired,
    //     TrialExpired,
    //     Future
    // }

    public static class SubscriptionStatus
    {
        public const string Live = "live";
        public const string Cancelled = "cancelled";
    }
}