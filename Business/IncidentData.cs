using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Business
{
    public partial class IncidentData {
        public IncidentData() { }
        public IncidentData(string fileName = null, byte[] byteArray = null)
        {
            if(fileName != null)
                fileName = ValidateDataFile(fileName);
            PopulateMemberObject(fileName, byteArray);
        }
        private void PopulateMemberObject(string fileName = null, byte[] byteArray = null)
        {
            if ((string.IsNullOrWhiteSpace(fileName) && byteArray == null)||
                (!string.IsNullOrWhiteSpace(fileName) && byteArray != null))
                throw new Exception("A fileName OR a byte array must be provided.");

            string theFile = null;
            MemoryStream memoryStream = null;

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                theFile = File.ReadAllText(fileName);
                //may need to hack the file here, replace ~ with Tilde...

                memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(theFile));
            }
            else
                memoryStream = new MemoryStream(byteArray);
            
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssK")
            };
            IncidentData theIncident = new IncidentData();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(theIncident.GetType(), settings);
            theIncident = serializer.ReadObject(memoryStream) as IncidentData;
            memoryStream.Close();

            address = theIncident.address;
            apparatus = theIncident.apparatus;
            description = theIncident.description;
            fire_department = theIncident.fire_department;
            version = theIncident.version;
        }
        private static string ValidateDataFile(string fileName)
        {
            if(string.IsNullOrWhiteSpace(fileName))
                throw new Exception("No valid file name provided.");

            //if the file path/filename are complete, no need to look for the file.
            if (File.Exists(fileName))
                return fileName;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            char sep = Path.DirectorySeparatorChar;
            string t = null;

            //not sure how this will look on a Mac...  Look for the file/try everything?
            List<string> pathsToTry = new List<string>() {
                $"{baseDir}SourceData{sep}",                                       // SourceData copied to output folder.
                $".{sep}SourceData{sep}",                                    // path = web\bin\SourceData
                };

            bool foundFile = false;

            foreach (string thing in pathsToTry)
            {
                t = Path.Combine(thing, fileName);
                foundFile = File.Exists(t);
                if (foundFile)
                {
                    fileName = t;
                    break;
                }
            }
            if (!foundFile)
                throw new FileNotFoundException("Couldn't find data file.", fileName);
            return fileName;
        }
    }
}
