using System.Collections.Generic;

namespace DealMeCore.Validation
{
    /// <summary>
    /// IValidationContext.
    /// </summary>
    public interface IValidationContext
    {
        /// <summary>
        /// Current validation state.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessage">The error message.</param>
        void AddError(string key, string errorMessage);

        /// <summary>
        /// Gets the complete list of errors.
        /// </summary>
        /// <returns>Dictionary with the list of errors (key - error key; value - list of related errors).</returns>
        IDictionary<string, IList<string>> GetErrors();
    }
}