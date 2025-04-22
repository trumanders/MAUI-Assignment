namespace mau_assignment_4.Models.Reptiles;

public class Alligator : Reptile
{
	public int? JawStrengthPSI { get; set; }
	public new ReptileEnum Species { get; } = ReptileEnum.Alligator;


	public void Float()
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
		JawStrengthPSI = string.IsNullOrEmpty(pageModel.JawStrengthPSI) ? null : int.Parse(pageModel.JawStrengthPSI!);
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Jaw strength: {(JawStrengthPSI == null ? "" : JawStrengthPSI + " PSI")}\n";
	}
}