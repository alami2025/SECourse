namespace FraudMonitoring;

/// <summary>
/// Receives incoming transactions, validates them, and forwards
/// each valid transaction to the fraud risk evaluator.
/// </summary>
public class TransactionReceiver : ITransactionReceiver
{
    private readonly IFraudRiskEvaluator _evaluator;

    public TransactionReceiver(IFraudRiskEvaluator evaluator)
    {
        _evaluator = evaluator;
    }

    public void Receive(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        Validate(transaction);

        // Hand off to the next step in the pipeline (Evaluate fraud risk).
        _evaluator.Evaluate(transaction);
    }

    private static void Validate(Transaction t)
    {
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
