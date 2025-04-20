namespace mau_assignment_4.Models.Birds;

public abstract class Bird : Animal
{
	public MigratoryPattern? MigratoryPattern { get; set; }
	public int? EggIncubationTemperature { get; set; }

	public void Sing()
	{
		throw new NotImplementedException();
	}

	public override void MapFromPageModel(MainPageModel pageModel)
	{
		base.MapFromPageModel(pageModel);
		MigratoryPattern = pageModel.MigratoryPattern;
		EggIncubationTemperature = string.IsNullOrEmpty(pageModel.EggIncubationTemperature) ? null : int.Parse(pageModel.EggIncubationTemperature!);
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Migratory pattern: {MigratoryPattern}\n" +
			$"Egg incubation temperature: {(EggIncubationTemperature == null ? "" : EggIncubationTemperature + " celcius")}\n";		
	}
}
