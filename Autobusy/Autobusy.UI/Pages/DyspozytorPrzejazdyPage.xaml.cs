using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class DyspozytorPrzejazdyPage : Page
{
	private List<Kurs> _kursy;
	private readonly List<Linia> _linie;

	private List<Przejazd> _przejazdy;
	private Kurs _selectedKurs;
	private Linia _selectedLinia;

	public DyspozytorPrzejazdyPage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			_linie = repo.List();
		}

		LiniaComboBox.ItemsSource = _linie.Select(x => x.Numer);
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}

	private void DodajPrzejazdButton_OnClick(object sender, RoutedEventArgs e)
	{
		var przejazd = new Przejazd
		{
			Kurs = _selectedKurs,
			RealizacjePrzejazdu = new List<RealizacjaPrzejazdu>(),
			Data = DateTime.Now
		};

		_przejazdy.Add(przejazd);

		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			repo.Add(przejazd);

			_przejazdy = repo.List(x => x.Kurs?.Id == _selectedKurs.Id);
		}

		PrzejazdyGrid.ItemsSource = _przejazdy;

		PrzejazdyGrid.Items.Refresh();
	}

	private void LiniaComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedNumerLinii = e.AddedItems[0].ToString();

		_selectedLinia = _linie.First(x => x.Numer == selectedNumerLinii);

		if (_selectedLinia.Kursy is null) _selectedLinia.Kursy = new List<Kurs>();

		_kursy = _selectedLinia.Kursy;

		KursComboBox.ItemsSource = _kursy.Select(x => x.DzienTygodnia + " " + x.GodzinaRozpoczecia.ToString("HH:mm"));
	}

	private void KursComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedIdKursu = e.AddedItems[0].ToString();
		int selectedIndexKursu = KursComboBox.ItemsSource.OfType<string>().ToList().IndexOf(selectedIdKursu);

		if (selectedIndexKursu == -1) return;

		_selectedKurs = _kursy.ElementAt(selectedIndexKursu);

		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			_przejazdy = repo.List(x => x.Kurs?.Id == _selectedKurs.Id);
		}

		DataContext = _przejazdy;

		PrzejazdyGrid.Items.Refresh();
	}

	private void WybierzKierowceButton_OnClick(object sender, RoutedEventArgs e)
	{
		new WyborKierowcyWindow().ShowDialog();

		if ((sender as Button)?.CommandParameter is not Przejazd przejazd) return;

		Przejazd przejazdFromList = _przejazdy.FirstOrDefault(x => x.Id == przejazd.Id);

		przejazdFromList.Kierowca = WyborKierowcyWindow.Kierowca;
	}

	private void WybierzAutobusButton_OnClick(object sender, RoutedEventArgs e)
	{
		new WyborAutobusuWindow().ShowDialog();

		if ((sender as Button)?.CommandParameter is not Przejazd przejazd) return;

		Przejazd przejazdFromList = _przejazdy.FirstOrDefault(x => x.Id == przejazd.Id);

		przejazdFromList.Autobus = WyborAutobusuWindow.Autobus;
	}

	public void SaveChanges()
	{
		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			repo.UpdateMany(_przejazdy);
		}
	}
}