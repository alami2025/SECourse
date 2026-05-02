using FraudMonitoring;

// Minimal demo wiring of the "Receive Transaction" feature.
// In the real system the evaluator would run the actual risk checks.
class DemoEvaluator : IFraudRiskEvaluator
{
    public void Evaluate(Transaction t) =>
        Console.WriteLine($"Evaluating {t.TransactionId} ({t.Amount} {t.Currency}).");
}

var receiver = new TransactionReceiver(new DemoEvaluator());

var tx = new Transaction(
    transactionId: "T-1001",
    cardNumber:    "4111111111111111",
    amount:        249.50m,
    currency:      "EUR",
    merchantId:    "M-42",
    country:       "DK",
    timestamp:     DateTime.UtcNow);

receiver.Receive(tx);
