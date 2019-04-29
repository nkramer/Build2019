using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Models
{
    public class Question
    {
        public string MessageId;
        public int Votes;
        public string Text;
        //        public bool IsAnswered;
    }

    public class QandAModel
    {
        public static readonly Dictionary<string, QandAModel> qandALookup = new Dictionary<string, QandAModel>();

        //public static QandAModel lastUsedModel = null; // HACK to pass model to view

        // EVIL HACK
        //public static string token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYWkw1YUd6V2FNbTI1YWotcXA3NXJtanRlQjF1MkxWYkFRQjdwbUZFX0diWnJrTERKYmZrY3U1VkZwQkZYbTVWMEQ2WmU4dFN1MmRaZXQ5RDVLaklKQ1NBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIiwia2lkIjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC81ZmJjYmRmMC0zNDg2LTQ2MjAtOWEyMy04NmEyYjQ1NjJhODUvIiwiaWF0IjoxNTU2NDkwNDMyLCJuYmYiOjE1NTY0OTA0MzIsImV4cCI6MTU1NjQ5NDMzMiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IjQyWmdZSENZV2F5K3hQeVpZNnk1MGRYM2hldTI3SHZwNDl3bzBQN3Q0dnc3SHZwOHplOEEiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlRlc3RCb3QiLCJhcHBpZCI6IjZmNGNjOTc4LWRmYmQtNGNjOS1hMDc5LWY4NTZiZWQyYjRhMCIsImFwcGlkYWNyIjoiMSIsImZhbWlseV9uYW1lIjoiQm93ZW4iLCJnaXZlbl9uYW1lIjoiTWVnYW4iLCJpcGFkZHIiOiIxMzEuMTA3LjE0Ny4xNDIiLCJuYW1lIjoiTWVnYW4gQm93ZW4iLCJvaWQiOiJmNmE3NTE5MS1jNThkLTQ2YjQtYWUxYS1iYWVmMjE5NDdhODMiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzdGRkVBQzY3NUY0RCIsInNjcCI6Ikdyb3VwLlJlYWRXcml0ZS5BbGwgTWFpbC5SZWFkIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwgcHJvZmlsZSBvcGVuaWQgZW1haWwiLCJzaWduaW5fc3RhdGUiOlsia21zaSJdLCJzdWIiOiIyd0lhT05oVUpZSEE5Mms1S0VkRnVvdXNBQnlSSDZublVxRml6V0hTY1JvIiwidGlkIjoiNWZiY2JkZjAtMzQ4Ni00NjIwLTlhMjMtODZhMmI0NTYyYTg1IiwidW5pcXVlX25hbWUiOiJNZWdhbkJATTM2NXg4NzQ1MDYuT25NaWNyb3NvZnQuY29tIiwidXBuIjoiTWVnYW5CQE0zNjV4ODc0NTA2Lk9uTWljcm9zb2Z0LmNvbSIsInV0aSI6InRBRC1yaUN4bzAtN0Rkc1NLd2xIQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjYyZTkwMzk0LTY5ZjUtNDIzNy05MTkwLTAxMjE3NzE0NWUxMCJdLCJ4bXNfc3QiOnsic3ViIjoibzlMR29WeWdGTWJCTGdkUk1xYk5idTFzdVVtdkh3V0ZySlo4S3FKaW44USJ9LCJ4bXNfdGNkdCI6MTUzMTc5NTg0MH0.PJqmKcLhbRc_HA2Hf1GOprRsQnbkdAQ5tXvzL5TgUt3jfnFDmT-8-cdpr4g7Q2k2gObZXVJDR_m07kS-UfsTN1o7M2NMf5n2cFEZjAfc_aS0iF8UaV5ymOT_hXuAHo5BzMhk6KjWmhjfTIIY_c_upJ7lPcyrxKOlGmo-KRyHYrCRJeues1vWO6fFwpRJ6YzSE2w0GVuIMz0h7C602AhoCcaOaGl4Sr3OWrhLbn7PV5Qu9IFeB3KHnLsUr3fcb3dgCePC2LHERNWNS_JwKf9FEn2pJ00Cm84-L0yzLEUi2S-t6NVdEjNYnYRn9R2z6G04hKIwQNog5g_ZHYKRUEZ3tA";

        public List<Question> Questions = new List<Question>();
        public Dictionary<string, bool> IsQuestionAnswered = new Dictionary<string, bool>(); // maps message id -> isAnswered
        public string RootMessageId = "1556490107000";
        public string RootChannel = "19:ad93df5384cf40f9b6d3449e54e5bc72@thread.skype";
        public string RootTeam = "feb5d096-02c1-4cec-a860-fafe61ddbe1c";
        public Activity BotFirstMessage = null;

        public static string Encode(string teamId, string channelId, string msgId)
            => $"{teamId}_{channelId}_{msgId}".Replace(':', '_'); // avoid asp.net bad chars -- see https://www.hanselman.com/blog/ExperimentsInWackinessAllowingPercentsAnglebracketsAndOtherNaughtyThingsInTheASPNETIISRequestURL.aspx

        public string Key => Encode(RootTeam, RootChannel, RootMessageId);
    }
}