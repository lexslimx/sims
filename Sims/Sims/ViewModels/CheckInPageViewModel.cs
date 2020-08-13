using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing;

namespace Sims.ViewModels
{
    public class CheckInPageViewModel : BindableBase
    {
        private INavigation _navigation { get; set; }
        ZXingScannerPage scanPage;
        public CheckInPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }



        async Task OpenScannerAsync()
        {
            //setup options
            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = true,
                TryHarder = true           
            };

            //add options and customize page
            scanPage = new ZXingScannerPage(options)
            {
                DefaultOverlayTopText = "Align the barcode within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = true
            };

            // Navigate to our scanner page
            await _navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _navigation.PopAsync();                  
                });
            };
        }


    }
}
