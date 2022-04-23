using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKierowcyPage : Page
{
	private List<Kierowca> _kierowcy;
	
	public DyspozytorKierowcyPage()
	{
		InitializeComponent();

		_kierowcy = new List<Kierowca>()
		{
			new Kierowca()
			{
				KierowcaId = 1,
				Imie = "Jan",
				Nazwisko = "Kowalski",
				Doswiadczenie = 7
			}
		};

		this.DataContext = _kierowcy;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}
}