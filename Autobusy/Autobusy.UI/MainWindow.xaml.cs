using System;
using System.ComponentModel;
using System.Windows;
using Autobusy.UI.Pages;

namespace Autobusy.UI
{
	public partial class MainWindow : Window
	{
		private DataGodzina _dataGodzina;
		
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
				
				case DyspozytorKierowcyPage:
					break;
				
				case DyspozytorKursyPage:
					break;
			}
		}
	}
}