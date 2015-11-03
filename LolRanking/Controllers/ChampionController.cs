using FirstSPA.Models;
using RiotSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FirstSPA.Controllers
{
    public class ChampionController : BaseController
    {
        // GET api/champions
        [ResponseType(typeof(List<Champion>))]
        public async Task<IHttpActionResult> Get()
        {
            List<Champion> list = new List<Champion>();
            var championsStaticList = StaticApi.GetChampions(Region.euw, RiotSharp.StaticDataEndpoint.ChampionData.image, Language.es_ES);
            foreach (var champ in championsStaticList.Champions)
            {
                list.Add(new Champion()
                {
                    ID = champ.Value.Id,
                    ImageUrl = String.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/champion/{1}", championsStaticList.Version, champ.Value.Image.Full),
                    Name = champ.Value.Name,
                    Disabled = true
                });
            }
            return this.Ok(list);
        }

    }
}
