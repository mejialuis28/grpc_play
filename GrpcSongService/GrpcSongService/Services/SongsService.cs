
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSongService
{
    public class SongsService : SongFinder.SongFinderBase
    {

        private static RepeatedField<Song> songs = new RepeatedField<Song>
        {
            new Song {SongName = "lonely day", Reproductions = 200},
            new Song {SongName = "the day that never comes", Reproductions = 100},
            new Song {SongName = "the last day", Reproductions = 50}
        };

        private readonly ILogger<SongsService> _logger;
        public SongsService(ILogger<SongsService> logger) => _logger = logger;        

        public override Task<SongFinderReply> FindSong(SongFinderRequest request, ServerCallContext context)
        {
            var songNames = songs.Where(s => 
                s.SongName.Contains(request.Term, System.StringComparison.OrdinalIgnoreCase));
            var reply = new SongFinderReply();
            reply.Songs.AddRange(songNames);
            return Task.FromResult(reply);
        }
    }
}