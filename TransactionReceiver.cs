namespace FraudMonitoring;

/// <summary>
/// A card transaction received from the bank's processing system.
/// </summary>
public class Transaction
{
    public string TransactionId { get; set; } = "";
    public string CardNumber    { get; set; } = "";
    public decimal Amount       { get; set; }
    public string Currency      { get; set; } = "";
    public string MerchantId    { get; set; } = "";
    public string Country       { get; set; } = "";
    public DateTime Timestamp   { get; set; }
}

/// <summary>
/// Entry point of the Fraud Monitoring System: accepts incoming
/// transactions, validates them, and stores them for risk evaluation.
/// </summary>
public class TransactionReceiver
{
    // In-memory store of accepted transactions.
    private readonly List<Transaction> _store = new();

    /// <summary>Receives a transaction, validates it, then stores it.</summary>
    public void Receive(Transaction transaction)
    {
        Validate(transaction);
        Store(transaction);
    }

    /// <summary>Checks that all required fields are present and valid.</summary>
    private void Validate(Transaction t)
    {
        if (t == null)
            throw new ArgumentNullException(nameof(t));
        if (string.IsNullOrWhiteSpace(t.TransactionId))
            throw new ArgumentException("TransactionId is required.");
        if (string.IsNullOrWhiteSpace(t.CardNumber))
            throw new ArgumentException("CardNumber is required.");
        if (t.Amount <= 0)
            throw new ArgumentException("Amount must be positive.");
        if (string.IsNullOrWhiteSpace(t.Currency))
            throw new ArgumentException("Currency is required.");
    }

    /// <summary>Persists the transaction so it can be evaluated for fraud.</summary>
    private void Store(Transaction t)
    {
        _store.Add(t);
    }
}
