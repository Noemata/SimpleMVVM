using System;
using System.Threading.Tasks;

using Windows.ApplicationModel.DataTransfer;

#nullable enable

namespace SimpleMVVM.Services
{
    /// <summary>
    /// A <see langword="class"/> that interacts with the system clipboard
    /// </summary>
    public sealed class ClipboardService : IClipboardService
    {
        public bool TryCopy(string text, bool flush = true)
        {
            try
            {
                DataPackage package = new DataPackage { RequestedOperation = DataPackageOperation.Copy };
                package.SetText(text);
                Clipboard.SetContent(package);

                if (flush)
                    Clipboard.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> TryGetTextAsync()
        {
            try
            {
                DataPackageView view = Clipboard.GetContent();

                // Try to extract the requested content
                string? item;

                if (view.Contains(StandardDataFormats.Text))
                {
                    item = await view.GetTextAsync();
                    view.ReportOperationCompleted(DataPackageOperation.Copy);
                }
                else
                    item = null;

                return item;
            }
            catch
            {
                return null;
            }
        }
    }
}
