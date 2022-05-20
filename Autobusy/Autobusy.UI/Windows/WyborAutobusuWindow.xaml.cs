using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class WyborAutobusuWindow : Window
{
	public static Autobus Autobus;

	private List<Autobus> _listaAutobusow;
	
	public WyborAutobusuWindow()
	{
		InitializeComponent();
		
		var autobusy = DatabaseOperations.GetAutobusy();
		
		AutobusyComboBox.ItemsSource = autobusy.Select(x=>x.NumerRejestracyjny).ToList();

		_listaAutobusow = autobusy;
	}

	private void WyborAutobusuWindow_OnClosing(object sender, CancelEventArgs e)
	{
		var selectedAutobusNumerRejestracyjny = AutobusyComboBox.SelectedItem as string;

		var selectedAutobus = _listaAutobusow.FirstOrDefault(x => x.NumerRejestracyjny == selectedAutobusNumerRejestracyjny);

		Autobus = selectedAutobus;
	}
}