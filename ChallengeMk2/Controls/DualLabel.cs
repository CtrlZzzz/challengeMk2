using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public class DualLabel : ContentView
    {
        private Label topText;    
        private Label bottomText;   

        private BoxView midLine;    // Line between labels
        private BoxView bottomSpace;    // To make Space at the bottom of the control


        //BINDABLE PROPERTIES
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
            Color.Black, 
            propertyChanged: OnTopTextColorChanged);
        public static readonly BindableProperty BottomTextColorProperty = BindableProperty.Create(
            nameof(BottomTextColor), 
            typeof(Color), 
            typeof(DualLabel), 
            Color.Black, 
            propertyChanged: OnBottomTextColorChanged);
        public static readonly BindableProperty TopTextFontAttributeProperty = BindableProperty.Create(
            nameof(TopTextFontAttribute), 
            typeof(FontAttributes), 
            typeof(DualLabel), 
            FontAttributes.None, 
            propertyChanged: OnTopTextFontAttributeChanged);
        public static readonly BindableProperty BottomTextFontAttributeProperty = BindableProperty.Create(
            nameof(BottomTextFontAttribute),
            typeof(FontAttributes),
            typeof(DualLabel),
            FontAttributes.None,
            propertyChanged: OnBottomTextFontAttributeChanged);
        public static readonly BindableProperty TopTextSizeProperty = BindableProperty.Create(
            nameof(TopTextSize),
            typeof(int),
            typeof(DualLabel),
            14,
            propertyChanged: OnTopTextSizeChanged);
        public static readonly BindableProperty BottomTextSizeProperty = BindableProperty.Create(
            nameof(BottomTextSize),
            typeof(int),
            typeof(DualLabel),
            25,
            propertyChanged: OnBottomTextSizeChanged);
        public static readonly BindableProperty TopTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(TopTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.Start,
            propertyChanged: OnTopTextHorizontalOptionsChanged);
        public static readonly BindableProperty BottomTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(BottomTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.End,
            propertyChanged: OnBottomTextHorizontalOptionsChanged);
        public static readonly BindableProperty TopTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(TopTextVerticalAlignement),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.End,
            propertyChanged: OnTopTextVerticalAlignementChanged);
        public static readonly BindableProperty BottomTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(BottomTextVerticalAlignement),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.Start,
            propertyChanged: OnBottomTextVerticalAlignementChanged);
        public static readonly BindableProperty MidLineColorProperty = BindableProperty.Create(
            nameof(MidLineColor),
            typeof(Color),
            typeof(DualLabel),
            Color.Black,
            propertyChanged: OnMidLineColorChanged);
        public static readonly BindableProperty MidLineHeightProperty = BindableProperty.Create(
            nameof(MidLineHeight),
            typeof(int),
            typeof(DualLabel),
            1,
            propertyChanged: OnMidLineHeightChanged);
        public static readonly BindableProperty BottomSpaceHeightProperty = BindableProperty.Create(
            nameof(BottomSpaceHeight),
            typeof(int),
            typeof(DualLabel),
            20,
            propertyChanged: OnBottomSpaceHeightChanged);


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
        public TextAlignment TopTextVerticalAlignement 
        {
            get => (TextAlignment)GetValue(TopTextVerticalAlignementProperty);
            set => SetValue(TopTextVerticalAlignementProperty, value);
        }
        public TextAlignment BottomTextVerticalAlignement
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



        //CONSTRUCTOR
        public DualLabel()
        {
            //Create elements :
            topText = new Label()
            {
                //TODO : add Font Family bindable prop !
                //Text = this.TopText,
                TextColor = this.TopTextColor,
                BackgroundColor = Color.Transparent,
                FontAttributes = this.TopTextFontAttribute,
                HorizontalOptions = this.TopTextHorizontalOptions,
                VerticalTextAlignment = this.TopTextVerticalAlignement,
                FontSize = this.TopTextSize
            };
            bottomText = new Label()
            {
                //TODO : add Font Family bindable prop !
                TextColor = this.BottomTextColor,
                BackgroundColor = Color.Transparent,
                FontAttributes = this.BottomTextFontAttribute,
                HorizontalOptions = this.BottomTextHorizontalOptions,
                VerticalTextAlignment = this.BottomTextVerticalAlignement,
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

            //Add Binding to elements
            topText.SetBinding(Label.TextProperty, new Binding(nameof(TopText), source: this) );
            bottomText.SetBinding(Label.TextProperty, new Binding(nameof(BottomText), source: this));
            //bottomText.FontAttributes.SetBinding(Label.FontAttributesProperty, new Binding(nameof(BottomTextFontAttribute), source: this));

            //And layout them :
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



        //PROPERTIES CHANGED
        private static void OnTopTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (Color)newValue;

            current.ChangeTopTextColor(value);
        }
        private static void OnBottomTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (Color)newValue;

            current.ChangeBottomTextColor(value);
        }
        private static void OnTopTextFontAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeTopTextFontAttribute(value);
        }
        private static void OnBottomTextFontAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeBottomTextFontAttribute(value);
        }
        private static void OnTopTextSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (int)newValue;

            current.ChangeTopTextSize(value);
        }
        private static void OnBottomTextSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (int)newValue;

            current.ChangeBottomTextSize(value);
        }
        private static void OnTopTextHorizontalOptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (LayoutOptions)newValue;

            current.ChangeTopTextHorizontalOptions(value);
        }
        private static void OnBottomTextHorizontalOptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (LayoutOptions)newValue;

            current.ChangeBottomTextHorizontalOptions(value);
        }
        private static void OnTopTextVerticalAlignementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (TextAlignment)newValue;

            current.ChangeTopTextVerticalAlignement(value);
        }
        private static void OnBottomTextVerticalAlignementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (TextAlignment)newValue;

            current.ChangeBottomTextVerticalAlignement(value);
        }
        private static void OnMidLineColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (Color)newValue;

            current.ChangeMidLineColor(value);
        }
        private static void OnMidLineHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (int)newValue;

            current.ChangeMidLineHeight(value);
        }
        private static void OnBottomSpaceHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (int)newValue;

            current.ChangeBottomSpaceHeight(value);
        }


        //PRIVATE METHODS
        private void ChangeTopTextColor(Color value)
        {
            topText.TextColor = value;
        }
        private void ChangeBottomTextColor(Color value)
        {
            bottomText.TextColor = value;
        }
        private void ChangeTopTextFontAttribute(FontAttributes value)
        {
            topText.FontAttributes = value;
        }
        private void ChangeBottomTextFontAttribute(FontAttributes value)
        {
            bottomText.FontAttributes = value;
        }
        private void ChangeTopTextSize(double value)
        {
            topText.FontSize = value;
        }
        private void ChangeBottomTextSize(double value)
        {
            bottomText.FontSize = value;
        }
        private void ChangeTopTextHorizontalOptions(LayoutOptions value)
        {
            topText.HorizontalOptions = value;
        }
        private void ChangeBottomTextHorizontalOptions(LayoutOptions value)
        {
            bottomText.HorizontalOptions = value;
        }
        private void ChangeTopTextVerticalAlignement(TextAlignment value)
        {
            topText.VerticalTextAlignment = value;
        }
        private void ChangeBottomTextVerticalAlignement(TextAlignment value)
        {
            bottomText.VerticalTextAlignment = value;
        }
        private void ChangeMidLineColor(Color value)
        {
            midLine.Color = value;
        }
        private void ChangeMidLineHeight(double value)
        {
            midLine.HeightRequest = value;
        }
        private void ChangeBottomSpaceHeight(double value)
        {
            bottomSpace.HeightRequest = value;
        }
    }
}