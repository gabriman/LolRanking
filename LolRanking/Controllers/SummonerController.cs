using FirstSPA.Models;
using FirstSPA.Models.JsonModels;
using RiotSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FirstSPA.Helpers;

namespace FirstSPA.Controllers
{
    public class SummonerController : BaseController
    {
        // GET api/Summoner
        /// <summary>
        /// Realiza la búsqueda de estadísticas de los amigos del jugador seleccionado. Para ello primero recupera el jugador y sus equipos, para posteriormente recuperar las 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        [System.Web.Mvc.OutputCache(Duration = 10, VaryByParam = "name")]
        [Route("api/{name}/friendsStats")]
        [ResponseType(typeof(List<SummonerStats>))]
        public async Task<IHttpActionResult> GetSummoner(string name, Region region = Region.euw)
        {
            try
            {
                var summonerRequested = Api.GetSummoner(region, name);
                var teams = summonerRequested.GetTeams();
                List<RiotSharp.TeamEndpoint.TeamMemberInfo> members = teams.SelectMany(t => t.Roster.MemberList).Distinct().ToList();

                var listOfIds = members.Select(m => (int) m.PlayerId).ToList();

                var friends = Api.GetSummoners(region, listOfIds);

                List<SummonerStats> stats = new List<SummonerStats>();

                foreach (var friend in friends)
                {
                    var rankedStats = GetCustomStats(friend);
                    if (rankedStats != null)
                        stats.Add(new SummonerStats()
                        {
                            ChampionStats = rankedStats.ToList(),
                            Name = friend.Name,
                            UrlImage = GetUrlImage(friend.ProfileIconId)
                        });
                }
                return Ok(stats);
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }

        }

        // GET api/gabriman88/stats
        /// <summary>
        /// Obtiene las estadísticas del jugador pasado por parámetro.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        [Route("api/{name}/stats")]
        [ResponseType(typeof(List<CustomChampionStats>))]
        public async Task<IHttpActionResult> GetSummonerRankedStats(string name, Region region = Region.euw)
        {
            var sum = Api.GetSummoner(region, name);
            try
            {
                var rankedStats = GetCustomStats(sum);
                return this.Ok(rankedStats);
            }
            catch (Exception e)
            {
                return this.NotFound();
            }

        }

        private static IEnumerable<CustomChampionStats> GetCustomStats(RiotSharp.SummonerEndpoint.Summoner sum)
        {
            try
            {
                return sum.GetStatsRanked().Select(stat => new CustomChampionStats(stat));
            }
            catch (Exception)
            {
                return null;
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }


        #region PRIVATE METHODS


        private string GetUrlImage(int iconId)
        {
            return String.Format(Helpers.Config.Read(Config.ConfigKeys.ImagesUrl), iconId);
        }


        #endregion

    }
}
