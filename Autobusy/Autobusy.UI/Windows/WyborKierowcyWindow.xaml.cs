using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class WyborKierowcyWindow : Window
{
	private static Kierowca _kierowca;

	private List<Kierowca> _listaKierowcow;
	
	public WyborKierowcyWindow(ref Kierowca kierowca)
	{
		InitializeComponent();

		_kierowca = kierowca;

		var kierowcy = DatabaseOperations.GetKierowcy();
		
		KierowcyComboBox.ItemsSource = kierowcy.Select(x=>x.Imie + " " + x.Nazwisko).ToList();

		_listaKierowcow = kierowcy;
	}

	private void WyborKierowcyWindow_OnClosing(object sender, CancelEventArgs e)
	{
		var selectedKierowcaImieNazwisko = KierowcyComboBox.SelectedItem as string;
		
		var selectedKierowcaImieNazwiskoSplit = selectedKierowcaImieNazwisko.Split(' ');
		
		var selectedKierowca = _listaKierowcow.FirstOrDefault(x=>x.Imie == selectedKierowcaImieNazwiskoSplit[0] && x.Nazwisko == selectedKierowcaImieNazwiskoSplit[1]);

		_kierowca = selectedKierowca;
	}
}