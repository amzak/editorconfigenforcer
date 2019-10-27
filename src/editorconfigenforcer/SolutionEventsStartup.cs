using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;

namespace EditorConfigEnforcer
{
    public class SolutionEventsStartup : IStartable
    {
        private readonly ServiceAccessor _accessor;
        private readonly JoinableTaskFactory _joinableTaskFactory;
        private readonly VsLogger _logger;
        private readonly SolutionEventsSink _eventsSink;

        public SolutionEventsStartup(ServiceAccessor accessor, 
            JoinableTaskFactory joinableTaskFactory,
            VsLogger logger,
            SolutionEventsSink eventsSink)
        {
            _accessor = accessor;
            _joinableTaskFactory = joinableTaskFactory;
            _logger = logger;
            _eventsSink = eventsSink;
        }

        public void Start()
        {
            SetupSolutionEventsSink();
        }

        private async Task SetupSolutionEventsSink()
        {
            var solution = _accessor.GetService(typeof(SVsSolution)) as IVsSolution;

            if (solution == null)
            {
                await _logger.Error("Can't subscribe to solution events")
                    .ConfigureAwait(false);
                return;
            }

            await _joinableTaskFactory.SwitchToMainThreadAsync();
            solution.AdviseSolutionEvents(_eventsSink, out _);
        }
    }
}