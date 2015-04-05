using System.Threading.Tasks;
using MyFridge.Droid.Services;
using MyFridge.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

[assembly: Dependency(typeof(ScannerService))]

namespace MyFridge.Droid.Services
{
    internal class ScannerService : IScannerService
    {
        public async Task<ScannerResult> Scan()
        {
            var scanner = new MobileBarcodeScanner(Forms.Context)
            {
                UseCustomOverlay = false,
                BottomText = "Scanning will happen automatically",
                TopText = "Hold your camera about \n6 inches away from the barcode",
            };

            var result = await scanner.Scan();

            return result.BarcodeFormat == BarcodeFormat.EAN_13 || result.BarcodeFormat == BarcodeFormat.EAN_8
                ? ScannerResult.Success(result.Text)
                : ScannerResult.Failed("Invalid barcode");
        }
    }
}