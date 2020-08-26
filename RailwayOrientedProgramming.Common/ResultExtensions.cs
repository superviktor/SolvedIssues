using System;

namespace RailwayOrientedProgramming.Common
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this T obj, string errorMessage) where T : class
        {
            return obj == null ? Result.Fail<T>(errorMessage) : Result.Success<T>(obj);
        }
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
                return result;

            action();

            return Result.Success();
        }
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            return result.IsFailure ? result : func();
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }

        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
