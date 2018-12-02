using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SafePlace.Views.MainWindowView
{
    class MainWindowViewModel : BaseViewModel
    {

        //Holds currently displayed page.
        private Page _displayPage;
        public Page DisplayPage
        {
            get
            {
                return _displayPage;
            }
            set
            {
                _displayPage = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _homePageCommand;
        public ICommand HomePageCommand
        {
            get
            {
                return _homePageCommand;
            }
            set
            {
                _homePageCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _settingsPageCommand;
        public ICommand SettingsPageCommand
        {
            get
            {
                return _settingsPageCommand;
            }
            set
            {
                _settingsPageCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _userRegistrationPageCommand;
        public ICommand UserRegistrationPageCommand
        {
            get
            {
                return _userRegistrationPageCommand;
            }
            set
            {
                _userRegistrationPageCommand = value;
                NotifyPropertyChanged();
            }
        }

    }
}
