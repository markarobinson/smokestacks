﻿using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(smokeStacks.Startup))]
namespace smokeStacks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}