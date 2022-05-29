using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class WyborAutobusuWindow : Window
{
	public static Autobus Autobus;

	private readonly List<Autobus> _listaAutobusow;

	public WyborAutobusuWindow()
	{
		InitializeComponent();

		List<Autobus> autobusy;

		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			autobusy = repo.List();
		}

		AutobusyComboBox.ItemsSource = autobusy.Select(x => x.NumerRejestracyjny).ToList();

		_listaAutobusow = autobusy;
	}

	private void WyborAutobusuWindow_OnClosing(object sender, CancelEventArgs e)
	{
		var selectedAutobusNumerRejestracyjny = AutobusyComboBox.SelectedItem as string;

		Autobus selectedAutobus = _listaAutobusow.FirstOrDefault(x => x.NumerRejestracyjny == selectedAutobusNumerRejestracyjny);

		Autobus = selectedAutobus;
	}
}