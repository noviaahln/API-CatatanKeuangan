using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CatatanKeuangan.Helpers;
using CatatanKeuangan.Models;

namespace CatatanKeuangan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly string __constr;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
            __constr = _config.GetConnectionString("koneksi");
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Pengguna registerData)
        {
            if (string.IsNullOrEmpty(registerData.email) || string.IsNullOrEmpty(registerData.password))
            {
                return BadRequest(new { message = "Email dan password harus diisi" });
            }

            PenggunaContext context = new PenggunaContext(__constr);

            var existingUser = context.GetPenggunaByEmail(registerData.email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email sudah terdaftar" });
            }

            bool isRegistered = context.CreatePengguna(registerData);

            if (isRegistered)
            {
                return Ok(new { message = "Registrasi berhasil" });
            }
            else
            {
                return StatusCode(500, new { message = "Registrasi gagal" });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login loginData)
        {
            if (string.IsNullOrEmpty(loginData.email) || string.IsNullOrEmpty(loginData.password))
            {
                return BadRequest(new { message = "Email dan password harus diisi" });
            }

            PenggunaContext penggunaContext = new PenggunaContext(__constr);
            var pengguna = penggunaContext.GetPenggunaByEmail(loginData.email);

            if (pengguna == null || pengguna.password != loginData.password)
            {
                return Unauthorized(new { message = "Email atau password salah" });
            }

            JwtHelper jwtHelper = new JwtHelper(_config);
            var token = jwtHelper.GenerateToken(pengguna);

            return Ok(new
            {
                token = token,
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
