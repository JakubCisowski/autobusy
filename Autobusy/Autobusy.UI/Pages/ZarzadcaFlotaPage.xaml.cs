using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class ZarzadcaFlotaPage : Page
{
	private List<Autobus> _autobusy;
	
	public ZarzadcaFlotaPage()
	{
		InitializeComponent();

		_autobusy = DatabaseOperations.GetAutobusy();

		this.DataContext = _autobusy;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();

		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());
	}

	public void SaveChanges()
	{
		// Save changes in database.
		DatabaseOperations.UpdateCollection(_autobusy);
	}

	private void DodajAutobusButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyAutobus = new Autobus()
		{
			DataProdukcji = DateTime.Now
		};

		_autobusy.Add(nowyAutobus);
		
		AutobusyGrid.Items.Refresh();
	}

	private void WycofanieAutobusuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Autobus autobus)
		{
			return;
		}

		_autobusy.Remove(autobus);
		DatabaseOperations.Delete(autobus);
		
		AutobusyGrid.Items.Refresh();
	}

	private void SerwisAutobusuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Autobus autobus)
		{
			return;
		}

		autobus.StanAutobusu = StanAutobusu.WSerwisie;

		var serwis = new Serwis()
		{
			NaprawianyAutobus = autobus,
			Data = DateTime.Now
		};
		
		DatabaseOperations.AddSerwis(serwis);
	}

	private void SerwisyButton_OnClick(object sender, RoutedEventArgs e)
	{
		new SerwisyWindow().ShowDialog();
	}
}