using System;
using System.Threading;
using Autofac;
using Autofac.Core;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EditorConfigEnforcer
{
    public class ServiceAsyncPackage : AsyncPackage
    {
        private IContainer _container;
        private readonly ContainerBuilder _containerBuilder;

        protected ServiceAsyncPackage()
        {
            _containerBuilder = new ContainerBuilder();
            RegisterServiceAccessor();
            RegisterJoinableTaskFactory();
        }

        private void RegisterServiceAccessor() => _containerBuilder.Register(context => new ServiceAccessor(GetService));

        private void RegisterJoinableTaskFactory() => _containerBuilder.RegisterInstance(JoinableTaskFactory);

        protected void RegisterModule<TModule>() where TModule : IModule, new() => _containerBuilder.RegisterModule<TModule>();

        protected void RegisterConfigDialog<TDialog>() where TDialog : class
        {
            _containerBuilder.Register(context => GetDialogPage(typeof(TDialog)) as TDialog)
                .As<TDialog>();
        }

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            _container = _containerBuilder.Build();
            return base.InitializeAsync(cancellationToken, progress);
        }

        protected override object GetService(Type serviceType)
        {
            if (_container?.IsRegistered(serviceType) ?? false)
            {
                return _container.Resolve(serviceType);
            }
            return base.GetService(serviceType);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                _container?.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}