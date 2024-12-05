﻿namespace ProjetoGaragemAS.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<PessoaCarroFavorito> Favoritos { get; set; } = new List<PessoaCarroFavorito>();
    }

}
