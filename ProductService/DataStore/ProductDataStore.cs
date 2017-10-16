using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductService.Models;

namespace ProductService.DataStore
{
    public class ProductDataStore : IProductsDataStore
    {
        public async Task<IEnumerable<PlpItem>> GetAllItemsAsync()
        {       
            return LoadJson();
        }

        private List<PlpItem> LoadJson()

        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r = new StreamReader(currentDirectory + "/../../../ProductService/DataStore/TestData.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PlpItem>>(json);
            }
        }
    }
}