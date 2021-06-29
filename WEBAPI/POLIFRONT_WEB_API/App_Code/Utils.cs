using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Db_Core;
using System.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Description résumée de Class1
/// </summary>
public class Utils
{
    //public static List<string> tableList { get; }
    public static string DecryptAes128(string cipherData, string keyString, string ivString)
    {
        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);

        try
        {
            using (var rijndaelManaged =
            new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            using (var memoryStream =
            new MemoryStream(Convert.FromBase64String(cipherData)))
            using (var cryptoStream =
            new CryptoStream(memoryStream,
            rijndaelManaged.CreateDecryptor(key, iv),
            CryptoStreamMode.Read))
            {
                return new StreamReader(cryptoStream).ReadToEnd();
            }
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    public static string EncryptAes128(string cipherData, string keyString, string ivString)
    {

        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);
        MemoryStream memoryStream = null;
        CryptoStream cryptoStream = null;
        try
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.Key = key;
                AES.IV = iv;
                AES.Mode = CipherMode.CBC;
                AES.Padding = PaddingMode.PKCS7;
                byte[] PlainText = Encoding.UTF8.GetBytes(cipherData);

                //AES.IV = SecretKey.GetBytes(16);
                using (ICryptoTransform Encryptor = AES.CreateEncryptor(key, iv))
                {
                    using (memoryStream = new MemoryStream())
                    {
                        using (cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(PlainText, 0, PlainText.Length);
                            cryptoStream.FlushFinalBlock();
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }

        }
        catch
        {
            throw;
        }
        finally
        {
            if (memoryStream != null)
                memoryStream.Close();
            if (cryptoStream != null)
                cryptoStream.Close();
        }
    }

    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        byte[] buffer4;
        if (hashedPassword == null)
        {
            return false;
        }
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        byte[] src = Convert.FromBase64String(hashedPassword);
        if ((src.Length != 0x31) || (src[0] != 0))
        {
            return false;
        }
        byte[] dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        byte[] buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20);
        }
        return ByteArraysEqual(buffer3, buffer4);
    }

    private static bool ByteArraysEqual(byte[] b1, byte[] b2)
    {
        if (b1 == b2) return true;
        if (b1 == null || b2 == null) return false;
        if (b1.Length != b2.Length) return false;
        for (int i = 0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i]) return false;
        }
        return true;
    }

    public static int getCurrID(string table_name)
    {
        try
        {
            string sql = @"SELECT
                              CASE
                                WHEN (SELECT COUNT(1) FROM " + table_name + @") = 0 THEN
		                            CASE
			                            WHEN (SELECT IDENT_CURRENT('" + table_name + @"')) = 1 THEN 1
			                            ELSE IDENT_CURRENT('" + table_name + @"') + 1
		                            END
                                ELSE IDENT_CURRENT('" + table_name + @"') + 1
                              END AS Current_Identity;";
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
            SqlDataReader reader = stmt.ExecuteReader();

            if (reader.Read())
            {
                //string result = reader["nextID"].ToString();
                //if(result != null)
                //{
                //    int.Parse(result);
                //}
                return int.Parse(reader["Current_Identity"].ToString());
            }
            return 0;
        }
        catch (Exception e)
        {
            Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
            return -1;
        }
    }

    public static List<string> tableList
    {
        get
        {
            List<string> tables = new List<string>();
            try
            {
                //string sql = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME ASC";
                string sql = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
                            WHERE 
                                TABLE_NAME in (
                                    SELECT DISTINCT(TABLE_NAME) FROM INFORMATION_SCHEMA.COLUMNS 
                                        WHERE 
                                            COLUMN_NAME = 'numero_interp'
                                ) 
                            ORDER BY TABLE_NAME ASC";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                SqlDataReader reader = stmt.ExecuteReader();
                while (reader.Read())
                {
                    tables.Add(reader["TABLE_NAME"].ToString());
                }
                return tables;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    public static void ReadError()
    {
        string strPath = @"C:\Users\Laguerre Vellie\Downloads\POLIFRONT\WEBAPI\log_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
        using (StreamReader sr = new StreamReader(strPath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    public static void ErrorLogging(string functionName, Exception ex)
    {
        bool append = true;
        string strPath = @"C:\Users\Laguerre Vellie\Downloads\POLIFRONT\WEBAPI\log_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
        if (!File.Exists(strPath))
        {
            File.Create(strPath).Dispose();
            append = false;
        }
        using (StreamWriter sw = File.AppendText(strPath))
        {
            if (!append)
            {
                sw.WriteLine("=============\tError Logging\t============");
            }
            sw.WriteLine("===========\tStart\t=============");
            sw.WriteLine("-----------\t" + functionName + "\t-----------");
            sw.WriteLine(/*"===========\t" + */DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + "\t===========");
            sw.WriteLine("Client IP:\t" + GetIp());
            sw.WriteLine("Error Message:\t" + ex.Message);
            sw.WriteLine("Stack Trace:\t" + ex.StackTrace);
            sw.WriteLine(/*"===========\t" + */DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + "\t===========");
            sw.WriteLine("===========\tEnd\t=============");

        }
    }

    public static void Logging(string title, string msg)
    {
        string strPath = @"C:\Users\Laguerre Vellie\Downloads\POLIFRONT\WEBAPI\log_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
        if (!File.Exists(strPath))
        {
            File.Create(strPath).Dispose();
        }
        using (StreamWriter sw = File.AppendText(strPath))
        {

            sw.WriteLine("=============\tDebug\t============");
            sw.WriteLine("===========\tStart\t=============");
            sw.WriteLine("-----------\t" + title + "\t-----------");
            sw.WriteLine(/*"===========\t" + */DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + "\t===========");
            sw.WriteLine("Client IP:\t" + GetIp());
            sw.WriteLine("Message:\t" + msg);
            sw.WriteLine(/*"===========\t" + */DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + "\t===========");
            sw.WriteLine("===========\tEnd\t=============");

        }
    }

    public static string GetIp()
    {
        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }
}