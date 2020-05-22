using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {
        private static List<Musica> musicas = new List<Musica>();
        private static int idMusica = 1;

        public void AtualizarMusica(int id, Musica musica)
        {
            var musicaObtida = Obter(id);
            musicaObtida?.Atualizar(musica);
        }

        public void DeletarMusica(int id)
        {
            var musica = this.Obter(id);
            musicas.Remove(musica);
        }

        public List<Musica> ListarMusicas()
        {
            return musicas;
        }

        public List<Musica> ListarMusicas(List<int> idsMusica)
        {
            return musicas.Where(x => idsMusica.Contains(x.Id)).ToList();
        }

        public Musica Obter(int id)
        {
            return musicas.Where(x => x.Id == id).FirstOrDefault();
        }

        public void SalvarMusica(Musica musica)
        {
            musica.Id = idMusica++;
            musicas.Add(musica);
        }
    }
}