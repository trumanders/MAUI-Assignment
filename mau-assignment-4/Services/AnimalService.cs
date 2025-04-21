namespace mau_assignment_4.Services;

// I chose not to inherit from ListService, since the methods in AnimalService
// have Animal specific logic, and since it creates an unnecessary 
// coupling between the AnimalService and the ListService. Injecting ListSerivce
// here keeps the AnimalService decoupled from the ListService.
public class AnimalService(
	IPropertyValidator _propertyValidator,
	IAlertService _alertService) : ListService<Animal>, IAnimalService
{
	#region Private fields
	private static SortOption _previousSortOption;
	private static bool _isReverseOrder = false;
	private static bool _isAnimalAdded;
	#endregion

	public ObservableCollection<Animal> Animals => Items;
	public Dictionary<Animal, FoodSchedule> AnimalFoodSchedules { get; set; } = [];

	#region Public methods
	/// <summary>
	/// Adds a validated animal to the list of animals based on the data from the page model
	/// </summary>
	/// <param name="pageModel">The page model containing the selected species and other properties</param>
	/// <returns>True: if the animal was added; False: if the animal was not added</returns>
	public bool Add(MainPageModel pageModel)
	{
		if (!ValidateProperties(pageModel))
			return false;

		var animal = CreateAnimal(pageModel);

		// Since FoodSchedule is not part of Animal class, it is added to the dictionary sparately
		if (pageModel.AddedFoodSchedule != null)
			AnimalFoodSchedules.Add(animal, pageModel.AddedFoodSchedule);

		Add(animal);

		_isAnimalAdded = true;
		return true;
	}

	/// <summary>
	/// Creates an animal instance based on the selected species
	/// and calls the Animal instance's MapFromPageModel method to set its properties.
	/// </summary>
	/// <param name="pageModel">The page model containing the selected species and other properties</param>
	/// <returns>An animal instance with its properties set</returns>
	/// <exception cref="ArgumentException">An exception is thrown if the selected species is invalid</exception>
	public static Animal CreateAnimal(MainPageModel pageModel)
	{
		var species = pageModel.SelectedSpecies!;
		Animal animal = species switch
		{
			BirdEnum.Eagle => new Eagle(),
			BirdEnum.Parrot => new Parrot(),
			BirdEnum.Penguin => new Penguin(),
			MammalEnum.Gorilla => new Gorilla(),
			MammalEnum.Bat => new Bat(),
			MammalEnum.Dolphin => new Dolphin(),
			ReptileEnum.Alligator => new Alligator(),
			ReptileEnum.Komodo => new Komodo(),
			ReptileEnum.Chameleon => new Chameleon(),
			_ => throw new ArgumentException("Invalid species")
		};
		animal.MapFromPageModel(pageModel);
		return animal;
	}

	/// <summary>
	/// Edits the specified animal with the data from the page model
	/// </summary>
	/// <param name="animal">The animal to be edited</param>
	/// <param name="pageModel">The page model containing the updated data</param>
	/// <returns></returns>
	public bool Edit(Animal animal, MainPageModel pageModel)
	{
		bool success = true;
		if (!ValidateProperties(pageModel))
			return false;

		var originalIndex = Items.IndexOf(animal);
		var changedAnimal = CreateAnimal(pageModel);

		if (!ChangeAt(changedAnimal, originalIndex))
		{
			_alertService.ShowEditErrorAlert();
			return false;
		}

		AnimalFoodSchedules.Remove(animal);
		AnimalFoodSchedules.Add(changedAnimal, pageModel.AddedFoodSchedule);

		_alertService.ShowSuccessfulEditAlert();
		AnimalFoodSchedules[animal] = pageModel.SelectedFoodSchedule;

		return true;
	}

	/// <summary>
	/// Removes the specified animal from the list
	/// </summary>
	/// <param name="animal">The animal to be removed from the list</param>
	public void Delete(Animal animal)
	{
		var index = Animals.IndexOf(animal);

		if (animal == null)
		{
			_alertService.ShowNoItemSelectedAlert();
			return;
		}

		if (!DeleteAt(index))
		{
			_alertService.ShowDeleteErrorAlert();
			return;
		}
	}

	/// <summary>
	/// Sorts the list of animals based on the specified sort option
	/// and reverses the order if the sort option is the same as the previous.
	/// </summary>
	/// <param name="sortOption">The sort option that determines how the list should be sorted</param>
	public void SortAnimals(SortOption sortOption)
	{
		// If sorting after adding animal, always sort in ascending order
		if (_isAnimalAdded)
		{
			_isReverseOrder = false;
			_isAnimalAdded = false;
		}
		else
		// Toggle between ascending and descending
		{
			if (sortOption == _previousSortOption)
				_isReverseOrder = !_isReverseOrder;
			else
				_isReverseOrder = false;
		}

		// Create new sorted list
		var sortedList = Items.OrderBy(a => a, new AnimalComparer(sortOption, _isReverseOrder))
			.ToList();

		// Replace old list with new sorted list
		Items.Clear();
		foreach (var item in sortedList)
		{
			Items.Add(item);
		}

		_previousSortOption = sortOption;
	}


	/// <summary>
	/// Provides the animal at the specified index
	/// </summary>
	/// <param name="index">The index of the animal</param>
	/// <returns>The animal at the specified index</returns>
	// Not implemented since SelectedAnimal is set directly from the UI when
	// selecting an animal from the ListView
	public Animal GetAnimalAt(int index)
	{
		return Animals[index];
	}

	/// <summary>
	/// Calls the IAlertService to display all animals' info
	/// </summary>
	public void ShowAnimalInfoStrings() =>
		_alertService.ShowInfoStringsAlert(GetAnimalInfoStrings());

	/// <summary>
	/// Calls the IPropertyValidator.ValidateProperties method to validate the properties of
	/// the MainPageModel instance.
	/// </summary>
	/// <param name="pageModel">The MainPageModel instance to be validated</param>
	/// <returns>True: if validation is successful; False: if validation fails</returns>
	private bool ValidateProperties(MainPageModel pageModel)
	{
		if (!_propertyValidator.ValidateProperties(pageModel))
		{
			_alertService.ShowInvalidInputAlert(_propertyValidator.GetValidationErrorMessages());
			return false;
		}
		return true;
	}
	#endregion

	/// <summary>
	/// Gathers all animals' info from its ToString()-method and returns them as a string array
	/// </summary>
	/// <returns>A string array containing all animals' info</returns>
	private string[] GetAnimalInfoStrings()
	{		
		var allAnimalInfo = Animals.Select(animal => GetAnimalInfo(animal) + Environment.NewLine + Environment.NewLine).ToArray();
		return allAnimalInfo;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="animal"></param>
	/// <returns></returns>
	private static string GetAnimalInfo(Animal animal) // Not implemented (not described in assignment)
	{
		return animal.ToString();
	}
}
