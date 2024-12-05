using System.Text;

namespace ConsoleMainApp.Helpers
{
    public static class FileHelper
    {
        public static byte[][] GetBytesMap(string inputFile)
        {
            var lines = File.ReadAllLines(inputFile).ToList();

            List<byte[]> mapList = new List<byte[]>();

            foreach (var line in lines)
            {
                var rowBytes = Encoding.ASCII.GetBytes(line);
                mapList.Add(rowBytes);
            }

            var map = mapList.ToArray();

            return map;
        }
    }
}
