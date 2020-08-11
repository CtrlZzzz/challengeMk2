using System;
using Xamarin.Forms;
using challengeMk2.Models;

namespace challengeMk2.DataSelectors
{
    public class SystemDetailTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GeneralInfoTemplate { get; set; }
        public DataTemplate SystemInfoTemplate { get; set; }
        public DataTemplate PrimaryStarInfoTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            StarSystem currentSystem = item as StarSystem;

            //return currentSystem.DataSelectorID switch
            //{
            //    0 => GeneralInfoTemplate,
            //    1 => SystemInfoTemplate,
            //    2 => PrimaryStarInfoTemplate,
            //    _ => GeneralInfoTemplate,
            //};

            switch (currentSystem.DataSelectorID)
            {
                case 0:
                    return GeneralInfoTemplate;
                case 1:
                    return SystemInfoTemplate;
                case 2:
                    return PrimaryStarInfoTemplate;
                default:
                    return GeneralInfoTemplate;
            }
        }
    }
}
