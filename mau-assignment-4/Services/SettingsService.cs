namespace mau_assignment_4.Services
{
	public class SettingsService : ISettingsService
	{
		public string? LastSavePath { get; set; } = null;

		public void ResetSavePath()
		{
			LastSavePath = null;
		}
	}
}
