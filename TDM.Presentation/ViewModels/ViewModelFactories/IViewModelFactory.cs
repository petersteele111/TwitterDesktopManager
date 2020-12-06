using System;
using System.Collections.Generic;
using System.Text;
using TDM.Presentation.State.Navigators;

namespace TDM.Presentation.ViewModels.ViewModelFactories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
