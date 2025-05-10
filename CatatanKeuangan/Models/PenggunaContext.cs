using CatatanKeuangan.Helpers;
using Npgsql;

namespace CatatanKeuangan.Models
{
    public class PenggunaContext
    {
        private string __constr;

        public PenggunaContext(string constr)
        {
            __constr = constr;
        }

        public List<Pengguna> ListPengguna()
        {
            List<Pengguna> listPengguna = new List<Pengguna>();
            string query = @"SELECT * FROM pengguna";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listPengguna.Add(new Pengguna()
                    {
                        id_pengguna = int.Parse(reader["id_pengguna"].ToString()),
                        nama_pengguna = reader["nama_pengguna"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return listPengguna;
        }

        public Pengguna GetPenggunaById(int id)
        {
            Pengguna pengguna = null;
            string query = @"SELECT * FROM pengguna WHERE id_pengguna = @id_pengguna";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pengguna", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pengguna = new Pengguna()
                    {
                        id_pengguna = int.Parse(reader["id_pengguna"].ToString()),
                        nama_pengguna = reader["nama_pengguna"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    };
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return pengguna;
        }

        public Pengguna GetPenggunaByEmail(string email)
        {
            Pengguna pengguna = null;
            string query = @"SELECT * FROM pengguna WHERE email = @email";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@email", email);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pengguna = new Pengguna()
                    {
                        id_pengguna = int.Parse(reader["id_pengguna"].ToString()),
                        nama_pengguna = reader["nama_pengguna"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    };
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return pengguna;
        }

        public bool CreatePengguna(Pengguna pengguna)
        {
            bool result = false;
            string query = @"INSERT INTO pengguna (nama_pengguna, email, password) VALUES (@nama_pengguna, @email, @password)";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama_pengguna", pengguna.nama_pengguna);
                cmd.Parameters.AddWithValue("@email", pengguna.email);
                cmd.Parameters.AddWithValue("@password", pengguna.password);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public bool UpdatePengguna(int id, Pengguna pengguna)
        {
            bool result = false;
            string query = @"UPDATE pengguna SET nama_pengguna = @nama_pengguna, email = @email, password = @password WHERE id_pengguna = @id_pengguna";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pengguna", id);
                cmd.Parameters.AddWithValue("@nama_pengguna", pengguna.nama_pengguna);
                cmd.Parameters.AddWithValue("@email", pengguna.email);
                cmd.Parameters.AddWithValue("@password", pengguna.password);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public bool DeletePengguna(int id)
        {
            bool result = false;
            string query = @"DELETE FROM pengguna WHERE id_pengguna = @id_pengguna";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pengguna", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle error properly
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
