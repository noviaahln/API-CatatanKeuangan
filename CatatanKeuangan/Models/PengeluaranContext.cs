using CatatanKeuangan.Helpers;
using Npgsql;

namespace CatatanKeuangan.Models
{
    public class PengeluaranContext
    {
        private string __constr;
        private string __errorMsqg;

        public PengeluaranContext(string constr)
        {
            __constr = constr;
        }

        public List<Pengeluaran> ListPengeluaran()
        {
            List<Pengeluaran> listPengeluaran = new List<Pengeluaran>();

            string query = @"SELECT * FROM pengeluaran";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pengeluaran pengeluaran = new Pengeluaran()
                    {
                        id_pengeluaran = Convert.ToInt32(reader["id_pengeluaran"]),
                        tanggal_pengeluaran = Convert.ToDateTime(reader["tanggal_pengeluaran"]),
                        jumlah_pengeluaran = Convert.ToDecimal(reader["jumlah_pengeluaran"]),
                        kategori_pengeluaran = reader["kategori_pengeluaran"].ToString()
                    };
                    listPengeluaran.Add(pengeluaran);
                }

                reader.Close();
                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return listPengeluaran;
        }

        // CREATE - tambah pengeluaran baru
        public bool CreatePengeluaran(Pengeluaran pengeluaran)
        {
            bool result = false;
            string query = @"INSERT INTO pengeluaran (tanggal_pengeluaran, jumlah_pengeluaran, kategori_pengeluaran)
                             VALUES (@tanggal_pengeluaran, @jumlah_pengeluaran, @kategori_pengeluaran)";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@tanggal_pengeluaran", pengeluaran.tanggal_pengeluaran);
                cmd.Parameters.AddWithValue("@jumlah_pengeluaran", pengeluaran.jumlah_pengeluaran);
                cmd.Parameters.AddWithValue("@kategori_pengeluaran", pengeluaran.kategori_pengeluaran);

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

        public bool UpdatePengeluaran(int id, Pengeluaran pengeluaran)
        {
            bool result = false;
            string query = @"UPDATE pengeluaran 
                             SET tanggal_pengeluaran = @tanggal_pengeluaran, 
                                 jumlah_pengeluaran = @jumlah_pengeluaran, 
                                 kategori_pengeluaran = @kategori_pengeluaran
                             WHERE id_pengeluaran = @id_pengeluaran";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@tanggal_pengeluaran", pengeluaran.tanggal_pengeluaran);
                cmd.Parameters.AddWithValue("@jumlah_pengeluaran", pengeluaran.jumlah_pengeluaran);
                cmd.Parameters.AddWithValue("@kategori_pengeluaran", pengeluaran.kategori_pengeluaran);
                cmd.Parameters.AddWithValue("@id_pengeluaran", id);

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

        // DELETE - hapus pengeluaran berdasarkan ID
        public bool DeletePengeluaran(int id)
        {
            bool result = false;
            string query = @"DELETE FROM pengeluaran WHERE id_pengeluaran = @id_pengeluaran";
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pengeluaran", id);
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
