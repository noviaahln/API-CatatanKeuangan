using CatatanKeuangan.Helpers;
using Npgsql;

namespace CatatanKeuangan.Models
{
    public class PemasukanContext
    {
        private string __constr;
        private string __errorMsqg;

        public PemasukanContext(string constr)
        {
            __constr = constr;
        }

        public List<Pemasukan> ListPemasukan()
        {
            List<Pemasukan> listPemasukan = new List<Pemasukan>();

            string query = @"SELECT * FROM pemasukan";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pemasukan pemasukan = new Pemasukan()
                    {
                        id_pemasukan = Convert.ToInt32(reader["id_pemasukan"]),
                        tanggal_pemasukan = Convert.ToDateTime(reader["tanggal_pemasukan"]),
                        jumlah_pemasukan = Convert.ToDecimal(reader["jumlah_pemasukan"]),
                        kategori_pemasukan = reader["kategori_pemasukan"].ToString()
                    };
                    listPemasukan.Add(pemasukan);
                }

                reader.Close();
                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return listPemasukan;
        }

        // CREATE - tambah pemasukan baru
        public bool CreatePemasukan(Pemasukan pemasukan)
        {
            bool result = false;
            string query = @"INSERT INTO pemasukan (tanggal_pemasukan, jumlah_pemasukan, kategori_pemasukan)
                             VALUES (@tanggal_pemasukan, @jumlah_pemasukan, @kategori_pemasukan)";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@tanggal_pemasukan", pemasukan.tanggal_pemasukan);
                cmd.Parameters.AddWithValue("@jumlah_pemasukan", pemasukan.jumlah_pemasukan);
                cmd.Parameters.AddWithValue("@kategori_pemasukan", pemasukan.kategori_pemasukan);

                result = cmd.ExecuteNonQuery() > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return result;
        }

        public bool UpdatePemasukan(int id, Pemasukan pemasukan)
        {
            bool result = false;
            string query = @"UPDATE pemasukan 
                             SET tanggal_pemasukan = @tanggal_pemasukan, 
                                 jumlah_pemasukan = @jumlah_pemasukan, 
                                 kategori_pemasukan = @kategori_pemasukan
                             WHERE id_pemasukan = @id_pemasukan";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@tanggal_pemasukan", pemasukan.tanggal_pemasukan);
                cmd.Parameters.AddWithValue("@jumlah_pemasukan", pemasukan.jumlah_pemasukan);
                cmd.Parameters.AddWithValue("@kategori_pemasukan", pemasukan.kategori_pemasukan);
                cmd.Parameters.AddWithValue("@id_pemasukan", id);

                result = cmd.ExecuteNonQuery() > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return result;
        }

        // DELETE - hapus pemasukan berdasarkan ID
        public bool DeletePemasukan(int id)
        {
            bool result = false;
            string query = @"DELETE FROM pemasukan WHERE id_pemasukan = @id_pemasukan";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pemasukan", id);
                result = cmd.ExecuteNonQuery() > 0;

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return result;
        }
    }
}
