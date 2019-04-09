/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Models
{
    /// <summary>
    /// Represents all the available types of Validation Rules.
    /// </summary>
    public enum RuleType
    {
        /// <summary>
        /// An unspecified RuleType. This should never occur.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// A Rule for validating a string is a properly formatted Email Address.
        /// </summary>
        EmailAddress,

        /// <summary>
        /// A Rule for validating a string is a properly formatted URL.
        /// </summary>
        Url,

        /// <summary>
        /// A Rule for validating a string is a properly formatted Credit Card Number.
        /// </summary>
        CreditCard,

        /// <summary>
        /// A Rule for validating an input is a valid Enum Field.
        /// </summary>
        EnumDataType,

        /// <summary>
        /// A Rule for validating an input is a valid Phone Number
        /// </summary>
        Phone,

        /// <summary>
        /// A Rule for validating a file name has one of the accepted File Extensions.
        /// </summary>
        FileExtensions,

        /// <summary>
        /// A Rule for validating an input is within a specified Range.
        /// </summary>
        Range,

        /// <summary>
        /// A Rule for validating an input is within a specified Maximum Length.
        /// </summary>
        MaxLength,

        /// <summary>
        /// A Rule for validating an input is within a specified Minimum Length.
        /// </summary>
        MinLength,

        /// <summary>
        /// A Rule for validating an input is not null
        /// </summary>
        Required,

        /// <summary>
        /// A Rule for validating an input is a string within the specified Minimum and Maximum Length.
        /// </summary>
        StringLength,

        /// <summary>
        /// A Rule for validating a string is a complete <see cref="System.Text.RegularExpressions.Regex.Match(string)"/> for the provided Regular Expression.
        /// </summary>
        RegularExpression,

        /// <summary>
        /// Alias for <see cref="RegularExpression"/>
        /// </summary>
        Regex = RegularExpression,

        /// <summary>
        /// A Custom Rule that falls outside the scope of the Framework.
        /// </summary>
        Custom
    }
}
