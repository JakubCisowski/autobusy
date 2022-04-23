using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;

namespace Autobusy.UI.Pages;

public partial class ZarzadcaFlotaPage : Page
{
	private List<Autobus> _autobusy;
	
	public ZarzadcaFlotaPage()
	{
		InitializeComponent();

		_autobusy = new List<Autobus>()
		{
			new Autobus()
			{
				// Create sample object
				AutobusId = 1,
				Marka = "Audi",
				NumerRejestracyjny = "K1-DIS",
				DataProdukcji = new DateTime(2010, 1, 1),
				StanAutobusu = StanAutobusu.Dobry,
				SpalanieNa100 = 8.4
			}
		};

		this.DataContext = _autobusy;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());
	}
}