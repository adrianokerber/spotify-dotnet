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
    public class MusicasController : Controller
    {
        private readonly IMusicaRepository _musicaRepository;
        private readonly MusicaService _musicaService;
        private readonly ILogger<MusicasController> _logger;

        public MusicasController(IMusicaRepository musicaRepository, MusicaService musicaService, ILogger<MusicasController> logger)
        {
            _musicaRepository = musicaRepository;
            _musicaService = musicaService;
            _logger = logger;
        }

        // GET api/musicas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var musicas = _musicaRepository.ListarMusicas();

            _logger.LogInformation("Músicas listadas {@Songs}", musicas);

            return Ok(musicas);
        }

        // GET api/musicas/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var musica = _musicaRepository.Obter(id);

            _logger.LogInformation("Música obtida {@Song}", musica);

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
            var musica = musicaRequest.MapearDtoParaDominio();
            var mensagens = _musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            _musicaRepository.SalvarMusica(musica);
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
            var musica = musicaRequest.MapearDtoParaDominio();
            var mensagens = _musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var musicaSalva = _musicaRepository.Obter(id);
            if (musicaSalva == null)
                return NotFound();

            musica.Id = musicaSalva.Id;

            _musicaRepository.AtualizarMusica(id, musica);
            return Ok();
        }

        // DELETE api/musicas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            // TODO: add notfound behaviour from DB
            _musicaRepository.DeletarMusica(id);
            return NoContent();
        }
    }
}