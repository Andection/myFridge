using System.Threading.Tasks;
using MyFridge.iOS.Services;
using MyFridge.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

[assembly: Dependency(typeof(ScannerService))]

namespace MyFridge.iOS.Services
{
    internal class ScannerService : IScannerService
    {
        public async Task<ScannerResult> Scan()
        {
            var scanner = new MobileBarcodeScanner(AppDelegate.RootController)
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