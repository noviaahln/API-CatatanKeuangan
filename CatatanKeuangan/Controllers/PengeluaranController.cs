using Microsoft.AspNetCore.Mvc;
using CatatanKeuangan.Models;

namespace CatatanKeuangan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PengeluaranController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PengeluaranController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/pengeluaran
        [HttpGet]
        public ActionResult<IEnumerable<Pengeluaran>> GetAll()
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PengeluaranContext context = new PengeluaranContext(connString);
            return Ok(context.ListPengeluaran());
        }

        // POST: api/pengeluaran
        [HttpPost]
        public ActionResult Create([FromBody] Pengeluaran pengeluaran)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PengeluaranContext context = new PengeluaranContext(connString);

            bool result = context.CreatePengeluaran(pengeluaran);

            if (result)
                return Ok(new { message = "Pengeluaran berhasil ditambahkan." });
            else
                return BadRequest(new { message = "Gagal menambahkan pengeluaran." });
        }

        // PUT: api/pengeluaran/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Pengeluaran pengeluaran)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PengeluaranContext context = new PengeluaranContext(connString);

            bool result = context.UpdatePengeluaran(id, pengeluaran);

            if (result)
                return Ok(new { message = "Pengeluaran berhasil diperbarui." });
            else
                return NotFound(new { message = "Pengeluaran tidak ditemukan atau gagal diperbarui." });
        }

        // DELETE: api/pengeluaran/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PengeluaranContext context = new PengeluaranContext(connString);

            bool result = context.DeletePengeluaran(id);

            if (result)
                return Ok(new { message = "Pengeluaran berhasil dihapus." });
            else
                return NotFound(new { message = "Pengeluaran tidak ditemukan atau gagal dihapus." });
        }
    }
}
