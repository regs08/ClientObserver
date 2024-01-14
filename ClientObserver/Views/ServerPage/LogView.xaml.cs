using ClientObserver.ViewModels;
using ClientObserver.Services;
namespace ClientObserver.Views;

public partial class LogView : ContentPage
{
	public LogView(LogService logService)
	{
		InitializeComponent();
		BindingContext = new LogViewModel(logService);
	}
}
