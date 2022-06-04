using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class PlanistaLiniePage : Page
{
	private readonly List<Linia> _linie;
	private List<PrzystanekWLinii> _przystanki;

	private Linia _selectedLinia;

	public PlanistaLiniePage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			_linie = repo.List(x => x.PrzystankiWLinii);
		}

		LinieComboBox.ItemsSource = _linie.Select(x => x.Numer);

		List<Przystanek> przystanki;

		using (var repo = new DatabaseRepository<Przystanek>(new AutobusyContext()))
		{
			przystanki = repo.List();
		}

		PrzystankiComboBox.ItemsSource = przystanki.Select(x => x.Nazwa);
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
		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			repo.UpdateMany(_linie);
		}
	}

	private void DodajLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		var linia = new Linia();

		new LiniaWindow(linia).ShowDialog();

		_linie.Add(linia);
		_selectedLinia = linia;

		if (linia.PrzystankiWLinii is null)
		{
			_przystanki = new List<PrzystanekWLinii>();
			linia.PrzystankiWLinii = _przystanki;
		}
		else
		{
			_przystanki = linia.PrzystankiWLinii;
		}

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			repo.Add(linia);
		}

		PrzystankiGrid.Items.Refresh();
		LinieComboBox.ItemsSource = _linie.Select(x => x.Numer);
		LinieComboBox.SelectedItem = linia.Numer;
	}

	private void EdytujLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		new LiniaWindow(_selectedLinia).ShowDialog();
	}

	private void UsunLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			repo.Delete(_selectedLinia);
		}

		_linie.Remove(_selectedLinia);

		_selectedLinia = _linie.FirstOrDefault();

		if (_selectedLinia.PrzystankiWLinii is null)
		{
			_przystanki = new List<PrzystanekWLinii>();
			_selectedLinia.PrzystankiWLinii = _przystanki;
		}
		else
		{
			_przystanki = _selectedLinia.PrzystankiWLinii;
		}

		this.DataContext = _przystanki;

		PrzystankiGrid.Items.Refresh();
		LinieComboBox.Items.Refresh();

		LinieComboBox.SelectedItem = _selectedLinia.Numer;
	}

	private void DodajPrzystanekButton_OnClick(object sender, RoutedEventArgs e)
	{
		var selectedPrzystanekNazwa = PrzystankiComboBox.SelectedItem.ToString();

		Przystanek przystanek;

		using (var repo = new DatabaseRepository<Przystanek>(new AutobusyContext()))
		{
			przystanek = repo.GetFirst(x => x.Nazwa == selectedPrzystanekNazwa, y => y.Przystanki);
		}

		var przystanekWLinii = new PrzystanekWLinii
		{
			Linia = _selectedLinia,
			Przystanek = przystanek,
			LiczbaPorzadkowa = (_przystanki.Max(x => x.LiczbaPorzadkowa as int?) ?? 0) + 1
		};

		_przystanki.Add(przystanekWLinii);

		_selectedLinia.PrzystankiWLinii.Add(przystanekWLinii);

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			var liniaFromDb = repo.GetById(_selectedLinia.Id);

			if (liniaFromDb.PrzystankiWLinii is null)
			{
				liniaFromDb.PrzystankiWLinii = new List<PrzystanekWLinii>();
			}

			liniaFromDb.PrzystankiWLinii.Add(przystanekWLinii);

			repo.Update(liniaFromDb);

			_selectedLinia = liniaFromDb;
		}

		PrzystankiGrid.Items.Refresh();
	}

	private void LinieComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedNumerLinii = e.AddedItems[0].ToString();

		_selectedLinia = _linie.First(x => x.Numer == selectedNumerLinii);

		using (var repo = new DatabaseRepository<PrzystanekWLinii>(new AutobusyContext()))
		{
			_przystanki = repo.List(x => x.Linia.Numer == selectedNumerLinii, y => y.Przystanek, z=>z.Linia);
			this.DataContext = _przystanki;
		}
	}

	private void UsuwaniePrzystankuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not PrzystanekWLinii przystanekWLinii)
		{
			return;
		}

		foreach (var przystanek in _przystanki)
		{
			if (przystanek.LiczbaPorzadkowa > przystanekWLinii.LiczbaPorzadkowa)
			{
				przystanek.LiczbaPorzadkowa--;
			}
		}

		_przystanki.Remove(przystanekWLinii);

		using (var repo = new DatabaseRepository<PrzystanekWLinii>(new AutobusyContext()))
		{
			repo.Delete(przystanekWLinii);
		}

		PrzystankiGrid.Items.Refresh();
	}

	private void RentownoscButton_OnClick(object sender, RoutedEventArgs e)
	{
		var selectedLiniaNumer = LinieComboBox.SelectedItem.ToString();

		var selectedLinia = _linie.First(x => x.Numer == selectedLiniaNumer);

		new RentownoscWindow(selectedLinia).ShowDialog();
	}
}