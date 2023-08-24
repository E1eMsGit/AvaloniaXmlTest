using AvaloniaXmlTest.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AvaloniaXmlTest.Domains
{
    public class XmlParser
    {
        public static List<RelayModel> GetRelays(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            XElement? relays = xdoc.Element("relays");
            List<RelayModel> _relaysList = new List<RelayModel>();

            if (relays is not null)
            {
                foreach (XElement relay in relays.Elements("relay"))
                {

                    XElement? name = relay.Element("name");
                    XElement? pult = relay.Element("pult");
                    XElement? module = relay.Element("module");
                    XElement? position = relay.Element("position");

                    _relaysList.Add(new RelayModel(
                        name?.Value,
                        Convert.ToInt32(pult?.Value),
                        Convert.ToInt32(module?.Value),
                        Convert.ToInt32(position?.Value)
                        ));
                }
            }

            return _relaysList;
        }
    }
}
