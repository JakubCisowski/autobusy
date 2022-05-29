using System.ComponentModel;
using System.Windows;
using Autobusy.UI.Help;
using Autobusy.UI.Pages;

namespace Autobusy.UI;

public partial class MainWindow : Window
{
	private readonly DataGodzina _dataGodzina;

	public MainWindow()
	{
		InitializeComponent();

		_dataGodzina = new DataGodzina();
		this.DataContext = _dataGodzina;
	}

	private void MainWindow_OnClosing(object sender, CancelEventArgs e)
	{
		// Save changes in database on currently displayed page.

		var currentPage = MainFrame.Content;

		switch (currentPage)
		{
			case ZarzadcaFlotaPage zarzadcaFlotaPage:
				zarzadcaFlotaPage.SaveChanges();
				break;

			case PlanistaPrzystankiPage planistaPrzystankiPage:
				planistaPrzystankiPage.SaveChanges();
				break;

			case PlanistaLiniePage planistaLiniePage:
				planistaLiniePage.SaveChanges();
				break;

			case PlanistaKursyPage planistaKursyPage:
				planistaKursyPage.SaveChanges();
				break;

			case DyspozytorKierowcyPage dyspozytorKierowcyPage:
				dyspozytorKierowcyPage.SaveChanges();
				break;

			case DyspozytorPrzejazdyPage dyspozytorPrzejazdyPage:
				dyspozytorPrzejazdyPage.SaveChanges();
				break;
		}
	}

	private void HelpButton_OnClick(object sender, RoutedEventArgs e)
	{
		new HelpWindow().ShowDialog();
	}
}