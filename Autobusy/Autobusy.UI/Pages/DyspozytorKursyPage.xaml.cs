using System.Windows;
using System.Windows.Controls;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKursyPage : Page
{
	public DyspozytorKursyPage()
	{
		InitializeComponent();
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}
}