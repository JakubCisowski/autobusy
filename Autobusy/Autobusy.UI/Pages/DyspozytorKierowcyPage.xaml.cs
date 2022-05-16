using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKierowcyPage : Page
{
	private List<Kierowca> _kierowcy;
	
	public DyspozytorKierowcyPage()
	{
		InitializeComponent();

		_kierowcy = DatabaseOperations.GetCollection<Kierowca>();

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

		double? averageFuelConsumption = kierowca.Przejazdy?.Sum(x => x.IloscSpalonegoPaliwa);

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
		DatabaseOperations.UpdateCollection(_kierowcy);
	}
}