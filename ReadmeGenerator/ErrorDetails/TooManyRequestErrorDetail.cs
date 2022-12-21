using System;
using Microsoft.AspNetCore.Http;
using OnRail.ResultDetails;

namespace Quera.ErrorDetails;

public class TooManyRequestErrorDetail : ErrorDetail {
    public TooManyRequestErrorDetail(string? title = nameof(TooManyRequestErrorDetail), string? message = null,
        int? statusCode = StatusCodes.Status429TooManyRequests, Exception? exception = null,
        object? moreDetails = null) : base(title ?? nameof(TooManyRequestErrorDetail),
        message, statusCode, exception, moreDetails) { }
}