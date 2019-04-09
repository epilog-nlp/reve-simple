/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.ComponentModel.Composition;
using System.Xml;
using System.Xml.Serialization;

namespace REvE.Configuration
{
    /// <summary>
    /// Class for deserializing a Configuration Model from an XML file.
    /// </summary>
    /// <typeparam name="TResult">The Configuration Model.</typeparam>
    [InheritedExport("xml", typeof(IConfigProvider<>))]
    public class XmlConfigProvider<TResult> : FileConfigProvider<TResult>
    {
        /// <summary>
        /// The name of the contract to use when resolving the <see cref="ConfigProvider{TResult}.DataSourceKey"/>
        /// </summary>
        public const string XmlDataSourceContractName = "xml-cfg";

        /// <summary>
        /// The default Application Configuration Key used to find the external Configuration File.
        /// </summary>
        public const string DefaultXmlProviderKey = "external-configuration-xml";

        /// <summary>
        /// Creates a new instance of <see cref="XmlConfigProvider{TResult}"/>.
        /// <paramref name="dataSourceKey"/> will be imported when resolving from an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">Application configuration key containing the data source location.</param>
        [ImportingConstructor]
        public XmlConfigProvider([Import(XmlDataSourceContractName, AllowDefault = true)] string dataSourceKey = DefaultXmlProviderKey) 
            : base(dataSourceKey) { }

        private XmlSerializer Serializer { get; set; }
        private XmlReader Reader { get; set; }

        /// <summary>
        /// Method for closing the connection opened by <see cref="ConfigProvider{TResult}.OpenConnection(string)"/>.
        /// </summary>
        protected override void CloseConnection() => Reader.Dispose();

        /// <summary>
        /// Method for retrieving and deserializing the <typeparamref name="TResult"/> from the XML Data Source.
        /// </summary>
        /// <returns>The deserialized result.</returns>
        protected override TResult Extract() => (TResult)Serializer.Deserialize(Reader);

        /// <summary>
        /// Method for instantiating a new <see cref="XmlSerializer"/> and <see cref="XmlReader"/> used to retrieve and deserialize the configuration.
        /// </summary>
        /// <param name="source">The XML file source relative to the <see cref="FileConfigProvider{TResult}.StartingDirectory"/>.</param>
        protected override void OpenConnection(string source)
        {
            Serializer = new XmlSerializer(typeof(TResult));
            Reader = XmlReader.Create(StartingDirectory + System.IO.Path.DirectorySeparatorChar + source);
        }

    }
}
