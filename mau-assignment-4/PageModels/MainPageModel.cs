using CommunityToolkit.Mvvm.Messaging;
using mau_assignment_4.Pages;

namespace mau_assignment_4.PageModels;

/// <summary>
/// This class represents data and logic for MainPage. It has logic that
/// enables / disables / shows / hides various elements to provide structure
/// to the GUI.
/// </summary>
public partial class MainPageModel : INotifyPropertyChanged
{
	#region Private properties

	private readonly IAnimalService _animalService;
	private readonly IFoodScheduleService _foodScheduleService;
	private const int _maxNameLength = 40;
	private string? _ageInYears;
	private string? _armSpanInCentimeters;
	private string? _divingDepthInMeters;
	private string? _eggIncubationTemperature;
	private string? _foodSchedule;
	private string? _jawStrengthPSI;
	private string? _jumpingHeightInCentimeters;
	private string? _lactationPeriodInWeeks;
	private string? _talonsLengthInMillimeters;
	private string? _tongueLengthInMillimeters;
	private string? _typicalNumberOfEggsLaid;
	private string? _weightInGrams;
	private string? _wingAreaInCm2;
	private string? _vocabularySize;
	private string? _personalName = null;
	private string? _addedFoodScheduleEvents;
	private string? _addedFoodScheduleName;

	private bool _hasPermanentInjury;
	private bool _hasRegrownTail;
	private bool _hasUniqueMarkings;
	private bool _isAlphaMale;
	private bool _isCurrentlyNursing;
	private bool _isEndangered;
	private bool _isHandFed;
	private bool _isPartOfBreedingProgram;
	private bool _isPregnant;
	private bool _isTrainedForPerformance;
	private bool _isVenomous;
	private bool _isSaveChangesEnabled;
	private bool _allowSaveChanges;
	private bool _isMainPageBlocked;
	private bool _isListAllSpeciesChecked;
	private bool __isAddFoodScheduleToAnimalButtonEnabled = false;
	private bool _isEditFoodScheduleButtonEnabled = false;

	private DietTypesEnum _dietTypes;
	private Gender? _gender;
	private MigratoryPattern? _migratoryPattern;

	private ImageSource? _animalImage;
	private Category? _selectedCategory;
	private Enum? _selectedSpecies;
	private Animal? _selectedAnimal;
	private Animal? _initialSelectedAnimal;
	private FoodSchedulePage _foodSchedulePage;
	private FoodSchedule _selectedFoodSchedule;
	private FoodSchedule _addedFoodSchedule;


	#endregion

	#region Public properties - Animal

	public bool IsEditFoodScheduleButtonEnabled
	{
		get => _isEditFoodScheduleButtonEnabled;
		set
		{
			_isEditFoodScheduleButtonEnabled = value;
		}
	}
	public bool AllowSaveChanges
	{
		get => _allowSaveChanges;
		set
		{
			_allowSaveChanges = value;
			IsSaveChangesEnabled = false;
		}
	}

	public bool IsSaveChangesEnabled
	{
		get => _isSaveChangesEnabled;
		set
		{
			if (_isSaveChangesEnabled != value)
			{
				_isSaveChangesEnabled = value;
				OnPropertyChanged(IsSaveChangesEnabled);
			}
		}
	}

	public bool IsAddFoodScheduleToUIButtonEnabled
	{
		get => __isAddFoodScheduleToAnimalButtonEnabled;
		set
		{
			if (__isAddFoodScheduleToAnimalButtonEnabled != value)
			{
				__isAddFoodScheduleToAnimalButtonEnabled = value;
				OnPropertyChanged(value, nameof(IsAddFoodScheduleToUIButtonEnabled));
			}
		}
	}

	public bool HasPermanentInjury
	{
		get => _hasPermanentInjury;
		set
		{
			if (_hasPermanentInjury != value)
			{
				_hasPermanentInjury = value;
				OnPropertyChanged(value, nameof(HasPermanentInjury));
			}
		}
	}

	public bool HasRegrownTail
	{
		get => _hasRegrownTail;
		set
		{
			if (_hasRegrownTail != value)
			{
				_hasRegrownTail = value;
				OnPropertyChanged(value, nameof(HasRegrownTail));
			}
		}
	}

	public bool HasUniqueMarkings
	{
		get => _hasUniqueMarkings;
		set
		{
			if (_hasUniqueMarkings != value)
			{
				_hasUniqueMarkings = value;
				OnPropertyChanged(value, nameof(HasUniqueMarkings));
			}
		}
	}

