using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CatatanKeuangan.Helpers;
using CatatanKeuangan.Models;

namespace CatatanKeuangan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string __constr;
        private readonly IConfiguration _config;
        private readonly AesEncryptionHelper _aes;

        public AuthController(IConfiguration config, AesEncryptionHelper aes)
        {
            _config = config;
            __constr = _config.GetConnectionString("koneksi");
            _aes = aes;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Pengguna registerData)
        {
            if (string.IsNullOrEmpty(registerData.email) || string.IsNullOrEmpty(registerData.password))
                return BadRequest(new { message = "Email dan password harus diisi" });

            var context = new PenggunaContext(__constr);

            var existingUser = context.GetPenggunaByEmail(registerData.email);
            if (existingUser != null)
                return BadRequest(new { message = "Email sudah terdaftar" });

            registerData.password = _aes.Encrypt(registerData.password);

            bool isRegistered = context.CreatePengguna(registerData);

            if (isRegistered)
                return Ok(new { message = "Registrasi berhasil" });

            return StatusCode(500, new { message = "Registrasi gagal" });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login loginData)
        {
            if (string.IsNullOrEmpty(loginData.email) || string.IsNullOrEmpty(loginData.password))
                return BadRequest(new { message = "Email dan password harus diisi" });

            var context = new PenggunaContext(__constr);
            var pengguna = context.GetPenggunaByEmail(loginData.email);

            if (pengguna == null)
                return Unauthorized(new { message = "Email tidak ditemukan" });

            var decryptedPassword = _aes.Decrypt(pengguna.password);
            if (decryptedPassword != loginData.password)
                return Unauthorized(new { message = "Email atau password salah" });

            var jwtHelper = new JwtHelper(_config);
            var token = jwtHelper.GenerateToken(pengguna);

            return Ok(new
            {
                token,
                user = new
                {
                    id = pengguna.id_pengguna,
                    nama = pengguna.nama_pengguna,
                    email = pengguna.email
                }
            });
        }
    }
}
