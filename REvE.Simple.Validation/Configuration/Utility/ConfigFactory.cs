/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.ComponentModel.Composition.Hosting;

namespace REvE.Configuration
{
    using static ConfigUtil;

    /// <summary>
    /// Static utility class for resolving specialized Configuration Providers.
    /// </summary>
    public static class ConfigFactory
    {
        private const string StartingDirectoryKey = "cfg-start-directory";
        private const string DirectoryOffsetKey = "cfg-relative-directory";
        private const string SearchPatternKey = "cfg-search-pattern";

        private static readonly string defaultDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string startingDirectory = AppSetting(StartingDirectoryKey, defaultDirectory);
        private static readonly string directoryOffset = AppSetting(DirectoryOffsetKey, "");

        private static readonly string searchDirectory = startingDirectory + directoryOffset;

        private static readonly string searchPattern = AppSetting(SearchPatternKey, "*.dll");

        /// <summary>
        /// Resolves the <see cref="IConfigProvider{TResult}"/> instance matching the provided <paramref name="contractName"/>.
        /// </summary>
        /// <typeparam name="TContract">The <see cref="IConfigProvider{TResult}"/> implementation or contract.</typeparam>
        /// <typeparam name="TConfigModel">The type of Model to retrieve.</typeparam>
        /// <param name="contractName">(Optional) name of the contract.</param>
        /// <returns>The resolved <typeparamref name="TContract"/></returns>
        public static TContract GetConfig<TContract, TConfigModel>(string contractName = null)
             where TContract : IConfigProvider<TConfigModel>
        {
            try
            {
                var catalog = new DirectoryCatalog(searchDirectory, searchPattern);
                var container = new CompositionContainer(catalog);
                return container.GetExportedValue<TContract>(contractName);
            }catch(ConfigurationResolutionException)
            {
                throw;
            }catch (Exception e)
            {
                throw new ConfigurationResolutionException($"Error resolving Configuration Provider. " +
                    $"Provider Type: {typeof(TContract)}. " +
                    $"Directory: {searchDirectory}. " +
                    $"{nameof(contractName)}: {contractName}" +
                    $"Reason:\r\n {e.Message}", e.InnerException);
            }
        }
    }
}
