using System;
using OnRail.ResultDetails;

namespace Quera.ErrorDetails {
    public class ProblemDirectoryIsEmptyError : ErrorDetail {
        public ProblemDirectoryIsEmptyError(string? title = nameof(ProblemDirectoryIsEmptyError),
            string? message = null, int? statusCode = null, Exception? exception = null, object? moreDetails = null) :
            base(title, message, statusCode, exception, moreDetails) { }
    }
}