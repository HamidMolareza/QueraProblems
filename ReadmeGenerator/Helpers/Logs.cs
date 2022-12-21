using System;
using System.Text;
using OnRail.ResultDetails;

namespace Quera.Helpers;

public static class Logs {
    public static void Log(this ResultDetail resultDetail) {
        Console.WriteLine(resultDetail.GetHeaderOfError());

        if (resultDetail is ErrorDetail error) {
            if (error.Exception is not null)
                Console.WriteLine(error.Exception.ToString());
            Console.WriteLine(error.StackTrace);
        }

        if (resultDetail.MoreDetails is not null) {
            foreach (var moreDetail in resultDetail.MoreDetails)
                Console.WriteLine(moreDetail);
        }
    }

    public static string GetHeaderOfError(this ResultDetail resultDetail) {
        var result = new StringBuilder();
        if (resultDetail.StatusCode is not null)
            result.Append($"{resultDetail.StatusCode}-");

        result.Append($"{resultDetail.Title}: {resultDetail.Message}");

        return result.ToString();
    }
}