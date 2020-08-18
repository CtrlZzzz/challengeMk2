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

        private Color firstTextColor;
        private Color secondTextColor;

        private FontAttributes firstTextAttribute;
        private FontAttributes secondTextAttribute;

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
        public static readonly BindableProperty FirstTextAttributeProperty = BindableProperty.Create(
            nameof(FirstTextAttribute), 
            typeof(FontAttributes), 
            typeof(DualLabel), 
            FontAttributes.None, 
            propertyChanged: OnFirstTextAttributeChanged);
        public static readonly BindableProperty SecondTextAttributeProperty = BindableProperty.Create(
            nameof(SecondTextAttribute),
            typeof(FontAttributes),
            typeof(DualLabel),
            FontAttributes.None,
            propertyChanged: OnSecondTextAttributeChanged);


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
        public FontAttributes FirstTextAttribute
        {
            get => (FontAttributes)GetValue(FirstTextAttributeProperty);
            set => SetValue(FirstTextAttributeProperty, value);
        }
        public FontAttributes SecondTextAttribute
        {
            get => (FontAttributes)GetValue(SecondTextAttributeProperty);
            set => SetValue(SecondTextAttributeProperty, value);
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
        private static void OnFirstTextAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeFirstTextAttribute(value);
        }
        private static void OnSecondTextAttributeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (DualLabel)bindable;
            var value = (FontAttributes)newValue;

            current.ChangeSecondTextAttribute(value);
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
            firstTextColor = value;
        }
        private void ChangeSecondTextColor(Color value)
        {
            secondTextColor = value;
        }
        private void ChangeFirstTextAttribute(FontAttributes value)
        {
            firstTextAttribute = value;
        }
        private void ChangeSecondTextAttribute(FontAttributes value)
        {
            secondTextAttribute = value;
        }

    }
}