namespace mau_assignment_4.Converters
{
    class BoolToGrayConverter : IValueConverter
    {
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
				return boolValue ? Colors.Black : Colors.Gray;
			return Colors.Gray;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
