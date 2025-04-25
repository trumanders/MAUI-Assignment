namespace mau_assignment_4.Models;

[XmlRoot("FoodSchedules")]
public partial class FoodSchedule : ObservableObject
{
	[ObservableProperty]
	private string? _name;

	[XmlArray("FoodScheduleEvents")]
	[XmlArrayItem("Event")]
	public ObservableCollection<string> FoodScheduleEvents { get; set; } = [];
}
