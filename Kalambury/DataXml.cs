using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Kalambury
{
    class DataXml
    {
        protected XmlDocument document = new XmlDocument();
        protected XmlNodeList nodelist;
        protected string xmlName = String.Empty;
        protected string root = String.Empty;
        protected string branch= String.Empty;

        protected virtual string XmlName
        {
            get
            {
                return xmlName;
            }
        }

        protected virtual string Root
        {
            get
            {
                return root;
            }
        }

        protected virtual string Branch
        {
            get
            {
                return branch;
            }
        }

    }

    //class Teams : DataXml
    //{
    //    public string Nick { get; set; }

    //}

    class PasswordsXml : DataXml
    {
        protected override string XmlName
        {
            get
            {
                return "Hasla.xml";
            }
        }

        protected override string Root
        {
            get
            {
                return "hasla";
            }
        }

        protected override string Branch
        {
            get
            {
                return "haslo";
            }
        }

        public XmlDocument LoadXml()
        {
            
            XmlDocument XmlFile = new XmlDocument();

                try
                {
                    XmlFile.Load(@XmlName);
                }
                 
                catch (ArgumentNullException ex)
                {
                    // The input value is null.
                    MessageBox.Show(ex.Message);
                }
                catch (FileNotFoundException ex)
                {

                    // The underlying file of the path cannot be found
                    MessageBox.Show(ex.Message);
                }

                catch (Exception ex)
                {
                   
                    MessageBox.Show(ex.Message);
                }

            return XmlFile;
        }

        public ObservableCollection<Passwordclass> LoadDataFromXml()
        {  
            document = LoadXml();
            nodelist = document.SelectNodes("/" + Root + "/" + Branch);

            ObservableCollection<Passwordclass> PasswordCollection = new ObservableCollection<Passwordclass>();

            try
            {
                foreach (XmlNode node in nodelist)
                {
                    if (node != null)
                    {
                        Passwordclass PasswordList = new Passwordclass();
                        PasswordList.Id = Convert.ToInt32(node["id"].InnerText);
                        PasswordList.Name = node["nazwa"].InnerText;
                        PasswordList.Category = node["kategoria"].InnerText;
                        PasswordList.Level = node["poziom"].InnerText;
                        PasswordCollection.Add(PasswordList);
                    }

                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            document.Save(@XmlName);

            return PasswordCollection;
        }

        public ObservableCollection<Passwordclass> LookForDataInXml(string Data1, string Data1Key)
        {
            ObservableCollection<Passwordclass> PasswordCollection = new ObservableCollection<Passwordclass>();
            
            document = LoadXml();

            try
            {
                
                XmlNodeList nodeList = document.DocumentElement.SelectNodes("//" + Branch + "[" + Data1Key +"=" + "'" + Data1 + "'" + "]");

                foreach (XmlNode node in nodeList)
                {

                    Passwordclass PasswordList = new Passwordclass();
                    PasswordList.Id = Convert.ToInt32(node["id"].InnerText);
                    PasswordList.Name = node["nazwa"].InnerText;
                    PasswordList.Category = node["kategoria"].InnerText;
                    PasswordList.Level = node["poziom"].InnerText;
                    PasswordCollection.Add(PasswordList);

                            
                }

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            document.Save(@XmlName);

            return PasswordCollection;
        }

        public ObservableCollection<Passwordclass> LookForDataInXml(string Data1, string Data1Key, string Data2, string Data2Key)
        {
            ObservableCollection<Passwordclass> PasswordCollection = new ObservableCollection<Passwordclass>();

            document = LoadXml();


            try
            {
                XmlNodeList nodeList = document.DocumentElement.SelectNodes("//" + Branch + "[" + Data1Key + "=" + "'" + Data1 + "'" + " and " + Data2Key + "=" + "'" + Data2 + "'" + "]");

                foreach (XmlNode node in nodeList)
                {

                    Passwordclass PasswordList = new Passwordclass();
                    PasswordList.Id = Convert.ToInt32(node["id"].InnerText);
                    PasswordList.Name = node["nazwa"].InnerText;
                    PasswordList.Category = node["kategoria"].InnerText;
                    PasswordList.Level = node["poziom"].InnerText;
                    PasswordCollection.Add(PasswordList);


                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            document.Save(@XmlName);

            return PasswordCollection;
        }

        public ObservableCollection<Passwordclass> LookForDataInXml(string Data1, string Data1Key, string Data2, string Data2Key, string Data3, string Data3Key)
        {
            ObservableCollection<Passwordclass> PasswordCollection = new ObservableCollection<Passwordclass>();

            document = LoadXml();

            try
            {
                XmlNodeList nodeList = document.DocumentElement.SelectNodes("//" + Branch + "[" + Data1Key + "=" + "'" + Data1 + "'" 
                    + " and " + Data2Key + "=" + "'" + Data2 + "'" 
                    + " and " + Data3Key + "=" + "'" + Data3 + "'" + "]");

                foreach (XmlNode node in nodeList)
                {

                    Passwordclass PasswordList = new Passwordclass();
                    PasswordList.Id = Convert.ToInt32(node["id"].InnerText);
                    PasswordList.Name = node["nazwa"].InnerText;
                    PasswordList.Category = node["kategoria"].InnerText;
                    PasswordList.Level = node["poziom"].InnerText;
                    PasswordCollection.Add(PasswordList);


                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            document.Save(@XmlName);

            return PasswordCollection;
        }
        
        public void AddDataToXml(int id, string name , string category, string level)
        {

            document = LoadXml();
           
            XmlNode node = document.CreateNode(XmlNodeType.Element, Branch, null);
 
            XmlNode xmlIdNode = document.CreateElement("id");
            xmlIdNode.InnerText = id.ToString();

            XmlNode xmlNameNode = document.CreateElement("nazwa");
            xmlNameNode.InnerText = name;

            XmlNode xmlCategoryNode = document.CreateElement("kategoria");
            xmlCategoryNode.InnerText = category;

            XmlNode xmlLevelNode = document.CreateElement("poziom");
            xmlLevelNode.InnerText = level;

            node.AppendChild(xmlIdNode);
            node.AppendChild(xmlNameNode);
            node.AppendChild(xmlCategoryNode);
            node.AppendChild(xmlLevelNode);

            document.DocumentElement.AppendChild(node);

            document.Save(@XmlName);
        }

        public void UpdateXmlData(ObservableCollection<Passwordclass> PasswordCollection)
        {
            
            document = LoadXml();
            XmlNode root = document.DocumentElement;

            //Remove all attribute and child nodes
            root.RemoveAll();

            foreach (var Password in PasswordCollection)
            {

                XmlNode node = document.CreateNode(XmlNodeType.Element, Branch, null);

                XmlNode xmlIdNode = document.CreateElement("id");
                xmlIdNode.InnerText = Password.Id.ToString();

                XmlNode xmlNameNode = document.CreateElement("nazwa");
                xmlNameNode.InnerText = Password.Name;

                XmlNode xmlCategoryNode = document.CreateElement("kategoria");
                xmlCategoryNode.InnerText = Password.Category;

                XmlNode xmlLevelNode = document.CreateElement("poziom");
                xmlLevelNode.InnerText = Password.Level;

                node.AppendChild(xmlIdNode);
                node.AppendChild(xmlNameNode);
                node.AppendChild(xmlCategoryNode);
                node.AppendChild(xmlLevelNode);

                document.DocumentElement.AppendChild(node);
            }
            document.Save(@XmlName);
        }

    }
}
