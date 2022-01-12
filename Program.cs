using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace beatsabermusic
{
    internal class Program
    {
        public static string RemoveSpecialCharacters(string p_Str)
        {
            StringBuilder l_SB = new StringBuilder();
            foreach (char l_C in p_Str)
                if (l_C is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '_' or ' ')
                    l_SB.Append(l_C);

            return l_SB.ToString();
        }
        
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your custom song folder.");
                var CustomSongPath = Console.ReadLine();
                //var CustomSongPath = @"C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Beat Saber_Data\CustomLevels";
                Console.WriteLine("Please enter the folder where your want your songs to be put in. (dangerous, be careful and put the CORRECT folder please)");
                var SongFolderPath = Console.ReadLine();
                //var SongFolderPath = @"C:\Users\Julie\3D Objects\songs";

                
                foreach (var i in Directory.GetDirectories(CustomSongPath))
                { 
                    try
                    {
                        var a = new Info();
                        using (var stream = new StreamReader($"{i}\\Info.dat") )
                        {
                            a = JsonConvert.DeserializeObject<Info>(stream.ReadToEnd());
                        }
                        var o = RemoveSpecialCharacters(a._songName);
                        Directory.CreateDirectory($"{SongFolderPath}\\{a._songAuthorName}\\");
                        if (File.Exists($"{SongFolderPath}\\{a._songAuthorName}\\{o}.ogg"))
                        {
                            continue;
                        }

                
                        //Console.WriteLine($"{i}\\{a._songFilename}");
                        //Console.WriteLine($"{SongFolderPath}\\{a._songAuthorName}\\{a._songName}.ogg");
                        File.Copy($"{i}/{a._songFilename}", $"{SongFolderPath}\\{a._songAuthorName}\\{o}.ogg");
                        Console.WriteLine(a._songName + " - " + a._songAuthorName);
                    }
                    catch (Exception e)
                    {
                        
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