using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// INotifyPropertyChanged implementation.
	/// </summary>
	public abstract class BaseModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void SetValue<T>(T value, ref T field, [CallerMemberName] string propertyName = null)
		{
			if (!Equals(value, field))
			{
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
