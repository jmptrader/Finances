﻿using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.Registration;
using Finances.Core;

namespace Finances.WinClient.CastleInstallers
{
    class ConnectionInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConnection>().ImplementedBy<Connection>());

        }
    }
}
