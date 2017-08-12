// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;

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

        public HostingEnvironmentFake()
        {
            EnvironmentName = "Test";
            ApplicationName = "Gestaoaju.Test";
            ContentRootPath = PlatformServices.Default.Application
                .ApplicationBasePath.Replace(@"test\bin\Debug\netcoreapp1.1", "src");
            WebRootPath = ContentRootPath;
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
            WebRootFileProvider = new PhysicalFileProvider(ContentRootPath);
        }
    }
}
