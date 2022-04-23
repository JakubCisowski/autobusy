using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;

namespace Autobusy.UI.Pages;

public partial class PlanistaPage : Page
{
	private List<PlanKursu> _planyKursu;
	
	public PlanistaPage()
	{
		InitializeComponent();

		_planyKursu = new List<PlanKursu>()
		{
			new PlanKursu()
			{
				Kurs = new Kurs(){KursId = 1, DzienOdbycia = DateTime.Today},
				PlanKursuId = 1,
				PlanowaGodzina = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0),
				Przystanek = new Przystanek(){PrzystanekId = 1, Adres = "Losowy", Nazwa = "Nazwa Przystanku", Numer = "P123"}
			}
		};
		
		this.DataContext = _planyKursu;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());;
	}
}