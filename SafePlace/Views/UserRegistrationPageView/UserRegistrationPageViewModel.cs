using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.UserRegistrationPageView
{
    class UserRegistrationPageViewModel
    {
        public ObservableCollection<string> Webcams { get; set; } = new ObservableCollection<string>();
    }
}
