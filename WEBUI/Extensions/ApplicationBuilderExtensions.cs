﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNpmStaticFiles(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "node_modules");
            var options = new StaticFileOptions() { FileProvider = new PhysicalFileProvider(path), RequestPath="/modules"};
            app.UseStaticFiles(options);
            return app;
        }
    }
}
