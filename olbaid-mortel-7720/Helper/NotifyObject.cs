using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace olbaid_mortel_7720.Helper
{
  public class NotifyObject : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
