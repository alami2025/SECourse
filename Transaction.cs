namespace FraudMonitoring;

/// <summary>
/// Represents a single card transaction received from the bank's
/// transaction processing system.
/// </summary>
public class Transaction
{
    public string TransactionId { get; }
    public string CardNumber { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string MerchantId { get; }
    public string Country { get; }
    public DateTime Timestamp { get; }

    public Transaction(
        string transactionId,
        string cardNumber,
        decimal amount,
        string currency,
        string merchantId,
        string country,
        DateTime timestamp)
    {
        TransactionId = transactionId;
        CardNumber = cardNumber;
        Amount = amount;
        Currency = currency;
        MerchantId = merchantId;
        Country = country;
        Timestamp = timestamp;
    }
}
