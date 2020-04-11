using System.IO;
using System.Text;

namespace DAL
{
    public static class FileIo
    {
        public static string ReadFromFile(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static void WriteToFile(string fileName, string text)
        {
            using (var streamWriter = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                streamWriter.Write(text);
            }
        }
    }
}