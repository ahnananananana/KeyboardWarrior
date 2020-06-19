using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public int gold;
    public int exp;
    public string nickName;
}


namespace STUDY
{
    public class DataManager
    {
        public static void SaveToFile<T>(T data, string fileName) where T : struct
        {
            string path = Application.persistentDataPath + "/GameData/";

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += fileName;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Create(path);
            formatter.Serialize(fs, data);
            fs.Close();
            Debug.Log(path);
        }

        public static void LoadFromFile<T>(ref T data, string fileName) where T : struct
        {
            string path = Application.persistentDataPath + "/GameData/";
            path += fileName;

            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = File.Open(path, FileMode.Open);
                data = (T)formatter.Deserialize(fs);
                fs.Close();
            }
        }

        public static void SaveXml(SaveData data)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            XmlElement root = doc.CreateElement("GameSaveData");
            doc.AppendChild(root);

            XmlNode child = doc.CreateNode(XmlNodeType.Element, "DATA", string.Empty);
            root.AppendChild(child);

            XmlElement gold = doc.CreateElement("Gold");
            gold.InnerText = data.gold.ToString();
            child.AppendChild(gold);

            XmlElement exp = doc.CreateElement("Exp");
            exp.InnerText = data.exp.ToString();
            child.AppendChild(exp);

            XmlElement nickName = doc.CreateElement("NickName");
            nickName.InnerText = data.nickName;
            child.AppendChild(nickName);

            string path = Application.persistentDataPath + "/GameData/";
            doc.Save(path + "test.xml");
        }

        public static void LoadXml()
        {
            SaveData data = new SaveData();

            XmlDocument doc = new XmlDocument();
            string path = Application.persistentDataPath + "/GameData/";
            doc.Load(path + "test.xml");

            XmlNode root = doc.DocumentElement;
            XmlNodeList child = root.ChildNodes;

            for (int i = 0; i < child.Count; ++i)
            {
                data.gold = int.Parse(child[i].SelectSingleNode("Gold").InnerText);
                data.exp = int.Parse(child[i].SelectSingleNode("Exp").InnerText);
                data.nickName = child[i].SelectSingleNode("NickName").InnerText;
            }

            Debug.Log(data.gold);
            Debug.Log(data.exp);
            Debug.Log(data.nickName);
        }
    }
}

