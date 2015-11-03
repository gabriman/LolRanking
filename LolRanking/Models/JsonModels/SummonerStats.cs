using System;
using System.Collections.Generic;

namespace FirstSPA.Models.JsonModels
{
    public class SummonerStats
    {
        public String Name { get; set; }
        public String UrlImage { get; set; }
        public List<CustomChampionStats> ChampionStats { get; set;}
        public CustomChampionStats RankedStats { get; set; }
    }
}