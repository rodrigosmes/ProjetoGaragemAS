using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var pessoas = _context.Pessoas.ToList();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pessoa = _context.Pessoas.Find(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");
            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PessoaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PessoaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pessoa = _context.Pessoas.Find(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            pessoa.Nome = dto.Nome;
            pessoa.Email = dto.Email;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = _context.Pessoas.Find(id);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{idPessoa}/favoritos/{idCarro}")]
        public IActionResult AddFavorite(int idPessoa, int idCarro)
        {
            var pessoa = _context.Pessoas.Find(idPessoa);
            var carro = _context.Carros.Find(idCarro);

            if (pessoa == null || carro == null)
                return NotFound("Pessoa ou carro não encontrado.");

            if (_context.Favoritos.Any(f => f.PessoaId == idPessoa && f.CarroId == idCarro))
                return BadRequest("Este carro já está nos favoritos.");

            _context.Favoritos.Add(new PessoaCarroFavorito { PessoaId = idPessoa, CarroId = idCarro });
            _context.SaveChanges();
            return Ok("Carro adicionado aos favoritos.");
        }

        [HttpDelete("{idPessoa}/favoritos/{idCarro}")]
        public IActionResult RemoveFavorite(int idPessoa, int idCarro)
        {
            var favorito = _context.Favoritos.FirstOrDefault(f => f.PessoaId == idPessoa && f.CarroId == idCarro);
            if (favorito == null) return NotFound("Favorito não encontrado.");

            _context.Favoritos.Remove(favorito);
            _context.SaveChanges();
            return Ok("Carro removido dos favoritos.");
        }

        [HttpGet("{idPessoa}/favoritos")]
        public IActionResult GetFavorites(int idPessoa)
        {
            var pessoa = _context.Pessoas.Find(idPessoa);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            var favoritos = _context.Favoritos
                .Where(f => f.PessoaId == idPessoa)
                .Select(f => f.Carro)
                .ToList();

            return Ok(favoritos);
        }
    }
}
