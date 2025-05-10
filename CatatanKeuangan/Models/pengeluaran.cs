namespace CatatanKeuangan.Models
{
    public class Pengeluaran
    {
        public int id_pengeluaran { get; set; }
        public DateTime tanggal_pengeluaran { get; set; }
        public Decimal jumlah_pengeluaran { get; set; }
        public string kategori_pengeluaran { get; set; }
    }
}
