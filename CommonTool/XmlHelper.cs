using System.Xml;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System;
using System.Text;

namespace CommonTool
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public class XmlHelper
    {
        #region 字段定义
        /// <summary>
        /// XML文件的物理路径
        /// </summary>
        private string _filePath = string.Empty;
        /// <summary>
        /// Xml文档
        /// </summary>
        private XmlDocument _xml;
        /// <summary>
        /// XML的根节点
        /// </summary>
        private XmlElement _element;
        #endregion
 
        #region 构造方法
        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的绝对路径</param>
        public XmlHelper(string xmlFilePath)
        {
            //获取XML文件的绝对路径
            _filePath = xmlFilePath;
        }
        #endregion
 
        #region 创建XML的根节点
        /// <summary>
        /// 创建XML的根节点
        /// </summary>
        private void CreateXMLElement()
        {
 
            //创建一个XML对象
            _xml = new XmlDocument();
 
            if (File.Exists(_filePath))
            {
                //加载XML文件
                _xml.Load(this._filePath);
            }
 
            //为XML的根节点赋值
            _element = _xml.DocumentElement;
        }
        #endregion
 
        #region 获取指定XPath表达式的节点对象
        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public XmlNode GetNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //返回XPath节点
            return _element.SelectSingleNode(xPath);
        }
        #endregion
 
        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public string GetValue(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //返回XPath节点的值
            return _element.SelectSingleNode(xPath).InnerText;
        }
        #endregion
 
        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public string GetAttributeValue(string xPath, string attributeName)
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //返回XPath节点的属性值
            return _element.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion
 
        #region 新增节点
        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将任意节点插入到当前Xml文件中。
        /// </summary>        
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //导入节点
            XmlNode node = _xml.ImportNode(xmlNode, true);
 
            //将节点插入到根节点下
            _element.AppendChild(node);
        }
 
        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将DataSet中的第一条记录插入Xml文件中。
        /// </summary>        
        /// <param name="ds">DataSet的实例，该DataSet中应该只有一条记录</param>
        public void AppendNode(DataSet ds)
        {
            //创建XmlDataDocument对象
            XmlDataDocument xmlDataDocument = new XmlDataDocument(ds);
 
            //导入节点
            XmlNode node = xmlDataDocument.DocumentElement.FirstChild;
 
            //将节点插入到根节点下
            AppendNode(node);
        }
        #endregion
 
        #region 删除节点
        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //获取要删除的节点
            XmlNode node = _xml.SelectSingleNode(xPath);
 
            //删除节点
            _element.RemoveChild(node);
        }
        #endregion //删除节点
 
        #region 保存XML文件
        /// <summary>
        /// 保存XML文件
        /// </summary>        
        public void Save()
        {
            //创建XML的根节点
            CreateXMLElement();
 
            //保存XML文件
            _xml.Save(this._filePath);
        }
        #endregion //保存XML文件
 
        #region 静态方法
 
        #region 创建根节点对象
        /// <summary>
        /// 创建根节点对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的绝对路径</param>        
        private static XmlElement CreateRootElement(string xmlFilePath)
        {
            //定义变量，表示XML文件的绝对路径
            string filePath = "";
 
            //获取XML文件的绝对路径
            filePath = xmlFilePath;
 
            //创建XmlDocument对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载XML文件
            xmlDocument.Load(filePath);
            
            //返回根节点
            return xmlDocument.DocumentElement;
        }

        /// <summary>
        /// 从xml字符串创建根节点对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static XmlElement CreateRootElementFromXmlString(string xml)
        {           
            //创建XmlDocument对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载XML文件
            xmlDocument.LoadXml(xml);

            //返回根节点
            return xmlDocument.DocumentElement;
        }
        #endregion
 
        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public static string GetValue(string xmlFilePath, string xPath)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);
 
            //返回XPath节点的值
            return rootElement.SelectSingleNode(xPath).InnerText;
        }

        /// <summary>
        /// 从xml字符串获取指定节点的值
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static string GetValueFromXmlString(string xml,string xPath)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElementFromXmlString(xml);

            //返回XPath节点的值
            return rootElement.SelectSingleNode(xPath).InnerText;
        }
        #endregion
 
        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);
 
            //返回XPath节点的属性值
            return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion
        #region 设置指定节点的值
        /// <summary>
        /// 写入指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的绝对路径</param>
        /// <param name="xName">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="newtext">新值</param>
        /// <returns>修改是否成功</returns>
        public static bool SetValue(string xmlFilePath, string xName, string newtext)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlElement xe = doc.DocumentElement;
                xe.SelectSingleNode(xName).InnerText = newtext;
                doc.Save(xmlFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #endregion
        #region 序列化与反序列化
        /// <summary>
        /// 序列化类为xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml<T>(T obj)
        {
            string xmlString = string.Empty;
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    xmlSerializer.Serialize(ms, obj);
            //    xmlString = Encoding.UTF8.GetString(ms.ToArray());
            //}
            Encoding encoding = Encoding.UTF8;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
                xmlTextWriter.WriteStartDocument(true);  //写入 standalone="yes"
                xmlTextWriter.Formatting = Formatting.None;
                xmlSerializer.Serialize(xmlTextWriter, obj, namespaces);
                xmlTextWriter.Flush();
                xmlTextWriter.Close();

                xmlString = encoding.GetString(memoryStream.ToArray());
            }
            return xmlString;
        }

        /// <summary>
        /// 序列化类到XML文件
        /// </summary>
        /// <param name="path">the path to save the xml file</param>
        /// <param name="obj">the object you want to serialize</param>
        public static bool SerializeToXml(string path, object obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                string content = string.Empty;
                ////serialize
                //using (StringWriter writer = new StringWriter())
                //{
                //    serializer.Serialize(writer, obj);
                //    content = writer.ToString();
                //}

                Encoding encoding = Encoding.UTF8;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");

                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, encoding);

                    //xmlTextWriter.Formatting = Formatting.None;
                    //xmlSerializer.Serialize(xmlTextWriter, obj, namespaces);
                    xmlTextWriter.WriteStartDocument(true);  //写入 standalone="yes"
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(xmlTextWriter, obj, namespaces);
                    xmlTextWriter.Flush();
                    xmlTextWriter.Close();

                    content = encoding.GetString(memoryStream.ToArray());
                }              
                //save to file
                using (StreamWriter stream_writer = new StreamWriter(path))
                {
                    stream_writer.Write(content);
                }

                

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <param name="path">the path of the xml file</param>
        /// <param name="object_type">the object type you want to deserialize</param>
        public static object DeserializeFromXml(string path, Type object_type)
        {
            XmlSerializer serializer = new XmlSerializer(object_type);
            using (StreamReader reader = new StreamReader(path))
            {
                return serializer.Deserialize(reader);
            }
        }

        #endregion
    }
}