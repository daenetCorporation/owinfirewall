# Application Firewall
Application Firewall is an ASP.NET middleware component, which enables application access based on the list of caller's IP address.
This simple firewall can be used for protecting of your web applications or REST APIs. 
Inside of this repository you will find two applications. One is implemented on top of .NET 4 and another one on top of .Net Core. 



## How does it work?
Owin Middleware, which provides IP blocking list OWIN defines a standard interface between .NET web servers and web applications. The goal of the OWIN interface is to decouple server and application, encourage the development of simple modules for .NET web development, and, by being an open standard, stimulate the open source ecosystem of .NET web development tools.

1. Add nuget package:
todo..

2. Whitelisting of IP addresses 
To specify users, which are considered to use it. Go to the startup of the application and type the particular IP addresses as shown in the code below.By default this OWIN middleware block all IP addresses.
```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
   loggerFactory.AddConsole(Configuration.GetSection("Logging"));
   loggerFactory.AddDebug();
   app.UseOwinFirewall(new Daenet.Owin.OwinFirewallOptions(new List<string>()
  { "81.207.142.79", "::1", "localhost", "222.111.228.225/27" }));
     app.UseMvc();
  }
```
This example demonstrates, how to enable specific IP addresses and the range of addresses by using of CIDR specification.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/OwinFirewallASP.NetCore/OwinIpList.JPG)

**If the IP address is unblocked** 
If the local IP address is unblocked in the IP blocking list, then it will display the following page
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owin.png)

**If the IP address is being denied**
Following example demonstrates, if the IP address has being blocked for a certain application.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owinFirewall.jpg)


