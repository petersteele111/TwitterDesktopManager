using System.Windows.Input;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.State.Navigators
{
    public enum ViewType {
        Home,
        MainLogin,
        NewUser,
        ReturningUser
    }
    
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}