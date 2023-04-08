using DietPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DietPlanner.Classes
{
    public class XmlFormat : IFile
    {
        FileStream stream = null;
        XmlSerializer serializer = null;
        string path;
        public XmlFormat(string path)
        {
            this.path = path;
        }
        public void Save<T>(T obj)
        {
            stream = new FileStream(path, FileMode.Create);
            serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, obj);
            stream.Close();
        }
        public T Load<T>()
        {
            T obj;
            if (!File.Exists(path))
            {
                return default(T);
            }
            else
            {
                stream = new FileStream(path, FileMode.Open);
                serializer = new XmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(stream);
                stream.Close();
                return obj;
            }
        }
    }
}
