/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Configuration
{
    /// <summary>
    /// Contract for a type that can provide a generic Configuration Model.
    /// </summary>
    /// <typeparam name="TResult">The Configuration Model.</typeparam>
    public interface IConfigProvider<TResult>
    {
        /// <summary>
        /// The generic Configuration Model.
        /// </summary>
        TResult Configuration { get; }
    }
}
