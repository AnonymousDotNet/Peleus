using System.IO;
using System.Text;

namespace Http
{
    public static class Dumper
    {
        /// <summary>
        /// http web response stream -> memory stream -> byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] Dump(Stream stream)
        {
            byte[] data = new byte[0];

            MemoryStream ms = new MemoryStream();
            try
            {
                int size = 1024;
                byte[] buffer = new byte[size];
                while (size > 0)
                {
                    int received = stream.Read(buffer, 0, size);
                    ms.Write(buffer, 0, received);
                    size = received;
                }
                data = ms.ToArray();
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex);
            }
            finally
            {
                ms.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Dump(Stream stream, Encoding encoding)
        {
            byte[] data = Dump(stream);
            return encoding.GetString(data);
        }
    }
}