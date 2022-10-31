using FACL_Locker_Room_Core.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FACL_Locker_Room_Core.Helper
{
    public class ReadWriteToJson : IReadWriteToJson
    {
        //Relative Path, the base location/parent folder of the data base
        private readonly string db = Path.Combine($"{new DirectoryInfo(".").Parent}", "FACL_Locker_Room_Core\\accounts\\");

        public async Task<bool> WriteToJson<T>(T model, string jsonFile)
        {
            //gets the parent folder and append it to the json file name to get the file relatively
            string fileLocation = Path.Combine(db, jsonFile);

            try
            {
                //serializes the model to a json object
                string json = JsonConvert.SerializeObject(model); 
                File.WriteAllTextAsync(fileLocation, json).Dispose();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> ReadToJson<T>(string jsonFile)
        {
            string fileLocation = Path.Combine(db, jsonFile);

            var fileContent = "";
            if(File.Exists(fileLocation))
            {
                fileContent = await File.ReadAllTextAsync(fileLocation);
            }
            else
            {
                File.Create(fileLocation).Dispose();
            }
            T resultObject = JsonConvert.DeserializeObject<T>(fileContent);
            T container = resultObject;

            return container;

        }
    }
}
