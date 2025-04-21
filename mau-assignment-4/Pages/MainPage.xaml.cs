namespace mau_assignment_4.Pages;

public partial class MainPage : ContentPage
{
	/// <summary>
	/// Initializes the main page, sets the binding context to the provided <paramref name="pageModel"/>.
	/// Adjusts the window size.
	/// </summary>
	/// <param name="pageModel">The model containing the data to bind to the page.</param>

	private readonly MainPageModel _pageModel;
	public MainPage(MainPageModel pageModel)
    {
        InitializeComponent();
		_pageModel = pageModel;
		this.BindingContext = _pageModel;

		if (DeviceInfo.Platform == DevicePlatform.WinUI)
		{
			var window = Application.Current?.Windows[0];
			if (window != null)
			{
				window.Width = 1350; 
				window.Height = 1000; 
			}
		}
	}

	/// <summary>
	/// Handles the TextChanged event for the Entry control, ensuring that only numeric input is allowed.
	/// If the new text is not a valid integer, the text is reverted to the previous value.
	/// </summary>
	private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
	{

		_pageModel.OnTextChangedCommand.Execute((sender, e));		
	}

	/// <summary>
	/// Handles the ItemTapped event for the ListView control,
	/// creating a snapshot of the selected animal and populating
	/// the GUI with the selected animal.
	/// The snapshot is used later when checking for changes int the UI.
	/// </summary>
	/// <param name="sender">The sender of the event</param>
	/// <param name="e">The selected item</param>
	private void OnAnimalItemClick(object sender, ItemTappedEventArgs e)
	{
		_pageModel.OnAnimalItemClickCommand.Execute(e.Item);		
	}

	/// <summary>
	/// When an item in the list of food schedules is clicked, enable the button to add a food schedule to the UI.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnFoodScheduleItemClicked(object sender, ItemTappedEventArgs e)
	{
		_pageModel.OnFoodScheduleItemClickedCommand.Execute(e);
	}

	private void OnMenuBarNewClicked(object sender, EventArgs e)
	{
		_pageModel.OnMenuBarNewClickedCommand.Execute(e);
	}

	private void OnMenuBarOpenClicked(object sender, EventArgs e)
	{
		_pageModel.OnMenuBarOpenClickedCommand.Execute(e);
	}

	private void OnMenuBarSaveClicked(object sender, EventArgs e)
	{
		_pageModel.OnMenuBarSaveClickedCommand.Execute(e);
	}

	private void OnMenuBarSaveAsTextFileClicked(object sender, EventArgs e)
	{
		_pageModel.OnMenuBarSaveAsTextFileClickedCommand.Execute(e);
	}
	
	private void OnMenuBarSaveAsJsonClicked(object sender, EventArgs e)
	{
		_pageModel.OnMenuBarSaveAsJsonClickedCommand.Execute(e);
	}
}