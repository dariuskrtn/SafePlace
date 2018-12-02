using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SafePlaceWpf.Behaviors
{
    class FloorViewBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        "Command", typeof(ICommand), typeof(FloorViewBehavior), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= SelectionChanged;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Command == null) return;
            var ClickedListView = sender as ListView;
            ListView ClickedList = e.Source as ListView;
            if (Command.CanExecute(e)) Command.Execute(ClickedList.SelectedItem);
        }
    }
}
