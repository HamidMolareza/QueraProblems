using System;
using System.IO;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.Try;

namespace Quera.Helpers;

public static class Utility {
    public static TResult ConvertTo<TSource, TResult>(this TSource source) =>
        (TResult)Convert.ChangeType(source, typeof(TResult))! ??
        throw new Exception($"Can not convert to {typeof(TResult)}");

    public static Task<Result> SaveDataAsync(string path, string data, int numOfTry) =>
        TryExtensions.Try(() => File.WriteAllTextAsync(path, data), numOfTry)
            .OnFailAddMoreDetails(new { path });
}