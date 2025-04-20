namespace mau_assignment_4.Pages;

public partial class FoodSchedulePage : ContentPage
{
	private readonly FoodSchedulePageModel _foodSchedulePageModel;
	private readonly MainPageModel _mainPageModel;
	
	public FoodSchedulePage(FoodSchedulePageModel foodSchedulePageModel, MainPageModel mainPageModel)
	{
		InitializeComponent();
		_foodSchedulePageModel = foodSchedulePageModel;
		_mainPageModel = mainPageModel;
		_mainPageModel.IsMainPageBlocked = true;
		this.BindingContext = _foodSchedulePageModel;
	}

	/// <summary>
	/// Disabled buttons when the page appears.
	/// </summary>
	protected override void OnAppearing()
	{
		base.OnAppearing();
		_foodSchedulePageModel.IsAddButtonEnabled = false;
		_foodSchedulePageModel.IsChangeButtonEnabled = false;
		_foodSchedulePageModel.IsDeleteButtonEnabled = false;
	}

	/// <summary>
	/// Enables the main page when the FoodSchedulePage is closed.
	/// </summary>
	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_mainPageModel.OnFoodSchedulePageClose();
	}

	/// <summary>
	/// When an item in the list of schedule events is clicked, invokes the
	/// OnEventListItemClickedCommand in the FoodSchedulePageModel.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e">The item that was clicked</param>
	private void OnEventListItemClicked(object sender, ItemTappedEventArgs e)
	{
		if (e.Item != null)
		{
			_foodSchedulePageModel?
				.OnEventListItemClickedCommand
				.Execute(e.Item);
		}
	}

	/// <summary>
	/// When the text in the entry for event change, invokes the
	/// OnEventEntryChangedCommand in the FoodSchedulePageModel.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e">The </param>
	private void OnEventEntryChanged(object sender, TextChangedEventArgs e)
	{
		_foodSchedulePageModel?
			.OnEventEntryChangedCommand
			.Execute(e);
	}

	/// <summary>
	/// When the text in the entry for name change, invokes the
	/// OnNameEntryChangedCommand in the FoodSchedulePageModel.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e">The value of the entry that was changed</param>
	private void OnNameEntryChanged(object sender, TextChangedEventArgs e)
	{
		_foodSchedulePageModel?
			.OnNameEntryChangedCommand
			.Execute(e);
	}
}