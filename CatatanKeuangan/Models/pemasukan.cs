namespace CatatanKeuangan.Models
{
    public class Pemasukan
    {
        public int id_pemasukan { get; set; }
        public DateTime tanggal_pemasukan { get; set; }
        public Decimal jumlah_pemasukan { get; set; }
        public string kategori_pemasukan { get; set; }
    }
}
