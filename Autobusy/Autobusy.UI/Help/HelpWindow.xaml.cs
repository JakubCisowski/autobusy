using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Autobusy.UI.Help;

public partial class HelpWindow : Window
{
	public HelpWindow()
	{
		InitializeComponent();

		var path = Path.Combine(Directory.GetCurrentDirectory(), "Help", "HelpReference.pdf");
		
		PdfBrowser.LoadUrl(path);
	}
}