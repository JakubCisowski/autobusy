using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class ZarzadcaFlotaPage : Page
{
	private readonly List<Autobus> _autobusy;

	public ZarzadcaFlotaPage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			_autobusy = repo.List();
		}

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
		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			repo.UpdateMany(_autobusy);
		}
	}

	private void DodajAutobusButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyAutobus = new Autobus
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

		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			repo.Delete(autobus);
		}

		AutobusyGrid.Items.Refresh();
	}

	private void SerwisAutobusuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Autobus autobus)
		{
			return;
		}

		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			var autobusFromDb = repo.GetById(autobus.Id);

			if (autobusFromDb.Serwisy is null)
			{
				autobusFromDb.Serwisy = new List<Serwis>();
			}
			
			var serwis = new Serwis
			{
				NaprawianyAutobus = autobusFromDb,
				Data = DateTime.Now
			};
			
			autobusFromDb.Serwisy.Add(serwis);

			autobusFromDb.StanAutobusu = StanAutobusu.WSerwisie;
			
			repo.Update(autobusFromDb);
		}
	}

	private void SerwisyButton_OnClick(object sender, RoutedEventArgs e)
	{
		new SerwisyWindow().ShowDialog();
	}
}