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
            get { return m_enableDebugBindings; }
            set
            {
                if(m_enableDebugBindings != value)
                {
                    m_enableDebugBindings = value;
                    this.OnEnableDebugBindingsChanged();
                }
            }
        }

        private bool m_enableDebugBindings;
        private bool m_started = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            m_started = true;
            #if(DEBUG)
            if (this.EnableDebugBindings)
                this.SetDebugTrace();
            #endif
            base.OnStartup(e);
        }

        protected void OnEnableDebugBindingsChanged()
        {
            if (this.EnableDebugBindings && m_started)
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
