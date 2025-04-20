namespace mau_assignment_4.Converters;

/// <summary>
/// Converts a Category enum to a boolean for visibility binding of the 
/// Category GUI elements.
/// </summary>

public class CategoryVisibleBoolConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null) return false;
		return (Category?)parameter == (Category)value;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
