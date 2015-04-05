using System.Threading.Tasks;

namespace MyFridge.Services
{
    public interface IScannerService
    {
        Task<ScannerResult> Scan();
    }

    public class ScannerResult
    {
        public static ScannerResult Success(string barcode)
        {
            return new ScannerResult(true, barcode, string.Empty);
        }

        public static ScannerResult Failed(string errorText)
        {
            return new ScannerResult(false, string.Empty, errorText);
        }

        private ScannerResult(bool isSuccess, string barcode, string errorMessage)
        {
            IsSuccess = isSuccess;
            Barcode = barcode;
            ErrorMessage = errorMessage;
        }

        public string Barcode { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}