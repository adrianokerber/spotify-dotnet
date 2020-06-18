using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Mappers;
using Crescer.Spotify.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbunsController : Controller
    {
        private IAlbumRepository albumRepository;
        private AlbumService albumService;
        public AlbunsController(IAlbumRepository albumRepository, AlbumService albumService)
        {
            this.albumRepository = albumRepository;
            this.albumService = albumService;
        }

        // GET api/albuns
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(albumRepository.ListarAlbuns());
        }

        // GET api/albuns/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var album = albumRepository.Obter(id);
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
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumService.CriarAlbum(album);

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
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var albumSalvo = albumRepository.Obter(id);
            if (albumSalvo == null)
                return NotFound();

            albumService.AtualizarAlbum(id, album);

            return Ok();
        }

        // DELETE api/albuns/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            // TODO: add notfound behaviour from DB
            albumRepository.DeletarAlbum(id);
            return NoContent();
        }
    }
}