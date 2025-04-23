namespace mau_assignment_4.Interfaces;
public interface IAlertService
{
	public Task ShowAlert(string title, string message, string closeButtonText);
	public Task ShowEditErrorAlert();
	public Task ShowInfoStringsAlert(IEnumerable<string> animalInfo);
	public Task ShowInvalidInputAlert(string message);
	public Task ShowDeleteErrorAlert();
	public Task ShowSuccessfulEditAlert();
	public Task ShowNoItemSelectedAlert();
	public Task ShowInvalidFoodScheduleXmlAlert(string message);
	public Task ShowSomethingWentWrongAlert(string message);
	public Task<bool> ShowAskSaveChangesBeforeSaveJson();
}
