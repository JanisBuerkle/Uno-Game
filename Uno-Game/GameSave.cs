using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Uno_Game;

public class GameSave
{
    public static void SaveData(object obj, string filename)
    {
        XmlSerializer sr = new XmlSerializer(obj.GetType());
        TextWriter writer = new StreamWriter(filename);
        sr.Serialize(writer, obj);
        writer.Close();
    }
}