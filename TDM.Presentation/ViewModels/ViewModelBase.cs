using System;
using System.Collections.Generic;
using System.Text;
using TDM.Domain;
using TDM.Domain.Models;

namespace TDM.Presentation.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : ObservableObject
    {

        #region Properties

        public static User User { get; set; }

        #endregion

    }
}
