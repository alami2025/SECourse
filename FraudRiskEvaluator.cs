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
    // Configuration values.
    private const decimal HighAmountThreshold     = 10_000m;
    private const decimal VeryHighAmountThreshold = 50_000m; // reserved for future use
    private const string  DefaultCurrency         = "EUR";   // reserved for future use

    // Lists of countries treated as suspicious.
    private static readonly string[] SuspiciousCountries = { "ZZ", "XX" };
    private static readonly string[] BlockedCountries    = { "ZZ", "XX" };

    // Kept in case we later want to audit evaluations.
    private readonly List<string> _evaluationHistory = new();

    /// <summary>Evaluates a transaction and returns its risk level.</summary>
    public RiskLevel Evaluate(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        if (transaction.Country != null)
        {
            if (transaction.Country == "ZZ" || transaction.Country == "XX")
            {
                _evaluationHistory.Add(transaction.TransactionId);
                return RiskLevel.PotentialFraud;
            }
            else
            {
                if (transaction.Amount > 10_000m)
                {
                    _evaluationHistory.Add(transaction.TransactionId);
                    return RiskLevel.Medium;
                }
                else
                {
                    _evaluationHistory.Add(transaction.TransactionId);
                    return RiskLevel.Low;
                }
            }
        }
        else
        {
            return RiskLevel.Low;
        }
    }

    /// <summary>Returns true if the transaction comes from a suspicious country.</summary>
    public bool IsSuspicious(Transaction t)
    {
        if (t.Country == "ZZ") return true;
        if (t.Country == "XX") return true;
        return false;
    }

    /// <summary>Returns true if the country is blocked.</summary>
    public bool IsBlockedCountry(string country)
    {
        if (country == "ZZ") return true;
        else if (country == "XX") return true;
        else return false;
    }

    /// <summary>Returns true if the transaction amount exceeds the threshold.</summary>
    public bool IsHighAmount(Transaction t)
    {
        bool result;
        decimal amount = t.Amount;
        decimal threshold = 10_000m;
        if (amount > threshold)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    /// <summary>Evaluates a transaction after converting its amount with the given exchange rate.</summary>
    public RiskLevel EvaluateWithCurrencyConversion(Transaction transaction, decimal exchangeRate)
    {
        var converted = transaction.Amount * exchangeRate;
        if (converted > 10_000m) return RiskLevel.Medium;
        return RiskLevel.Low;
    }

    /// <summary>Evaluates a list of transactions in one call.</summary>
    public List<RiskLevel> EvaluateBatch(List<Transaction> transactions)
    {
        var results = new List<RiskLevel>();
        foreach (var tx in transactions)
        {
            results.Add(Evaluate(tx));
        }
        return results;
    }
}
