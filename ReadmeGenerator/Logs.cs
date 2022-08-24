using System;
using OnRail.ResultDetails;

namespace Quera {
    public static class Logs {
        public static void Log(this ResultDetail resultDetail) {
            Console.Write(resultDetail.StatusCode is not null ? $"{resultDetail.StatusCode}-": string.Empty);
            Console.WriteLine($"{resultDetail.Title}: {resultDetail.Message}");

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
    }
}