using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.Uwp.LexDbSample.ViewModels;

namespace XamlBrewer.Uwp.LexDbSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();

            viewModel = ((MainPageViewModel)this.DataContext);
            viewModel.PropertyChanged += MainPage_PropertyChanged;
        }

        /// <summary>
        /// TODO: Fix binding in CoverFlow. Make SelectedItem a dependency property.
        /// </summary>
        private void MainPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedCar" && viewModel.SelectedCar != null)
            {
                this.CoverFlow.SelectedItem = viewModel.SelectedCar;
            }
        }

        /// <summary>
        /// TODO: Fix binding in CoverFlow. Make SelectedItem a dependency property.
        /// </summary>
        private void CoverFlow_SelectedItemChanged(Controls.CoverFlowEventArgs e)
        {
            viewModel.SelectedCar = this.CoverFlow.SelectedItem as VintageMuscleCarViewModel;
        }
    }
}
