using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class LiniaWindow : Window
{
	private Linia _linia;
	
	public LiniaWindow(Linia linia)
	{
		InitializeComponent();
		
		_linia = linia;
		
		this.DataContext = _linia;
	}

	private void LiniaWindow_OnClosing(object sender, CancelEventArgs e)
	{
		DatabaseOperations.UpdateCollection(new List<Linia> { _linia });
	}
}