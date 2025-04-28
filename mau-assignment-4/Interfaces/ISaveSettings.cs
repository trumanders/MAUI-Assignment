namespace mau_assignment_4.Interfaces;

public interface ISaveSettings
{
	public string? SaveLocation { get; set; }
	public SaveFileFormat SaveFileFormat { get; set; }
	public void SetSaveSettings(SaveFileFormat saveFileFormat, string saveLocation);
}