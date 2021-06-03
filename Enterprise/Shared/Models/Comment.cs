using System;
using Infrastructure.Enterprise.Abstractions.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Shared.Models
{
  public class Comment : Model
  {
    [JsonProperty("comment_id")]
    public string CommentId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("commented_by_id")]
    public string CommentedById { get; set; }

    [JsonProperty("comment_type")]
    public string CommentType { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("operation_type")]
    public string OperationType { get; set; }

    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; }

    [JsonProperty("transaction_type")]
    public string TransactionType { get; set; }

  }
}