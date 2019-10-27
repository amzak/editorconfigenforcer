using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EditorConfigEnforcer
{
    public class EnforcerService
    {
        private const string EditorConfigFileName = ".editorconfig";

        private readonly VsLogger _logger;
        private readonly ConfigDialog _config;

        public EnforcerService(VsLogger logger, ConfigDialog config)
        {
            _logger = logger;
            _config = config;

            config.OnChange = HandleConfigChange;
        }

        private void HandleConfigChange() => UpdateEditorConfig();

        public async Task UpdateEditorConfig()
        {
            if (string.IsNullOrEmpty(_config.DownloadUrl) ||
                string.IsNullOrEmpty(_config.ProjectsRoot))
            {
                await _logger.Error("Invalid config")
                    .ConfigureAwait(false);
                return;
            }

            try
            {
                var editorConfig = Path.Combine(_config.ProjectsRoot, EditorConfigFileName);
                WebClient webClient = new WebClient();
                await webClient.DownloadFileTaskAsync(_config.DownloadUrl, editorConfig)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await _logger.Exception(ex)
                    .ConfigureAwait(false);
                throw;
            }
        }
    }
}