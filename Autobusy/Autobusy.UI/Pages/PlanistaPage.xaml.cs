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
				PlanKursuId = 1,
				PrzystanekWLinii = new PrzystanekWLinii(){LiczbaPorzadkowa = 1}
			}
		};
		
		this.DataContext = _planyKursu;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new GlownyPage());
	}
}