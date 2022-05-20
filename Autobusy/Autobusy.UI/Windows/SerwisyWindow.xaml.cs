using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class SerwisyWindow : Window
{
	private List<Autobus> _autobusy;
	private List<Serwis> _serwisy;
	
	public SerwisyWindow()
	{
		InitializeComponent();

		_autobusy = DatabaseOperations.GetAutobusy();

		AutobusComboBox.ItemsSource = _autobusy.Select(x => x.NumerRejestracyjny);
	}

	private void AutobusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedAutobusNumerRejestracyjny = e.AddedItems[0].ToString();

		var selectedAutobus = _autobusy.FirstOrDefault(x => x.NumerRejestracyjny == selectedAutobusNumerRejestracyjny);

		_serwisy = selectedAutobus.Serwisy;
		
		this.DataContext = _serwisy;
	}

	private void SerwisyWindow_OnClosing(object sender, CancelEventArgs e)
	{
		DatabaseOperations.UpdateSerwisy(_serwisy);
	}
}