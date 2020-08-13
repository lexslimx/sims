using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Sims.ViewModels
{
    public class CheckInPageViewModel : BindableBase
    {
        private INavigation _navigation { get; set; }
        public CheckInPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        async void OpenScannerAsync()
        {
            //setup options
            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = true,
                TryHarder = true
            };

            //add options and customize page
            var scanPage = new ZXingScannerPage(options)
            {
                DefaultOverlayTopText = "Align the barcode within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = false
            };

            // Navigate to our scanner page
            //await _navigation.PushAsync(scanPage);

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

        private DelegateCommand _openScannerCommand;
        public DelegateCommand OpenScannerCommand => _openScannerCommand ?? (_openScannerCommand = new DelegateCommand(OpenScannerAsync));

        private string _ninNumber;
        public string NinNumber
        {
            get
            {
                return _ninNumber;
            }
            set
            {
                SetProperty(ref _ninNumber, value);
            }
        }
    }
}
