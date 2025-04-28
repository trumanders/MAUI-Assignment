using mau_assignment_4.Enums;

namespace mau_assignment_4.Services;

public class SaveSettings : ISaveSettings
{
	public string? SaveLocation { get; set; }
	public SaveFileFormat SaveFileFormat { get; set; } = SaveFileFormat.None;
	public void SetSaveSettings(SaveFileFormat saveFileFormat, string saveLocation)
	{
		SaveFileFormat = saveFileFormat;
		SaveLocation = saveLocation;
	}
}
