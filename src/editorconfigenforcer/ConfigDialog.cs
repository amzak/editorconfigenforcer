using System;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace EditorConfigEnforcer
{
    public class ConfigDialog : DialogPage
    {
        [DisplayName("Download url")]
        [Description("Url to get .editor config file from")]
        public string DownloadUrl { get; set; }

        [DisplayName("Projects root")]
        [Description("Destination folder for .editconfig file")]
        public string ProjectsRoot { get; set; }

        public Action OnChange;

        private void InvokeOnChange() => OnChange?.Invoke();

        protected override void OnApply(PageApplyEventArgs e)
        {
            base.OnApply(e);
            InvokeOnChange();
        }

    }
}