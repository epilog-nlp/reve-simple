/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Models
{
    /// <summary>
    /// Represents the Result of an operation.
    /// </summary>
    /// <typeparam name="TPayload">The payload returned from the operation.</typeparam>
    public class Result<TPayload>
    {
        private Result(bool isError, TPayload payload, string message = default)
        {
            IsError = isError;
            Payload = payload;
            Message = message;
        }

        /// <summary>
        /// Indicates the <see cref="Result{TPayload}"/> is an Error.
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// An optional message providing additional information about the operation result.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The payload returned from the operation.
        /// </summary>
        public TPayload Payload { get; }

        /// <summary>
        /// Creates a Success Result.
        /// </summary>
        /// <param name="payload">The payload returned from the operation.</param>
        /// <param name="message">The optional message to attach to the Result.</param>
        /// <returns>A Success Result.</returns>
        public static Result<TPayload> Success(TPayload payload = default, string message = default)
            => new Result<TPayload>(false, payload, message);

        /// <summary>
        /// Creates an Error Result.
        /// </summary>
        /// <param name="payload">The payload returned from the operation.</param>
        /// <param name="message">The optional message to attach to the Result.</param>
        /// <returns>An Error Result.</returns>
        public static Result<TPayload> Error(TPayload payload = default, string message = default)
            => new Result<TPayload>(true, payload, message);
    }
}
