using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace HisInterfaceValidate
{
    class XMLFormat
    {
        public static string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            //声明
            //XmlNode xmlnode = xd.CreateXmlDeclaration("1.0", "UTF-8", null);
            xd.LoadXml(sUnformattedXml);
            //xd.InsertBefore(xmlnode, xd.FirstChild);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }

        public static string AddXMLNode(string xmlDoc, string name, string value)
        {
            // 添节点
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDoc);
            XmlNode xmlNode = xml.CreateNode("element", name, "");
            xmlNode.InnerText = value;
            xml.DocumentElement.AppendChild(xmlNode);
            return xml.OuterXml;
        }
    }
}
