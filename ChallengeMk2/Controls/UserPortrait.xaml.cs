using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ChallengeMk2.Controls
{
    public partial class UserPortrait : ContentView
    {
        public UserPortrait()
        {
            InitializeComponent();

            PortraitStroke.SetBinding(Shape.StrokeProperty, new Binding(nameof(PortraitStrokeColor), source: this));
            PortraitFill.SetBinding(Shape.FillProperty, new Binding(nameof(PortraitFillColor), source: this));
            PortraitGrid.SetBinding(Grid.WidthRequestProperty, new Binding(nameof(PortraitWidth), source: this));
            PortraitGrid.SetBinding(Grid.HeightRequestProperty, new Binding(nameof(PortraitHeight), source: this));
        }

        public static readonly BindableProperty PortraitStrokeColorProperty = BindableProperty.Create(
            nameof(PortraitStrokeColor),
            typeof(SolidColorBrush),
            typeof(UserPortrait),
            new SolidColorBrush(Color.DeepPink));
        public static readonly BindableProperty PortraitFillColorProperty = BindableProperty.Create(
            nameof(PortraitFillColor),
            typeof(SolidColorBrush),
            typeof(UserPortrait),
            new SolidColorBrush(Color.DeepPink));
        public static readonly BindableProperty PortraitWidthProperty = BindableProperty.Create(
            nameof(PortraitWidth),
            typeof(int),
            typeof(UserPortrait),
            50);
        public static readonly BindableProperty PortraitHeightProperty = BindableProperty.Create(
            nameof(PortraitHeight),
            typeof(int),
            typeof(UserPortrait),
            50);

        public SolidColorBrush PortraitStrokeColor
        {
            get => (SolidColorBrush) GetValue(PortraitStrokeColorProperty);
            set => SetValue(PortraitStrokeColorProperty, value);
        }
        public SolidColorBrush PortraitFillColor
        {
            get => (SolidColorBrush) GetValue(PortraitFillColorProperty);
            set => SetValue(PortraitFillColorProperty, value);
        }
        public int PortraitWidth
        {
            get => (int) GetValue(PortraitWidthProperty);
            set => SetValue(PortraitWidthProperty, value);
        }
        public int PortraitHeight
        {
            get => (int) GetValue(PortraitHeightProperty);
            set => SetValue(PortraitHeightProperty, value);
        }
    }
}