namespace mau_assignment_4.Converters;

/// <summary>
/// Converts an Enum flag to a boolean for XAML data binding, allowing checkbox selection based on flag matching.
/// </summary>

public class EnumFlagToBoolConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null ) return false;

		var currentPropertyDietTypes = (DietTypesEnum)value;
		var checkBoxDietType = (DietTypesEnum?)parameter;
		bool isCheck = (currentPropertyDietTypes & checkBoxDietType) == checkBoxDietType;
		// if checkbox parameter is a flag in the property enum, return true (check checkbox), otherwise false (don't check checkbox)
		return isCheck;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null) return false;

		var isCheckBoxChecked = (bool)value;

		// if checkbox is unchecked, return inverted parameter to tell setter to remove the flag.
		var flagToReturn = isCheckBoxChecked ? (DietTypesEnum?)parameter : ~(DietTypesEnum?)parameter;
		return flagToReturn;
	}
}