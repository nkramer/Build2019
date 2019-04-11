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
                    //connector.Conversations.CreateConversation(new ConversationParameters() { })
                    var reply = activity.CreateReply("dfj");
                    reply.Attachments.Add(GetPopUpSignInCard());
                    await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
                    return new HttpResponseMessage(HttpStatusCode.Accepted);

                    //await EchoBot.EchoMessage(connector, activity);
                    //return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
            }
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
                    new CardAction(ActionTypes.Signin, "Do it!", value: baseUri + "/popUpSignin.html?height=200&width=200"),
                }
            };

            return heroCard.ToAttachment();
        }

    }
}
