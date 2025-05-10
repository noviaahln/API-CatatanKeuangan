using Microsoft.AspNetCore.Mvc;
using CatatanKeuangan.Models;

namespace CatatanKeuangan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenggunaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PenggunaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/pengguna
        [HttpGet]
        public ActionResult<IEnumerable<Pengguna>> GetAll()
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);
            return Ok(context.ListPengguna());
        }

        // GET: api/pengguna/{id}
        [HttpGet("{id}")]
        public ActionResult<Pengguna> GetById(int id)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);

            var pengguna = context.ListPengguna().FirstOrDefault(p => p.id_pengguna == id);
            if (pengguna == null)
                return NotFound(new { message = "Pengguna tidak ditemukan" });

            return Ok(pengguna);
        }

        // GET: api/pengguna/email/{email}
        [HttpGet("email/{email}")]
        public ActionResult<Pengguna> GetByEmail(string email)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);

            var pengguna = context.GetPenggunaByEmail(email);
            if (pengguna == null)
                return NotFound(new { message = "Pengguna tidak ditemukan" });

            return Ok(pengguna);
        }

        // POST: api/pengguna
        [HttpPost]
        public ActionResult Create([FromBody] Pengguna pengguna)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);

            bool result = context.CreatePengguna(pengguna);

            if (result)
                return Ok(new { message = "Pengguna berhasil ditambahkan." });
            else
                return BadRequest(new { message = "Gagal menambahkan pengguna." });
        }

        // PUT: api/pengguna/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Pengguna pengguna)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);

            bool result = context.UpdatePengguna(id, pengguna);

            if (result)
                return Ok(new { message = "Pengguna berhasil diperbarui." });
            else
                return NotFound(new { message = "Pengguna tidak ditemukan atau gagal diperbarui." });
        }

        // DELETE: api/pengguna/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);

            bool result = context.DeletePengguna(id);

            if (result)
                return Ok(new { message = "Pengguna berhasil dihapus." });
            else
                return NotFound(new { message = "Pengguna tidak ditemukan atau gagal dihapus." });
        }
    }
}
