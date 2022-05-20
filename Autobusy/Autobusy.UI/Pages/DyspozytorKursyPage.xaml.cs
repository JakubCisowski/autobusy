using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKursyPage : Page
{
	private List<Linia> _linie;
	private Linia _selectedLinia;
	private List<Kurs> _kursy;
	private Kurs _selectedKurs;

	private List<Przejazd> _przejazdy;
	
	public DyspozytorKursyPage()
	{
		InitializeComponent();

		_linie = DatabaseOperations.GetLinie();

		LiniaComboBox.ItemsSource = _linie.Select(x => x.Numer);
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}

	private void DodajPrzejazdButton_OnClick(object sender, RoutedEventArgs e)
	{
		throw new NotImplementedException();
	}

	private void LiniaComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedNumerLinii = e.AddedItems[0].ToString();
		
		_selectedLinia = _linie.First(x => x.Numer == selectedNumerLinii);

		if (_selectedLinia.Kursy is null)
		{
			_selectedLinia.Kursy = new List<Kurs>();
		}
		
		_kursy = _selectedLinia.Kursy;

		KursComboBox.ItemsSource = _kursy.Select(x => x.DzienTygodnia.ToString() + " " + x.GodzinaRozpoczecia.ToString("HH:mm"));
	}

	private void KursComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedIdKursu = e.AddedItems[0].ToString();
		var selectedIndexKursu = KursComboBox.ItemsSource.OfType<string>().ToList().IndexOf(selectedIdKursu);

		if (selectedIndexKursu == -1)
		{
			return;
		}

		_selectedKurs = _kursy.ElementAt(selectedIndexKursu);

		_przejazdy = _selectedKurs.Przejazdy ?? new List<Przejazd>();

		this.DataContext = _przejazdy;
		
		PrzejazdyGrid.Items.Refresh();
	}

	private void WybierzKierowceButton_OnClick(object sender, RoutedEventArgs e)
	{
		var kierowca = new Kierowca();
		new WyborKierowcyWindow(ref kierowca).ShowDialog();
	}

	private void WybierzAutobusButton_OnClick(object sender, RoutedEventArgs e)
	{
		throw new NotImplementedException();
	}
}