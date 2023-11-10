namespace PaycheckChallenge.Domain.Interfaces;
public interface ICpfValidator
{
    bool IsValid(string document);
}
