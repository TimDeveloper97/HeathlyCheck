using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthDeclaration.Helpers
{
    public class JsonHelper<T> where T : new()
    {
        public static async Task<bool> WriteItemAsync(string pFile, T item)
        {
            try
            {
                if (item == null) item = new T();

                var jsonOut = JsonConvert.SerializeObject(item, Formatting.Indented);
                File.WriteAllText(pFile, jsonOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public static async Task<T> ReadItemAsync(string pFile)
        {
            try
            {
                string jsonIn = File.ReadAllText(pFile);
                var item = JsonConvert.DeserializeObject<T>(jsonIn);

                return await Task.FromResult(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(new T());
            }
        }
    }
}
