namespace mau_assignment_4.Converters
{
    class BoolToGrayConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Colors.Black : Colors.Gray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
