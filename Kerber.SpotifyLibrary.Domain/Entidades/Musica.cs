using System;

namespace Kerber.SpotifyLibrary.Domain.Entidades
{
    public class Musica
    {
        public Musica(string nome, double duracao, string id = default)
        {
            this.Nome = nome;
            this.Duracao = duracao;
            this.Id = id;
        }

        public string Id { get; set; }
        public string Nome { get; private set; }
        public double Duracao { get; private set; }

        public void Atualizar(Musica musica)
        {
            Nome = musica.Nome;
            Duracao = musica.Duracao;
        }
    }
}