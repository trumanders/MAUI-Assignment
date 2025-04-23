namespace mau_assignment_4.Models;

public class Animal
{
	private static int _id = 0;
	public int Id { get; set; }
	public string? PersonalName { get; set; }
	public int? AgeInYears { get; set; }
	public Gender? Gender { get; set; }
	public bool IsVenomous { get; set; }
	public bool IsEndangered { get; set; }
	public DietTypesEnum DietTypes { get; set; }
	public Enum? Species { get; set; }

	public Animal()
	{
		Id = _id++;
	}

	public virtual void MapFromPageModel(MainPageModel pageModel)
	{
		PersonalName = pageModel.PersonalName;
		AgeInYears = string.IsNullOrEmpty(pageModel.AgeInYears) ? null : int.Parse(pageModel.AgeInYears!);
		Gender = pageModel.Gender;
		IsVenomous = pageModel.IsVenomous;
		IsEndangered = pageModel.IsEndangered;
		DietTypes = pageModel.DietTypes;
		Species = pageModel.SelectedSpecies;
	}

	public override string ToString()
	{
		return
			$"{GetType().Name}\n" +
			$"ID: { Id }\n" +
			$"Personal name: {(PersonalName == null ? "" : PersonalName)}\n" +
			$"Age: {(AgeInYears == null ? "" : AgeInYears + " years")}\n" +
			$"Gender: {Gender}\n" +
			$"Venomous: {(IsVenomous ? "Yes" : "No")}\n" +
			$"Endangered: {(IsEndangered ? "Yes" : "No")}\n" +
			$"Diet types: {DietTypes}\n";			
	}
}