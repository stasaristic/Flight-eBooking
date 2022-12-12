using Microsoft.Data.SqlClient;
using System.Data;


namespace Flight_eBooking.Data
{
    public class DbFunctions
    {

        private SqlConnection Conn;
        private SqlCommand cmd;
        private DataTable dt;
        private string ConnStr;
        private SqlDataAdapter sda;
        private SqlDataReader reader;


        public DbFunctions()
        {
            ConnStr = "Data Source=DESKTOP-S851O4Q;Initial Catalog=Flight-eBooking-Db;Integrated Security=True;Pooling=False;Trusted_Connection=True;MultipleActiveResultSets=true";
            Conn = new SqlConnection(ConnStr);
            cmd = new SqlCommand();
            cmd.Connection = Conn;
        }

        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConnStr);
            sda.Fill(dt);
            return dt;
        }

        public int SetData(string Query)
        {
            int rcnt = 0;
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            cmd.CommandText = Query;
            rcnt = cmd.ExecuteNonQuery();
            Conn.Close();
            return rcnt;
        }

        public int GetCount(string Query)
        {
            Int32 count = 0;
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            cmd.CommandText = Query;
            count = (Int32)cmd.ExecuteScalar();
            Conn.Close();
            return count;
        }

        public string ReadData(string Query, string result)
        {

            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            cmd.CommandText = Query;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //result = reader.GetString();
            }

            return result;
        }
    }
}
