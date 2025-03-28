using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OWB.TADemo
{
    public static class OWBJson
    {
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
        public static string stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        public static Dictionary<string, object> parse(string jsonString)
        {
            Dictionary<string, object> hashMap = null;
            try
            {
                hashMap = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            }
            catch
            {

            }
            return hashMap;
        }
    }
}
