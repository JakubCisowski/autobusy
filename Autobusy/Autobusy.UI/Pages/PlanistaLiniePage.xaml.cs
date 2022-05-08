using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class PlanistaLiniePage : Page
{
	private List<Linia> _linie;
	private List<PrzystanekWLinii> _przystanki;

	private Linia _selectedLinia;
	
	public PlanistaLiniePage()
	{
		InitializeComponent();
		
		_linie = DatabaseOperations.GetLinie();
		LinieComboBox.ItemsSource = _linie.Select(x=>x.Numer);
		
		var przystanki = DatabaseOperations.GetCollection<Przystanek>();
		PrzystankiComboBox.ItemsSource = przystanki.Select(x=>x.Nazwa);
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
		DatabaseOperations.UpdateCollection(_linie);
	}

	private void DodajLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		var linia = new Linia();
		
		new LiniaWindow(linia).ShowDialog();
		
		_linie.Add(linia);
		_selectedLinia = linia;

		if (linia.Przystanki is null)
		{
			_przystanki = new List<PrzystanekWLinii>();
			linia.Przystanki = _przystanki;
		}
		else
		{
			_przystanki = linia.Przystanki;
		}

		PrzystankiGrid.Items.Refresh();
		LinieComboBox.ItemsSource = _linie.Select(x=>x.Numer);
		LinieComboBox.SelectedItem = linia.Numer;
	}

	private void EdytujLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		new LiniaWindow(_selectedLinia).ShowDialog();
	}

	private void UsunLinieButton_OnClick(object sender, RoutedEventArgs e)
	{
		while (_selectedLinia.Przystanki.Count > 0)
		{
			DatabaseOperations.Delete(_selectedLinia.Przystanki.First());
		}
		
		DatabaseOperations.Delete(_selectedLinia);

		_linie.Remove(_selectedLinia);
		
		_selectedLinia = _linie.FirstOrDefault();
		
		if (_selectedLinia.Przystanki is null)
		{
			_przystanki = new List<PrzystanekWLinii>();
			_selectedLinia.Przystanki = _przystanki;
		}
		else
		{
			_przystanki = _selectedLinia.Przystanki;
		}

		this.DataContext = _przystanki;
		
		PrzystankiGrid.Items.Refresh();
		LinieComboBox.Items.Refresh();

		LinieComboBox.SelectedItem = _selectedLinia.Numer;
	}

	private void DodajPrzystanekButton_OnClick(object sender, RoutedEventArgs e)
	{
		var selectedPrzystanek = PrzystankiComboBox.SelectedItem.ToString();
		var przystanek = DatabaseOperations.GetCollection<Przystanek>().FirstOrDefault(x => x.Nazwa == selectedPrzystanek);

		var przystanekWLinii = new PrzystanekWLinii()
		{
			Linia = _selectedLinia,
			Przystanek = przystanek,
			LiczbaPorzadkowa = (_przystanki.Max(x=>x.LiczbaPorzadkowa as int?) ?? 0) + 1
		};
		
		_przystanki.Add(przystanekWLinii);
		
		DatabaseOperations.AddPrzystanekWLinii(przystanekWLinii);
		
		PrzystankiGrid.Items.Refresh();
	}

	private void LinieComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedNumerLinii = e.AddedItems[0].ToString();
		
		_selectedLinia = _linie.First(x => x.Numer == selectedNumerLinii);

		var przystanki = DatabaseOperations.GetPrzystankiWLinii()
			.Where(x => x.Linia.Numer == selectedNumerLinii).ToList();

		_przystanki = przystanki;
		this.DataContext = _przystanki;
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

		DatabaseOperations.Delete(przystanekWLinii);
		
		PrzystankiGrid.Items.Refresh();
	}
}