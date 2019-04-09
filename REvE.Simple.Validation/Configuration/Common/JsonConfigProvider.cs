/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using Newtonsoft.Json;
using System.ComponentModel.Composition;
using System.IO;

namespace REvE.Configuration
{
    /// <summary>
    /// Class for deserializing a Configuration Model from a JSON file.
    /// </summary>
    /// <typeparam name="TResult">The Configuration Model.</typeparam>
    [InheritedExport("json", typeof(IConfigProvider<>))]
    public class JsonConfigProvider<TResult> : FileConfigProvider<TResult>
    {
        /// <summary>
        /// The name of the contract to use when resolving the <see cref="ConfigProvider{TResult}.DataSourceKey"/>
        /// </summary>
        public const string JsonDataSourceContractName = "json-cfg";

        /// <summary>
        /// The default Application Configuration Key used to find the external Configuration File.
        /// </summary>
        public const string DefaultJsonProviderKey = "external-configuration-json";

        /// <summary>
        /// Creates a new instance of <see cref="JsonConfigProvider{TResult}"/>.
        /// <paramref name="dataSourceKey"/> will be imported when resolving from an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">Application configuration key containing the data source location.</param>
        [ImportingConstructor]
        public JsonConfigProvider([Import(JsonDataSourceContractName, AllowDefault = true)] string dataSourceKey = DefaultJsonProviderKey) 
            : base(dataSourceKey) { }

        private StreamReader Reader { get; set; }

        /// <summary>
        /// Method for closing the connection opened by <see cref="ConfigProvider{TResult}.OpenConnection(string)"/>.
        /// </summary>
        protected override void CloseConnection() => Reader.Dispose();

        /// <summary>
        /// Method for retrieving and deserializing the <typeparamref name="TResult"/> from the JSON Data Source.
        /// </summary>
        /// <returns>The deserialized result.</returns>
        protected override TResult Extract() => JsonConvert.DeserializeObject<TResult>(Reader.ReadToEnd());

        /// <summary>
        /// Method for instantiating a new <see cref="StreamReader"/> used to retrieve the configuration.
        /// </summary>
        /// <param name="source">The JSON file source relative to the <see cref="FileConfigProvider{TResult}.StartingDirectory"/>.</param>
        protected override void OpenConnection(string source) 
            => Reader = new StreamReader(StartingDirectory + Path.DirectorySeparatorChar + source);

    }
}
