using System.Windows;
using System.Windows.Controls;

namespace Autobusy.UI.Pages;

public partial class PlanistaMenuPage : Page
{
	public PlanistaMenuPage()
	{
		InitializeComponent();
	}

	private void PrzystankiButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaPrzystankiPage());
	}

	private void LinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaLiniePage());
	}

	private void KursyButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaKursyPage());
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());
	}
}