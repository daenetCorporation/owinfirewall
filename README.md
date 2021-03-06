# Application Firewall
Application Firewall is an ASP.NET middleware component, which enables application access based on the list of caller's IP address.
This simple firewall can be used for protecting of your web applications or REST APIs. 
Inside of this repository you will find two applications. One is implemented on top of .NET 4 and another one on top of .Net Core. 



## How does it work?
Middleware is software that is assembled into an application pipeline to handle requests and responses. Each component chooses whether to pass the request on to the next component in the pipeline, and can perform certain actions before and after the next component is invoked in the pipeline.
1. Add nuget package:
**First**, install the Daenet.Firewall.Middleware [NuGet Package](https://www.nuget.org/packages/Daenet.Firewall.Middleware/1.0.0) into your application.

2. Whitelisting of IP addresses 
To specify users, which are considered to use it. Go to the startup of the application and type the particular IP addresses as shown in the picture below.By default this OWIN middleware block all IP addresses.
This example demonstrates, how to enable specific IP addresses and the range of addresses by using of CIDR specification.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/OwinFirewallASP.NetCore/OwinIpList.JPG)

**If the IP address is unblocked** 
If the local IP address is unblocked in the IP blocking list, then it will display the following page
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owin.png)

**If the IP address is being denied**
Following example demonstrates, if the IP address has being blocked for a certain application.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owinFirewall.jpg)


