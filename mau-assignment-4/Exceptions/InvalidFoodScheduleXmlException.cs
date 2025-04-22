namespace mau_assignment_4.Exceptions;

public class InvalidFoodScheduleXmlException : Exception
{
	public string FilePath { get; }
	public string XmlErrorDetail { get; }
	public InvalidFoodScheduleXmlException(string filePath, string xmlErrorDetail)
		: base($"The XML file '{ filePath }' is invalid. Error: {xmlErrorDetail}")
	{
		FilePath = filePath;
		XmlErrorDetail = xmlErrorDetail;
	}

	public InvalidFoodScheduleXmlException(string filePath, string xmlErrorDetail, Exception innerException)
		: base($"The XML file '{filePath}' is invalid. Error: {xmlErrorDetail}", innerException)
	{
		FilePath = filePath;
		XmlErrorDetail = xmlErrorDetail;
	}
}