	public bool IsAlphaMale
	{
		get => _isAlphaMale;
		set
		{
			if (_isAlphaMale != value)
			{
				_isAlphaMale = value;
				OnPropertyChanged(value, nameof(IsAlphaMale));
			}
		}
	}

	public bool IsCurrentlyNursing
	{
		get => _isCurrentlyNursing;
		set
		{
			if (_isCurrentlyNursing != value)
			{
				_isCurrentlyNursing = value;
				OnPropertyChanged(value, nameof(IsCurrentlyNursing));
			}
		}
	}

	public bool IsEndangered
	{
		get => _isEndangered;
		set
		{
			if (_isEndangered != value)
			{
				_isEndangered = value;
				OnPropertyChanged(value, nameof(IsEndangered));
			}
		}
	}

	public bool IsHandFed
	{
		get => _isHandFed;
		set
		{
			if (_isHandFed != value)
			{
				_isHandFed = value;
				OnPropertyChanged(value, nameof(IsHandFed));
			}
		}
	}

	public bool IsPartOfBreedingProgram
	{
		get => _isPartOfBreedingProgram;
		set
		{
			if (_isPartOfBreedingProgram != value)
			{
				_isPartOfBreedingProgram = value;
				OnPropertyChanged(value, nameof(IsPartOfBreedingProgram));
			}
		}
	}

	public bool IsPregnant
	{
		get => _isPregnant;
		set
		{
			if (_isPregnant != value)
			{
				_isPregnant = value;
				OnPropertyChanged(value, nameof(IsPregnant));
			}
		}
	}

	public bool IsTrainedForPerformance
	{
		get => _isTrainedForPerformance;
		set
		{
			if (_isTrainedForPerformance != value)
			{
				_isTrainedForPerformance = value;
				OnPropertyChanged(value, nameof(IsTrainedForPerformance));
			}
		}
	}

	public bool IsVenomous
	{
		get => _isVenomous;
		set
		{
			if (_isVenomous != value)
			{
				_isVenomous = value;
				OnPropertyChanged(value, nameof(IsVenomous));
			}
		}
	}

	public string? AgeInYears
	{
		get => _ageInYears;
		set
		{
			if (_ageInYears != value)
			{
				_ageInYears = value;
				OnPropertyChanged(value, nameof(AgeInYears));
			}
		}
	}

	public string? ArmSpanInCentimeters
	{
		get => _armSpanInCentimeters;
		set
		{
			if (_armSpanInCentimeters != value)
			{
				_armSpanInCentimeters = value;
				OnPropertyChanged(value, nameof(ArmSpanInCentimeters));
			}
		}
	}
	public string? DivingDepthInMeters
	{
		get => _divingDepthInMeters;
		set
		{
			if (_divingDepthInMeters != value)
			{
				_divingDepthInMeters = value;
				OnPropertyChanged(value, nameof(DivingDepthInMeters));
			}
		}
	}
	public string? EggIncubationTemperature
	{
		get => _eggIncubationTemperature;
		set
		{
			if (_eggIncubationTemperature != value)
			{
				_eggIncubationTemperature = value;
				OnPropertyChanged(value, nameof(EggIncubationTemperature));
			}
		}
	}
	public string? JawStrengthPSI
	{
		get => _jawStrengthPSI;
		set
		{
			if (_jawStrengthPSI != value)
			{
				_jawStrengthPSI = value;
				OnPropertyChanged(value, nameof(JawStrengthPSI));
			}
		}
	}
	public string? JumpHeightInCentimeters
	{
		get => _jumpingHeightInCentimeters;
		set
		{
			if (_jumpingHeightInCentimeters != value)
			{
				_jumpingHeightInCentimeters = value;
				OnPropertyChanged(value, nameof(JumpHeightInCentimeters));
			}
		}
	}
	public string? LactationPeriodInWeeks
	{
		get => _lactationPeriodInWeeks;
		set
		{
			if (_lactationPeriodInWeeks != value)
			{
				_lactationPeriodInWeeks = value;
				OnPropertyChanged(value, nameof(LactationPeriodInWeeks));
			}
		}
	}
	public string? TalonsLengthInMillimeters
	{
		get => _talonsLengthInMillimeters;
		set
		{
			if (_talonsLengthInMillimeters != value)
			{
				_talonsLengthInMillimeters = value;
				OnPropertyChanged(value, nameof(TalonsLengthInMillimeters));
			}
		}
	}
	public string? TongueLengthInMillimeters
	{
		get => _tongueLengthInMillimeters;
		set
		{
			if (_tongueLengthInMillimeters != value)
			{
				_tongueLengthInMillimeters = value;
				OnPropertyChanged(value, nameof(TongueLengthInMillimeters));
			}
		}
	}
	public string? TypicalNumberOfEggsLaid
	{
		get => _typicalNumberOfEggsLaid;
		set
		{
			if (_typicalNumberOfEggsLaid != value)
			{
				_typicalNumberOfEggsLaid = value;
				OnPropertyChanged(value, nameof(TypicalNumberOfEggsLaid));
			}
		}
	}
	public string? WeightInGrams
	{
		get => _weightInGrams;
		set
		{
			if (_weightInGrams != value)
			{
				_weightInGrams = value;
				OnPropertyChanged(value, nameof(WeightInGrams));
			}
		}
	}
	public string? WingAreaInCm2
	{
		get => _wingAreaInCm2;
		set
		{
			if (_wingAreaInCm2 != value)
			{
				_wingAreaInCm2 = value;
				OnPropertyChanged(value, nameof(WingAreaInCm2));
			}
		}
	}
	public string? VocabularySize
	{
		get => _vocabularySize;
		set
		{
			if (_vocabularySize != value)
			{
				_vocabularySize = value;
				OnPropertyChanged(value, nameof(VocabularySize));
			}
		}
	}

