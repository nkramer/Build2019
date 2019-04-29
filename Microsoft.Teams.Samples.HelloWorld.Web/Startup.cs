/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Owin;
using Microsoft.Owin;
using System.Configuration;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Teams.Samples.HelloWorld.TokenStorage;
using System.IdentityModel.Claims;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(Microsoft.Teams.Samples.HelloWorld.Web.Startup))]

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
