using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public class PopupLabel : ContentView
    {
        readonly Label popupLabel;
        readonly BoxView popupBackground;


        public PopupLabel()
        {
            popupLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            popupBackground = new BoxView()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            popupLabel.SetBinding(Label.TextProperty, new Binding(nameof(PopupLabelText), source: this));
            popupLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(PopupLabelFontSize), source: this));
            popupLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(PopupLabelFontAttributes), source: this));
            popupLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(PopupLabelTextColor), source: this));
            popupBackground.SetBinding(BoxView.WidthRequestProperty, new Binding(nameof(PopupBackgroundWidth), source: this));
            popupBackground.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(PopupBackgroundheight), source: this));
            popupBackground.SetBinding(BoxView.CornerRadiusProperty, new Binding(nameof(PopupBackgroundCornerRadius), source: this));
            popupBackground.SetBinding(BoxView.ColorProperty, new Binding(nameof(PopupBackgroundColor), source: this));

            Content = new Grid
            {
                Children =
                {
                    popupBackground,
                    popupLabel
                }
            };
            Content.Scale = 0.1;
        }


        public static readonly BindableProperty PopupLabelTextProperty = BindableProperty.Create(
            nameof(PopupLabelText),
            typeof(string),
            typeof(PopupLabel),
            "Pop");
        public static readonly BindableProperty PopupLabelFontSizeProperty = BindableProperty.Create(
            nameof(PopupLabelFontSize),
            typeof(int),
            typeof(PopupLabel),
            20);
        public static readonly BindableProperty PopupLabelFontAttributesProperty = BindableProperty.Create(
            nameof(PopupLabelFontAttributes),
            typeof(FontAttributes),
            typeof(PopupLabel),
            FontAttributes.Bold);
        public static readonly BindableProperty PopupLabelTextColorProperty = BindableProperty.Create(
            nameof(PopupLabelTextColor),
            typeof(Color),
            typeof(PopupLabel),
            Color.Black);
        public static readonly BindableProperty PopupBackgroundWidthProperty = BindableProperty.Create(
            nameof(PopupBackgroundWidth),
            typeof(int),
            typeof(PopupLabel),
            40);
        public static readonly BindableProperty PopupBackgroundheightProperty = BindableProperty.Create(
            nameof(PopupBackgroundheight),
            typeof(int),
            typeof(PopupLabel),
            40);
        public static readonly BindableProperty PopupBackgroundCornerRadiusProperty = BindableProperty.Create(
            nameof(PopupBackgroundCornerRadius),
            typeof(int),
            typeof(PopupLabel),
            10);
        public static readonly BindableProperty PopupBackgroundColorProperty = BindableProperty.Create(
            nameof(PopupBackgroundColor),
            typeof(Color),
            typeof(PopupLabel),
            Color.Yellow);

        public string PopupLabelText
        {
            get => (string) GetValue(PopupLabelTextProperty);
            set => SetValue(PopupLabelTextProperty, value);
        }
        public int PopupLabelFontSize
        {
            get => (int) GetValue(PopupLabelFontSizeProperty);
            set => SetValue(PopupLabelFontSizeProperty, value);
        }
        public FontAttributes PopupLabelFontAttributes
        {
            get => (FontAttributes) GetValue(PopupLabelFontAttributesProperty);
            set => SetValue(PopupLabelFontAttributesProperty, value);
        }
        public Color PopupLabelTextColor
        {
            get => (Color) GetValue(PopupLabelTextColorProperty);
            set => SetValue(PopupLabelTextColorProperty, value);
        }
        public int PopupBackgroundWidth
        {
            get => (int) GetValue(PopupBackgroundWidthProperty);
            set => SetValue(PopupBackgroundWidthProperty, value);
        }
        public int PopupBackgroundheight
        {
            get => (int) GetValue(PopupBackgroundheightProperty);
            set => SetValue(PopupBackgroundheightProperty, value);
        }
        public int PopupBackgroundCornerRadius
        {
            get => (int) GetValue(PopupBackgroundCornerRadiusProperty);
            set => SetValue(PopupBackgroundCornerRadiusProperty, value);
        }
        public Color PopupBackgroundColor
        {
            get => (Color) GetValue(PopupBackgroundColorProperty);
            set => SetValue(PopupBackgroundColorProperty, value);
        }


        //TODO => ASYNC VOID !!!! Try to delay a little bit the animation to give it time to appear...but i should find a way to do it without async void
        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            await AnimatePopup();
        }

        async Task AnimatePopup()
        {
            await Task.Delay(200);

            var anim = new Animation
            {
                { 0, 0.9, new Animation(v => Content.Scale = v, 0.1, 1.5) },
                { 0.9, 1, new Animation(v => Content.Scale = v, 1.5, 1) },
            };

            anim.Commit(Content, "PopupAnim", length: 500, easing: Easing.CubicInOut, repeat: () => false);
        }
    }
}