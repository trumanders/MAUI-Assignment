namespace mau_assignment_4.Models.Birds;

public class Penguin : Bird
{
	public int? DivingDepthInMeters { get; set; }
	public bool IsHandFed { get; set; }

	public void Toboggan()
	{
		throw new NotImplementedException();
	}

	public void Swim()
	{
		throw new NotImplementedException();
	}

	public override void MapFromPageModel(MainPageModel pageModel)
	{
		base.MapFromPageModel(pageModel);
		DivingDepthInMeters = string.IsNullOrEmpty(pageModel.DivingDepthInMeters) ? null : int.Parse(pageModel.DivingDepthInMeters!);
		IsHandFed = pageModel.IsHandFed;
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Diving depth: {(DivingDepthInMeters == null
				? "" : (DivingDepthInMeters + (DivingDepthInMeters > 1 ? " meters" : " meter")))}\n" +
			$"Egg incubation temperature: {(EggIncubationTemperature == null ? "" : EggIncubationTemperature + " celcius")}\n";
	}
}
