namespace mau_assignment_4.Models.Reptiles;

public class Chameleon : Reptile
{
	public int? TongueLengthInMillimeters { get; set; }
	public bool HasRegrownTail { get; set; }
	public new ReptileEnum? Species { get; set; }


	public void Camouflage()
	{
		throw new NotImplementedException();
	}

	public override void MapFromPageModel(MainPageModel pageModel)
	{
		base.MapFromPageModel(pageModel);
		TongueLengthInMillimeters = string.IsNullOrEmpty(pageModel.TongueLengthInMillimeters) ? null : int.Parse(pageModel.TongueLengthInMillimeters!);
		HasRegrownTail = pageModel.HasRegrownTail;
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Tongue length : {(TongueLengthInMillimeters == null
				? "" : (TongueLengthInMillimeters + (TongueLengthInMillimeters > 1 ? " millimeters" : " millimeter")))}\n" +
			$"Regrown tail: {(HasRegrownTail ? "Yes" : "No")}\n";
	}
}
