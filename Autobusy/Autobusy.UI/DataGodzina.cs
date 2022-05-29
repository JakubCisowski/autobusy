using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Autobusy.UI;

public class DataGodzina : INotifyPropertyChanged
{
	private readonly DispatcherTimer _timer;
	private DateTime _dataGodzina;

	public DataGodzina()
	{
		this.Data = DateTime.Now;

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
		this.Data = DateTime.Now;
	}

	protected void OnPropertyChanged([CallerMemberName] string name = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}