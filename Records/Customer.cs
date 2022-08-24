#if NET6_0_OR_GREATER
using System;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Records
{
    public record Customer(
        string customer_id,
        string display_name,
        string salutation,
        string first_name,
        string last_name,
        string email,
        string phone,
        string mobile,
        string website,
        CustomFields[] custom_fields
        
        //     {
        //     "display_name": "Bowman Furniture",
        //     "salutation": "Mr.",
        //     "first_name": "Benjamin",
        //     "last_name": "George",
        //     "email": "benjamin.george@bowmanfurniture.com",
        //     "tags": [
        //     {
        //         "tag_id": "460000000054182",
        //         "tag_option_id": "460000000054280"
        //     }
        //     ],
        //     "company_name": "Bowman Furniture",
        //     "phone": 23467278,
        //     "mobile": 938237475,
        //     "department": "Marketing",
        //     "designation": "Evangelist",
        //     "website": "www.bowmanfurniture.com",
        //     "billing_address": {
        //         "attention": "Benjamin George",
        //         "street": "Harrington Bay Street",
        //         "city": "Salt Lake City",
        //         "state": "CA",
        //         "zip": 92612,
        //         "country": "U.S.A",
        //         "state_code": "CA",
        //         "fax": 4527389
        //     },
        //     "shipping_address": {
        //         "attention": "Benjamin George",
        //         "street": "Harrington Bay Street",
        //         "city": "Salt Lake City",
        //         "state": "CA",
        //         "zip": 92612,
        //         "country": "U.S.A",
        //         "state_code": "CA",
        //         "fax": 4527389
        //     },
        //     "payment_terms": 5,
        //     "payment_terms_label": "Due on receipt",
        //     "currency_code": "USD",
        //     "ach_supported": true,
        //     "twitter": "BowmanFurniture",
        //     "facebook": "BowmanFurniture",
        //     "skype": "Bowman Furniture",
        //     "notes": "Bowman Furniture",
        //     "is_portal_enabled": true,
        //     "gst_no": "33AAAAA0000A1Z5",
        //     "gst_treatment": "business_gst",
        //     "place_of_contact": "TN",
        //     "vat_treatment": "overseas",
        //     "vat_reg_no": 51423456782,
        //     "is_taxable": true,
        //     "tax_id": "903000002345",
        //     "tax_authority_id": null,
        //     "tax_authority_name": "ATO",
        //     "tax_exemption_id": "903000006345",
        //     "tax_exemption_code": "GST FREE",
        //     "default_templates": {
        //         "invoice_template_id": "90300000311340",
        //         "creditnote_template_id": "90300000232140"
        //     },
        //     "custom_fields": [
        //     {
        //         "label": "label",
        //         "value": 129890
        //     }
        //     ]
        // }
        
        );
}
#endif
