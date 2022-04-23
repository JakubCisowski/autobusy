using System.Windows;
using System.Windows.Controls;

namespace Autobusy.UI.Pages;

public partial class DyspozytorMenuPage : Page
{
	public DyspozytorMenuPage()
	{
		InitializeComponent();
	}

	private void KursyButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorKursyPage());
	}

	private void KierowcyButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorKierowcyPage());
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());
	}
}