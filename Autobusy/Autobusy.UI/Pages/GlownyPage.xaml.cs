using System.Windows;
using System.Windows.Controls;

namespace Autobusy.UI.Pages;

public partial class GlownyPage : Page
{
	public GlownyPage()
	{
		InitializeComponent();
	}

	private void DyspozytorButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}

	private void PlanistaButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaPage());
	}

	private void ZarzadcaButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new ZarzadcaFlotaPage());
	}
}