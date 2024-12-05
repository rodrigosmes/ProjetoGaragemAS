using Microsoft.AspNetCore.Mvc;
using ProjetoGaragemAS.Dtos;
using ProjetoGaragemAS.Models;
using ProjetoGaragemAS.Data;

namespace ProjetoGaragemAS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarroController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var carros = _context.Carros.ToList();
            return Ok(carros);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var carro = _context.Carros.Find(id);
            if (carro == null) return NotFound("Carro não encontrado.");
            return Ok(carro);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CarroCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var carro = new Carro
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Preco = dto.Preco
            };

            _context.Carros.Add(carro);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = carro.Id }, carro);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CarroUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var carro = _context.Carros.Find(id);
            if (carro == null) return NotFound("Carro não encontrado.");

            carro.Marca = dto.Marca;
            carro.Modelo = dto.Modelo;
            carro.Ano = dto.Ano;
            carro.Preco = dto.Preco;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carro = _context.Carros.Find(id);
            if (carro == null) return NotFound("Carro não encontrado.");

            _context.Carros.Remove(carro);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
