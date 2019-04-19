using System;
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

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            
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
                    var details = connector.GetTeamsConnectorClient().Teams.FetchTeamDetails(channelId);
                    string teamid = details.AadGroupId;
                    string userid = activity.From.AadObjectId;

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

    }
}
