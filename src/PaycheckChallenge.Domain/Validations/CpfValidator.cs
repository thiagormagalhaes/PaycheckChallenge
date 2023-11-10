using Caelum.Stella.CSharp.Validation;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Validations;
internal class CpfValidator : ICpfValidator
{
    public bool IsValid(string document)
        => new CPFValidator().IsValid(document);
}
