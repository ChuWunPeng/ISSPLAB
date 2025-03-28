using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace SDK
{
    public static class OWBJson
    {
        public static T parse<T>(string jsonString)
        {
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                return default(T);
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

        public static string serialize(object jsonObject,string[] props = null)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            if(props == null)
            {
                return JsonConvert.SerializeObject(jsonObject, Formatting.Indented, jsonSettings);
            }
            else
            {
                jsonSettings.ContractResolver = new LimitPropsContractResolver(props);
                return JsonConvert.SerializeObject(jsonObject, Formatting.Indented, jsonSettings);
            }
        }

        /// <summary>
        /// 列出不包含的字段
        /// </summary>
        public class LimitPropsContractResolver : DefaultContractResolver
        {
            string[] props = null;
            public LimitPropsContractResolver(string[] props)
            {
                this.props = props;
            }
            
            protected override IList<JsonProperty> CreateProperties(Type type,
            MemberSerialization memberSerialization)
            {
                IList<JsonProperty> list =
                base.CreateProperties(type, memberSerialization);
                return list.Where(p => !props.Contains(p.PropertyName)).ToList();
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

        public static T deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
