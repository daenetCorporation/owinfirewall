﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Daenet.Firewall.Middleware
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Installs the <see cref="SecManAuthorizationMiddleware"/> in the asp.Net pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void UseDaenetFirewall(
            this IApplicationBuilder app, DaenetFirewallOptions options = null)
        {
            if (options == null)
                throw new ArgumentException("Specified 'FirewallOptions' have to be properly configured :(");

            app.UseMiddleware<DaenetFirewallMiddleware>(options);
        }
    }


    /// <summary>
    ///
    /// </summary>
    public class DaenetFirewallMiddleware 
    {
        /// <summary>
        /// We store in asp.Net pipeline under this key all required authorization options.
        /// </summary>
        internal const string OptionsKey = "DaenetMiddleware.Options";

        private DaenetFirewallOptions m_Opts;

        private RequestDelegate m_Next;

        /// <summary>
        /// Creates the middleware instance.
        /// </summary>
        /// <param name="next">Nex middleware in chain.</param>
        /// <param name="opts"></param>
        public DaenetFirewallMiddleware(RequestDelegate next, DaenetFirewallOptions opts)
         
        {
            m_Opts = opts;
            m_Next = next;
        }

        /// <summary>
        /// Setups the authorization settings relevant for Security Manager.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public  async Task Invoke(HttpContext context)
        {
            if (m_Opts.AllowedIPs.Contains(context.Connection.RemoteIpAddress.ToString()))
            {

            }
            else
            {
                await context.Response.WriteAsync("daenet Firewall - Access denied. Your IP is blocked for this application! :( <br></br>\r\n");
                await context.Response.WriteAsync($"Your IP {context.Connection.RemoteIpAddress.ToString()} <br></br><br></br>\r\n\r\n");
                await context.Response.WriteAsync($"To specify the list of allowed IP addresses ue following line: <br></br>\r\n");
                await context.Response.WriteAsync("");

                await context.Response.WriteAsync(@"app.UseDaenetFirewall(new Daenet.Firewall.DaenetFirewallOptions(new List<string>() {{ ""214.114.220.225/27"", ""52.56.7.0/27"" }));<br></br>\r\n");

                if (context.Request.Query["debug"] == "true")
                {
                    foreach (var ip in m_Opts.AllowedIPs)
                    {
                        await context.Response.WriteAsync($"{ip} <br></br>\r\n");
                    }
                }

                 context.Response.StatusCode = 401;
                return;
            }


            await  m_Next.Invoke(context);
        }
    }

}
