using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication1
{
    public class Cls_DataLayer
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private string connecstring = "";
        private SqlDataAdapter da;


        #region member
        private string servername = "";

        public string Servername
        {
            get { return servername; }
            set { servername = value; }
        }
        private string databasename = "";

        public string Databasename
        {
            get { return databasename; }
            set { databasename = value; }
        }
        private string uid = "";

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        private string pwd = "";

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        private int demsothuoctinh = 0;
        #endregion

        public int Demsothuoctinh
        {
            get { return demsothuoctinh; }
            set { demsothuoctinh = value; }
        }
        //constructor
        public Cls_DataLayer(string path)
        {
            docchuoiketnoitufile(path);
            cnn = new SqlConnection(connecstring);
        }

        public Cls_DataLayer()
        {
            // TODO: Complete member initialization
        }
        //methed
        private void NoiCacThuocTinhChuoiKetNoi()
        {
            if (!string.IsNullOrEmpty(servername))
                connecstring += "server=" + servername;
            if (!string.IsNullOrEmpty(databasename))
                connecstring += ";database=" + databasename;
            if (demsothuoctinh > 2)
            {
                if (!string.IsNullOrEmpty(uid))
                    connecstring += ";uid=" + uid;
                if (!string.IsNullOrEmpty(pwd))
                    connecstring += ";pwd=" + pwd;
            }
            else
            {
                connecstring += ";integrated security=true";
            }
        }
        public void docchuoiketnoitufile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Substring(0, line.IndexOf('=')).ToLower())
                    {
                        case "servername":
                            servername = line.Substring(line.IndexOf('=') + 1);
                            demsothuoctinh++;
                            break;
                        case "database":
                            databasename = line.Substring(line.IndexOf('=') + 1);
                            demsothuoctinh++;
                            break;
                        case "userid":
                            uid = line.Substring(line.IndexOf('=') + 1);
                            demsothuoctinh++;
                            break;
                        case "password":
                            pwd = line.Substring(line.IndexOf('=') + 1);
                            demsothuoctinh++;
                            break;
                    }
                }
                NoiCacThuocTinhChuoiKetNoi();
            }
        }
        public bool kiemtraketnoi(ref string err)
        {
            bool kq = false;
            try
            {
                cnn.Open();
                kq = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return kq;
        }


        public SqlDataReader MyExcuteReader(ref string err, string sql, CommandType ct, params SqlParameter[] param)
        {
            SqlDataReader _reader = null;
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 600;
                cmd.CommandType = ct;
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                _reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return _reader;
        }

        public DataTable GetDataTable(ref string err, string sql, CommandType ct, params SqlParameter[] param)
        {
            DataTable dt = null;
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 600;
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return dt;
        }
        public DataSet GetDataSet(ref string err, string sql, string tenbang, CommandType ct, params SqlParameter[] param)
        {

            DataSet ds = new DataSet();
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 600;
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }


                da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return ds;
        }
        public object GetValue(ref string err, string sql, CommandType ct, params SqlParameter[] param)
        {
            object _obj = null;
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 600;
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                _obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return _obj;
        }
        public bool MyExcuteNonQuery(ref string err, string sql, CommandType ct, params SqlParameter[] param)
        {
            bool kq = false;
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 600;
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                cmd.ExecuteNonQuery();
                kq = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return kq;
        }
        public bool MyExcuteNonQuery(ref string err, ref int sodong, string sql, CommandType ct, params SqlParameter[] param)
        {
            bool kq = false;
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 600;
                if (param != null)
                {
                    foreach (SqlParameter item in param)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                sodong = cmd.ExecuteNonQuery();
                kq = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return kq;
        }
    }
}
