using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public partial class AnimatedLabel : ContentView
    {
        public AnimatedLabel()
        {
            InitializeComponent();

            AnimLabel.SetBinding(Label.TextProperty, new Binding(nameof(AnimLabelText), source: this));
            AnimLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(AnimLabelFontSize), source: this));
            AnimLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(AnimLabelFontAttributes), source: this));
            AnimLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(AnimLabelTextColor), source: this));
            AnimBackground.SetBinding(BoxView.WidthRequestProperty, new Binding(nameof(AnimBackgroundWidth), source: this));
            AnimBackground.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(AnimBackgroundheight), source: this));
            AnimBackground.SetBinding(BoxView.CornerRadiusProperty, new Binding(nameof(AnimBackgroundCornerRadius), source: this));
            AnimBackground.SetBinding(BoxView.ColorProperty, new Binding(nameof(AnimBackgroundColor), source: this));
        }

        public static readonly BindableProperty AnimLabelTextProperty = BindableProperty.Create(
            nameof(AnimLabelText),
            typeof(string),
            typeof(AnimatedLabel),
            "Pop");
        public static readonly BindableProperty AnimLabelFontSizeProperty = BindableProperty.Create(
            nameof(AnimLabelFontSize),
            typeof(int),
            typeof(AnimatedLabel),
            20);
        public static readonly BindableProperty AnimLabelFontAttributesProperty = BindableProperty.Create(
            nameof(AnimLabelFontAttributes),
            typeof(FontAttributes),
            typeof(AnimatedLabel),
            FontAttributes.Bold);
        public static readonly BindableProperty AnimLabelTextColorProperty = BindableProperty.Create(
            nameof(AnimLabelTextColor),
            typeof(Color),
            typeof(AnimatedLabel),
            Color.Black);
        public static readonly BindableProperty AnimBackgroundWidthProperty = BindableProperty.Create(
            nameof(AnimBackgroundWidth),
            typeof(int),
            typeof(AnimatedLabel),
            40);
        public static readonly BindableProperty AnimBackgroundheightProperty = BindableProperty.Create(
            nameof(AnimBackgroundheight),
            typeof(int),
            typeof(AnimatedLabel),
            40);
        public static readonly BindableProperty AnimBackgroundCornerRadiusProperty = BindableProperty.Create(
            nameof(AnimBackgroundCornerRadius),
            typeof(int),
            typeof(AnimatedLabel),
            10);
        public static readonly BindableProperty AnimBackgroundColorProperty = BindableProperty.Create(
            nameof(AnimBackgroundColor),
            typeof(Color),
            typeof(AnimatedLabel),
            Color.Yellow);

        public string AnimLabelText
        {
            get => (string) GetValue(AnimLabelTextProperty);
            set => SetValue(AnimLabelTextProperty, value);
        }
        public int AnimLabelFontSize
        {
            get => (int) GetValue(AnimLabelFontSizeProperty);
            set => SetValue(AnimLabelFontSizeProperty, value);
        }
        public FontAttributes AnimLabelFontAttributes
        {
            get => (FontAttributes) GetValue(AnimLabelFontAttributesProperty);
            set => SetValue(AnimLabelFontAttributesProperty, value);
        }
        public Color AnimLabelTextColor
        {
            get => (Color) GetValue(AnimLabelTextColorProperty);
            set => SetValue(AnimLabelTextColorProperty, value);
        }
        public int AnimBackgroundWidth
        {
            get => (int) GetValue(AnimBackgroundWidthProperty);
            set => SetValue(AnimBackgroundWidthProperty, value);
        }
        public int AnimBackgroundheight
        {
            get => (int) GetValue(AnimBackgroundheightProperty);
            set => SetValue(AnimBackgroundheightProperty, value);
        }
        public int AnimBackgroundCornerRadius
        {
            get => (int) GetValue(AnimBackgroundCornerRadiusProperty);
            set => SetValue(AnimBackgroundCornerRadiusProperty, value);
        }
        public Color AnimBackgroundColor
        {
            get => (Color) GetValue(AnimBackgroundColorProperty);
            set => SetValue(AnimBackgroundColorProperty, value);
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            AnimatePopup();
        }

        void AnimatePopup()
        {
            var labelAnimation = new Animation
            {
                { 0, 0.9, new Animation(v => AnimLabel.Scale = v, 0.1, 1.5) },
                { 0.9, 1, new Animation(v => AnimLabel.Scale = v, 1.5, 1) }
            };
            var backgroundAnimation = new Animation
            {
                { 0, 0.9, new Animation(v => AnimBackground.Scale = v, 0.1, 1.5) },
                { 0.9, 1, new Animation(v => AnimBackground.Scale = v, 1.5, 1) }
            };

            labelAnimation.Commit(AnimLabel, "PopupAnim", length: 500, easing: Easing.CubicInOut, repeat: () => false);
            backgroundAnimation.Commit(AnimBackground, "PopupAnim", length: 500, easing: Easing.CubicInOut, repeat: () => false);
        }
    }
}