﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using WpfHartDebugTool.Core.ViewModels.Base;

namespace WpfHartDebugTool.Core.ViewModels.Home
{
    public class ThirdViewModel : BaseViewModel
    {

        private MvxCommand saveAndCloseCommand;

        public MvxCommand SaveAndCloseCommand
        {
            get
            {
                saveAndCloseCommand = saveAndCloseCommand ?? new MvxCommand(DoSaveAndClose);
                return saveAndCloseCommand;
            }
        }

        private MvxPresentationHint popToRootHint = Mvx.Resolve<MvxPresentationHint>();

        private void DoSaveAndClose()
        {
            //do whatever work one would do to 'save', and send a message to pop to root               
            ChangePresentation(popToRootHint);
        }
    }
}