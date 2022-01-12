using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace beatsabermusic
{
    internal class Program
    {
        private static string RemoveSpecialCharacters(string str)
        {
            var stringBuilder = new StringBuilder();
            foreach (var c in str.Where(c => c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '_' or ' '))
                stringBuilder.Append(c);
            return stringBuilder.ToString();
        }
        
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your custom song folder.");
                var CustomSongPath = Console.ReadLine();
                Console.WriteLine("Please enter the folder where your want your songs to be put in. (dangerous, be careful and put the CORRECT folder please)");
                var SongFolderPath = Console.ReadLine();
                int Fails = 0;

                foreach (var Diretory in Directory.GetDirectories(CustomSongPath))
                { 
                    try
                    {
                        var MapInfo = new Info();
                        using (var stream = new StreamReader($"{Diretory}\\Info.dat") )
                        {
                            MapInfo = JsonConvert.DeserializeObject<Info>(stream.ReadToEnd());
                        }
                        
                        MapInfo._songName = RemoveSpecialCharacters(MapInfo._songName);
                        Directory.CreateDirectory($"{SongFolderPath}\\{MapInfo._songAuthorName}\\");
                        if (File.Exists($"{SongFolderPath}\\{MapInfo._songAuthorName}\\{MapInfo._songName}.ogg"))
                            continue;
                        
                        File.Copy($"{Diretory}/{MapInfo._songFilename}", $"{SongFolderPath}\\{MapInfo._songAuthorName}\\{MapInfo._songName}.ogg");
                        Console.WriteLine($"{MapInfo._songName} - {MapInfo._songAuthorName}");
                    }
                    catch (Exception e)
                    {
                        Fails++;
                        Console.WriteLine($"A music couldn't be saved ({Fails})");
                    }
                }
        }
    }

    class Info
    {
        public string _songName { get; set; }
        public string _songAuthorName { get; set; }
        public string _songFilename { get; set; }
    }
    
}