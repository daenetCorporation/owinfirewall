# Owin Firewall
Owin Middleware, which provides IP blocking list.
OWIN firewall provides a simple OWIN middleware for IP address blocking of your web application or REST API. This repository contains two applications. One is implemented for .NET 4 and another one for .Net Core. By default this OWIN middleware block all IP addresses.

# What is Owin
Owin Middleware, which provides IP blocking list OWIN defines a standard interface between .NET web servers and web applications. The goal of the OWIN interface is to decouple server and application, encourage the development of simple modules for .NET web development, and, by being an open standard, stimulate the open source ecosystem of .NET web development tools.

# Whitelisting of IP addresses 
To specify users, which are considered to use it. Go to the startup of the application and type the particular IP addresses as shown in the code below.
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

# If the local IP Address is unblocked 
If the local IP address is unblocked in the IP blocking list, then it will display the following page
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owin.png)

# In case when access is being denied
This web page will appear when the IP address is blocked for a certain application.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/owinFirewall.jpg)

# How to add NuGet Package to the application
After cloning the project and the NuGet package to the solution. To make it applicable, do the following: open the prompt window, build the solution and create NuGet package by typing the command ```dotnet pack``` it will create the .nupkg files in the debug folder of OwinFirewall. 
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/AddNugetPackageToApplication0.png)
Copy the .nupkg files in any other folder. Now open the solution in Visual Studio, by right clicking on the references of OwinFwWebApp it shall open you the NuGet Package Manager. In package source box enter the path of the folder where you copied your .nupkg files, it shows you the OwinFirewall NuGet package. Finally, install the owinFirewall package.
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/PackageManager.png)
![](https://github.com/daenetCorporation/owinfirewall/blob/master/Images/Package1.png)
