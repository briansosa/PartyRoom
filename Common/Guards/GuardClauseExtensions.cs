using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Guards
{
    /// <summary>
    /// A collection of common guard clauses, implented as extensions.
    /// </summary>
    /// <example>
    /// Guard.Against.Null(input, nameof(input));
    /// </example>
    public static class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an emtpy GUID or null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void EmptyOrNullGuid(this IGuardClause guardClause, Guid input, string parameterName)
        {
            if (Guid.Empty == input)
            {
                throw new ArgumentException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an emtpy DateTime or null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void EmptyOrNullDateTime(this IGuardClause guardClause, DateTime input, string parameterName)
        {
            if (DateTime.MinValue == input)
            {
                throw new ArgumentException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Null(this IGuardClause guardClause, object input, string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is an empty or white space string.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NullOrWhiteSpace(this IGuardClause guardClause, string input, string parameterName)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" /> or greater than <see cref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, int input, string parameterName, int rangeFrom, int rangeTo)
        {
            OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException" /> if <see cref="input" /> is less than <see cref="rangeFrom" /> or greater than <see cref="rangeTo" />.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void OutOfRange(this IGuardClause guardClause, decimal input, string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(this IGuardClause guardClause, int input, string parameterName)
        {
            Zero<int>(guardClause, input, parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(this IGuardClause guardClause, decimal input, string parameterName)
        {
            Zero<decimal>(guardClause, input, parameterName);
        }

        private static void OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0)
            {
                throw new ArgumentOutOfRangeException($"Input {parameterName} was out of range", parameterName);
            }
        }

        public static void Higher(this IGuardClause guardClause, DateTime input, DateTime otherInput, string paramNameInput, string paramNameOther)
        {
            if (DateTime.Compare(input, otherInput) <= 0)
            {
                throw new ArgumentException($"{paramNameInput} should be higher than {paramNameOther}");
            }
        }
        public static void HigherOrEquals(this IGuardClause guardClause, DateTime input, DateTime otherInput, string paramNameInput, string paramNameOther)
        {
            if (DateTime.Compare(input, otherInput) < 0)
            {
                throw new ArgumentException($"{paramNameInput} should be higher or equals than {paramNameOther}");
            }
        }

        public static void Higher<T>(this IGuardClause guardClause, T input, T otherInput, string paramNameInput, string paramNameOther)
        {
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(input, otherInput) <= 0)
            {
                throw new ArgumentException($"{paramNameInput} should be higher than {paramNameOther}");
            }
        }
        public static void HigherOrEquals<T>(this IGuardClause guardClause, T input, T otherInput, string paramNameInput, string paramNameOther)
        {
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(input, otherInput) < 0)
            {
                throw new ArgumentException($"{paramNameInput} should be higher or equals than {paramNameOther}");
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if <see cref="input" /> is zero.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void Zero<T>(this IGuardClause guardClause, T input, string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);
            }
        }
    }
}
