using System;
using System.Collections.Generic;
using System.Text;
using TDM.Presentation.State.Navigators;

namespace TDM.Presentation.ViewModels.ViewModelFactories
{
    public class ViewModelFactory : IViewModelFactory
    {
        CreateViewModel<LoginViewModel> _createLoginViewModel;
        CreateViewModel<HomeViewModel> _createHomeViewModel;
        CreateViewModel<NewUserViewModel> _createNewUserViewModel;
        CreateViewModel<ExistingUserViewModel> _createExistingUserViewModel;

        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel, CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<NewUserViewModel> createNewUserViewModel, CreateViewModel<ExistingUserViewModel> createExistingUserViewModel)
        {
            _createLoginViewModel = createLoginViewModel;
            _createHomeViewModel = createHomeViewModel;
            _createNewUserViewModel = createNewUserViewModel;
            _createExistingUserViewModel = createExistingUserViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.MainLogin:
                    return _createLoginViewModel();
                case ViewType.NewUser:
                    return _createNewUserViewModel();
                case ViewType.ReturningUser:
                    return _createExistingUserViewModel();
                default:
                    throw new ArgumentException("View Model Type Not Found", "viewType");
            }
        }
    }
}
