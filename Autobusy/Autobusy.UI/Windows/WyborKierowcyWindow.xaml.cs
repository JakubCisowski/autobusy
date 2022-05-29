using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class WyborKierowcyWindow : Window
{
	public static Kierowca Kierowca;

	private readonly List<Kierowca> _listaKierowcow;

	public WyborKierowcyWindow()
	{
		InitializeComponent();

		List<Kierowca> kierowcy;

		using (var repo = new DatabaseRepository<Kierowca>(new AutobusyContext()))
		{
			kierowcy = repo.List();
		}

		KierowcyComboBox.ItemsSource = kierowcy.Select(x => x.Imie + " " + x.Nazwisko).ToList();

		_listaKierowcow = kierowcy;
	}

	private void WyborKierowcyWindow_OnClosing(object sender, CancelEventArgs e)
	{
		var selectedKierowcaImieNazwisko = KierowcyComboBox.SelectedItem as string;

		string[] selectedKierowcaImieNazwiskoSplit = selectedKierowcaImieNazwisko.Split(' ');

		Kierowca selectedKierowca = _listaKierowcow.FirstOrDefault(x => x.Imie == selectedKierowcaImieNazwiskoSplit[0] && x.Nazwisko == selectedKierowcaImieNazwiskoSplit[1]);

		Kierowca = selectedKierowca;
	}
}