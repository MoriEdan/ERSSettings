using System.Windows;

namespace ERSSettings.Helpers
{
    internal class ClipboardHelper
    {
        internal static void CopyText(string text) => Clipboard.SetText(text);
    }
}