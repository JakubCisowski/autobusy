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
		var includeKierowcyInfo = KierowcyCheckBox.IsChecked.Value;
		var includeLinieInfo = LinieCheckBox.IsChecked.Value;
		var includeEntityStatistics = BazaDanychCheckBox.IsChecked.Value;

		if (!includeKierowcyInfo && !includeLinieInfo && !includeEntityStatistics)
		{
			MessageBox.Show("Nie wybrano żadnej z opcji raportu");
		}
		else
		{
			ReportHelper.CreateReport(KierowcyCheckBox.IsChecked.Value, LinieCheckBox.IsChecked.Value, BazaDanychCheckBox.IsChecked.Value);

			MessageBox.Show("Raport został wygenerowany");
		}
	}
}