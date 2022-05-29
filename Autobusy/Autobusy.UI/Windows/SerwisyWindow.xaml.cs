using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class SerwisyWindow : Window
{
	private readonly List<Autobus> _autobusy;
	private List<Serwis> _serwisy;

	public SerwisyWindow()
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Autobus>(new AutobusyContext()))
		{
			_autobusy = repo.List();
		}

		AutobusComboBox.ItemsSource = _autobusy.Select(x => x.NumerRejestracyjny);
	}

	private void AutobusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedAutobusNumerRejestracyjny = e.AddedItems[0].ToString();

		Autobus selectedAutobus = _autobusy.FirstOrDefault(x => x.NumerRejestracyjny == selectedAutobusNumerRejestracyjny);

		_serwisy = selectedAutobus.Serwisy;

		DataContext = _serwisy;
	}

	private void SerwisyWindow_OnClosing(object sender, CancelEventArgs e)
	{
		using (var repo = new DatabaseRepository<Serwis>(new AutobusyContext()))
		{
			repo.UpdateMany(_serwisy);
		}
	}
}