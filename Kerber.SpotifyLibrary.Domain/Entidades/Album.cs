using System.Collections.Generic;

namespace Kerber.SpotifyLibrary.Domain.Entidades
{
    public class Album
    {
        public Album(string nome, string id = default)
        {
            this.Nome = nome;
            this.Id = id;
        }
        
        public Album(string nome, List<Musica> musicas, string id = default)
        {
            this.Nome = nome;
            this.Musicas = musicas;
            this.Id = id;
        }

        public string Id { get; set; }

        public List<Musica> Musicas { get; private set; }

        public string Nome { get; private set; }

        public void Atualizar(Album album)
        {
            Musicas = album.Musicas;
            Nome = album.Nome;
        }

        public void Deconstruct(out string id, out List<Musica> musicas, out string nome)
        {
            id = this.Id;
            musicas = this.Musicas;
            nome = this.Nome;
        }
    }
}