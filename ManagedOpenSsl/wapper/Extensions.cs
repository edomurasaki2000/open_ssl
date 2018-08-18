using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace OpenSSL.wapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string getContentType(this WebHeaderCollection col)
        {

            return col[HttpResponseHeader.ContentType];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static long getContentLength(this WebHeaderCollection col)
        {
            long l = -1;
            if (col[HttpResponseHeader.ContentLength] != null)
                l = long.Parse(col[HttpResponseHeader.ContentLength]);
            return l;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="schema"></param>
        /// <param name="value"></param>
        public static void setAuthorization(this WebHeaderCollection col, string schema, string value)
        {
            col.Add(HttpRequestHeader.Authorization,
                string.Format("{0} {1}", schema, value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void setBasicAuthorization(this WebHeaderCollection col, string username, string password)
        {
            setAuthorization(col, "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password))
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string getString(this System.IO.MemoryStream ms, System.Text.Encoding enc = null)
        {
            enc = enc == null ? System.Text.Encoding.UTF8 : enc;
            return enc.GetString(ms.ToArray());
        }
    }
   
}
