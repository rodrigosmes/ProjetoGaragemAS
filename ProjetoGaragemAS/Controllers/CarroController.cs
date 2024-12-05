using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            var carros = await _context.Carros.ToListAsync();
            return Ok(carros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro == null) return NotFound("Carro não encontrado.");
            return Ok(carro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarroCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var carro = new Carro
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Preco = dto.Preco
            };

            await _context.Carros.AddAsync(carro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = carro.Id }, carro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarroUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var carro = await _context.Carros.FindAsync(id);
            if (carro == null) return NotFound("Carro não encontrado.");

            carro.Marca = dto.Marca;
            carro.Modelo = dto.Modelo;
            carro.Ano = dto.Ano;
            carro.Preco = dto.Preco;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro == null) return NotFound("Carro não encontrado.");

            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
