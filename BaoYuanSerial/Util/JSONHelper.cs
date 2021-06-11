
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaoYuanSerial.Util
{
    public class JSONHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o, Formatting.None, _jsonSerializerSettings);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();

            /*******json设置*********/
            //忽略默认值
            serializer.DefaultValueHandling = DefaultValueHandling.Ignore;

            StringReader sr = new StringReader(json);

            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>对象列表</returns>
        public static List<T> DeserializeJsonToList<T>(String jsonString) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(jsonString);

            /*******json设置*********/
            //忽略默认值
            serializer.DefaultValueHandling = DefaultValueHandling.Ignore;

            //日期格式

            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        [JsonIgnore]
        private static readonly JsonSerializerSettings _jsonSerializerSettings
            = new JsonSerializerSettings
            {
                Converters
                    = new JsonConverter[]
                                {
                                    new IsoDateTimeConverter
                                        {
                                            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                                        }
                                }
            };

        private static JsonSerializerSettings setJsonSerializerSettings()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            //jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat; 

            return jsonSerializerSettings;
        }
    }
}