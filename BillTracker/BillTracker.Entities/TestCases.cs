using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BillTracker.Entities
{
    [Serializable]
   
        public class cases
    {
        [XmlElement(ElementName ="test-case-type")]
        public String TestCaseType { get; set; }

        [XmlElement(ElementName = "expected-output")]
        public String expectedOutput { get; set; }

        [XmlElement(ElementName = "name")]
        public String Name { get; set; }
      
        [XmlElement(ElementName = "weight")]
        public int weight { get; set; }

        [XmlElement(ElementName = "mandatory")]
        public String mandatory { get; set; }

        [XmlElement(ElementName = "desc")]
        public String desc { get; set; }
    }
}
