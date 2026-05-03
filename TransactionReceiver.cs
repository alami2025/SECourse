namespace FraudMonitoring;

/// <summary>
/// A card transaction received from the bank's processing system.
/// </summary>
public class Transaction
{
    public string TransactionId { get; set; } = string.Empty;
    public string CardNumber    { get; set; } = string.Empty;
    public decimal Amount       { get; set; }
    public string Currency      { get; set; } = string.Empty;
    public string MerchantId    { get; set; } = string.Empty;
    public string Country       { get; set; } = string.Empty;
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

    /// <summary>Read-only view of stored transactions, exposed for verification.</summary>
    public IReadOnlyList<Transaction> Transactions => _store.AsReadOnly();

    /// <summary>Receives a transaction, validates it, then stores it.</summary>
    public void Receive(Transaction transaction)
    {
        Validate(transaction);
        _store.Add(transaction);
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
}
