using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Utility
{
    public static class Serializer
    {

        #region 二进制
        /// <summary>
        /// 序列化二进制文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static void SaveBinary(string path, object data)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(path);
            bf.Serialize(fs, data);
            fs.Close();
        }

        /// <summary>
        /// 反序列化二进制文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadBinary<T>(string path)
        {
            if (!File.Exists(path))
            {
                return default(T);
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(path);
            object data = bf.Deserialize(fs);
            fs.Close();
            return (T)data;
        }
        #endregion

        #region xml
        /// <summary>
        /// 序列化XML文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public static void SaveXml(string path, object data, Type type)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            XmlSerializer xs = new XmlSerializer(type);
            FileStream fs = File.Create(path);
            xs.Serialize(fs, data);
            fs.Close();
        }

        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T LoadXml<T>(string path, Type type)
        {
            if (!File.Exists(path))
            {
                return default(T);
            }
            XmlSerializer xs = new XmlSerializer(type);
            FileStream fs = File.OpenRead(path);
            object data = xs.Deserialize(fs);
            fs.Close();
            return (T)data;
        }
        #endregion

        #region json
        /// <summary>
        /// 序列化JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = Int32.MaxValue;
            return json.Serialize(data);
        }

        /// <summary>
        /// 反序列化JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            return js.Deserialize<T>(json);
        }
        #endregion

    }
}