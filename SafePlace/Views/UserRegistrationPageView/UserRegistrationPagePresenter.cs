using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.UserRegistrationPageView
{
    class UserRegistrationPagePresenter
    {
        private readonly UserRegistrationPageViewModel _viewModel;
        public UserRegistrationPagePresenter(UserRegistrationPageViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
