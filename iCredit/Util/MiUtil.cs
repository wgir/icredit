using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Data.Entity;
using CrediAdmin.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Globalization;

namespace CrediAdmin.Util
{
    public class MiUtil
    {
        //private SeaContext db;
        public string strConn;

        
        //private SeaContext db = new SeaContext();
        public MiUtil(string connectionString)
        {

            //if (connectionString.Equals("Armetales"))
            //    strConn = new ArmetalesContext().Database.Connection.ConnectionString;
            //else 

            //strConn = new BaseDatosContext().Database.Connection.ConnectionString;

        }

        public string getstrConn()
        {
            return strConn;
        }
        /// <summary>
        /// Ejecuta una consulta sin devolver nada. 
        /// </summary>
        /// <param name="query">
        /// 
        /// </param>
        public void ejecutarConsulta(string query)
        {
            // string strConn = db.Database.Connection.ConnectionString;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<SelectListItem> llenarLista(string query)
        {
            // db.Database.Connection.ConnectionString

            SqlConnection conn = new SqlConnection(strConn);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "datos");
            DataTable dt = ds.Tables["datos"];
            List<SelectListItem> items = new List<SelectListItem>();
            var emptyItem = new SelectListItem();
            /* emptyItem = new SelectListItem()
             {
                 Value = "",
                 Text = ""
             };
             items.Add(emptyItem);*/
            // For each row, print the values of each column.
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    emptyItem = new SelectListItem()
                    {
                        Value = row[0].ToString(),
                        Text = row[1].ToString()
                    };
                }
                items.Add(emptyItem);
            }


