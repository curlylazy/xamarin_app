using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;
using Xamarin.Forms;

namespace mobileapp
{
    // https://forums.xamarin.com/discussion/100620/how-to-add-image-in-xaml
    // https://askxammy.com/working-with-gridlayout-in-xamarin-forms/
    // https://www.youtube.com/watch?v=NNm4_7VSxV0
    // https://stackoverflow.com/questions/49595895/xamarinforms-listview-itemtapped-and-itemselected-is-running-together-when-selec

    // about toolbar item
    // https://www.c-sharpcorner.com/article/toolbar-item-in-xamarin-cross-platform-with-toolbar-order-example/

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private StoreContext context = null;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        private async void BtnForm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageList());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}
