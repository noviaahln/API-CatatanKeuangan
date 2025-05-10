using Microsoft.AspNetCore.Mvc;
using CatatanKeuangan.Models;

namespace CatatanKeuangan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PemasukanController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PemasukanController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/pemasukan
        [HttpGet]
        public ActionResult<IEnumerable<Pemasukan>> GetAll()
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PemasukanContext context = new PemasukanContext(connString);
            return Ok(context.ListPemasukan());
        }

        // POST: api/pemasukan
        [HttpPost]
        public ActionResult Create([FromBody] Pemasukan pemasukan)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PemasukanContext context = new PemasukanContext(connString);

            bool result = context.CreatePemasukan(pemasukan);

            if (result)
                return Ok(new { message = "Pemasukan berhasil ditambahkan." });
            else
                return BadRequest(new { message = "Gagal menambahkan pemasukan." });
        }

        // PUT: api/pemasukan/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Pemasukan pemasukan)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PemasukanContext context = new PemasukanContext(connString);

            bool result = context.UpdatePemasukan(id, pemasukan);

            if (result)
                return Ok(new { message = "Pemasukan berhasil diperbarui." });
            else
                return NotFound(new { message = "Pemasukan tidak ditemukan atau gagal diperbarui." });
        }

        // DELETE: api/pemasukan/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PemasukanContext context = new PemasukanContext(connString);

            bool result = context.DeletePemasukan(id);

            if (result)
                return Ok(new { message = "Pemasukan berhasil dihapus." });
            else
                return NotFound(new { message = "Pemasukan tidak ditemukan atau gagal dihapus." });
        }
    }
}
