using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EditorConfigEnforcer
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideOptionPage(typeof(ConfigDialog), "EditorConfig enforcer", "General", 0, 0, true)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class EditorConfigEnforcerPackage : ServiceAsyncPackage
    {
        private const string PackageGuidString = "fdabe4a3-865b-448e-ab77-43c2ce420353";

        public EditorConfigEnforcerPackage()
        {
            RegisterConfigDialog<ConfigDialog>();
            RegisterModule<Bootstrapper>();
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress)
                .ConfigureAwait(false);

            await UpdateEditorConfig()
                .ConfigureAwait(false);
        }

        private Task UpdateEditorConfig()
        {
            var enforcerService = GetService(typeof(EnforcerService)) as EnforcerService;
            if (enforcerService == null)
            {
                return Task.CompletedTask;
            }

            return enforcerService.UpdateEditorConfig();
        }
    }
}
