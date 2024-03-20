using ClientObserver.ViewModels;
using ClientObserver.Services;
namespace ClientObserver.Views
{
public partial class PhotoView : ContentPage
{
	public PhotoView(ImageReceiverService imageReceiverServices)
	{
		InitializeComponent();
		BindingContext = new PhotoViewModel(imageReceiverServices);
	}
}
}