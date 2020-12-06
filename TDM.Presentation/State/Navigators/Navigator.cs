using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TDM.Domain;
using TDM.Presentation.Annotations;
using TDM.Presentation.Commands;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            } 
        }
    }
}