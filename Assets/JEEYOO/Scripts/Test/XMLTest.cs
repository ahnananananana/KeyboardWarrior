using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLTest : MonoBehaviour
{

    public string a;
    public float b;
    public float c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadXml();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadExpTable();
        }
    }

    public void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "Alien";
        child.AppendChild(name);

        XmlElement maxHP = xmlDoc.CreateElement("MaxHP");
        maxHP.InnerText = "100";
        child.AppendChild(maxHP);

        XmlElement level = xmlDoc.CreateElement("Level");
        level.InnerText = "1";
        child.AppendChild(level);

        xmlDoc.Save("./Assets/JEEYOO/Resources/XML/Character.xml");
    }

    public void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Character");
        Debug.Log(textAsset);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodelist = xmlDoc.SelectNodes("CharacterInfo/Character");

        foreach(XmlNode node in nodelist)
        {
            a = node.SelectSingleNode("Name").InnerText;
            b = float.Parse(node.SelectSingleNode("MaxHP").InnerText);
            c = float.Parse(node.SelectSingleNode("Level").InnerText);
            Debug.Log("Name :: " + a);
            Debug.Log("MaxHP :: " + b);
            Debug.Log("Level :: " + c);
        }
    }

    public void LoadExpTable()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/ExpTableSource");
        Debug.Log(textAsset);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodelist = xmlDoc.SelectNodes("ExpTable");

        foreach(XmlNode node in nodelist)
        {
            Debug.Log("Level :: " + node["Field"]["Level"].InnerText);
        }
    }
}
