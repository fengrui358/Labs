using System.Windows.Controls;
using MvvmCross.Core.ViewModels;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views
{
    public class CustomViewPresenter : MvxMultiRegionWpfViewPresenter
    {
        ContentControl _contentControl;

        public CustomViewPresenter(ContentControl contentControl)
            : base(contentControl)
        {
            _contentControl = contentControl;
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxPanelPopToRootPresentationHint)
            {
                var mainView = _contentControl.Content as MainView;
                if (mainView != null)
                {
                    mainView.PopToRoot();
                }
            }

            base.ChangePresentation(hint);
        }
    }
}
