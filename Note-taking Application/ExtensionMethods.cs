using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application
{
    public static class ExtensionMethods
    {
        public static T? DeserializeObject<T>(this string path)
        {
            string json = File.ReadAllText(path);
            T? result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
