using System;

namespace Quera {
    public static class Utility {
        public static TResult ConvertTo<TSource, TResult>(this TSource source) =>
            (TResult) Convert.ChangeType(source, typeof(TResult)) ??
            throw new Exception($"Can not convert to {typeof(TResult)}");
    }
}