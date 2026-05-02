namespace FraudMonitoring;

/// <summary>
/// Risk level assigned to a transaction after fraud evaluation.
/// </summary>
public enum RiskLevel
{
    Low,
    Medium,
    PotentialFraud
}

/// <summary>
/// Evaluates the fraud risk of a transaction using simple rules:
/// transaction amount threshold and country of origin.
/// </summary>
public class FraudRiskEvaluator
{
    // Amount above this threshold is considered high-value and risky.
    private const decimal HighAmountThreshold = 10_000m;

    // Fictitious countries treated as suspicious.
    private static readonly string[] SuspiciousCountries = { "ZZ", "XX" };

    /// <summary>Evaluates a transaction and returns its risk level.</summary>
    public RiskLevel Evaluate(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        if (IsSuspiciousLocation(transaction))
            return RiskLevel.PotentialFraud;

        if (IsHighAmount(transaction))
            return RiskLevel.Medium;

        return RiskLevel.Low;
    }

    /// <summary>Returns true if the transaction comes from a suspicious country.</summary>
    private bool IsSuspiciousLocation(Transaction t)
    {
        return SuspiciousCountries.Contains(t.Country);
    }

    /// <summary>Returns true if the transaction amount exceeds the threshold.</summary>
    private bool IsHighAmount(Transaction t)
    {
        return t.Amount > HighAmountThreshold;
    }
}
