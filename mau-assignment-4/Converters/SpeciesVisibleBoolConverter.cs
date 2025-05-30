﻿namespace mau_assignment_4.Converters;

/// <summary>
/// Converts Species enum to boolean value for managing visibility for the Species GUI elements.
/// </summary>

public class SpeciesVisibleBoolConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null) return false;
		if (parameter == null) return false;
		return parameter.Equals(value);
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
