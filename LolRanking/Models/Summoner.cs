using System.ComponentModel.DataAnnotations;

namespace FirstSPA.Models
{
    public class Summoner
    {
        public int Id { get; set; }

        public int SummonerId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ProfileIconId { get; set; }

        public int SummonerLevel { get; set; }
    }
}