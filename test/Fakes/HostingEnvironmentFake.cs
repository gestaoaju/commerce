// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Gestaoaju.Fakes
{
    public class HostingEnvironmentFake : IHostingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }

        private string AppVeyorBuildFolder
        {
            get
            {
                var buildFolder = Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER");

                if (buildFolder == null)
                {
                    return null;
                }

                return $@"{buildFolder}\src\bin\Any CPU\Release\netcoreapp2.0";
            }
        }

        private string LocalBuildFolder => AppContext.BaseDirectory
            .Replace(@"test\bin\Debug\netcoreapp2.0", "src");

        public HostingEnvironmentFake()
        {
            EnvironmentName = "Test";
            ApplicationName = "Gestaoaju.Test";
            ContentRootPath = AppVeyorBuildFolder ?? LocalBuildFolder;
            WebRootPath = ContentRootPath;
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
            WebRootFileProvider = new PhysicalFileProvider(ContentRootPath);

            Console.WriteLine($"CurrentBuildPath={ContentRootPath}");
            Console.WriteLine($"CurrentBuildPathExists={Directory.Exists(ContentRootPath)}");
        }
    }
}
