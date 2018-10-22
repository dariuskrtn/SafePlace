using SafePlace.WpfComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SafePlace.Behaviors
{
    //Generic behavior implementing multiple choice in Combobox component
    class MultiComboboxBehavior<T> : Behavior<CheckBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
        "SelectedItems", typeof(ICollection<T>), typeof(MultiComboboxBehavior<T>), new PropertyMetadata(null));

        public ICollection<T> SelectedItems
        {
            get { return (ICollection<T>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Checked += OnChecked;
            AssociatedObject.Unchecked += OnUnchecked;

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Checked -= OnChecked;
            AssociatedObject.Unchecked -= OnUnchecked;
        }

        public void OnChecked(object sender, RoutedEventArgs e)
        {
            if (SelectedItems == null) return;
            var item = AssociatedObject.DataContext as MultiComboboxItem<T>;
            if (!SelectedItems.Contains(item.Item))
            {
                SelectedItems.Add(item.Item);
            }
        }

        public void OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (SelectedItems == null) return;
            var item = AssociatedObject.DataContext as MultiComboboxItem<T>;
            if (SelectedItems.Contains(item.Item))
            {
                SelectedItems.Remove(item.Item);
            }
        }

    }
}
