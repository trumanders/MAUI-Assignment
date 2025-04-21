namespace mau_assignment_4.Models.Mammals;

public class Mammal : Animal
{
	public int? WeightInGrams { get; set; }
	public int? LactationPeriodInWeeks { get; set; }
	public bool IsCurrentlyNursing { get; set; }
	public bool IsPregnant { get; set; }

	public override void MapFromPageModel(MainPageModel pageModel)
	{
		base.MapFromPageModel(pageModel);
		WeightInGrams = string.IsNullOrEmpty(pageModel.WeightInGrams) ? null : int.Parse(pageModel.WeightInGrams!);
		LactationPeriodInWeeks = string.IsNullOrEmpty(pageModel.LactationPeriodInWeeks) ? null : int.Parse(pageModel.LactationPeriodInWeeks!);
		IsCurrentlyNursing = pageModel.IsCurrentlyNursing;
		IsPregnant = pageModel.IsPregnant;
	}
	public override string ToString()
	{
		return
			$"{base.ToString()}" +
			$"Weight : {(WeightInGrams == null
				? "" : (WeightInGrams + (WeightInGrams > 1 ? " grams" : " gram")))}\n" +
			$"Lactation period : {(LactationPeriodInWeeks == null
				? "" : (LactationPeriodInWeeks + (LactationPeriodInWeeks > 1 ? " weeks" : " week")))}\n" +
			$"Nursing: {(IsCurrentlyNursing ? "Yes" : "No")}\n" +
			$"Pregnant: {(IsPregnant ? "Yes" : "No")}\n";
	}
}


