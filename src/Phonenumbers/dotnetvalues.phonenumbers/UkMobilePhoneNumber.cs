using System;
using System.Text.RegularExpressions;

namespace DotNetValues.Phonenumbers
{
    [Serializable]
    public struct UkMobilePhoneNumber : IEquatable<UkMobilePhoneNumber>
    {
        public string InternationalDialingCode;
        public string CountryCode;

        private readonly string _value;

        public UkMobilePhoneNumber(string phoneNumber)
        {
            InternationalDialingCode = "44";
            CountryCode = "UK";
            
            if (!IsValidNumber(phoneNumber))
                throw new InvalidUkMobilePhoneNumberException(phoneNumber);

            _value = Regex.Match(phoneNumber, @"(^\+44|^0)(?<number>7\d{9})$").Groups["number"].ToString();
        }

        public static bool IsValidNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"(^\+447|^07)\d{9}$");
        }

        public string Value
        {
            get
            {
                return string.IsNullOrEmpty(_value) ? string.Empty : string.Format("0{0}", _value);
            }
        }

        public string WithInternationalDialingCode()
        {
            return string.Format("+44{0}", _value);
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UkMobilePhoneNumber && Equals((UkMobilePhoneNumber) obj);
        }

        public bool Equals(UkMobilePhoneNumber other)
        {
            return other._value == _value;
        }

        public static bool operator ==(UkMobilePhoneNumber left, UkMobilePhoneNumber right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(UkMobilePhoneNumber left, UkMobilePhoneNumber right)
        {
            return !(left == right);
        }

        public class InvalidUkMobilePhoneNumberException : Exception
        {
            public InvalidUkMobilePhoneNumberException(string phoneNumber)
                : base(string.Format("{0} is not a valid Uk Mobile Phone Number", phoneNumber))
            {
            }
        }
    }
}