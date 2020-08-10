using System;
using challengeMk2.Models;
namespace challengeMk2.ViewModels
{
    public class SystemDetailCarouselViewModel : BaseViewModel
    {
        public SystemDetailCarouselViewModel(StarSystem selectedSystem = null)
        {
            Title = "Star System Details";
        }
    }
}
