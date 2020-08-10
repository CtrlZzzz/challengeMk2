using System;
using System.Collections.Generic;

using challengeMk2.ViewModels;
using Xamarin.Forms;

namespace challengeMk2.Views
{
    public partial class SystemDetailCarouselPage : ContentPage
    {
        public SystemDetailCarouselPage(SystemDetailCarouselViewModel selectedSystemViewModel)
        {
            InitializeComponent();

            BindingContext = selectedSystemViewModel;
        }
    }
}
