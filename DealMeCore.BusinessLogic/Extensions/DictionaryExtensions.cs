using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealMeCore.BusinessLogic.Models;

namespace DealMeCore.BusinessLogic.Extensions
{
    /// <summary>
    /// Extension methods for Dictionary instances. 
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Generates a list of error details in required format.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Error message details.</returns>
        public static IList<FieldErrorDetailDto> GenerateErrorMessageDetails(this IDictionary<string, IList<string>> target)
        {
            var errorMessages = new List<FieldErrorDetailDto>();

            foreach (KeyValuePair<string, IList<string>> keyModelStatePair in target)
            {
                string key = keyModelStatePair.Key;

                IList<string> errors = keyModelStatePair.Value;

                if (errors != null && errors.Any())
                {
                    errorMessages
                        .AddRange(
                            errors
                                .Select(
                                    e => new FieldErrorDetailDto()
                                    {
                                        Field = GetFieldNameByKey(key),
                                        Message = e
                                    })
                                .ToArray()
                        );
                }
            }

            return errorMessages;
        }

        private static string GetFieldNameByKey(string fieldKey)
        {
            int dotIndex = fieldKey.IndexOf('.');

            return dotIndex < 0 ? fieldKey : fieldKey.Substring(dotIndex + 1);
        }
    }
}
