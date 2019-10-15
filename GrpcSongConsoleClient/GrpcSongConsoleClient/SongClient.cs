using Grpc.Net.Client;
using GrpcSongService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrpcSongConsoleClient
{
    public class SongClient
    {

        private readonly SongFinder.SongFinderClient _client;
        public SongClient(SongFinder.SongFinderClient client)
        {
            _client = client;
        }

        public async Task GetSongs()
        {
            var searchTerm = "";
            while (searchTerm != "exit")
            {
                Console.Write("Enter a search term for to get songs back: ");
                searchTerm = Console.ReadLine();

                var response = await _client.FindSongAsync(
                              new SongFinderRequest { Term = searchTerm });

                if (response.Songs is { Count: var count } && count > 0)
                {                  
                    foreach (var song in response.Songs)
                    {
                        Console.WriteLine($"Song Name: {song.SongName}, Reproductions: {song.Reproductions}");
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("No matches found \n");
                }


            }
        }

        
    }
}
