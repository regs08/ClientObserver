/*
using ClientObserver.ViewModels;
namespace ClientObserver.Views;

public partial class CreateConfigView : ContentPage
{
    private CreateServerConfigViewModel _viewModel;

    public CreateConfigView(CreateServerConfigViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e, string selectionType)
    {
        var selectedItems = e.CurrentSelection.Cast<string>().ToList();

        switch (selectionType)
        {
            case "SubTopic":
                _viewModel.MyUserEntry.SelectedSubTopics = selectedItems;
                break;
            case "PubTopic":
                _viewModel.MyUserEntry.SelectedPubTopics = selectedItems;
                break;
            case "Label":
                _viewModel.MyUserEntry.SelectedLabels = selectedItems;
                break;
                // Add more cases as needed
        }
    }

    private void OnSubTopicSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        OnSelectionChanged(sender, e, "SubTopic");
    }

    private void OnPubTopicSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        OnSelectionChanged(sender, e, "PubTopic");
    }

    private void OnLabelSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        OnSelectionChanged(sender, e, "Label");
    }


    private void OnNewEntryCompleted(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        var label = entry?.Text;

        if (!string.IsNullOrEmpty(label))
        {
            // Check if the sender is associated with SubTopics or PubTopics
            if (sender == customSelectedSubTopicsEntry)
            {
                _viewModel.AddEntryToSelectedList(label, entryType: "SubTopic");
            }
            else if (sender == customSelectedPubTopicsEntry)
            {
                _viewModel.AddEntryToSelectedList(label, entryType: "PubTopic");
            }
            else if (sender == customSelectedLabelsEntry)
            {
                _viewModel.AddEntryToSelectedList(label, entryType:"Label");
            }

            entry.Text = ""; // Clear the Entry field
        }
    }

}
*/