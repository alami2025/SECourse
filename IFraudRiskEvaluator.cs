namespace FraudMonitoring;

/// <summary>
/// Downstream component that scores a received transaction for fraud risk.
/// The receiver depends only on this abstraction.
/// </summary>
public interface IFraudRiskEvaluator
{
    void Evaluate(Transaction transaction);
}
