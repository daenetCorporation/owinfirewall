using Microsoft.Owin;
using Daenet.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace Daenet.Owin
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Installs the <see cref="SecManAuthorizationMiddleware"/> in the OWIN pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void UseOwinFirewall(
            this IAppBuilder app, OwinFirewallOptions options = null)
        {
            if (options == null)
                throw new ArgumentException("Specified 'FirewallOptions' have to be properly configured :(");

            app.Use<OwinMiddleware>(options);
        }
    }


    /// <summary>
    ///
    /// </summary>
    public class OwinMiddleware : Microsoft.Owin.OwinMiddleware
    {
        /// <summary>
        /// We store in owin pipeline under this key all required authorization options.
        /// </summary>
        internal const string OptionsKey = "OwinMiddleware.Options";

        private OwinFirewallOptions m_Opts;

        private Microsoft.Owin.OwinMiddleware m_Next;

        /// <summary>
        /// Creates the middleware instance.
        /// </summary>
        /// <param name="next">Nex middleware in chain.</param>
        /// <param name="opts"></param>
        public OwinMiddleware(Microsoft.Owin.OwinMiddleware next, OwinFirewallOptions opts)
            : base(next)
        {
            m_Opts = opts;
            m_Next = next;
        }

        /// <summary>
        /// Setups the authorization settings relevant for Security Manager.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            if (m_Opts.AllowedIPs.Contains(context.Request.RemoteIpAddress))
            {

            }
            else
            {
                await context.Response.WriteAsync("daenet Owin Firewall - Access denied. Your IP is blocked for this application! :( <br></br>\r\n");
                await context.Response.WriteAsync($"Your IP {context.Request.RemoteIpAddress} <br></br><br></br>\r\n\r\n");
                await context.Response.WriteAsync($"To specify the list of allowed IP addresses ue following line: <br></br>\r\n");
                await context.Response.WriteAsync("");

                await context.Response.WriteAsync(@"app.UseOwinFirewall(new Daenet.Owin.OwinFirewallOptions(new List<string>() {{ ""214.114.220.225/27"", ""52.56.7.0/27"" }));<br></br>\r\n");
             
                if (context.Request.Query["debug"] != null && context.Request.Query["debug"] == "true")
                {
                    foreach (var ip in m_Opts.AllowedIPs)
                    {
                        await context.Response.WriteAsync($"{ip} <br></br>\r\n");
                    }
                }
                
                context.Response.StatusCode = 401;
                return;
            }

            
            if (context.Request.Method != "OPTIONS")
            {/*
                
                if (m_Opts.ProtectedPath != null && context.Request.Uri.AbsoluteUri.ToLower().Contains(m_Opts.ProtectedPath))
                {
                    if (context.Environment.ContainsKey(OptionsKey) == false)
                        context.Environment.Add(OptionsKey, m_Opts);

                    if (context.Authentication.User != null && !(context.Authentication.User.Identity is GenericIdentity))
                    {
                        if (context.Authentication.User.Identity.AuthenticationType != "device")
                        {
                            if (!context.Request.Headers.ContainsKey(m_Opts.AuthHeaderName))
                            {
                                await context.Response.WriteAsync(m_Opts.ErrorOnMissingHeader);
                                context.Response.StatusCode = 401;
                                return;
                            }
                            else
                            {
                                var hVal = context.Request.Headers[m_Opts.AuthHeaderName];
                                var tokens = hVal.Split(',');

                                if (tokens.Length != 2)
                                {
                                    await context.Response.WriteAsync("Invalid token format.");
                                    context.Response.StatusCode = 401;
                                    return;
                                }

                                long id;

                                if (long.TryParse(tokens[0], out id))
                                {
                                    if (this.m_Opts.OnValidateHeader.Invoke(id, tokens[1]))
                                    {
                                        GenericIdentity identity = new GenericIdentity("device", "device");
                                        context.Authentication.User = new System.Security.Claims.ClaimsPrincipal(identity);
                                    }
                                    else
                                    {
                                        await context.Response.WriteAsync("Invalid DeviceKey or DeviceId");
                                        context.Response.StatusCode = 401;
                                        return;
                                    }
                                }
                                else
                                {
                                    await context.Response.WriteAsync("Invalid deviceId in token.");
                                    context.Response.StatusCode = 401;
                                    return;
                                }
                            }


                        }
                    }
                }*/
            }

            await m_Next.Invoke(context);
        }
    }

}
