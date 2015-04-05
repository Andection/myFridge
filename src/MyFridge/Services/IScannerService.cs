using System.Threading.Tasks;

namespace MyFridge.Services
{
    public interface IScannerService
    {
        Task<ScannerResult> Scan();
    }

    public class ScannerResult
    {
        public ScannerResult(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}