	public string? PersonalName
	{
		get
		{
			if (string.IsNullOrEmpty(_personalName))
				PersonalName = "";
			return _personalName;
		}
		set
		{
			if (_personalName != value)
			{
				_personalName = value;
				OnPropertyChanged(value, nameof(PersonalName));
			}
		}
	}

	public Gender? Gender
	{
		get => _gender;
		set
		{
			if (_gender != value)
			{
				_gender = value;
				OnPropertyChanged(value, nameof(Gender));
			}
		}
	}
	public MigratoryPattern? MigratoryPattern
	{
		get => _migratoryPattern;
		set
		{
			if (_migratoryPattern != value)
			{
				_migratoryPattern = value;
				OnPropertyChanged(value, nameof(MigratoryPattern));
			}
		}
	}

	public ImageSource? AnimalImage
	{
		get => _animalImage;
		set
		{
			if (_animalImage != value)
			{
				_animalImage = value;
				OnPropertyChanged(nameof(AnimalImage));
			}
		}
	}

	/// <summary>
	/// DietTypes is a flagged enum. To indicate wheter a checkbox is checked
	/// or not from the xaml file, the flag is inverted for "not checked" and
	/// not inverted for "is checked". This works because each checkbox must have
	/// a single flag. By subtractic 1 from value, we know if the flag is inverted
	/// or not. If not inverted there is no matching bit between value and value-1.
	/// If the flag is inverted, we treat that as "not checked" and the flag can be
	/// removed by using the AND bit operator with the inverted bits.
	/// </summary>
	public DietTypesEnum DietTypes
	{
		get => _dietTypes;
		set
		{
			var checkBoxIsChecked = (value & (value - 1)) == 0;
			_dietTypes = checkBoxIsChecked ? _dietTypes | value : _dietTypes & value;

			OnPropertyChanged(value, nameof(DietTypes));
		}
	}
	#endregion

	#region Public properties - UI
	public ObservableCollection<FoodSchedule> FoodSchedules => _foodScheduleService.Items;

	public ObservableCollection<Animal> Animals => _animalService.Animals;

	public List<string> ValidationMessages { get; } = new();

	public bool IsMainPageBlocked
	{
		get => _isMainPageBlocked;
		set
		{
			if (_isMainPageBlocked != value)
			{
				_isMainPageBlocked = value;
				OnPropertyChanged(value, nameof(IsMainPageBlocked));
			}
		}
	}

	/// <summary>
	/// If category is changed manually by user, the selected species is set to null (no selection)
	/// </summary>
	public Category? SelectedCategory
	{
		get => _selectedCategory;
		set
		{
			if (_selectedCategory != value)
			{
				_selectedCategory = value;

				if (!_isListAllSpeciesChecked)
				{
					SelectedSpecies = null;
				}
				OnPropertyChanged(value, nameof(SelectedCategory));
				OnPropertyChanged(nameof(CategorySpecies));
			}
		}
	}

