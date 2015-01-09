﻿namespace Examples
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Bond;
    using Bond.Protocols;

    static class Program
    {
        static void Main()
        {
            var config = new Config
            {
                Variant = "Simple",
                Enabled = true,
                Urls = { "http://example.com", "http://www.example.com" }
            };

            var xmlString = new StringBuilder();
            var xmlWriter = new SimpleXmlWriter(XmlWriter.Create(xmlString, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            }));

            Serialize.To(xmlWriter, config);
            xmlWriter.Flush();
            Console.WriteLine(xmlString);

            const string configString =
@"<Config>
  <Urls>
    <Item>http://example.com</Item>
  </Urls>
  <Enabled>false</Enabled>
  <Variant>Complex</Variant>
</Config>";

            var reader = new SimpleXmlReader(XmlReader.Create(new StringReader(configString)));
            config = Deserialize<Config>.From(reader);
            Debug.Assert(config.Enabled == false);
        }
    }
}
