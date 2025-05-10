using Npgsql;
using System.Data;

namespace CatatanKeuangan.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public SqlDBHelper(string constr)
        {
            __constr = constr;
            connection = new NpgsqlConnection(__constr);
            connection.ConnectionString = __constr;
        }
        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public void CloseConnection()
        { 
            connection.Close();
        }
    }
}
