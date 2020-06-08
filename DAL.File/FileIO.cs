using System.IO;
using System.Text;

namespace DAL.File
{
    public static class FileIo
    {
        public static string Read(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static void Write(string path, string text)
        {
            using (var streamWriter = new StreamWriter(path, false, Encoding.UTF8))
            {
                streamWriter.Write(text);
            }
        }
    }
}