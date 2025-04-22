using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SettingsViewModel : INotifyPropertyChanged
{
    private double _taxRate = Preferences.Get("TaxRate", 0.07);

    public double TaxRate
    {
        get => _taxRate;
        set
        {
            if (_taxRate != value)
            {
                _taxRate = value;
                Preferences.Set("TaxRate", _taxRate);
                OnPropertyChanged();
            }
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
