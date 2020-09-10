using System.Net;
using Xamarin.Forms;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataSelectors
{
    public class PuzzleResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OkResultTemplate { get; set; }
        public DataTemplate AcceptedResultTemplate { get; set; }
        public DataTemplate ResetResultTemplate { get; set; }
        public DataTemplate MessageResultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentResult = item as TryResult;

            return currentResult.Status switch
            {
                HttpStatusCode.Accepted => AcceptedResultTemplate,
                HttpStatusCode.InternalServerError => MessageResultTemplate,
                HttpStatusCode.OK => OkResultTemplate,
                HttpStatusCode.ResetContent => ResetResultTemplate,
                _ => MessageResultTemplate,
            };
        }
    }
}
