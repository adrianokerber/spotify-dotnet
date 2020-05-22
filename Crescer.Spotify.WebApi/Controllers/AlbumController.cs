using System.Collections.Generic;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private IAlbumRepository albumRepository;
        private IMusicaRepository musicaRepository;
        private AlbumService albumService;
        public AlbumController(IAlbumRepository albumRepository, IMusicaRepository musicaRepository, AlbumService albumService)
        {
            this.albumRepository = albumRepository;
            this.musicaRepository = musicaRepository;
            this.albumService = albumService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(albumRepository.Obter(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumRepository.SalvarAlbum(album);            
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
               return BadRequest(mensagens);

            albumRepository.AtualizarAlbum(id, album);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            albumRepository.DeletarAlbum(id);
            return Ok();
        }

        private Album MapearDtoParaDominio(AlbumDto album)
        {
            List<Musica> musicas = musicaRepository.ListarMusicas(album.IdsMusicas);            
            return new Album(album.Nome, musicas);
        }

    }
}