using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class PlanistaKursyPage : Page
{
	private List<Linia> _linie;
	private Linia _selectedLinia;
	
	private List<Kurs> _kursy;
	
	public PlanistaKursyPage()
	{
		InitializeComponent();

		_linie = DatabaseOperations.GetLinie();
		
		LinieComboBox.ItemsSource = _linie.Select(x=>x.Numer);
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();
		
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaMenuPage());
	}

	private void DodajKursButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyKurs = new Kurs()
		{
			Linia = _selectedLinia,
			PlanyKursu = new List<PlanKursu>()
		};

		foreach (var przystanekWLinii in _selectedLinia.Przystanki)
		{
			nowyKurs.PlanyKursu.Add(new PlanKursu(){PrzystanekWLinii = przystanekWLinii, Kurs = nowyKurs});
		}

		_kursy.Add(nowyKurs);
		
		DatabaseOperations.AddKurs(nowyKurs);
		
		KursyGrid.Items.Refresh();
	}

	private void LinieComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedNumerLinii = e.AddedItems[0].ToString();
		
		_selectedLinia = _linie.First(x => x.Numer == selectedNumerLinii);

		if (_selectedLinia.Kursy is null)
		{
			_selectedLinia.Kursy = new List<Kurs>();
		}
		
		_kursy = _selectedLinia.Kursy;
		
		this.DataContext = _kursy;
	}

	private void UsuwanieKursuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Kurs kurs)
		{
			return;
		}

		_kursy.Remove(kurs);
		DatabaseOperations.DeleteKurs(kurs);
		
		KursyGrid.Items.Refresh();
	}

	private void EdycjaKursuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Kurs kurs)
		{
			return;
		}

		new KursWindow(kurs).ShowDialog();
	}
	
	public void SaveChanges()
	{
		// Save changes in database.
		DatabaseOperations.UpdateKursy(_kursy);
	}
}