            //List<string> lista = new List(items, "Value", "Text");
            return items;

        }

        public int guardarRegistro(string tabla, params string[] valores)
        {
            // string strConn = db.Database.Connection.ConnectionString;
            string q;
            q = "insert into " + tabla;
            q = q + "(";
            for (int i = 0; i < valores.Length / 2; i++)
                q = q + "" + valores[i] + ",";
            q = q.Substring(0, q.Length - 1);
            q = q + ")";
            q = q + "values (";
            for (int i = (valores.Length / 2); i < valores.Length; i++)
            {
                if (!valores[i].ToUpper().Trim().Equals("GETDATE()") && !valores[i].ToUpper().Trim().Contains("CONVERT"))
                    q = q + "'" + valores[i] + "',";
                else
                    q = q + valores[i] + ",";
            }
            q = q.Substring(0, q.Length - 1);
            q = q + ")";

            int Id = 0;
            string strSQL = q + ";SELECT @@Identity";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand c = new SqlCommand(strSQL, conn);
                conn.Open();
                Id = Convert.ToInt32(c.ExecuteScalar());
            }
            return Id;
        }

        public static string fechaToSQL(DateTime fecha, int formato)
        {
            string valorRetorno = "NULL";
            int year = new int();
            int mes = new int();
            int dia = new int();
            dia = fecha.Day;
            mes = fecha.Month;
            year = fecha.Year;
            if (formato == 0) //date
                valorRetorno = "" + year.ToString() + "-" + right("00" + mes.ToString(), 2) + "-" + right("00" + dia.ToString(), 2);
            else    //datetime
                valorRetorno = "" + year.ToString() + "-" + mes.ToString() + "-" + dia.ToString() + " " + fecha.Hour.ToString() + ":" + fecha.Minute.ToString() + ":" + fecha.Second.ToString();
            return valorRetorno;
        }

        public static string fechaToPeriodo(DateTime fecha)
        {
            string valorRetorno = "NULL";
            int year = new int();
            int mes = new int();
            int dia = new int();
            dia = fecha.Day;
            mes = fecha.Month;
            year = fecha.Year;
            valorRetorno = year.ToString() + right("00" + mes.ToString(), 2);
            return valorRetorno;
        }
        public static bool isDate(string inputDate)
        {
            bool isDate = true;
            try
            {
                DateTime dateValue;
                dateValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        /// <summary>
        /// Retorna el nombre de usaurio logueado en la sesion
        /// </summary>
        /// <returns></returns>
        public string getUsuario()
        {
            //return HttpContext.Current.User.Identity.Name;
            //return System.Web.HttpContext.Current.User.Identity.Name;
            return HttpContext.Current.User.Identity.GetUserId();
        }


        /// <summary>
        /// REtorna el objeto usuario a partir de un nombre de usuario
        /// </summary>
        /// <param name="usname"></param>
        /// <returns></returns>
        //public Usuario getUsuario(string usname)
        //{
        //    Usuario u = new Usuario();
        //    var usuario = db.Usuario.Where(x => x.UserName.Equals(usname)).ToList();
        //    foreach (Usuario us in usuario)
        //    {
        //        return us;
        //    }
        //    return u;
        //}







        public bool existenRegistros(string query)
        {
            // db.Database.Connection.ConnectionString
            //string strConn = db.Database.Connection.ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            var da = new SqlDataAdapter(query, conn);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }


        public DataTable tabla(string q)
        {
            // El resultado lo guardaremos en una tabla
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.CommandTimeout = 600;
                tabla.Load(cmd.ExecuteReader());
                return tabla;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string str;
                str = "Source:" + ex.Source;
                str += "\n" + "Message:" + ex.Message;

            }
            catch (System.Exception ex)
            {
                string str;
                str = "Source:" + ex.Source;
                str += "\n" + "Message:" + ex.Message;

            }
            return tabla;
        }


        public string getCampo(string query, string campo)
        {
            // db.Database.Connection.ConnectionString
            //string strConn = db.Database.Connection.ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            var da = new SqlDataAdapter(query, conn);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow drv in dt.Select())
            {
                if (drv[campo] is System.DBNull)
                    return "";
                if (drv[campo].GetType() == typeof(string))
                    return (string)drv[campo];
                if (drv[campo].GetType() == typeof(int) || drv[campo].GetType() == typeof(Int64))
                    return drv[campo].ToString();
                if (drv[campo].GetType() == typeof(decimal))
                    return drv[campo].ToString();
            }
            return "";
        }




        public static string right(string cadena, int numCaracteres)
        {
            return cadena.Substring(cadena.Length - numCaracteres);
        }


        public string nombreArchivo(string nombre)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "pdfs\\" + nombre + ".pdf";
        }


        public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] key = { };
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public string EncryptMD5(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DecryptMD5(string cipherString)
        {
            bool useHashing = true;

            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }



        public string browserName(HttpRequestBase Request)
        {

            string s = "Browser Capabilities\n"
                  + "Type = " + Request.Browser.Type + "\n"
                  + "Name = " + Request.Browser.Browser + "\n"
                  + "Version = " + Request.Browser.Version + "\n"
                  + "Major Version = " + Request.Browser.MajorVersion + "\n"
                  + "Minor Version = " + Request.Browser.MinorVersion + "\n"
                  + "Platform = " + Request.Browser.Platform + "\n"
                  + "Is Beta = " + Request.Browser.Beta + "\n"
                  + "Is Crawler = " + Request.Browser.Crawler + "\n"
                  + "Is AOL = " + Request.Browser.AOL + "\n"
                  + "Is Win16 = " + Request.Browser.Win16 + "\n"
                  + "Is Win32 = " + Request.Browser.Win32 + "\n"
                  + "Supports Frames = " + Request.Browser.Frames + "\n"
                  + "Supports Tables = " + Request.Browser.Tables + "\n"
                  + "Supports Cookies = " + Request.Browser.Cookies + "\n"
                  + "Supports VBScript = " + Request.Browser.VBScript + "\n"
                  + "Supports JavaScript = " +
                      Request.Browser.EcmaScriptVersion.ToString() + "\n"
                  + "Supports Java Applets = " + Request.Browser.JavaApplets + "\n"
                  + "Supports ActiveX Controls = " + Request.Browser.ActiveXControls
                        + "\n"
                  + "Supports JavaScript Version = " +
                      Request.Browser["JavaScriptVersion"] + "\n";
            return Request.Browser.Browser;
        }

        public int cantColumnasArchivo(string path, string separador)
        {
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            string[] value = line.Split(';');
            return value.Length;
        }

        public void crearTablaFromCSV(string path, string nombreTabla)
        {
            //StreamReader sr = new StreamReader(path, System.Text.Encoding.Default, false);
            //StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1252));
            //Encoding enc = GetEncoding(path);
            //StreamReader sr = new StreamReader(path, enc);

            //StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-1"));
           // StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-3"));
            
            
            const Int32 BufferSize = 128;
            StreamReader sr = new StreamReader(path, Encoding.UTF8, true, BufferSize);
            //StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-15"));
            //StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF7);
            //StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1252));
            //StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-1"));
            
            //string contenido = sr.ReadToEnd();

            string[] value; // = line.Split(';');
            string qt = "", q = "";
            int fila = 0, col = 0;
            while (!sr.EndOfStream)
            {
                value = sr.ReadLine().Split(';');
                if (fila == 0)
                {
                    qt = "If not exists (select name from sysobjects where name = '" + nombreTabla + "')";
                    qt = qt + " CREATE TABLE " + nombreTabla + "(";
                }
                q = "insert into " + nombreTabla + " values(";
                foreach (string dc in value)
                {
                    if (fila == 0)
                        qt = qt + "c" + col.ToString() + " varchar(1000),";
                    q = q + "'" + dc + "',";
                    col++;
                }

                if (fila == 0)
                {
                    qt = qt.Substring(0, qt.Length - 1) + ")";
                    this.ejecutarConsulta(qt);
                    qt = "delete from " + nombreTabla;
                    this.ejecutarConsulta(qt);
                }

                q = q.Substring(0, q.Length - 1) + ")";

                if (fila > 0)
                    this.ejecutarConsulta(q);

                fila++;
            }


        }

       
        public static string encriptar(string id)
        {
            string strEncrypted = "";
            string clave = DateTime.Today.Year.ToString() + MiUtil.right("00" + DateTime.Today.Month.ToString(), 2) + MiUtil.right("00" + DateTime.Today.Day.ToString(), 2);
            strEncrypted = MiUtil.Encrypt(id.ToString(), clave);
            strEncrypted = HttpUtility.UrlEncode(strEncrypted);
            strEncrypted= strEncrypted.Replace('%','!');
             //'%' with '!'.
            return strEncrypted;
        }

        public static string desEncriptar(string id)
        {
          string strDecrypted = "";
          string clave = DateTime.Today.Year.ToString() + MiUtil.right("00" + DateTime.Today.Month.ToString(), 2) + MiUtil.right("00" + DateTime.Today.Day.ToString(), 2);
          strDecrypted = id.Replace('!', '%');
          strDecrypted = HttpUtility.UrlDecode(strDecrypted);
          strDecrypted = Decrypt(strDecrypted, clave);
          return strDecrypted;
        }

        public static decimal? nullTodecimal(decimal? valor)
        {
            decimal? d = valor==null ? 0 : valor;
            return  d;
        }
        public static List<SelectListItem> getMeses(int? mesActual)
        {
            List<SelectListItem> meses = new List<SelectListItem>();
            meses.Add(new SelectListItem { Text = "Enero", Value = "1", Selected = 1 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Febrero", Value = "2", Selected = 2 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Marzo", Value = "3", Selected = 3 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Abril", Value = "4", Selected = 4 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Mayo", Value = "5", Selected = 5 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Junio", Value = "6", Selected = 6 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Julio", Value = "7", Selected = 7 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Agosto", Value = "8", Selected = 8 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Septiembre", Value = "9", Selected = 9 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Octubre", Value = "10", Selected = 10 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Noviembre", Value = "11", Selected = 11 == mesActual ? true : false });
            meses.Add(new SelectListItem { Text = "Diciembre", Value = "12", Selected = 12 == mesActual ? true : false });

            return meses;
        }

        public static List<SelectListItem> llenarCombo(CrediAdminContext db, string q, string xdefecto)
        {
            List<SelectListItem> combo = new List<SelectListItem>();
            var lista = db.Database.SqlQuery<string>(q).ToList();
            foreach (string s in lista)
            {
                if (s.Equals(xdefecto))
                    combo.Add(new SelectListItem { Text = (string)s, Value = (string)s, Selected = true });
                else
                    combo.Add(new SelectListItem { Text = (string)s, Value = (string)s, Selected = false });

            }
            return combo;
        }

        public static string getNombreMes(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                string nombreMes = formatoFecha.GetMonthName(numeroMes);
                return nombreMes.ToUpper();
            }
            catch
            {
                return "Desconocido";
            }
        } 

        public static string getNombreUsuario(int? usuarioId,string aspnetuserId)
        {
            CrediAdminContext db = new CrediAdminContext();
            usuario us=null;
            if (usuarioId!=null)
                if (usuarioId > 0)
                    us=db.usuario.Find(usuarioId);
                else
                    us = db.usuario.Where(u=>u.aspnetusersId.Equals(aspnetuserId)).FirstOrDefault();

            if (us != null)
                return us.UsuNombre;
            return "";
        }

    }
}