/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Runtime.Serialization;

namespace REvE.Configuration
{
    /// <summary>
    /// Exception thrown when the Framework fails to resolve the Configuration or Configuration Provider.
    /// </summary>
    public class ConfigurationResolutionException : Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConfigurationResolutionException()
        {
        }

        /// <summary>
        /// Simple constructor for adding custom message to the exception.
        /// </summary>
        /// <param name="message">The custom error message.</param>
        public ConfigurationResolutionException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Simple constructor for adding custom message to the exception and capturing an inner exception.
        /// </summary>
        /// <param name="message">The custom error message.</param>
        /// <param name="innerException">The inner exception caught before converting to <see cref="ConfigurationResolutionException"/></param>
        public ConfigurationResolutionException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationResolutionException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ConfigurationResolutionException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { 
        }
    }
}
