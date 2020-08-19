using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ChallengeMk2.Controls
{
    public class DualLabel : ContentView
    {
        private Label firstText;
        private Label secondText;


        //BINDABLE PROPERTIES
        public static readonly BindableProperty FirstTextProperty = BindableProperty.Create(
            nameof(FirstText), 
            typeof(string), 
            typeof(DualLabel), 
            "First Text", 
            propertyChanged: OnFirstTextChanged);
        public static readonly BindableProperty SecondTextProperty = BindableProperty.Create(
            nameof(SecondText), 
            typeof(string), 
            typeof(DualLabel), 
            "Second Text", 
            propertyChanged: OnSecondTextChanged);
        public static readonly BindableProperty FirstTextColorProperty = BindableProperty.Create(
            nameof(FirstTextColor), 
            typeof(Color), 
            typeof(DualLabel), 
            Color.Black, 
            propertyChanged: OnFirstTextColorChanged);
        public static readonly BindableProperty SecondTextColorProperty = BindableProperty.Create(
            nameof(SecondTextColor), 
            typeof(Color), 
            typeof(DualLabel), 
            Color.Black, 
            propertyChanged: OnSecondTextColorChanged);
        public static readonly BindableProperty FirstTextFontAttributeProperty = BindableProperty.Create(
            nameof(FirstTextFontAttribute), 
            typeof(FontAttributes), 
            typeof(DualLabel), 
            FontAttributes.None, 
            propertyChanged: OnFirstTextFontAttributeChanged);
        public static readonly BindableProperty SecondTextFontAttributeProperty = BindableProperty.Create(
            nameof(SecondTextFontAttribute),
            typeof(FontAttributes),
            typeof(DualLabel),
            FontAttributes.None,
            propertyChanged: OnSecondTextFontAttributeChanged);
        public static readonly BindableProperty FirstTextSizeProperty = BindableProperty.Create(
            nameof(FirstTextSize),
            typeof(double),
            typeof(DualLabel),
            12,
            propertyChanged: OnFirstTextSizeChanged);
        public static readonly BindableProperty SecondTextSizeProperty = BindableProperty.Create(
            nameof(SecondTextSize),
            typeof(double),
            typeof(DualLabel),
            30,
            propertyChanged: OnSecondTextSizeChanged);
        public static readonly BindableProperty FirstTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(FirstTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.Start,
            propertyChanged: OnFirstTextHorizontalOptionsChanged);
        public static readonly BindableProperty SecondTextHorizontalOptionsProperty = BindableProperty.Create(
            nameof(SecondTextHorizontalOptions),
            typeof(LayoutOptions),
            typeof(DualLabel),
            LayoutOptions.End,
            propertyChanged: OnSecondTextHorizontalOptionsChanged);
        public static readonly BindableProperty FirstTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(FirstTextVerticalAlignement),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.End,
            propertyChanged: OnFirstTextVerticalAlignementChanged);
        public static readonly BindableProperty SecondTextVerticalAlignementProperty = BindableProperty.Create(
            nameof(SecondTextVerticalAlignement),
            typeof(TextAlignment),
            typeof(DualLabel),
            TextAlignment.Start,
            propertyChanged: OnSecondTextVerticalAlignementChanged);


        public string FirstText 
        { 
            get => (string)GetValue(FirstTextProperty); 
            set => SetValue(FirstTextProperty, value); 
        }
        public string SecondText 
        { 
            get => (string)GetValue(SecondTextProperty); 
            set => SetValue(SecondTextProperty, value); 
        }
        public Color FirstTextColor 
        { 
            get => (Color)GetValue(FirstTextColorProperty);
            set => SetValue(FirstTextColorProperty, value);
        }
        public Color SecondTextColor
        {
            get => (Color)GetValue(SecondTextColorProperty);
            set => SetValue(SecondTextColorProperty, value);
        }
        public FontAttributes FirstTextFontAttribute
        {
            get => (FontAttributes)GetValue(FirstTextFontAttributeProperty);
            set => SetValue(FirstTextFontAttributeProperty, value);
        }
        public FontAttributes SecondTextFontAttribute
        {
            get => (FontAttributes)GetValue(SecondTextFontAttributeProperty);
            set => SetValue(SecondTextFontAttributeProperty, value);
        }
        public double FirstTextSize
        {
            get => (double)GetValue(FirstTextSizeProperty);
            set => SetValue(FirstTextSizeProperty, value);
        }
        public double SecondTextSize
        {
            get => (double)GetValue(SecondTextSizeProperty);
            set => SetValue(SecondTextSizeProperty, value);
        }
        public LayoutOptions FirstTextHorizontalOptions 
        {
            get => (LayoutOptions)GetValue(FirstTextHorizontalOptionsProperty);
            set => SetValue(FirstTextHorizontalOptionsProperty, value);
        }
        public LayoutOptions SecondTextHorizontalOptions
        {
            get => (LayoutOptions)GetValue(SecondTextHorizontalOptionsProperty);
            set => SetValue(SecondTextHorizontalOptionsProperty, value);
        }
        public TextAlignment FirstTextVerticalAlignement 
        {
            get => (TextAlignment)GetValue(FirstTextVerticalAlignementProperty);
            set => SetValue(FirstTextVerticalAlignementProperty, value);
        }
        public TextAlignment SecondTextVerticalAlignement
        {
            get => (TextAlignment)GetValue(SecondTextVerticalAlignementProperty);
            set => SetValue(SecondTextVerticalAlignementProperty, value);
        }



        //CONSTRUCTOR
        public DualLabel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }



        //PROPERTIES CHANGED
        private static void OnFirstTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (string)newValue;

            current.ChangeFirstText(value);
        }
        private static void OnSecondTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (string)newValue;

            current.ChangeSecondText(value);
        }
        private static void OnFirstTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (Color)newValue;

            current.ChangeFirstTextColor(value);
        }
        private static void OnSecondTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (Color)newValue;

            current.ChangeSecondTextColor(value);
        }
        private static void OnFirstTextFontAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeFirstTextFontAttribute(value);
        }
        private static void OnSecondTextFontAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeSecondTextFontAttribute(value);
        }
        private static void OnFirstTextSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (double)newValue;

            current.ChangeFirstTextSize(value);
        }
        private static void OnSecondTextSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (double)newValue;

            current.ChangeSecondTextSize(value);
        }
        private static void OnFirstTextHorizontalOptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (LayoutOptions)newValue;

            current.ChangeFirstTextHorizontalOptions(value);
        }
        private static void OnSecondTextHorizontalOptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (LayoutOptions)newValue;

            current.ChangeSecondTextHorizontalOptions(value);
        }
        private static void OnFirstTextVerticalAlignementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (TextAlignment)newValue;

            current.ChangeFirstTextVerticalAlignement(value);
        }
        private static void OnSecondTextVerticalAlignementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (TextAlignment)newValue;

            current.ChangeSecondTextVerticalAlignement(value);
        }


        private void ChangeFirstText(string value)
        {
            firstText.Text = value;
        }
        private void ChangeSecondText(string value)
        {
            secondText.Text = value;
        }
        private void ChangeFirstTextColor(Color value)
        {
            firstText.TextColor = value;
        }
        private void ChangeSecondTextColor(Color value)
        {
            secondText.TextColor = value;
        }
        private void ChangeFirstTextFontAttribute(FontAttributes value)
        {
            firstText.FontAttributes = value;
        }
        private void ChangeSecondTextFontAttribute(FontAttributes value)
        {
            secondText.FontAttributes = value;
        }
        private void ChangeFirstTextSize(double value)
        {
            firstText.FontSize = value;
        }
        private void ChangeSecondTextSize(double value)
        {
            secondText.FontSize = value;
        }
        private void ChangeFirstTextHorizontalOptions(LayoutOptions value)
        {
            firstText.HorizontalOptions = value;
        }
        private void ChangeSecondTextHorizontalOptions(LayoutOptions value)
        {
            secondText.HorizontalOptions = value;
        }
        private void ChangeFirstTextVerticalAlignement(TextAlignment value)
        {
            firstText.VerticalTextAlignment = value;
        }
        private void ChangeSecondTextVerticalAlignement(TextAlignment value)
        {
            secondText.VerticalTextAlignment = value;
        }

    }
}