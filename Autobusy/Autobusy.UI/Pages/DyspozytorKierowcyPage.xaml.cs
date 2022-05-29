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
			_kierowcy = repo.List();
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
		var nowyKierowca = new Kierowca();

		_kierowcy.Add(nowyKierowca);

		KierowcyGrid.Items.Refresh();
	}

	private void SpalanieButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not Kierowca kierowca)
		{
			return;
		}

		var averageFuelConsumption = kierowca.Przejazdy?.Sum(x => x.IloscSpalonegoPaliwa);

		if (averageFuelConsumption.HasValue)
		{
			MessageBox.Show($"Średnie spalanie kierowcy {kierowca.Imie} {kierowca.Nazwisko} wynosi: {averageFuelConsumption}.", "Średnie spalanie kierowcy");
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
		if ((sender as Button)?.CommandParameter is not Kierowca kierowca)
		{
			return;
		}

		var przejazdyKierowcy = kierowca.Przejazdy;

		double spoznieniaSuma = 0;
		var spoznieniaIlosc = 0;

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
				MessageBox.Show($"Średni czas spóźnienia kierowcy {kierowca.Imie} {kierowca.Nazwisko} wynosi: {spoznieniaSuma / spoznieniaIlosc} minut.", "Średni czas spóźnienia kierowcy");

				return;
			}
		}

		MessageBox.Show($"Brak danych o spóźnieniach dla kierowcy {kierowca.Imie} {kierowca.Nazwisko}.", "Średni czas spóźnienia kierowcy");
	}
}