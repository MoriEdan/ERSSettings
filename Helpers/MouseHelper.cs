using System.Windows.Input;

namespace ERSSettings.Helpers
{
    internal class MouseHelper
    {
        internal static void ShowWaitCursor(bool show) => Mouse.OverrideCursor = show ? Cursors.Wait : null;
    }
}