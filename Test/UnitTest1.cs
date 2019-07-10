using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Test
{
    [TestClass]
    public class InstanceObject
    {
        [TestMethod]
        public void InstantiateWithFilename()
        {
            IncidentData myIncident = new IncidentData("F01705150050.json");
            Assert.AreEqual(myIncident.version, "1.0.29");
        }
        [TestMethod]
        public void InstantiateWithCharArray()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            char sep = Path.DirectorySeparatorChar;
            string file = $"{baseDir}SourceData{sep}";
            file = Path.Combine(file, "F01705150050.json");

            IncidentData myIncident = new IncidentData(byteArray: File.ReadAllBytes(file));
            Assert.AreEqual(myIncident.version, "1.0.29");
        }

        [TestMethod]
        public void VerifyTildePopulated()
        {
            IncidentData myIncident = new IncidentData("F01705150050.json");
            Assert.IsNotNull(myIncident?.apparatus[0]?.unit_status?.tilde);
        }
    }
}
