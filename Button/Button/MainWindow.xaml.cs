using System.Windows;

namespace Button
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Custom button clicked!");
        }
    }
}