	/// <summary>
	/// The selected species determines what species class to cast animal to.
	/// Selected Category is also updated because might change when the option
	/// "list all species" is enabled and user selects a species from another category
	/// than the currently selected.
	/// </summary>
	public Enum? SelectedSpecies
	{
		get => _selectedSpecies;
		set
		{
			if (_selectedSpecies != value)
			{
				_selectedSpecies = value;

				if (SelectedSpecies is BirdEnum) SelectedCategory = Category.Bird;
				if (SelectedSpecies is MammalEnum) SelectedCategory = Category.Mammal;
				if (SelectedSpecies is ReptileEnum) SelectedCategory = Category.Reptile;

				OnPropertyChanged(value, nameof(SelectedSpecies));
				OnPropertyChanged(value, nameof(SelectedCategory));
			}
		}
	}

	public Animal? SelectedAnimal
	{
		get => _selectedAnimal;
		set
		{
			if (_selectedAnimal != value)
			{
				_selectedAnimal = value;
				OnPropertyChanged(nameof(SelectedAnimal));
				OnPropertyChanged(nameof(FoodSchedule));
			}
		}
	}

	public string? AddedFoodScheduleName
	{
		get => _addedFoodScheduleName;
		set
		{
			if (_addedFoodScheduleName != value)
			{
				_addedFoodScheduleName = value;
				OnPropertyChanged(nameof(AddedFoodScheduleName));
			}
		}
	}

	public string? AddedFoodScheduleEvents
	{
		get => _addedFoodScheduleEvents;
		set
		{
			if (_addedFoodScheduleEvents != value)
			{
				_addedFoodScheduleEvents = value;
				OnPropertyChanged(nameof(AddedFoodScheduleEvents));
			}
		}
	}
	public FoodSchedule AddedFoodSchedule
	{
		get => _addedFoodSchedule;
		set
		{
			if (_addedFoodSchedule != value)
			{
				_addedFoodSchedule = value;
				OnPropertyChanged(nameof(AddedFoodSchedule));
			}
			if (_addedFoodSchedule != null)
			{
				AddedFoodScheduleName = _addedFoodSchedule.Name;
				AddedFoodScheduleEvents = string.Join("\n\n", _addedFoodSchedule.FoodScheduleEvents);
			}
		}
	}


	public FoodSchedule SelectedFoodSchedule
	{
		get => _selectedFoodSchedule;
		set
		{
			if (_selectedFoodSchedule != value)
			{
				_selectedFoodSchedule = value;
				//OnPropertyChanged(nameof(SelectedFoodSchedule));
			}
			IsEditFoodScheduleButtonEnabled = _selectedFoodSchedule != null;
		}
	}

	/// <summary>
	/// Selected Species is set to null and reset after species list is updated
	/// to prevent out of bounds exceoption when "list all species" is disabled,
	/// which makes the species list shrink.
	/// </summary>
	public bool IsListAllSpeciesChecked
	{
		get => _isListAllSpeciesChecked;
		set
		{
			_isListAllSpeciesChecked = value;
			var previousSelectedSpecies = SelectedSpecies;

			if (!value)
				SelectedSpecies = null;

			OnPropertyChanged(nameof(IsListAllSpeciesChecked));
			OnPropertyChanged(nameof(CategorySpecies));
			SelectedSpecies = previousSelectedSpecies;
		}
	}

