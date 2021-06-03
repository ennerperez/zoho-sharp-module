using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Abstractions.Models
{
    public class Response
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Response<T> : Response
    {
        public T @Object { get; set; }
    }
}

// using System.Collections.Generic;
// using Newtonsoft.Json;
//
// namespace Infrastructure.Enterprise.Models
// {

//
//   internal class PageContext
//   {
//     [JsonProperty("page")]
//     public int Page { get; set; }
//
//     [JsonProperty("per_page")]
//     public int PerPage { get; set; }
//
//     [JsonProperty("has_more_page")]
//     public bool HasMorePage { get; set; }
//
//     [JsonProperty("applied_filter")]
//     public string AppliedFilter { get; set; }
//
//     [JsonProperty("sort_column")]
//     public string SortColumn { get; set; }
//
//     [JsonProperty("sort_order")]
//     public string SortOrder { get; set; }
//   }
//
//
//   internal class CustomerResponse : Response
//   {
//     [JsonProperty("Customer")]
//     public Customer Entity { get; set; }
//   }
//
//   internal class PlanResponse : Response
//   {
//     [JsonProperty("plans")]
//     public List<Plan> Entities { get; set; }
//
//     [JsonProperty("page_context")]
//     public PageContext PageContext { get; set; }
//   }
//
//   // internal class SubscriptionResponse : Response
//   // {
//   //   [JsonProperty("subscription")]
//   //   public Subscription Entity { get; set; }
//   // }
//
//   internal class SubscriberResponse : Response
//   {
//     [JsonProperty("list_of_details")]
//     public List<Subscriber> Entities { get; set; }
//   }
//
//   public class FromEmail
//   {
//     [JsonProperty("is_org_email_id")]
//     public bool IsOrgEmailId { get; set; }
//
//     [JsonProperty("user_name")]
//     public string UserName { get; set; }
//
//     [JsonProperty("organization_contact_id")]
//     public string OrganizationContactId { get; set; }
//
//     [JsonProperty("email")]
//     public string Email { get; set; }
//
//     [JsonProperty("selected")]
//     public bool Selected { get; set; }
//   }
//
//   public class ToContact
//   {
//     [JsonProperty("phone")]
//     public string Phone { get; set; }
//
//     [JsonProperty("mobile")]
//     public string Mobile { get; set; }
//
//     [JsonProperty("last_name")]
//     public string LastName { get; set; }
//
//     [JsonProperty("contact_person_id")]
//     public string ContactPersonId { get; set; }
//
//     [JsonProperty("salutation")]
//     public string Salutation { get; set; }
//
//     [JsonProperty("first_name")]
//     public string FirstName { get; set; }
//
//     [JsonProperty("email")]
//     public string Email { get; set; }
//
//     [JsonProperty("selected")]
//     public bool Selected { get; set; }
//   }
//
//   internal class RequestPaymentResponse : Response
//   {
//     [JsonProperty("data")]
//     public Card Entity { get; set; }
//   }
//
//   internal class CardsResponse : Response
//   {
//     [JsonProperty("cards")]
//     public List<Card> Entities { get; set; }
//
//     [JsonProperty("page_context")]
//     public PageContext PageContext { get; set; }
//   }
//
//   internal class ChargeResponse : Response
//   {
//     [JsonProperty("invoice")]
//     public Invoice Entity { get; set; }
//   }
// }