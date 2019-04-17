using System.Collections.Generic;
using System.Linq;

namespace DealMeCore.Validation
{
    /// <summary>
    /// Validation context for model in scope of current request.
    /// </summary>
    public class ValidationContext : IValidationContext
    {
        private readonly IDictionary<string, IList<string>> errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationContext"/> class.
        /// </summary>
        public ValidationContext()
        {
            errors = new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// Returns current validation state.
        /// </summary>
        public bool IsValid => !errors.Any();

        /// <summary>
        /// Includes validation error to the context.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessage">The error message.</param>
        public void AddError(string key, string errorMessage)
        {
            if (errors.TryGetValue(key, out IList<string> existingErrors))
            {
                existingErrors.Add(errorMessage);
            }
            else
            {
                errors.Add(key, new List<string>() { errorMessage });
            }
        }

        /// <summary>
        /// Gets the complete list of errors.
        /// </summary>
        /// <returns>Dictionary with the list of errors (key - error key; value - list of related errors).</returns>
        public IDictionary<string, IList<string>> GetErrors()
        {
            return errors;
        }
    }
}
