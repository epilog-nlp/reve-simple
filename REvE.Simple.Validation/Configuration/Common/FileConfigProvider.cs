/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;

namespace REvE.Configuration
{
    using static ConfigUtil;

    /// <summary>
    /// Base class for deserializing Configuration Models from File sources.
    /// </summary>
    /// <typeparam name="TResult">The Configuration Model.</typeparam>
    public abstract class FileConfigProvider<TResult> : ConfigProvider<TResult>
    {
        /// <summary>
        /// Basic constructor for specifying the application configuration key containing the data source location.
        /// </summary>
        /// <param name="dataSourceKey">Application configuration key containing the data source location.</param>
        protected FileConfigProvider(string dataSourceKey) : base(dataSourceKey)
        {
        }

        /// <summary>
        /// The default directory to use if an override isn't provided in the Application Configuration.
        /// </summary>
        protected static readonly string defaultDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// The Working Directory to start in when opening the File.
        /// </summary>
        protected string StartingDirectory => AppSetting(StartingDirectoryKey, defaultDirectory);
        
        /// <summary>
        /// The Application Configuration Key used to populate <see cref="FileConfigProvider{TResult}.StartingDirectory"/>.
        /// </summary>
        protected virtual string StartingDirectoryKey => "cfg-resource-directory";
    }
}