	/// <summary>
	/// Returns a list of species based on the selected category and
	/// whether "list all species" is enabled.
	/// </summary>
	public IReadOnlyList<Enum> CategorySpecies
	{
		get
		{
			if (_isListAllSpeciesChecked)
			{
				return BirdSpecies
					.Concat(MammalSpecies)
					.Concat(ReptileSpecies)
					.ToList();
			}
			if (SelectedCategory == Category.Bird) return BirdSpecies;
			if (SelectedCategory == Category.Mammal) return MammalSpecies;
			if (SelectedCategory == Category.Reptile) return ReptileSpecies;
			return new List<Enum>();
		}
	}
	public IReadOnlyList<Gender> Genders => Enum.GetValues<Gender>();
	public IReadOnlyList<Category> Categories => Enum.GetValues<Category>();
	public IReadOnlyList<Enum> BirdSpecies => Enum.GetValues<BirdEnum>().Cast<Enum>().ToList();
	public IReadOnlyList<Enum> MammalSpecies => Enum.GetValues<MammalEnum>().Cast<Enum>().ToList();
	public IReadOnlyList<Enum> ReptileSpecies => Enum.GetValues<ReptileEnum>().Cast<Enum>().ToList();
	public IReadOnlyList<Enum> MigratoryPatterns => Enum.GetValues<MigratoryPattern>().Cast<Enum>().ToList();
	public event PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Commands
	public ICommand OnAddAnimalClickCommand { get; set; }
	public ICommand OnSelectImageClickCommand { get; set; }
	public ICommand OnRemoveImageClickCommand { get; set; }
	public ICommand OnSortAnimalsClickCommand { get; set; }
	public ICommand OnDeleteAnimalClickCommand { get; set; }
	public ICommand OnChangeAnimalClickCommand { get; set; }
	public ICommand OnClearClickCommand { get; set; }
	public ICommand OnAllAnimalInfoClickCommand { get; set; }
	public ICommand OnMainPageClickCommand { get; set; }
	public ICommand OnAddFoodScheduleToUIClickCommand { get; set; }
	public ICommand OnEditFoodScheduleClickCommand { get; set; }
	public ICommand OnCreateFoodScheduleClickCommand { get; set; }
	public ICommand OnFoodScheduleItemClickedCommand { get; set; }
	public ICommand OnTextChangedCommand { get; set; }
	public ICommand OnAnimalItemClickCommand { get; set; }
	public ICommand OnFoodScheduleItemClicked { get; set; }
	public ICommand OnMenuBarNewClickedCommand { get; set; }
	public ICommand OnMenuBarOpenClickedCommand { get; set; }
	public ICommand OnMenuBarSaveClickedCommand { get; set; }
	public ICommand OnMenuBarSaveAsClickedCommand { get; set; }



	#endregion

	#region Public methods
	public MainPageModel(IAnimalService animalService, IFoodScheduleService foodScheduleService)
	{
		_animalService = animalService;
		_foodScheduleService = foodScheduleService;
		InitializeMessagingCenter();
		InitializeCommands();
	}

	/// <summary>
	/// Sets all Animal properties in the UI based on the passed in animal instance.
	/// </summary>
	/// <param name="animal">The object to map from, to the UI</param>
	public void PopulateGUIFromSelectedAnimal()
	{
		PersonalName = SelectedAnimal.PersonalName;
		AgeInYears = SelectedAnimal.AgeInYears.ToString();
		Gender = SelectedAnimal.Gender;
		IsVenomous = SelectedAnimal.IsVenomous;
		IsEndangered = SelectedAnimal.IsEndangered;
		_dietTypes = SelectedAnimal.DietTypes;
		UpdateAddedFoodSchedule(SelectedAnimal);

		// Fix this so that it gets the correct foodschedule from the dictionary in AnimalService.
		//FoodScheduleInfo = string.Join(Environment.NewLine, animal.GetFoodSchedule()!.GetFoodListInfoStrings());

		OnPropertyChanged(nameof(DietTypes));

		if (SelectedAnimal is Bird bird)
		{
			SelectedCategory = Category.Bird;

			MigratoryPattern = bird.MigratoryPattern;
			EggIncubationTemperature = bird.EggIncubationTemperature.ToString();

			if (SelectedAnimal is Eagle eagle)
			{
				SelectedSpecies = BirdEnum.Eagle;
				TalonsLengthInMillimeters = eagle.TalonsLengthInMillimeters.ToString();
				HasPermanentInjury = eagle.HasPermanentInjury;
			}

			if (SelectedAnimal is Parrot parrot)
			{
				SelectedSpecies = BirdEnum.Parrot;
				VocabularySize = parrot.VocabularySize.ToString();
				IsTrainedForPerformance = parrot.IsTrainedForPerformance;
			}

			if (SelectedAnimal is Penguin penguin)
			{
				SelectedSpecies = BirdEnum.Penguin;
				DivingDepthInMeters = penguin.DivingDepthInMeters.ToString();
				IsHandFed = penguin.IsHandFed;
			}
		}

		if (SelectedAnimal is Mammal mammal)
		{
			SelectedCategory = Category.Mammal;

			WeightInGrams = mammal.WeightInGrams.ToString();
			LactationPeriodInWeeks = mammal.LactationPeriodInWeeks.ToString();
			IsCurrentlyNursing = mammal.IsCurrentlyNursing;
			IsPregnant = mammal.IsPregnant;


			if (SelectedAnimal is Bat bat)
			{
				SelectedSpecies = MammalEnum.Bat;
				WingAreaInCm2 = bat.WingAreaInCm2.ToString();
			}

			if (SelectedAnimal is Gorilla gorilla)
			{
				SelectedSpecies = MammalEnum.Gorilla;

				ArmSpanInCentimeters = gorilla.ArmSpanInCentimeters.ToString();
				IsAlphaMale = gorilla.IsAlphaMale;
			}

			if (SelectedAnimal is Dolphin dolphin)
			{
				SelectedSpecies = MammalEnum.Dolphin;

				JumpHeightInCentimeters = dolphin.JumpHeightInCentimeters.ToString();
				HasUniqueMarkings = dolphin.HasUniqueMarkings;
			}
		}

		if (SelectedAnimal is Reptile reptile)
		{
			SelectedCategory = Category.Reptile;

			TypicalNumberOfEggsLaid = reptile.TypicalNumberOfEggsLaid.ToString();

			if (SelectedAnimal is Alligator alligator)
			{
				SelectedSpecies = ReptileEnum.Alligator;
				JawStrengthPSI = alligator.JawStrengthPSI.ToString();
			}

			if (SelectedAnimal is Chameleon chameleon)
			{
				SelectedSpecies = ReptileEnum.Chameleon;
				TongueLengthInMillimeters = chameleon.TongueLengthInMillimeters.ToString();
				HasRegrownTail = chameleon.HasRegrownTail;
			}

			if (SelectedAnimal is Komodo komodo)
			{
				SelectedSpecies = ReptileEnum.Komodo;
				IsPartOfBreedingProgram = komodo.IsPartOfBreedingProgram;
			}
		}
		AllowSaveChanges = true;
	}

