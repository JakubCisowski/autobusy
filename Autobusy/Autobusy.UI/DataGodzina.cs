using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Autobusy.UI;

public class DataGodzina : INotifyPropertyChanged
{
	private DateTime _dataGodzina;

	private readonly DispatcherTimer _timer;

	public DataGodzina()
	{
		Data = DateTime.Now;

		_timer = new DispatcherTimer
		{
			Interval = TimeSpan.FromSeconds(1)
		};
		_timer.Tick += Timer_Tick;
		_timer.Start();
	}

	public DateTime Data
	{
		get => _dataGodzina;
		set
		{
			_dataGodzina = value;
			OnPropertyChanged();
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void Timer_Tick(object sender, EventArgs e)
	{
		Data = DateTime.Now;
	}

	protected void OnPropertyChanged([CallerMemberName] string name = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}