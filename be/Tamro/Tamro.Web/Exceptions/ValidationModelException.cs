using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Tamro.Web.Exceptions
{
    public class ValidationModelException : Exception
    {
        public ValidationModelException(ModelStateDictionary modelState) : base(ErrorCodes.ValidationFailed)
        {
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x =>
                    new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public List<ValidationError> Errors { get; }

        public string ErrorsToString()
        {
            return String.Join("; ", Errors.Select(x => $"{x.Field}:{x.Message}"));
        }
    }

    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }
    }

    public static class ErrorCodes
    {
        public const string RequiredField = "required_field";
        public const string ValidationFailed = "validation_failed";
    }
}
