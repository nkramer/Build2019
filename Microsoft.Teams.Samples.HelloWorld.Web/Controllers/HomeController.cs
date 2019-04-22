using ContosoAirlines.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public async Task<ActionResult> Index()
        {
            string teamId = Request.QueryString["team"];
            string channelId = Request.QueryString["channel"];
            string msgId = Request.QueryString["message"];
            string key = $"{teamId}/{channelId}/{msgId}";

            if (teamId == null)
            {
                teamId = "21ad502b-d790-4359-a10f-8fa1d5722a29";
                channelId = "19:81eff88fa60f4386aab1b5a0a5e4c797@thread.skype";
                msgId = "1555716696233";
            }

            QandAModel model;
            if (qandALookup.ContainsKey(key))
            {
                model = qandALookup[key];
            }
            else
            {
                model = new QandAModel() { RootTeam = teamId, RootChannel = channelId, RootMessageId = msgId };
                qandALookup[key] = model;
            }

            var svc = new GraphService();
            string token = await MessagesController.GetToken();
            svc.accessToken = token;
            await svc.RefreshQandA(model);

            return View(model);
        }

        public static readonly Dictionary<string, QandAModel> qandALookup = new Dictionary<string, QandAModel>();

        [Route("hello")]
        public ActionResult Hello()
        {
            return View("Index");
        }

        [Route("first")]
        public async Task<ActionResult> First()
        {
            string token = await MessagesController.GetToken();
            var model = new QandAModel();

            var svc = new GraphService();
            svc.accessToken = token;
            await svc.RefreshQandA(model);

            return View(model);
        }

        [Route("second")]
        public ActionResult Second()
        {
            return View();
        }

        [Route("configure")]
        public ActionResult Configure()
        {
            return View();
        }
    }
}
