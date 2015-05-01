namespace Validator.App
{
    using System.Collections.Generic;

    /// <summary>
    /// Validator service contract. Only built-in types
    /// </summary>
    public interface IValidatorContract
    {
        IEnumerable<string> Validate(IEnumerable<IDictionary<string, string>> rawDataItems);
    }
}
