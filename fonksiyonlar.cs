using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;

namespace kargo_otomasyonu
{
    class fonksiyonlar
    {
        public SqlConnection baglan()
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-6AJENE80\\SQLEXPRESS;Initial Catalog=KargoTakip;Integrated Security=True");
            baglan.Open();
            return baglan;
        } 
        public string sifrele(string a)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(a);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach(byte item in dizi)
            {
                sb.Append(item.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
