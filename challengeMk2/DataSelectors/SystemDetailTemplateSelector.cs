using System;
using Xamarin.Forms;
using challengeMk2.Models;
using System.Collections.Generic;

namespace challengeMk2.DataSelectors
{
    public class SystemDetailTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GeneralInfoTemplate { get; set; }
        public DataTemplate SystemInfoTemplate { get; set; }
        public DataTemplate PrimaryStarInfoTemplate { get; set; }

        private Dictionary<int, DataTemplate> InfoTemplates; 

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            InfoTemplates = new Dictionary<int, DataTemplate>
            {
                { 0, GeneralInfoTemplate },
                { 1, SystemInfoTemplate },
                { 2, PrimaryStarInfoTemplate }
            };

            StarSystem currentSystem = item as StarSystem;

            return InfoTemplates[currentSystem.DataSelectorID];
        }
    }
}
