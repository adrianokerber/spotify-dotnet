using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Models;
using Microsoft.AspNetCore.Http;
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
        // GET api/musicas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(musicaRepository.ListarMusicas());
        }

        // GET api/musicas/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var musica = musicaRepository.Obter(id);
            if (musica == null)
                return NotFound();

            return Ok(musica);
        }

        // POST api/musicas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] MusicaDto musicaRequest)
        {
            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            musicaRepository.SalvarMusica(musica);
            return StatusCode(201);
        }

        // PUT api/musicas/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(string id, [FromBody] MusicaDto musicaRequest)
        {
            // TODO: evaluate if we should return the updated object
            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var musicaSalva = musicaRepository.Obter(id);
            if (musicaSalva == null)
                return NotFound();

            musica.Id = musicaSalva.Id;

            musicaRepository.AtualizarMusica(id, musica);
            return Ok();
        }

        // DELETE api/musicas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            // TODO: add notfound behaviour from DB
            musicaRepository.DeletarMusica(id);
            return NoContent();
        }

        private Musica MapearDtoParaDominio(MusicaDto musica)
        {
            var albumObtido = albumRepository.Obter(musica.IdAlbum);
            return new Musica(musica.Nome, musica.Duracao, albumObtido);
        }
    }
}