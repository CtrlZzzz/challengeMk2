using System;
using Xamarin.Forms;
using ChallengeMk2.Models;
using System.Collections.Generic;

namespace ChallengeMk2.DataSelectors
{
    public class SystemDetailTemplateSelector : DataTemplateSelector
    {
        Dictionary<int, DataTemplate> InfoTemplates;


        public DataTemplate GeneralInfoTemplate { get; set; }
        public DataTemplate SystemInfoTemplate { get; set; }
        public DataTemplate SystemInfo2Template { get; set; }
        public DataTemplate PrimaryStarInfoTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            InfoTemplates = new Dictionary<int, DataTemplate>
            {
                { 0, GeneralInfoTemplate },
                { 1, SystemInfoTemplate },
                { 2, SystemInfo2Template},
                { 3, PrimaryStarInfoTemplate }
            };

            StarSystem currentSystem = item as StarSystem;

            return InfoTemplates[currentSystem.DataSelectorID];
        }
    }
}
