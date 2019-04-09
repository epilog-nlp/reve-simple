/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.ComponentModel.Composition;

namespace REvE.Configuration
{
    using static ConfigUtil;

    /// <summary>
    /// Class for resolving a Configuration Model from a data source.
    /// </summary>
    /// <typeparam name="TResult">The Configuration Model.</typeparam>
    [InheritedExport(typeof(IConfigProvider<>))]
    public abstract class ConfigProvider<TResult> : IConfigProvider<TResult>
    {
        /// <summary>
        /// Basic constructor for specifying the application configuration key containing the data source location.
        /// </summary>
        /// <param name="dataSourceKey">Application configuration key containing the data source location.</param>
        protected ConfigProvider(string dataSourceKey)
        {
            DataSourceKey = dataSourceKey;
            Initialize();
        }

        /// <summary>
        /// Application configuration key containing the data source location.
        /// </summary>
        protected virtual string DataSourceKey { get; }

        /// <summary>
        /// The configuration value associated with <see cref="ConfigProvider{TResult}.DataSourceKey"/>.
        /// </summary>
        protected virtual string DataSource => AppSetting<string>(DataSourceKey);

        /// <summary>
        /// The deserialized configuration model.
        /// </summary>
        public TResult Configuration { get; protected set; }

        /// <summary>
        /// Method for establishing connection to the data source described by <see cref="ConfigProvider{TResult}.DataSource"/>.
        /// Called in <see cref="ConfigProvider{TResult}.Initialize"/>.
        /// </summary>
        /// <param name="source">Data Source provided by <see cref="ConfigProvider{TResult}.DataSource"/>.</param>
        protected abstract void OpenConnection(string source);

        /// <summary>
        /// Method for retrieving and deserializing the <typeparamref name="TResult"/> from an open connection to the data source.
        /// </summary>
        /// <returns>The deserialized result.</returns>
        protected abstract TResult Extract();

        /// <summary>
        /// Method for closing the connection opened by <see cref="ConfigProvider{TResult}.OpenConnection(string)"/>.
        /// </summary>
        protected abstract void CloseConnection();

        /// <summary>
        /// Handles all operations necessary for retrieving <see cref="ConfigProvider{TResult}.Configuration"/>.
        /// Called in the constructor.
        /// </summary>
        protected virtual void Initialize()
        {
            try
            {
                OpenConnection(DataSource);
                Configuration = Extract();
                CloseConnection();
            } catch(Exception ex)
            {
                throw BuildException(ex);
            }
        }

        /// <summary>
        /// Builds a <see cref="ConfigurationResolutionException"/> from the current context.
        /// </summary>
        /// <param name="ex">The inner exception.</param>
        /// <returns>A <see cref="ConfigurationResolutionException"/> built on the current context.</returns>
        protected virtual ConfigurationResolutionException BuildException(Exception ex)
            => new ConfigurationResolutionException($"Error retrieving {nameof(Configuration)} from {nameof(DataSource)} {DataSource}. " +
                $"Reason:\r\n {ex.Message}", ex);
    }
}
