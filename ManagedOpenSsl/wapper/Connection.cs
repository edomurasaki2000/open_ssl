using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace OpenSSL.wapper
{
    /// <summary>
    /// 
    /// </summary>
    public class SslConnection : IDisposable
    {

        TcpClient tcp = null;
        OpenSSL.SSL.SslStream ssl = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void connect(Uri url)
        {
            tcp = new TcpClient();

            tcp.Connect(url.Host, 443);

            ssl = new OpenSSL.SSL.SslStream(tcp.GetStream());
            ssl.AuthenticateAsClient(url.Host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int read(byte[] buffer, int offset, int length)
        {
            return ssl.Read(buffer, offset, length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="args"></param>
        public void write(string fmt, params object[] args)
        {
            write(string.Format(fmt, args));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void write(string line)
        {
            var bytes = Encoding.UTF8.GetBytes(line);
            ssl.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getLine()
        {
            bool got15 = false;
            var sb = new StringBuilder();
            while (true)
            {
                int i = ssl.ReadByte();
                sb.Append((char)i);
                if (i == '\r') got15 = true;
                else if (i == '\n' && got15)
                    break;
                else
                    got15 = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            ssl.Dispose();
            tcp.Close();
        }
    }

}
