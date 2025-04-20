namespace mau_assignment_4.Interfaces;

public interface IPropertyValidator
{
	public string GetValidationErrorMessages();
	public bool ValidateProperties(MainPageModel pageModel);
}
