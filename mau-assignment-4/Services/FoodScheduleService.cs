namespace mau_assignment_4.Services;

public class FoodScheduleService : ListService<FoodSchedule>, IFoodScheduleService
{
	public FoodScheduleService(ISaveSettings _saveSettings, IAlertService _alertService) : base(_saveSettings, _alertService) {	}	
}
