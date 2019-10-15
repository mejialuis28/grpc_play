using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcSongService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcWebClient.Controllers
{
    [ApiController]
    public class SongsController : ControllerBase
    {
 
        private readonly ILogger<SongsController> _logger;

        public SongsController(ILogger<SongsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/songs/{term}")]
        public async Task<SongFinderReply> GetSongs(string term)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:44347");
            var client = new SongFinder.SongFinderClient(channel);
            return await client.FindSongAsync(new SongFinderRequest { Term = term });
        }
    }
}
