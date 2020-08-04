using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using BillTracker.Entities;

namespace BillTracker.Test.Utility
{
   public  class FileUtility
    {
        private  static FileStream stream;
        private static XmlSerializer serialize;
        private String filePath;
    

        [XmlArray("Testcases"), XmlArrayItem(typeof(cases), ElementName = "test-cases")]
        private  List<cases> Testcases;

        public string FilePath { get => filePath; set => filePath = value; }

        public FileUtility()
        {
            serialize = new XmlSerializer(typeof(List<cases>));
            this.Testcases = new List<cases>();
        }
        public  async Task<string> WriteTestCaseResuItInXML(cases Cases)
        {
            try { 
            
            if (File.Exists("../../../../testcases.xml"))
            {
                 stream = new FileStream("../../../../testcases.xml", FileMode.Open, FileAccess.Read);
                 
                var testcases =  serialize.Deserialize(stream) as List<cases>;
                if (testcases != null)
                {
                    foreach (cases item in testcases)
                    {
                        Testcases.Add(item);
                    }
                }
                stream.Close();
                Testcases.Add(Cases);
                File.Delete("../../../../testcases.xml");
                stream = new FileStream("../../../../testcases.xml", FileMode.CreateNew, FileAccess.Write);
                serialize.Serialize(stream, Testcases);
                    stream.Close();
            }
            else
            {
                
               stream = new FileStream("../../../../testcases.xml", FileMode.OpenOrCreate, FileAccess.Write);
               Testcases.Add(Cases);
                serialize.Serialize(stream, Testcases);
                stream.Close();

            }
                return "test case registered";
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public  void WriteTestCaseResuItInText(String testresult)
        {
            try
            {
                File.AppendAllText(FilePath,  testresult + "\n");
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public  void CreateTextFile()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath).Dispose();
                }

                else
                {
                    File.Delete(FilePath);
                    File.Create(FilePath).Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
