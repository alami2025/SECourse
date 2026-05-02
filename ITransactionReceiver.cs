namespace FraudMonitoring;

/// <summary>
/// Entry point of the Fraud Monitoring System. Accepts transactions
/// arriving from the Bank Transaction Processing System.
/// </summary>
public interface ITransactionReceiver
{
    void Receive(Transaction transaction);
}
