using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiotSharp;
using System.Web.Http;
using FirstSPA.Helpers;

namespace FirstSPA.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly RiotApi Api = RiotApi.GetInstance(Helpers.Config.Read(Config.ConfigKeys.ApiKey));
        protected readonly StaticRiotApi StaticApi = StaticRiotApi.GetInstance(Helpers.Config.Read(Config.ConfigKeys.ApiKey));

    }
}