using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoWorkingApp.Data
{
    public class JsonManager<T> {
        public List<T> GetCollection() {

            string collectionPath = $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";

            List<T> myCollection = new List<T>();

            string currentContent = string.Empty;

            if(File.Exists(collectionPath)) {

                var streamReader = new StreamReader(collectionPath);
                currentContent = streamReader.ReadToEnd();
                myCollection = JsonConvert.DeserializeObject<List<T>>(currentContent);
                streamReader.Close();
            } else {

                var jsonCollection = JsonConvert.SerializeObject(myCollection, Formatting.Indented);
                var streamWriter = new StreamWriter(collectionPath);
                streamWriter.WriteLine(jsonCollection);
                streamWriter.Close();
            }
            return myCollection;    
        }

        public bool SaveCollection(List<T> collection) {
            string collectionPath = $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";

            try
            {
                var jsonCollection = JsonConvert.SerializeObject(collection, Formatting.Indented);
                var streamWriter = new StreamWriter(collectionPath);
                streamWriter.WriteLine(jsonCollection);
                streamWriter.Close();
                
            }
            catch
            {
                return false;
            }

            return true;

        }
    }
}