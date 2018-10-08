using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SafePlace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service.ConsoleLogger Logger = new Service.ConsoleLogger(); //might be bad practice, but currently convenient

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new WpfApp1.ViewModels.BaseViewModel();
            //FloorGrid.Children.Add() //will need to define an object of type UIElement (or that inherits it)

        }

        /// <summary>
        /// A test event to check which button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CamerasButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogInfo("You have clicked " + ((Button)sender).Name); 
        }
        /// <summary>
        /// When the mouse enters or leaves the graphical element of a camera, this method is called.
        /// This method is yet to be meaningfully implemented, but the meta code is like:
        /// ((CameraObject)sender).ToggleListVisibility();  If the list was visible, it becomes invisible and vice versa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.Beep(); //Perhaps more convenient than debuging provided you have hearing and audio.
        }
    }
}
