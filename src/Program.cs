// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Gestaoaju
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) => WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }        
    }
}
