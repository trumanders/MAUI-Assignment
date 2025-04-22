namespace mau_assignment_4.Services;

public class PropertyValidator : IPropertyValidator
{
	private const int _maxNameLength = 40;
	private List<string> _validationMessages = [];

	/// <summary>
	/// Retrieves the validation error messages collected during property validation.
	/// </summary>
	/// <returns>A string containing the combined validation error messages</returns>
	public string GetValidationErrorMessages()
	{
		var result = string.Join("\n", _validationMessages);
		return result;
	}

	/// <summary>
	/// Validates the properties of the MainPageModel instance.
	/// It iterates through each property of the MainPageModel, checks its value,
	/// and performs specific validation based on the property name.
	/// </summary>
	/// <param name="pageModel">The MainPageModel instance to be validated</param>
	/// <returns>True: if all properties are valid; False: if any property is invalid</returns>
	public bool ValidateProperties(MainPageModel pageModel)
	{
		_validationMessages.Clear();
		var properties = pageModel.GetType().GetProperties();

		foreach (var property in properties)
		{
			var value = property.GetValue(pageModel)?.ToString();
			switch (property.Name)
			{
				case nameof(pageModel.SelectedSpecies):
					ValidateSpecies(value!);
					break;

				case nameof(pageModel.AgeInYears):
					ValidateInt(min: 0, max: 300, value!, "Age");
					break;

				case nameof(pageModel.ArmSpanInCentimeters):
					ValidateInt(min: 0, max: 300, value!, "Age");
					break;

				case nameof(pageModel.DivingDepthInMeters):
					ValidateInt(min: 0, max: 500, value!, "Diving depth");
					break;

				case nameof(pageModel.EggIncubationTemperature):
					ValidateInt(min: 20, max: 40, value!, "Egg incubation temperature");
					break;

				case nameof(pageModel.JawStrengthPSI):
					ValidateInt(min: 0, max: 4000, value!, "Jaw strength");
					break;

				case nameof(pageModel.JumpHeightInCentimeters):
					ValidateInt(min: 0, max: 1200, value!, "Jumping height");
					break;

				case nameof(pageModel.LactationPeriodInWeeks):
					ValidateInt(min: 0, max: 200, value!, "Lactation period");
					break;

				case nameof(pageModel.TalonsLengthInMillimeters):
					ValidateInt(min: 0, max: 120, value!, "Talons length");
					break;

				case nameof(pageModel.TongueLengthInMillimeters):
					ValidateInt(min: 0, max: 1000, value!, "Tongue length");
					break;

				case nameof(pageModel.TypicalNumberOfEggsLaid):
					ValidateInt(min: 0, max: 2000, value!, "Typical number of eggs laid");
					break;

				case nameof(pageModel.WeightInGrams):
					ValidateInt(min: 1, max: 15000000, value!, "Weight");
					break;

				case nameof(pageModel.WingAreaInCm2):
					ValidateInt(min: 50, max: 2500, value!, "Wing area");
					break;

				case nameof(pageModel.VocabularySize):
					ValidateInt(min: 0, max: 2000, value!, "Vocabulary size");
					break;

				case nameof(pageModel.PersonalName):
					ValidateName(value!);
					break;

				default:
					break;
			}
		}

		return _validationMessages.Count == 0;
	}

	/// <summary>
	/// Validates the selected species property of the MainPageModel instance.
	/// If value is null, it adds the appropriate error message.
	/// If value is an int and is within the specified range, it validates true.
	/// If valie is not an int or is not within the specified range, it adds the appropriate error message.
	/// </summary>
	/// <param name="min">The minimum allowed value</param>
	/// <param name="max">The maximum allowed value</param>
	/// <param name="inputValue">The value to be validated</param>
	/// <param name="propertyText">A string representing the property name</param>
	private void ValidateInt(int min, int max, string inputValue, string propertyText)
	{
		if (string.IsNullOrEmpty(inputValue))
			return;

		if (int.TryParse(inputValue, out int intValue))
		{
			if (intValue >= min && intValue <= max)
				return;

			_validationMessages.Add($"{propertyText}: Must be number between {min} and {max}");
		}
	}

	/// <summary>
	/// Validates the name property of the MainPageModel instance.
	/// </summary>
	/// <param name="value">The name value to be validated</param>
	private void ValidateName(string value)
	{
		if (string.IsNullOrEmpty(value) || value.Length > _maxNameLength)
		{
			_validationMessages.Add($"Name must be 1 - {_maxNameLength} characters");
		}
	}

	/// <summary>
	/// Validates the species property of the MainPageModel instance.
	/// </summary>
	/// <param name="species">The species value to be validated</param>
	private void ValidateSpecies(object species)
	{
		if (species == null)
			_validationMessages.Add("Species must be set.");
	}
}
