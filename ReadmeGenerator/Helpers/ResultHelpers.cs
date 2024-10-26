using System.Text;
using OnRail;
using OnRail.ResultDetails;

namespace Quera.Helpers;

public static class ResultHelpers {
    public static string ToStr(this Result result) {
        var sb = new StringBuilder();

        sb.AppendLine($"Success: {result.IsSuccess}")
            .AppendLine(result.Detail.ToStr());

        return sb.ToString();
    }

    public static string ToStr<T>(this Result<T> result) {
        var sb = new StringBuilder();

        sb.AppendLine($"Success: {result.IsSuccess}")
            .AppendLine($"Value: {result.Value}")
            .AppendLine(result.Detail.ToStr());

        return sb.ToString();
    }

    public static string ToStr(this ResultDetail? detail) {
        var sb = new StringBuilder();

        if (detail is null) {
            sb.AppendLine("Detail: No more data");
        }
        else {
            sb.AppendLine("Detail:")
                .AppendLine(detail.GetHeader())
                .AppendLine(detail.GetDetail());
        }

        return sb.ToString();
    }

    public static Result CombineResults(Result result1, Result result2) {
        if (result1.IsSuccess && result2.IsSuccess) return Result.Ok();
        if (!result1.IsSuccess && !result2.IsSuccess) {
            var errorDetail = new ErrorDetail(title: "Two tasks failed!",
                message: Utility.CombineStrings(result1.Detail?.Message!, result2.Detail?.Message),
                moreDetails: new { result1 = result1.Detail, result2 = result2.Detail }
            );
            return Result.Fail(errorDetail);
        }

        // One of them failed.
        return !result1.IsSuccess ? result1 : result2;
    }

    private static string GetHeader(this ResultDetail resultDetail) {
        var result = new StringBuilder();
        if (resultDetail.StatusCode is not null)
            result.Append($"{resultDetail.StatusCode}-");

        result.Append($"{resultDetail.Title}: {resultDetail.Message ?? "No message"}");

        return result.ToString();
    }

    private static string GetDetail(this ResultDetail resultDetail) {
        var sb = new StringBuilder();
        if (resultDetail is ErrorDetail error) {
            if (error.Exception is not null)
                sb.AppendLine(error.Exception.ToString());
            sb.AppendLine(error.StackTrace.ToString());
        }

        if (resultDetail.MoreDetails is not null) {
            sb.AppendLine("MoreDetails:");
            foreach (var moreDetail in resultDetail.MoreDetails)
                sb.AppendLine($"\t{moreDetail}");
        }

        return sb.ToString();
    }
}