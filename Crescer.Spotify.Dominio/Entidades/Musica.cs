using System;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Musica
    {
        public Musica() { }
        public Musica(string nome, double duracao)
        {
            this.Nome = nome;
            this.Duracao = duracao;
        }
        public Musica(string nome, double duracao, Album album)
        {
            this.Nome = nome;
            this.Duracao = duracao;
            this.Album = album;
        }

        public int Id { get; set; }
        public string Nome { get; private set; }
        public double Duracao { get; private set; }
        public Album Album { get; set; }

        public void Atualizar(Musica musica)
        {
            Nome = musica.Nome;
            Duracao = musica.Duracao;
            Album = musica.Album;
        }
    }
}