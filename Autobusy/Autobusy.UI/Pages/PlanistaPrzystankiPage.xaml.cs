using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Pages;

public partial class PlanistaPrzystankiPage : Page
{
	private readonly List<Przystanek> _przystanki;

	public PlanistaPrzystankiPage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Przystanek>(new AutobusyContext()))
		{
			_przystanki = repo.List();
		}

		this.DataContext = _przystanki;
	}

	private void DodajPrzystanekButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyPrzystanek = new Przystanek();

		_przystanki.Add(nowyPrzystanek);

		PrzystankiGrid.Items.Refresh();
	}

	private void UsuwaniePrzystankuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Przystanek przystanek)
		{
			return;
		}

		_przystanki.Remove(przystanek);

		using (var repo = new DatabaseRepository<Przystanek>(new AutobusyContext()))
		{
			repo.Delete(przystanek);
		}

		PrzystankiGrid.Items.Refresh();
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();

		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaMenuPage());
	}

	public void SaveChanges()
	{
		// Save changes in database.
		using (var repo = new DatabaseRepository<Przystanek>(new AutobusyContext()))
		{
			repo.UpdateMany(_przystanki);
		}
	}
}