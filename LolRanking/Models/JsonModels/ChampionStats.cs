using Newtonsoft.Json;
using RiotSharp.StatsEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstSPA.Models.JsonModels
{
    /// <summary>
    /// Clase que agrega estadísticas calculadas al SumaryChampionStats
    /// </summary>
    public class CustomChampionStats
    {
        public ChampionStats ChampionStats { get; set; }
        public CalculatedChampionStat CalculatedChampionStat { get; set; }

        public CustomChampionStats(ChampionStats championStat)
        {
            this.ChampionStats = championStat;
            this.CalculatedChampionStat = new CalculatedChampionStat(championStat.Stats);
        }

    }

    /// <summary>
    /// Datos calculados a partir de los datos del campeón
    /// </summary>
    public class CalculatedChampionStat
    {
        /// <summary>
        /// Wins / (Wins+Looses) * 100
        /// </summary>
        [JsonProperty("winrate")]
        private decimal WinRate { get; set; }

        [JsonProperty("games")]
        private int Games { get; set; }

        /// <summary>
        /// Minions killed by game
        /// </summary>
        [JsonProperty("totalMinionKills_byGame")]
        private int TotalMinionKills_byGame { get; set; }

        /// <summary>
        /// (Kills + Assists) / Deads
        /// </summary>
        [JsonProperty("kda")]
        private decimal KDA { get; set; }

        public CalculatedChampionStat(ChampionStat stats)
        {
            CalculateTotalMinionsKills_byGame(stats);
            CalculateKda(stats);
            CalculateGames(stats);
            CalculateWinRate(stats);
        }

        private void CalculateKda(ChampionStat stats)
        {
            if (stats == null || stats.TotalSessionsPlayed == 0)
                this.KDA = 0;
            else
            {
                if (stats.TotalDeathsPerSession == 0)
                    stats.TotalDeathsPerSession = 1;
                this.KDA = Decimal.Round((decimal)(stats.TotalChampionKills + stats.TotalAssists) / stats.TotalDeathsPerSession, 2);
            }
        }

        private void CalculateTotalMinionsKills_byGame(ChampionStat stats)
        {
            if (stats == null || stats.TotalSessionsPlayed == 0)
                this.TotalMinionKills_byGame = 0;
            else
                this.TotalMinionKills_byGame = stats.TotalMinionKills / stats.TotalSessionsPlayed;
        }

        private void CalculateGames(ChampionStat stats)
        {
            this.Games = stats == null ? 0 : stats.TotalSessionsPlayed;
        }

        private void CalculateWinRate(ChampionStat stats)
        {
            this.WinRate = stats == null ? 0 : Math.Round(stats.TotalSessionsWon / (decimal) stats.TotalSessionsPlayed, 2);
        }
    }
}