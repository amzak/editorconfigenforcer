using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System.Threading.Tasks;

namespace EditorConfigEnforcer
{
    public class VsLogger
    {
        private readonly IVsActivityLog _logger;
        private readonly JoinableTaskFactory _joinableTaskFactory;

        public VsLogger(ServiceAccessor serviceAccessor, JoinableTaskFactory joinableTaskFactory)
        {
            _logger = serviceAccessor.GetService(typeof(SVsActivityLog)) as IVsActivityLog;
            _joinableTaskFactory = joinableTaskFactory;
        }

        public async Task Info(string message)
        {
            await _joinableTaskFactory.SwitchToMainThreadAsync();
            _logger?.LogEntry((uint)__ACTIVITYLOG_ENTRYTYPE.ALE_INFORMATION, "EditorConfig enforcer", message);
        }

        public async Task Exception(Exception ex)
        {
            await _joinableTaskFactory.SwitchToMainThreadAsync();
            _logger?.LogEntry((uint)__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, "EditorConfig enforcer", ex.ToString());
        }

        public async Task Error(string error)
        {
            await _joinableTaskFactory.SwitchToMainThreadAsync();
            _logger?.LogEntry((uint)__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR, "EditorConfig enforcer", error);
        }
    }
}