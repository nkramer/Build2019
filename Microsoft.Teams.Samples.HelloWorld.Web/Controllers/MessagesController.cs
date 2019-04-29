﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using ContosoAirlines.Models;
using System.Web;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public static Activity lastPost = null;
        public static Uri lastUri = null;

        public static async Task<HttpResponseMessage> UpdateCard()
        {
            if (lastPost != null)
            {
                using (var connector = new ConnectorClient(lastUri))
                {
                    var attachment = lastPost.Attachments[0];
                    var card = (HeroCard) (attachment.Content);
                    card.Subtitle = "5 unanswered";
                    connector.Conversations.UpdateActivity(lastPost);
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            lastUri = new Uri(activity.ServiceUrl);
            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                if (activity.IsComposeExtensionQuery())
                {
                    var response = MessageExtension.HandleMessageExtensionQuery(connector, activity);
                    return response != null
                        ? Request.CreateResponse<ComposeExtensionResponse>(response)
                        : new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    string channelId = activity.Conversation.Id.Split(';')[0];
                    string messageId = activity.Conversation.Id.Split(new string[] { ";messageid=" }, StringSplitOptions.None)[1];
                    var details = connector.GetTeamsConnectorClient().Teams.FetchTeamDetails(channelId);
                    string teamid = details.AadGroupId;
                    string userid = activity.From.AadObjectId;

                    //string baseUri = "https://303ad795.ngrok.io";
                    string baseUri = "http://localhost:3333";
                    string url = $"{baseUri}/?team={teamid}&channel={channelId}&message={messageId}";

                    var heroCard = new HeroCard
                    {
                        Title = "Q&A tracker",
                        Buttons = new List<CardAction>
                        {
                            new CardAction(type: ActionTypes.OpenUrl,
                            title: "View questions",
                            value: url)
                        }
                    };

                    Activity reply = activity.CreateReply("");
                    lastPost = reply;
                    reply.Attachments.Add(heroCard.ToAttachment());
                    var response = await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
                    lastPost.Id = response.Id;
                    return new HttpResponseMessage(HttpStatusCode.Accepted);


                    //string prompt = AdminConsentPromptUrl();

                    //string token = await GetToken();
                    //var svc = new GraphService();
                    //svc.accessToken = token;
                    //svc.GetTeam(teamid, channelId, messageId);


                    await Conversation.SendAsync(activity, () => CreateGetTokenDialog());
                    return new HttpResponseMessage(HttpStatusCode.Accepted);

                    //var reply = activity.CreateReply("dfj");
                    //reply.Attachments.Add(GetPopUpSignInCard());
                    //await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
                    //return new HttpResponseMessage(HttpStatusCode.Accepted);

                    //await EchoBot.EchoMessage(connector, activity);
                    //return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
            }
        }

        private GetTokenDialog CreateGetTokenDialog()
        {
            string ConnectionName = "sample";
            return new GetTokenDialog(
                connectionName: ConnectionName,
                signInMessage: $"Please sign in to {ConnectionName} to proceed.",
                buttonLabel: "Sign In",
                retries: 0,//2, 
                retryMessage: "Hmm. Something went wrong, let's try again.");
        }


        private static Attachment GetPopUpSignInCard()
        {

            //string baseUri = Convert.ToString(ConfigurationManager.AppSettings["BaseUri"]);
            string baseUri = "https://303ad795.ngrok.io/";
            var heroCard = new HeroCard
            {
                Title = "Time to sign in",
                Buttons = new List<CardAction>
                {
                    new CardAction(type: ActionTypes.Signin, 
                    title: "Do it!", 
                    value: baseUri + "/popUpSignin.html?height=200&width=200"),
                }
            };

            return heroCard.ToAttachment();
        }


        public static string tenantName = "M365x168483.onmicrosoft.com";

        public static string AdminConsentPromptUrl()
        {
            string appId = Startup.appId;
            string redirectUri = Startup.redirectUri;

            string adminConsentPrompt = $"https://login.microsoftonline.com/common/adminconsent?client_id={appId}&state=12345&redirect_uri={redirectUri}";
            return adminConsentPrompt;
        }

        public static async Task<string> GetToken(/*RootModel rootModel*/)
        {
            string token;
            //if (HomeController.useAppPermissions)
            //{
            string appId = Startup.appId;
            string redirectUri = Startup.redirectUri;
            string appSecret = HttpUtility.UrlEncode(Startup.appSecret);

                string tenant = tenantName;
                string response = await HttpHelpers.POST($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token",
                      $"grant_type=client_credentials&client_id={appId}&client_secret={appSecret}"
                      + "&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default");
                token = response.Deserialize<TokenResponse>().access_token;
            //}
            //else
            //{
            //    token = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();
            //}
            //graphService.accessToken = token;
            return token;
        }
    }

    // Used only for de-serializing JSON
    public class TokenResponse
    {
        public string access_token { get; set; }
    }
}
