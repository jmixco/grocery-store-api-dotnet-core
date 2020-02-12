﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Installers
{
    interface IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration);

    }
}
