using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;
using Autobusy.UI.Windows;

namespace Autobusy.UI.Pages;

public partial class PlanistaKursyPage : Page
{
	private readonly List<Linia> _linie;
	private List<Kurs> _kursy;
	private Linia _selectedLinia;

	public PlanistaKursyPage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			_linie = repo.List(x=>x.PrzystankiWLinii, y=>y.Kursy);
		}

		LinieComboBox.ItemsSource = _linie.Select(x => x.Numer);
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();

		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new PlanistaMenuPage());
	}

	private void DodajKursButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyKurs = new Kurs
		{
			PlanyKursu = new List<PlanKursu>(),
			Przejazdy = new List<Przejazd>()
		};
		
		_kursy.Add(nowyKurs);
		
		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			var liniaFromDb = repo.GetById(_selectedLinia.Id, x => x.PrzystankiWLinii, y => y.Kursy);
		
			nowyKurs.Linia = liniaFromDb;

			if (liniaFromDb.Kursy is null)
			{
				liniaFromDb.Kursy = new List<Kurs>();
			}
			
			liniaFromDb.Kursy.Add(nowyKurs);
			
			repo.SaveChanges();
		}

		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			var kursFromDb = repo.GetById(nowyKurs.Id, x => x.PlanyKursu);

			if (kursFromDb.PlanyKursu is null)
			{
				kursFromDb.PlanyKursu = new List<PlanKursu>();
			}
			
			foreach (var przystanekWLinii in _selectedLinia.PrzystankiWLinii)
			{
				var nowyPlanKursu = new PlanKursu() { PrzystanekWLinii = przystanekWLinii };
				
				kursFromDb.PlanyKursu.Add(nowyPlanKursu);
			}
			
			repo.SaveChanges();
		}

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

		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			repo.Delete(kurs);
		}

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
		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			repo.UpdateMany(_kursy);
		}
	}
}