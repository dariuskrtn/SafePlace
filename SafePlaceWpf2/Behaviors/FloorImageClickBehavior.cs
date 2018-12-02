using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SafePlaceWpf.Behaviors
{
    class FloorImageClickBehavior : Behavior<Image>
    {

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        "Command", typeof(ICommand), typeof(FloorImageClickBehavior), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;

        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command == null) return;
            Image ClickedImage = e.Source as Image;
            Point ClickedPoint = e.GetPosition(ClickedImage);
            if (Command.CanExecute(e)) Command.Execute(ClickedPoint);
        }

    }
}
