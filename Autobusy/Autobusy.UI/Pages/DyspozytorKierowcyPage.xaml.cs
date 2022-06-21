using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKierowcyPage : Page
{
	private readonly List<Kierowca> _kierowcy;

	public DyspozytorKierowcyPage()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Kierowca>(new AutobusyContext()))
		{
			_kierowcy = repo.List(x=>x.Przejazdy);
		}

		this.DataContext = _kierowcy;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();

		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}

	private void DodajKierowceButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyKierowca = new Kierowca()
		{
			Przejazdy = new List<Przejazd>()
		};

		_kierowcy.Add(nowyKierowca);

		KierowcyGrid.Items.Refresh();
	}

	private void SpalanieButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();
		
		if ((sender as Button)?.CommandParameter is not Kierowca kierowca)
		{
			return;
		}
		
		var przejazdyKierowcy = kierowca.Przejazdy;
		
		if (przejazdyKierowcy == null || przejazdyKierowcy.Count == 0)
		{
			using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
			{
				przejazdyKierowcy = repo.List(x => x.KierowcaId == kierowca.Id);
			}
		}

		var averageFuelConsumption = przejazdyKierowcy?.Sum(x => x.IloscSpalonegoPaliwa);

		if (averageFuelConsumption.HasValue && averageFuelConsumption.Value > double.Epsilon)
		{
			MessageBox.Show($"Średnie spalanie kierowcy {kierowca.Imie} {kierowca.Nazwisko} wynosi: {Math.Abs(averageFuelConsumption.Value).ToString("0.00")}", "Średnie spalanie kierowcy");
		}
		else
		{
			MessageBox.Show($"Brak danych o spalaniu dla kierowcy {kierowca.Imie} {kierowca.Nazwisko}.", "Średnie spalanie kierowcy");
		}
	}

	public void SaveChanges()
	{
		using (var repo = new DatabaseRepository<Kierowca>(new AutobusyContext()))
		{
			repo.UpdateMany(_kierowcy);
		}
	}

	private void SpoznieniaButton_OnClick(object sender, RoutedEventArgs e)
	{
		SaveChanges();
		
		if ((sender as Button)?.CommandParameter is not Kierowca kierowca)
		{
			return;
		}

		var przejazdyKierowcy = kierowca.Przejazdy;

		if (przejazdyKierowcy == null || przejazdyKierowcy.Count == 0)
		{
			using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
			{
				przejazdyKierowcy = repo.List(x => x.KierowcaId == kierowca.Id);
			}
		}

		double spoznieniaSuma = 0;
		var spoznieniaIlosc = 0;

		using (var repo = new DatabaseRepository<PlanKursu>(new AutobusyContext()))
		{
			foreach (var przejazd in kierowca.Przejazdy)
			{
				foreach (var realizacjaPrzejazdu in przejazd.RealizacjePrzejazdu)
				{
					realizacjaPrzejazdu.PlanKursu = repo.GetById(realizacjaPrzejazdu.PlanKursuId);
				}
			}
		}

		if (przejazdyKierowcy != null)
		{
			foreach (var przejazdKierowcy in przejazdyKierowcy)
			{
				if (przejazdKierowcy.RealizacjePrzejazdu is not null && przejazdKierowcy.RealizacjePrzejazdu.Count > 0)
				{
					spoznieniaIlosc++;
					spoznieniaSuma += przejazdKierowcy.RealizacjePrzejazdu.Sum(x => (x.FaktycznaGodzina - x.PlanKursu.PlanowaGodzina).TotalMinutes);
				}
			}

			if (spoznieniaIlosc != 0)
			{
				MessageBox.Show($"Średni czas spóźnienia kierowcy {kierowca.Imie} {kierowca.Nazwisko} wynosi: {Math.Abs(spoznieniaSuma / spoznieniaIlosc).ToString("0.00")} minut.", "Średni czas spóźnienia kierowcy");

				return;
			}
		}

		MessageBox.Show($"Brak danych o spóźnieniach dla kierowcy {kierowca.Imie} {kierowca.Nazwisko}.", "Średni czas spóźnienia kierowcy");
	}
}