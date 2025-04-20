namespace mau_assignment_4.Converters;

/// <summary>
/// Converts an empty string to an integer and vice versa for XAML data binding.
/// </summary>

public class EmptyStringToIntConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return value?.ToString() ?? "";
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (string.IsNullOrEmpty(value?.ToString()) || !int.TryParse(value?.ToString(), out int result))
			return null;
		return result;
	}
}
