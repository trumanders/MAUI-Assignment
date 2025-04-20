namespace mau_assignment_4.Converters;

/// <summary>
/// Inverts boolean value for property - xaml binding
/// </summary>
public class BooleanNegativeConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null && value is bool boolValue)
			return !boolValue;
		return value!;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null && value is bool boolValue)
			return !boolValue;
		return value!;
	}
}