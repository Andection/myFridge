using System.Threading.Tasks;
using System.Windows;
using MyFridge.Services;
using MyFridge.WinPhone.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

[assembly: Dependency(typeof(WinPhoneScanner))]

namespace MyFridge.WinPhone.Services
{
    public class WinPhoneScanner : IScannerService
    {
        public async Task<ScannerResult> Scan()
        {
            var scanner = new MobileBarcodeScanner(Deployment.Current.Dispatcher)
            {
                UseCustomOverlay = false,
                BottomText = "Scanning will happen automatically",
                TopText = "Hold your camera about \n6 inches away from the barcode",
            };

            var result = await scanner.Scan();
            return result.BarcodeFormat == BarcodeFormat.EAN_13 || result.BarcodeFormat == BarcodeFormat.EAN_8
                ? ScannerResult.Success(result.Text)
                : ScannerResult.Failed("not support barcode format");
        }
    }
}