	public void UpdateAddedFoodSchedule(Animal selectedAimal)
	{
		if (SelectedAnimal == null) return;

		if (_animalService.AnimalFoodSchedules.TryGetValue(SelectedAnimal, out var foodSchedule) && foodSchedule != null)
		{
			AddedFoodScheduleEvents = string.Join("\n\n", foodSchedule.FoodScheduleEvents);
			AddedFoodScheduleName = foodSchedule.Name;
			return;
		}
		AddedFoodScheduleEvents = string.Empty;
		AddedFoodScheduleName = string.Empty;
	}

	public void UpdateAddedFoodSchedule()
	{
		
		AddedFoodSchedule = SelectedFoodSchedule;
	}

	/// <summary>
	/// Sets an instance of Animal to the private field.
	/// </summary>
	/// <param name="initialSelectedAnimal">The instance of Animal to assign the private field to</param>
	public void CreateAnimalSnapshot()
	{
		_initialSelectedAnimal = SelectedAnimal;
	}

	/// <summary>
	/// Notifies UI about change in the property. Calls method to determine if saving changes
	/// should be enabled based on whether any of the UI properties has been changed by the user.
	/// </summary>
	/// <param name="newValue">The new value of the propery that is changing</param>
	/// <param name="propertyName">The name of the property to notify the UI about</param>
	public void OnPropertyChanged(object? newValue, [CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		if (newValue != null && propertyName != null)
			SetEnableSaveChanges(newValue.ToString()!, propertyName!);
	}

	/// <summary>
	/// Notifies UI about change in the property
	/// </summary>
	/// <param name="propertyName">The name of the property to notify the UI about</param>
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public void OnFoodSchedulePageClose()
	{
		IsMainPageBlocked = false;
		_foodSchedulePage = null;
	}

	public void ClearUI()
	{
		HasPermanentInjury = false;
		HasRegrownTail = false;
		HasUniqueMarkings = false;
		IsAlphaMale = false;
		IsCurrentlyNursing = false;
		IsEndangered = false;
		IsHandFed = false;
		IsPartOfBreedingProgram = false;
		IsPregnant = false;
		IsTrainedForPerformance = false;
		IsVenomous = false;

		AgeInYears = string.Empty;
		ArmSpanInCentimeters = string.Empty;
		DivingDepthInMeters = string.Empty;
		EggIncubationTemperature = string.Empty;
		JawStrengthPSI = string.Empty;
		JumpHeightInCentimeters = string.Empty;
		LactationPeriodInWeeks = string.Empty;
		TalonsLengthInMillimeters = string.Empty;
		TongueLengthInMillimeters = string.Empty;
		TypicalNumberOfEggsLaid = string.Empty;
		WeightInGrams = string.Empty;
		WingAreaInCm2 = string.Empty;
		VocabularySize = null;
		_personalName = null;

		Gender = null;
		MigratoryPattern = null;
		_dietTypes = DietTypesEnum.None;

		AnimalImage = null;
		SelectedCategory = null;
		SelectedSpecies = null;
		IsListAllSpeciesChecked = false;

		SelectedAnimal = null;
		SelectedFoodSchedule = null;
		OnPropertyChanged(nameof(DietTypes));
		OnPropertyChanged(nameof(PersonalName));
		OnPropertyChanged(nameof(Animals));
		IsAddFoodScheduleToUIButtonEnabled = false;
		IsSaveChangesEnabled = false;
		AddedFoodScheduleEvents = string.Empty;
		AddedFoodScheduleName = string.Empty;
		AddedFoodSchedule = null;
	}

	public void ClearAnimalCollection()
	{
		_animalService.Animals.Clear();
	}

	public void ClearFoodScheduleCollection()
	{
		_foodScheduleService.Items.Clear();
	}

	#endregion

	#region Private methods

	/// <summary>
	/// Resets the UI, empties all fields and selections
	/// </summary>


	/// <summary>
	/// Enables save changes based on wheter saving changes should be allowed,
	/// and on whether the property is changed since it populated the UI.
	/// </summary>
	/// <param name="propertyName">The name of the property that is being checked for changes</param>
	/// <param name="newValue">The new value of the property being checked for changes</param>
	private void SetEnableSaveChanges(string propertyName, object newValue)
	{
		if (!IsSaveChangesEnabled && AllowSaveChanges)
		{
			var initialSelectedAnimalValue = _initialSelectedAnimal!.GetType().GetProperty(propertyName)?.GetValue(_initialSelectedAnimal);

			if (newValue != initialSelectedAnimalValue)
				IsSaveChangesEnabled = true;
		}
	}

	private void InitializeCommands()
	{
		// When FoodSchedulePage is open, clicking inside MainPage activates the FoodSchedulePage
		OnMainPageClickCommand = new Command(() =>
		{
			if (_foodSchedulePage != null)
			{
				Application.Current.ActivateWindow(_foodSchedulePage.Window);
			}
		});

		// If save changes is enabled the method calls animal service
		// to save the changes to the selected animal.
		OnChangeAnimalClickCommand = new Command(() =>
		{
			if (IsSaveChangesEnabled)
			{
				_animalService.Edit(SelectedAnimal!, this);
				IsSaveChangesEnabled = false;
			}
		});

		// Shows file picker to select an image to add to the UI
		OnSelectImageClickCommand = new Command(async () =>
		{
			var result = await FilePicker.PickAsync(new PickOptions
			{
				PickerTitle = "Select an image",
				FileTypes = FilePickerFileType.Images
			});

			if (result == null)
			{
				return;
			}

			var stream = await result.OpenReadAsync();
			AnimalImage = ImageSource.FromStream(() => stream);
		});

		// Add an Animal object based on the PageModel's UI properties to the
		// Animal list by calling the Animal Service. If adding successful,
		// disable saving of changes and clear the UI
		OnAddAnimalClickCommand = new Command(() =>
		{
			if (!_animalService.Add(this))
				return;

			AllowSaveChanges = false;
			ClearUI();
		});

		OnDeleteAnimalClickCommand = new Command(() =>
		{
			_animalService.Delete(SelectedAnimal!);
			OnPropertyChanged(nameof(Animals));
			AllowSaveChanges = false;
			ClearUI();
		});

		// Clears the UI and disables the possibility for save changes to enable.
		OnClearClickCommand = new Command(() =>
		{
			AllowSaveChanges = false;
			ClearUI();
		});

		// Deletes the currently selected animal from the list of animals
		// Notifies UI about the change and clears UI after deletion.
		OnDeleteAnimalClickCommand = new Command(() =>
		{
			_animalService.Delete(SelectedAnimal!);
			OnPropertyChanged(nameof(Animals));
			AllowSaveChanges = false;
			ClearUI();
		});

		// Checks which sort button was clicked and calls service
		// to sort the animal list based on the passed in sorting option
		OnSortAnimalsClickCommand = new Command<object>((obj) =>
		{
			if (obj is SortOption sortOption)
			{
				if (sortOption == SortOption.Name)
				{
					_animalService.SortAnimals(SortOption.Name);
				}

				if (sortOption == SortOption.Species)
				{
					_animalService.SortAnimals(SortOption.Species);
				}
			}
			OnPropertyChanged(nameof(Animals));
		});

		// Removes the image from the UI
		OnRemoveImageClickCommand = new Command(() =>
		{
			AnimalImage = null;
		});


		// Calls service to show all animal info strings
		OnAllAnimalInfoClickCommand = new Command(() =>
		{
			_animalService.ShowAnimalInfoStrings();
		});

		OnCreateFoodScheduleClickCommand = new Command(() =>
		{
			IsMainPageBlocked = true;
			OpenFoodSchedulePage(new FoodSchedulePageModel());
		});

		OnEditFoodScheduleClickCommand = new Command(() =>
		{
			if (!IsEditFoodScheduleButtonEnabled)
				return;
			
			IsMainPageBlocked = true;
			 OpenFoodSchedulePage(new FoodSchedulePageModel(SelectedFoodSchedule));
		});

		OnAddFoodScheduleToUIClickCommand = new Command(() =>
		{
			AddedFoodSchedule = SelectedFoodSchedule;
			IsSaveChangesEnabled = true;
		});

		OnFoodScheduleItemClickedCommand = new Command(() =>
		{
			IsAddFoodScheduleToUIButtonEnabled = true;
		});

		OnTextChangedCommand = new Command<object>(param =>
		{				
			var (sender, e) = ((object, TextChangedEventArgs))param;
			if (sender is Entry entry)
			{
				if (!string.IsNullOrEmpty(entry.Text) && !int.TryParse(e.NewTextValue, out _))
				{
					((Entry)sender).Text = e.OldTextValue;
				}
			}
		});

		OnAnimalItemClickCommand = new Command<object>((obj) =>
		{
			AllowSaveChanges = false;
			if (obj is Animal)
			{
				CreateAnimalSnapshot();
				PopulateGUIFromSelectedAnimal();
			}
		});

		OnFoodScheduleItemClicked = new Command(() =>
		{
			IsAddFoodScheduleToUIButtonEnabled = true;
		});

		OnMenuBarNewClickedCommand = new Command(() =>
		{
			ClearAnimalCollection();
			ClearFoodScheduleCollection();
		});

		OnMenuBarOpenClickedCommand = new Command(() =>
		{

		});
		
		OnMenuBarSaveClickedCommand = new Command(() =>
		{

		});
		
		OnMenuBarSaveAsClickedCommand = new Command(() =>
		{

		});
	}

	/// <summary>
	/// Opens a new instance of the page FoodSchedulePage and passes the associated Page Model,
	/// and sets the Window size.
	/// </summary>
	/// <param name="foodSchedulePageModel">The Page Mode associated with the Page.</param>
	private void OpenFoodSchedulePage(FoodSchedulePageModel foodSchedulePageModel)
	{
		Application.Current.OpenWindow(new Window
		{
			Page = new FoodSchedulePage(foodSchedulePageModel, this),
			Title = "Food Schedule",
			Width = 700,
			Height = 700
		});
	}

	/// <summary>
	/// Registers messages to to listen for.
	/// </summary>
	private void InitializeMessagingCenter()
	{
		// If AddingFoodSchedule was sent, the FoodSchedule object inside the message m is added to the list of FoodSchedule
		// by calling the FoodScheduleService
		WeakReferenceMessenger.Default.Register<AddingFoodScheduleMessage>(this, (r, m) =>
		{
			_foodScheduleService.Add(m.Value);
		});

		// If EditingFoodSchedule was sent, AND the edited FoodSchedule is the same as the currently
		// added FoodSchedule (meaning it is visible in the UI), the changes are reflected in the UI
		// by calling UpdateAddedFoodSchedule()
		WeakReferenceMessenger.Default.Register<EditingFoodScheduleMessage>(this, (r, m) =>
		{
			if (m.Value is FoodSchedule foodSchedule)
			{
				if (foodSchedule == AddedFoodSchedule) //LÄGG INTE IN SCHEDULE OM SCHEDULE-RUTAN ÄR TOM!

					UpdateAddedFoodSchedule();
			}
		});
	}

	#endregion
}