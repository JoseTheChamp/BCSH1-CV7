using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueLibrary
{
    public class PlayersFileSerializerDeserializer
    {
        readonly IPlayersSaveableLoadable players;
        public string FilePath { get; set; }

        public PlayersFileSerializerDeserializer(IPlayersSaveableLoadable players, string file)
        {
            this.players = players;
            this.FilePath = file;
        }

        public void Save()
        {
            Debug.WriteLine("Serializer-SAVE  cesta: " + FilePath);
            /*
            if (!File.Exists(FilePath))
            {
                Debug.WriteLine("Saving - novy soubor jemno:   " + FilePath);
                File.Create(FilePath);
                File
            }
            */
            using (StreamWriter outputFile = new StreamWriter(FilePath, false))
            {
                Player[] playersArray = players.Save();
                foreach (Player player in playersArray)
                {
                    outputFile.WriteLine(Serialize(player));
                }
            }
        }

        public void Load()
        {
            Debug.WriteLine("Serializer-LOAD");
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath, Encoding.UTF8);
                Player[] playerArray = new Player[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    playerArray[i] = Deserialize(lines[i]);
                }
                players.Load(playerArray);
            }
        }

        private static string Serialize(Player p) //TODO
        {
            Debug.WriteLine("Writing " + p);
            return p.Name + "@" + p.Club + "@" + p.GoalCount;
        }

        private static Player Deserialize(string s) //TODO
        {
            string[] attributes  = s.Split('@');
            FootballClub club = FootballClub.None;
            switch (attributes[1]) {
                case "None":
                    club = FootballClub.None;
                    break;
                case "Chelsea":
                    club = FootballClub.Chelsea;
                    break;
                case "Arsenal":
                    club = FootballClub.Arsenal;
                    break;
                case "RealMadrid":
                    club = FootballClub.RealMadrid;
                    break;
                case "Barcelona":
                    club = FootballClub.Barcelona;
                    break;
                case "FCPorto":
                    club = FootballClub.FCPorto;
                    break;
            }
            int goals = int.Parse(attributes[2]);
            Player loaded = new Player(attributes[0], club, goals);
            Debug.WriteLine("Reading:  " + loaded);
            return loaded;
        }
    }
}
