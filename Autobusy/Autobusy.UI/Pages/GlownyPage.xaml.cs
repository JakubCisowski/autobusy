using System.Windows;
using System.Windows.Controls;
using Autobusy.UI.Windows;

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

		window.MainFrame.Navigate(new PlanistaMenuPage());
	}

	private void ZarzadcaButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new ZarzadcaFlotaPage());
	}

	private void RaportyButton_OnClick(object sender, RoutedEventArgs e)
	{
		new RaportWindow().ShowDialog();
	}
}