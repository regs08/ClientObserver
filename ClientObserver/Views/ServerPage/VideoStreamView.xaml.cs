using ClientObserver.Services;
using ClientObserver.ViewModels;
namespace ClientObserver.Views
{
    public partial class VideoStreamView : ContentPage
    {
        public VideoStreamView(VideoStreamViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
