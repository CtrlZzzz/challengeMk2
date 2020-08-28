using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public class DualLabel : ContentView
    {
        readonly Label topText;
        readonly Label bottomText;
        readonly BoxView midLine;
        readonly BoxView bottomSpace;

        public DualLabel()
        {
            topText = new Label()
            {
                //TODO : add Font Family bindable prop !
                TextColor = this.TopTextColor,
                BackgroundColor = Color.Transparent,
                FontAttributes = this.TopTextFontAttribute,
                HorizontalOptions = this.TopTextHorizontalOptions,
                VerticalTextAlignment = this.TopTextVerticalAlignment,
                FontSize = this.TopTextSize
            };
            bottomText = new Label()
            {
                //TODO : add Font Family bindable prop !
                TextColor = this.BottomTextColor,
                BackgroundColor = Color.Transparent,
                FontAttributes = this.BottomTextFontAttribute,
                HorizontalOptions = this.BottomTextHorizontalOptions,
                VerticalTextAlignment = this.BottomTextVerticalAlignment,
                FontSize = this.BottomTextSize
            };
            midLine = new BoxView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Color = this.MidLineColor,
                HeightRequest = this.MidLineHeight
            };
            bottomSpace = new BoxView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Color = Color.Transparent,
                HeightRequest = this.BottomSpaceHeight
            };

            topText.SetBinding(Label.TextProperty, new Binding(nameof(TopText), source: this));
            bottomText.SetBinding(Label.TextProperty, new Binding(nameof(BottomText), source: this));
            topText.SetBinding(Label.TextColorProperty, new Binding(nameof(TopTextColor), source: this));
            bottomText.SetBinding(Label.TextColorProperty, new Binding(nameof(BottomTextColor), source: this));
            topText.SetBinding(Label.FontAttributesProperty, new Binding(nameof(TopTextFontAttribute), source: this));
            bottomText.SetBinding(Label.FontAttributesProperty, new Binding(nameof(BottomTextFontAttribute), source: this));
            topText.SetBinding(Label.FontSizeProperty, new Binding(nameof(TopTextSize), source: this));
            bottomText.SetBinding(Label.FontSizeProperty, new Binding(nameof(BottomTextSize), source: this));
            topText.SetBinding(Label.HorizontalOptionsProperty, new Binding(nameof(TopTextHorizontalOptions), source: this));
            bottomText.SetBinding(Label.HorizontalOptionsProperty, new Binding(nameof(BottomTextHorizontalOptions), source: this));
            topText.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(TopTextVerticalAlignment), source: this));
            bottomText.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(BottomTextVerticalAlignment), source: this));
            midLine.SetBinding(BoxView.ColorProperty, new Binding(nameof(MidLineColor), source: this));
            midLine.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(MidLineHeight), source: this));
            bottomSpace.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(BottomSpaceHeight), source: this));

            AdaptTextAlignment();

            Content = new StackLayout
            {
                Spacing = 0,

                Children =
                {
                    topText,
                    midLine,
                    bottomText,
                    bottomSpace
                }
            };
        }

        public static readonly BindableProperty TopTextProperty = BindableProperty.Create(
            nameof(TopText),
            typeof(string),
            typeof(DualLabel),
            "Top Text");
        public static readonly BindableProperty BottomTextProperty = BindableProperty.Create(
            nameof(BottomText),
            typeof(string),
            typeof(DualLabel),
            "Bottom Text");
        public static readonly BindableProperty TopTextColorProperty = BindableProperty.Create(
            nameof(TopTextColor),
            typeof(Color),
            typeof(DualLabel),
            Color.Black);
        public static readonly BindableProperty BottomTextColorProperty = BindableProperty.Create(
            nameof(BottomTextColor),
            typeof(Color),
            typeof(DualLabel),
            Color.Black);
        public static readonly BindableProperty TopTextFontAttributeProperty = BindableProperty.Create(
            nameof(TopTextFontAttribute),
            typeof(FontAttributes),
            typeof(DualLabel),
            FontAttributes.None);
        public static readonly BindableProperty BottomTextFontAttributeProperty = BindableProperty.Create(
            nameof(BottomTextFontAttribute),
            typeof(FontAttributes),
            typeof(DualLabel),
            FontAttributes.None);
        public static readonly BindableProperty TopTextSizeProperty = BindableProperty.Create(
            nameof(TopTextSize),
            typeof(int),
            typeof(DualLabel),
            12);
        public static readonly BindableProperty BottomTextSizeProperty = BindableProperty.Create(
            nameof(BottomTextSize),
            typeof(int),
            typeof(DualLabel),
            25);
        public static readonly BindableProperty TopTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(TopTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.Start);
        public static readonly BindableProperty BottomTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(BottomTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.End);
        public static readonly BindableProperty TopTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(TopTextVerticalAlignment),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.End);
        public static readonly BindableProperty BottomTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(BottomTextVerticalAlignment),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.Start);
        public static readonly BindableProperty MidLineColorProperty = BindableProperty.Create(
            nameof(MidLineColor),
            typeof(Color),
            typeof(DualLabel),
            Color.Black);
        public static readonly BindableProperty MidLineHeightProperty = BindableProperty.Create(
            nameof(MidLineHeight),
            typeof(int),
            typeof(DualLabel),
            1);
        public static readonly BindableProperty BottomSpaceHeightProperty = BindableProperty.Create(
            nameof(BottomSpaceHeight),
            typeof(int),
            typeof(DualLabel),
            20);

        public string TopText
        {
            get => (string)GetValue(TopTextProperty);
            set => SetValue(TopTextProperty, value);
        }
        public string BottomText
        {
            get => (string)GetValue(BottomTextProperty);
            set => SetValue(BottomTextProperty, value);
        }
        public Color TopTextColor
        {
            get => (Color)GetValue(TopTextColorProperty);
            set => SetValue(TopTextColorProperty, value);
        }
        public Color BottomTextColor
        {
            get => (Color)GetValue(BottomTextColorProperty);
            set => SetValue(BottomTextColorProperty, value);
        }
        public FontAttributes TopTextFontAttribute
        {
            get => (FontAttributes)GetValue(TopTextFontAttributeProperty);
            set => SetValue(TopTextFontAttributeProperty, value);
        }
        public FontAttributes BottomTextFontAttribute
        {
            get => (FontAttributes)GetValue(BottomTextFontAttributeProperty);
            set => SetValue(BottomTextFontAttributeProperty, value);
        }
        public int TopTextSize
        {
            get => (int)GetValue(TopTextSizeProperty);
            set => SetValue(TopTextSizeProperty, value);
        }
        public int BottomTextSize
        {
            get => (int)GetValue(BottomTextSizeProperty);
            set => SetValue(BottomTextSizeProperty, value);
        }
        public LayoutOptions TopTextHorizontalOptions
        {
            get => (LayoutOptions)GetValue(TopTextHorizontalOptionsProperty);
            set => SetValue(TopTextHorizontalOptionsProperty, value);
        }
        public LayoutOptions BottomTextHorizontalOptions
        {
            get => (LayoutOptions)GetValue(BottomTextHorizontalOptionsProperty);
            set => SetValue(BottomTextHorizontalOptionsProperty, value);
        }
        public TextAlignment TopTextVerticalAlignment
        {
            get => (TextAlignment)GetValue(TopTextVerticalAlignementProperty);
            set => SetValue(TopTextVerticalAlignementProperty, value);
        }
        public TextAlignment BottomTextVerticalAlignment
        {
            get => (TextAlignment)GetValue(BottomTextVerticalAlignementProperty);
            set => SetValue(BottomTextVerticalAlignementProperty, value);
        }
        public Color MidLineColor
        {
            get => (Color)GetValue(MidLineColorProperty);
            set => SetValue(MidLineColorProperty, value);
        }
        public int MidLineHeight
        {
            get => (int)GetValue(MidLineHeightProperty);
            set => SetValue(MidLineHeightProperty, value);
        }
        public int BottomSpaceHeight
        {
            get => (int)GetValue(BottomSpaceHeightProperty);
            set => SetValue(BottomSpaceHeightProperty, value);
        }

        void AdaptTextAlignment()
        {
            if (topText.HorizontalOptions.Alignment == LayoutAlignment.Start)
            {
                topText.HorizontalTextAlignment = TextAlignment.Start;
            }
            else if (topText.HorizontalOptions.Alignment == LayoutAlignment.Center)
            {
                topText.HorizontalTextAlignment = TextAlignment.Center;
            }
            else
            {
                topText.HorizontalTextAlignment = TextAlignment.End;
            }

            if (bottomText.HorizontalOptions.Alignment == LayoutAlignment.Start)
            {
                bottomText.HorizontalTextAlignment = TextAlignment.Start;
            }
            else if (bottomText.HorizontalOptions.Alignment == LayoutAlignment.Center)
            {
                bottomText.HorizontalTextAlignment = TextAlignment.Center;
            }
            else
            {
                bottomText.HorizontalTextAlignment = TextAlignment.End;
            }
        }
    }
}