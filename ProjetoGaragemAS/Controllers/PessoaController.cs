using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoGaragemAS.Dtos;
using ProjetoGaragemAS.Models;
using ProjetoGaragemAS.Data;

namespace ProjetoGaragemAS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PessoaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");
            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PessoaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PessoaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            pessoa.Nome = dto.Nome;
            pessoa.Email = dto.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{idPessoa}/favoritos/{idCarro}")]
        public async Task<IActionResult> AddFavorite(int idPessoa, int idCarro)
        {
            var pessoa = await _context.Pessoas.FindAsync(idPessoa);
            var carro = await _context.Carros.FindAsync(idCarro);

            if (pessoa == null || carro == null)
                return NotFound("Pessoa ou carro não encontrado.");

            if (await _context.Favoritos.AnyAsync(f => f.PessoaId == idPessoa && f.CarroId == idCarro))
                return BadRequest("Este carro já está nos favoritos.");

            await _context.Favoritos.AddAsync(new PessoaCarroFavorito { PessoaId = idPessoa, CarroId = idCarro });
            await _context.SaveChangesAsync();
            return Ok("Carro adicionado aos favoritos.");
        }

        [HttpDelete("{idPessoa}/favoritos/{idCarro}")]
        public async Task<IActionResult> RemoveFavorite(int idPessoa, int idCarro)
        {
            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.PessoaId == idPessoa && f.CarroId == idCarro);

            if (favorito == null) return NotFound("Favorito não encontrado.");

            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
            return Ok("Carro removido dos favoritos.");
        }

        [HttpGet("{idPessoa}/favoritos")]
        public async Task<IActionResult> GetFavorites(int idPessoa)
        {
            var pessoa = await _context.Pessoas.FindAsync(idPessoa);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            var favoritos = await _context.Favoritos
                .Where(f => f.PessoaId == idPessoa)
                .Select(f => f.Carro)
                .ToListAsync();

            return Ok(favoritos);
        }
    }
}
