﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PatientPortal.Internal.Startup))]
namespace PatientPortal.Internal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}