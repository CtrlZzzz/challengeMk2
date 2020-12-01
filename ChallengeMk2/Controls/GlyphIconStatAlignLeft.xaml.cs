using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public partial class GlyphIconStatAlignLeft : ContentView
    {
        public GlyphIconStatAlignLeft()
        {
            InitializeComponent();

            TitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(TitleLabelText), source: this));
            TitleLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(TitleLabelFontSize), source: this));
            TitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(TitleLabelFontAttributes), source: this));
            TitleLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TitleLabelTextColor), source: this));

            ValueLabel.SetBinding(Label.TextProperty, new Binding(nameof(ValueLabelText), source: this));
            ValueLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(ValueLabelFontSize), source: this));
            ValueLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(ValueLabelFontAttributes), source: this));
            ValueLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(ValueLabelTextColor), source: this));

            GlyphIcon.SetBinding(FontImageSource.GlyphProperty, new Binding(nameof(GlyphIconGlyph), source: this));
            GlyphIcon.SetBinding(FontImageSource.FontFamilyProperty, new Binding(nameof(GlyphIconFontFamily), source: this));
            GlyphIcon.SetBinding(FontImageSource.SizeProperty, new Binding(nameof(GlyphIconSize), source: this));
            GlyphIcon.SetBinding(FontImageSource.ColorProperty, new Binding(nameof(GlyphIconColor), source: this));

        }

        public static readonly BindableProperty TitleLabelTextProperty = BindableProperty.Create(
            nameof(TitleLabelText),
            typeof(string),
            typeof(GlyphIconStatAlignLeft),
            "Statistic name");
        public static readonly BindableProperty TitleLabelFontSizeProperty = BindableProperty.Create(
            nameof(TitleLabelFontSize),
            typeof(int),
            typeof(GlyphIconStatAlignLeft),
            12);
        public static readonly BindableProperty TitleLabelFontAttributesProperty = BindableProperty.Create(
            nameof(TitleLabelFontAttributes),
            typeof(FontAttributes),
            typeof(GlyphIconStatAlignLeft),
            FontAttributes.Bold);
        public static readonly BindableProperty TitleLabelTextColorProperty = BindableProperty.Create(
            nameof(TitleLabelTextColor),
            typeof(Color),
            typeof(GlyphIconStatAlignLeft),
            Color.Black);

        public static readonly BindableProperty ValueLabelTextProperty = BindableProperty.Create(
            nameof(ValueLabelText),
            typeof(string),
            typeof(GlyphIconStatAlignLeft),
            "Value");
        public static readonly BindableProperty ValueLabelFontSizeProperty = BindableProperty.Create(
            nameof(ValueLabelFontSize),
            typeof(int),
            typeof(GlyphIconStatAlignLeft),
            15);
        public static readonly BindableProperty ValueLabelFontAttributesProperty = BindableProperty.Create(
            nameof(ValueLabelFontAttributes),
            typeof(FontAttributes),
            typeof(GlyphIconStatAlignLeft),
            FontAttributes.Bold);
        public static readonly BindableProperty ValueLabelTextColorProperty = BindableProperty.Create(
            nameof(ValueLabelTextColor),
            typeof(Color),
            typeof(GlyphIconStatAlignLeft),
            Color.Black);

        public static readonly BindableProperty GlyphIconGlyphProperty = BindableProperty.Create(
            nameof(GlyphIconGlyph),
            typeof(string),
            typeof(GlyphIconStatAlignLeft),
            "&#xf05a;");
        public static readonly BindableProperty GlyphIconFontFamilyProperty = BindableProperty.Create(
            nameof(GlyphIconFontFamily),
            typeof(string),
            typeof(GlyphIconStatAlignLeft),
            "fas");
        public static readonly BindableProperty GlyphIconSizeProperty = BindableProperty.Create(
            nameof(GlyphIconSize),
            typeof(int),
            typeof(GlyphIconStatAlignLeft),
            30);
        public static readonly BindableProperty GlyphIconColorProperty = BindableProperty.Create(
            nameof(GlyphIconColor),
            typeof(Color),
            typeof(GlyphIconStatAlignLeft),
            Color.Black);

        public string TitleLabelText
        {
            get => (string) GetValue(TitleLabelTextProperty);
            set => SetValue(TitleLabelTextProperty, value);
        }
        public int TitleLabelFontSize
        {
            get => (int) GetValue(TitleLabelFontSizeProperty);
            set => SetValue(TitleLabelFontSizeProperty, value);
        }
        public FontAttributes TitleLabelFontAttributes
        {
            get => (FontAttributes) GetValue(TitleLabelFontAttributesProperty);
            set => SetValue(TitleLabelFontAttributesProperty, value);
        }
        public Color TitleLabelTextColor
        {
            get => (Color) GetValue(TitleLabelTextColorProperty);
            set => SetValue(TitleLabelTextColorProperty, value);
        }

        public string ValueLabelText
        {
            get => (string) GetValue(ValueLabelTextProperty);
            set => SetValue(ValueLabelTextProperty, value);
        }
        public int ValueLabelFontSize
        {
            get => (int) GetValue(ValueLabelFontSizeProperty);
            set => SetValue(ValueLabelFontSizeProperty, value);
        }
        public FontAttributes ValueLabelFontAttributes
        {
            get => (FontAttributes) GetValue(ValueLabelFontAttributesProperty);
            set => SetValue(ValueLabelFontAttributesProperty, value);
        }
        public Color ValueLabelTextColor
        {
            get => (Color) GetValue(ValueLabelTextColorProperty);
            set => SetValue(ValueLabelTextColorProperty, value);
        }

        public string GlyphIconGlyph
        {
            get => (string) GetValue(GlyphIconGlyphProperty);
            set => SetValue(GlyphIconGlyphProperty, value);
        }
        public string GlyphIconFontFamily
        {
            get => (string) GetValue(GlyphIconFontFamilyProperty);
            set => SetValue(GlyphIconFontFamilyProperty, value);
        }
        public int GlyphIconSize
        {
            get => (int) GetValue(GlyphIconSizeProperty);
            set => SetValue(GlyphIconSizeProperty, value);
        }
        public Color GlyphIconColor
        {
            get => (Color) GetValue(GlyphIconColorProperty);
            set => SetValue(GlyphIconColorProperty, value);
        }
    }
}