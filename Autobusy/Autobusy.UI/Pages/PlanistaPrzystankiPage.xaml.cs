using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Pages;

public partial class PlanistaPrzystankiPage : Page
{
	private List<Przystanek> _przystanki;
	
	public PlanistaPrzystankiPage()
	{
		InitializeComponent();
		
		_przystanki = DatabaseOperations.GetCollection<Przystanek>();

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
		DatabaseOperations.Delete(przystanek);
		
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
		DatabaseOperations.UpdateCollection(_przystanki);
	}
}