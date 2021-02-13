using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Kerber.SpotifyLibrary.WebApi.Mappers;
using Kerber.SpotifyLibrary.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kerber.SpotifyLibrary.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbunsController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly AlbumService _albumService;
        private readonly ILogger<AlbunsController> _logger;

        public AlbunsController(IAlbumRepository albumRepository, AlbumService albumService, ILogger<AlbunsController> logger)
        {
            _albumRepository = albumRepository;
            _albumService = albumService;
            _logger = logger;
        }

        // GET api/albuns
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var albuns = _albumRepository.ListarAlbuns();

            _logger.LogInformation("Álbuns listados {@Albums}", albuns);

            return Ok(albuns);
        }

        // GET api/albuns/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var album = _albumRepository.Obter(id);

            _logger.LogInformation("Álbum obtido {@Album}", album);

            if (album == null)
                return NotFound();

            return Ok(album);
        }

        // POST api/albuns
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AlbumDto albumRequest)
        {
            var album = albumRequest.MapearDtoParaDominio();
            var mensagens = _albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            _albumService.CriarAlbum(album);

            return StatusCode(201);
        }

        // PUT api/albuns/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(string id, [FromBody] AlbumDto albumRequest)
        {
            var album = albumRequest.MapearDtoParaDominio();
            var mensagens = _albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var albumSalvo = _albumRepository.Obter(id);
            if (albumSalvo == null)
                return NotFound();

            _albumService.AtualizarAlbum(id, album);

            return Ok();
        }

        // DELETE api/albuns/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            // TODO: add notfound behaviour from DB
            _albumRepository.DeletarAlbum(id);
            return NoContent();
        }
    }
}