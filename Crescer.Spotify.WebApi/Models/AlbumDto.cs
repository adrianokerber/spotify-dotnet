using System.Collections.Generic;

namespace Crescer.Spotify.WebApi.Models
{
    public class AlbumDto
    {
        public string Nome { get; set; }
        public List<int> IdsMusicas { get; set; }
    }
}