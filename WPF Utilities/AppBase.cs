using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFUtilities.Debug;

namespace WPFUtilities
{
    /// <summary>
    /// A base class for the main application class
    /// </summary>
    public class AppBase : Application
    {
        
        public bool EnableDebugBindings
        {
            get { return _enableDebugBindings; }
            set
            {
                if(_enableDebugBindings != value)
                {
                    _enableDebugBindings = value;
                    this.OnEnableDebugBindingsChanged();
                }
            }
        }

        private bool _enableDebugBindings;
        private bool _started = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            _started = true;
            #if(DEBUG)
            if (this.EnableDebugBindings)
                this.SetDebugTrace();
            #endif
            base.OnStartup(e);
        }

        protected void OnEnableDebugBindingsChanged()
        {
            if (this.EnableDebugBindings && _started)
                this.SetDebugTrace();
            else
                PresentationTraceSources.Refresh();
        }

        protected void SetDebugTrace()
        {
            PresentationTraceSources.Refresh();
            PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level =
                SourceLevels.Warning | SourceLevels.Error;
        }
    }
}
