namespace mau_assignment_4.Services;

class AlertService : IAlertService
{
	public Task ShowInvalidInputAlert(string message)
	{
		return ShowAlert("Invalid input", message ?? "Invalid input", "OK");
	}

	public Task ShowInfoStringsAlert(IEnumerable<string> strings)
	{
		return ShowAlert("All items info", string.Join("", strings) ?? "No items found", "OK");
	}

	public Task ShowEditErrorAlert()
	{
		return ShowAlert("Edit", "Error while editing.", "OK");
	}
	
	public Task ShowDeleteErrorAlert()
	{
		return ShowAlert("Delete", "Error while deleting.", "OK");
	}

	public Task ShowSuccessfulEditAlert()
	{
		return ShowAlert("Changes saved!", "Changes sucessfully saved.", "OK");
	}

	public Task ShowNoItemSelectedAlert()
	{
		return ShowAlert("No item selected", "Please select an item.", "OK");
	}

	public Task ShowInvalidFoodScheduleXmlAlert(string message)
	{
		return ShowAlert("Invalid food schedule xml file!", message, "Ok");
	}

	public Task ShowSomethingWentWrongAlert(string message)
	{
		return ShowAlert("Something went wrong", "Error: " + message, "Ok");
	}

	public Task<bool> ShowAskSaveChangesBeforeSaveJson()
	{
		var mainPage = Application.Current?.Windows[0].Page;

		if (mainPage != null)
		{
			return mainPage.DisplayAlert(
				"Update list?",
				"You have unsaved changes. Do you want to inclide them in the list before saving to file?",
				"Yes",
				"No"
			).ContinueWith(task => task.Result);
		}
		return Task.FromResult(false);
	}

	/// <summary>
	/// Displays an alert based on the title, message and close button text provided
	/// </summary>
	/// <param name="title">The title of the alert</param>
	/// <param name="message">The message of the alert</param>
	/// <param name="closeButtonText">The text of the close button</param>
	/// <returns></returns>
	public Task ShowAlert(string title, string? message, string closeButtonText)
	{
		var mainPage = Application.Current?.Windows[0].Page;

		if (mainPage != null)
		{
			return mainPage.DisplayAlert(title, message, closeButtonText);
		}

		return Task.CompletedTask;
	}
}
