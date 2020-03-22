using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Functional
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly MaybeValueWrapper value;

        private Maybe(T value)
        {
            this.value = value == null ? null : new MaybeValueWrapper(value);
        }

        public static Maybe<T> None => new Maybe<T>();

        public bool HasNoValue => !HasValue;

        public bool HasValue => value != null;

        public T Value
        {
            get
            {
                if (HasNoValue)
                    throw new InvalidOperationException();

                return value.Value;
            }
        }

        public static Maybe<T> From(T obj)
        {
            return new Maybe<T>(obj);
        }

        public static implicit operator Maybe<T>(T value)
        {
            if (value?.GetType() == typeof(Maybe<T>))
            {
                return (Maybe<T>)(object)value;
            }

            return new Maybe<T>(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (value is Maybe<T>)
                return maybe.Equals(value);

            if (maybe.HasNoValue)
                return false;

            return maybe.Value.Equals(value);
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Maybe<T>))
            {
                if (obj is T)
                {
                    obj = new Maybe<T>((T)obj);
                }

                if (!(obj is Maybe<T>))
                    return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
                return true;

            if (HasNoValue || other.HasNoValue)
                return false;

            return value.Value.Equals(other.value.Value);
        }

        public override int GetHashCode()
        {
            if (HasNoValue)
                return 0;

            return value.Value.GetHashCode();
        }

        public override string ToString()
        {
            if (HasNoValue)
                return "No value";

            return Value.ToString();
        }

        private class MaybeValueWrapper
        {
            internal readonly T Value;

            public MaybeValueWrapper(T value)
            {
                Value = value;
            }
        }
    }
}
