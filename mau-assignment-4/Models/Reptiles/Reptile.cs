namespace mau_assignment_4.Models.Reptiles;

public class Reptile : Animal
{
	public int? TypicalNumberOfEggsLaid { get; set; }
	public void ShedSkin()
	{
		throw new NotImplementedException();
	}

	public override void MapFromPageModel(MainPageModel pageModel)
	{
		base.MapFromPageModel(pageModel);
		TypicalNumberOfEggsLaid = string.IsNullOrEmpty(pageModel.TypicalNumberOfEggsLaid) ? null : int.Parse(pageModel.TypicalNumberOfEggsLaid!);
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Typical number of eggs laid: {TypicalNumberOfEggsLaid}\n";
	}
}
