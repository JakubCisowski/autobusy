using System.Windows;
using Autobusy.Logic.Helpers;

namespace Autobusy.UI.Windows;

public partial class RaportWindow : Window
{
	public RaportWindow()
	{
		InitializeComponent();
	}

	private void GenerujButton_OnClick(object sender, RoutedEventArgs e)
	{
		ReportHelper.CreateReport(KierowcyCheckBox.IsChecked.Value, LinieCheckBox.IsChecked.Value, BazaDanychCheckBox.IsChecked.Value);
		
		MessageBox.Show("Raport został wygenerowany");
	}
}