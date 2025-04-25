namespace mau_assignment_4.Interfaces;

public interface IAnimalService : IListService<Animal>
{
	public ObservableCollection<Animal> Animals { get; }
	public Dictionary<Animal, FoodSchedule> AnimalFoodSchedules { get; }
	public bool Add(MainPageModel pageModel);
	public void SortAnimals(SortOption sortOptíon);
	public bool Edit(Animal animal, MainPageModel pageModel);
	public void Delete(Animal animal);
	public void ShowAnimalInfoStrings();
}
