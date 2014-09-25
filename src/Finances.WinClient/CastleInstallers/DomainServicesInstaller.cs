﻿using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.Registration;
using Finances.WinClient.DomainServices;
using Finances.WinClient.ViewModels;
using Finances.WinClient.InterceptorSelectors;

namespace Finances.WinClient.CastleInstallers
{
    class DomainServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBankService>().ImplementedBy<BankService>());

            container.Register(Component.For<IBankAccountService>().ImplementedBy<BankAccountService>());

            //container.Register(Component.For<IBankViewModelFactory>().ImplementedBy<BankViewModelFactory>());

            //container.Register(Component.For<IBanksViewModel>().ImplementedBy<BanksViewModel>());

            //container.Register(Component.For<IBankEditorViewModel>().ImplementedBy<BankEditorViewModel>());

        ////    container.Kernel.ProxyFactory.AddInterceptorSelector(new DomainServiceInterceptorSelector());
        
        }
    }
}