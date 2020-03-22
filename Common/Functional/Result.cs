using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Functional
{
    public class Result : IEquatable<Result>
    {
        protected Result(bool isSuccess, string errorMessage)
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("Cannot set a result as successful WITH an error message");

            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("Cannot set a result as failed WITHOUT an error message");

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public bool IsFailure => !IsSuccess;
        public bool IsSuccess { get; set; }

        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result<T> FailWithDefaultReturnValue<T>(string errorMessage)
        {
            return new Result<T>(default(T), false, errorMessage);
        }

        public static bool operator !=(Result result, Result value)
        {
            return !(result == value);
        }

        public static bool operator ==(Result result, Result value)
        {
            return result.Equals(value);
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> SuccessWithReturnValue<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Result)
            {
                var other = (Result)obj;
                return Equals(other);
            }
            else
                return false;
        }

        public bool Equals(Result other)
        {
            var result = ErrorMessage == other.ErrorMessage && IsSuccess == other.IsSuccess;
            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = 44554417;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ErrorMessage);
            hashCode = hashCode * -1521134295 + IsFailure.GetHashCode();
            hashCode = hashCode * -1521134295 + IsSuccess.GetHashCode();
            return hashCode;
        }
    }

    public class Result<T> : Result
    {
        private T value;

        public override int GetHashCode()
        {
            return base.GetHashCode() * value?.GetHashCode() ?? 1;
        }

        protected internal Result(T value, bool isSucess, string errorMessage) : base(isSucess, errorMessage)
        {
            if (isSucess && value == null)
                throw new InvalidOperationException("Cannot set a result with value null and flag it as success");

            this.value = value;
        }

        public T Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("There is no value to access on a failed Result");

                return value;
            }
        }
    }
}
