using System.Text;
using OnRail;
using OnRail.ResultDetails;

namespace Quera.Helpers;

public static class ResultHelpers {
    public static string ToStr(this Result result) {
        var sb = new StringBuilder();
        sb.AppendLine($"Success: {result.IsSuccess}");

        if (result.Detail is null) {
            sb.AppendLine($"Detail: No more data");
        }
        else {
            sb.AppendLine("Detail:")
                .AppendLine(result.Detail.GetHeader())
                .AppendLine(result.Detail.GetDetail());
        }

        return sb.ToString();
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