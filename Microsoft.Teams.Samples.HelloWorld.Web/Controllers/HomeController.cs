using ContosoAirlines.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Graph;
using Microsoft.Teams.Samples.HelloWorld.TokenStorage;
using System.Web;
using System.Security.Claims;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using Microsoft.Teams.Samples.HelloWorld.Web.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/MarkAsAnswered")]
        public ActionResult MarkAsAnswered(string key, string messageId)
        {
            QandAModel model = qandALookup[key];
            model.IsQuestionAnswered[messageId] = true;
            return View("Index", model);
        }

//        [Authorize]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            if (!Request.IsAuthenticated)
            {
                //// Signal OWIN to send an authorization request to Azure
                //Request.GetOwinContext().Authentication.Challenge(
                //    new AuthenticationProperties { RedirectUri = "/" },
                //    OpenIdConnectAuthenticationDefaults.AuthenticationType);
                return View();
            }

            string teamId = Request.QueryString["team"];
            string channelId = Request.QueryString["channel"];
            string msgId = Request.QueryString["message"];

            if (teamId == null)
            {
                teamId = "21ad502b-d790-4359-a10f-8fa1d5722a29";
                channelId = "19:81eff88fa60f4386aab1b5a0a5e4c797@thread.skype";
                msgId = "1555716696233";
            }

            string key = QandAModel.Encode(teamId, channelId, msgId);
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

            var client = GetAuthenticatedClient();

            //var svc = new GraphService();
            //svc.accessToken = await GetUserDelegatedToken();
            //svc.accessToken = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
            //string token = await MessagesController.GetToken();
            //svc.accessToken = token;
            await RefreshQandA(client, model);

            return View(model);
        }

        public async Task RefreshQandA(GraphServiceClient client, QandAModel qanda)
        {
            var msgs = await client.Teams[qanda.RootTeam].Channels[qanda.RootChannel].Messages[qanda.RootMessageId].Replies.Request().Top(50).GetAsync();
            //IEnumerable<ChatMessage> m = msgs.
            //ChatMessage[] msgs = await HttpGetList<ChatMessage>($"/teams/{qanda.RootTeam}/channels/{qanda.RootChannel}/messages/{qanda.RootMessageId}/replies?$top=50");

            // merge w/ existing Qs
            //var questions =
            //    from m in msgs
            //    where IsQuestion(m)
            //    select new Question() { MessageId = m.Id, /*IsAnswered = false, */Text = m.Body.Content, Votes = m.Reactions.Count() };
            //qanda.Questions = questions.OrderByDescending(m => m.Votes).ToList();

            //foreach (var q in questions)
            //{
            //    if (!qanda.IsQuestionAnswered.ContainsKey(q.MessageId))
            //        qanda.IsQuestionAnswered[q.MessageId] = false;
            //}

            await MessagesController.UpdateCard();
        }

        private bool IsQuestion(ChatMessage msg)
        {
            if (msg.From.User == null)  // sender is bot
                return false;
            if (msg.Mentions == null) // no @mention
                return true;
            foreach (var men in msg.Mentions)
            {
                if (men.Mentioned.Application != null
                    && (men.Mentioned.Application.AdditionalData["applicationIdentityType"] as string) == "bot")
                    return false;
            }
            return true;
        }


        private static GraphServiceClient GetAuthenticatedClient()
        {
            return new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        string token= await GetUserDelegatedToken();
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);
                    }));
        }

        private static async Task<string> GetUserDelegatedToken()
        {
            // Get the signed in user's id and create a token cache
            string signedInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            SessionTokenStore tokenStore = new SessionTokenStore(signedInUserId,
                new HttpContextWrapper(System.Web.HttpContext.Current));

            var idClient = new ConfidentialClientApplication(
                Startup.appId, Startup.redirectUri, new ClientCredential(Startup.appSecret),
                tokenStore.GetMsalCacheInstance(), null);

            var accounts = await idClient.GetAccountsAsync();

            // By calling this here, the token can be refreshed
            // if it's expired right before the Graph call is made
            var result = await idClient.AcquireTokenSilentAsync(
                Startup.graphScopes.Split(' '), accounts.FirstOrDefault());
            return result.AccessToken;
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
            //await svc.RefreshQandA(model);

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
