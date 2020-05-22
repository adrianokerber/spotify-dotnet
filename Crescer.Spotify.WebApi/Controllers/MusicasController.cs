using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MusicasController : Controller
    {
        private IMusicaRepository musicaRepository;
        private IAlbumRepository albumRepository;
        private MusicaService musicaService;
        
        public MusicasController(IMusicaRepository musicaRepository, MusicaService musicaService, IAlbumRepository albumRepository)
        {
            this.musicaRepository = musicaRepository;
            this.musicaService = musicaService;
            this.albumRepository = albumRepository;            
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(musicaRepository.ListarMusicas());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(musicaRepository.Obter(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MusicaDto musicaRequest)
        {
            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            musicaRepository.SalvarMusica(musica);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MusicaDto musicaRequest)
        {
            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            musicaRepository.AtualizarMusica(id, musica);            
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            musicaRepository.DeletarMusica(id);
            return Ok();
        }

        private Musica MapearDtoParaDominio(MusicaDto musica)
        {
            var albumObtido = albumRepository.Obter(musica.IdAlbum);
            return new Musica(musica.Nome, musica.Duracao, albumObtido);
        }
    }
}