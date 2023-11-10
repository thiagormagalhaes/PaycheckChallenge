using System.ComponentModel;

namespace PaycheckChallenge.Domain.Enums;

public enum TransactionType : int
{
    [Description("Desconto")]
    Discount,
    [Description("Remuneração")]
    Compensation
}
