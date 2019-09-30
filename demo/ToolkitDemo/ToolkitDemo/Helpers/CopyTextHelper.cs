using Plugin.Clipboard;

namespace ToolkitDemo.Helpers
{
    public class CopyTextHelper : ICopyTextHelper
    {
        public void CopyToClipboard(string textToCopy)
        {
            CrossClipboard.Current.SetText(textToCopy);
        }
    }
}
