using System.Xml.Linq;
using UnityEngine;

namespace CrazyWizardNotes
{
    public static class XMLExtension
    {
        public static Rect ParseRect(this XElement xElement)
        {
            return new Rect(
                float.Parse(xElement.Attribute("posX").Value),
                float.Parse(xElement.Attribute("posY").Value),
                float.Parse(xElement.Attribute("sizeX").Value),
                float.Parse(xElement.Attribute("sizeY").Value));
        }

        public static XElement ToXml(this Rect rect, string name = "Rect")
        {
            return new XElement(name,
                new XAttribute("posX", rect.x),
                new XAttribute("posY", rect.y),
                new XAttribute("sizeX", rect.size.x),
                new XAttribute("sizeY", rect.size.y));
        }

        public static Color ParseColor(this XElement xElement)
        {
            return new Color(
                float.Parse(xElement.Attribute("R").Value),
                float.Parse(xElement.Attribute("G").Value),
                float.Parse(xElement.Attribute("B").Value),
                float.Parse(xElement.Attribute("A").Value));
        }

        public static XElement ToXml(this Color color, string name = "Color")
        {
            return new XElement(name,
                new XAttribute("R", color.r),
                new XAttribute("G", color.g),
                new XAttribute("B", color.b),
                new XAttribute("A", color.a));
        }
    }
}