using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_nhom
{
    public class Database
    {
        private string connectionString = @"Data Source=DESKTOP-ES9SF4I\SQLEXPRESS;Initial Catalog=QL_PhongTro;Integrated Security=True";
        private SqlConnection conn;
        private DataTable dt;
        private SqlCommand cmd;

        public Database()
        {
            try
            {
                conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connected failed:" + ex.Message);
            }
        }
        //3hamdungcho moithemsuaxoa
        public DataTable SelectData(string sql, List<CustomParamefer> lstPara = null)
        {
            try
            {
                conn.Open();//open ketnoi
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;//set command type cho cnd
                if (lstPara != null)
                {
                    foreach (var para in lstPara)//gán các tham số cho cmd
                    {
                        cmd.Parameters.AddWithValue(para.key, para.value);
                    }

                }
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loii load du lieu: " + ex.Message);
                return null;
            }

            finally
            {
                conn.Close();
            }
        }
        public int ExeCute(string sql, List<CustomParamefer> lstPara = null)
        {
            try
            {
                //căn sửa lại hàm execute như sau
                //string sql, List<CustomParameter> IstPara la tham so truyền vao
                //CustomParameter đã được định nghĩa ở phần trước- Xem lại part 3
                conn.Open();//mở kết nõi
                cmd = new SqlCommand(sql, conn);//thực thi câu lệnh sql
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var p in lstPara)//gần các tham số cho cmd
                {
                    cmd.Parameters.AddWithValue(p.key, p.value);
                }
                var rs = cmd.ExecuteNonQuery();//lãy kết quả thực thi truy văn
                return (int)rs;//trã về kết quả
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi thực thi câu lệnh: " + ex.Message);
                return -1;
            }
            finally
            {

                conn.Close();//cuối cùng đống kết nõi
            }
        }

        public DataRow Select(string sql)
        {
            try
            {
                conn.Open();//mở kết nối
                cmd = new SqlCommand(sql, conn);//truyền giá trị vào cmd
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());//thực thi câu lệnh
                return dt.Rows[0];//trà vễ kết quà
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi load thông tin chi tiềt: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();//cuối cùng đông kết nõi
            }
        }
    }
    public class CustomParamefer
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
