using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace WebApi.Controller
{
    [ApiController]
    [Route("api/v1/estudantes")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly PasswordHasher<Student> _passwordHasher;

        public StudentController(IStudentRepository estudanteRepository)
        {
            _studentRepository = estudanteRepository;
            _passwordHasher = new PasswordHasher<Student>();
        }

        private string GenerateToken(int userId, string email)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("Ab3Z9kM2pQ7xT4Na8LR5cD6EfJ1SYm0HUwBV9tKePG3Zd2CA7n")
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Email, email)
    };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpPost]
        public IActionResult Post([FromBody] StudentViewModel studentViewModel) { 

            Student student = new Student(studentViewModel.Name, studentViewModel.Email, studentViewModel.Password, studentViewModel.CreationDate);
            student.Password = _passwordHasher.HashPassword(student, student.Password);
            _studentRepository.Create(student);
            var token = GenerateToken(student.Id, student.Email);

            return Ok(token);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_studentRepository.FindAll());
        }

        [HttpGet("login")]
        public IActionResult Login(
            [FromQuery] string email,
            [FromQuery] string password)
        {
            var student = _studentRepository
                .FindAll()
                .FirstOrDefault(e => e.Email == email);

            if (student == null)
                return Unauthorized("Usuario não encontrado"); 

            var result = _passwordHasher.VerifyHashedPassword(
                student,
                student.Password,
                password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Não autorizado, Senha incorreta.");

            var token = GenerateToken(student.Id, student.Email);

            return Ok(token);
        }
    }
}
