namespace mau_assignment_4.PageModels;

/// <summary>
/// This class is used to manage the state of the FoodSchedulePage.
/// </summary>
public partial class FoodSchedulePageModel : INotifyPropertyChanged
{
	private readonly FoodSchedule _foodSchedule;
	private TaskCompletionSource<bool>? _taskCompletionSource;

	private string? _foodScheduleName;
	private string? _selectedScheduleEvent;
	private string? _scheduleEvent;
	private bool _isAddButtonEnabled;
	private bool _isChangeButtonEnabled;
	private bool _isDeleteButtonEnabled;
	private bool _isSaveButtonEnabled;
	private bool _isEditing = false;

	public ObservableCollection<string> FoodScheduleEvents { get; set; } = [];

	/// <summary>
	/// Keeps track of whether the page is in edit mode or not.
	/// </summary>
	public bool IsEditing
	{
		get => _isEditing;
		set
		{
			if (_isEditing != value)
			{
				_isEditing = value;
				OnPropertyChanged(nameof(IsEditing));
				OnPropertyChanged(nameof(Header));
			}
		}
	}

	/// <summary>
	/// The header of the page. If the page is in edit mode, the header will be "Edit Food Schedule". Otherwise, it will be "Add Food Schedule".
	/// </summary>
	public string Header => _isEditing ? "Edit Food Schedule" : "Add Food Schedule";
	public bool IsAddButtonEnabled
	{
		get => _isAddButtonEnabled;
		set
		{
			_isAddButtonEnabled = value;
			OnPropertyChanged(nameof(IsAddButtonEnabled));
		}
	}

	/// <summary>
	/// The state of the change button. If the button is enabled, the user can change a schedule event in the list of schedule events
	/// to the new schedule event entered in the entry for schedule event.
	/// </summary>
	public bool IsChangeButtonEnabled
	{
		get => _isChangeButtonEnabled;
		set
		{
			_isChangeButtonEnabled = value;
			OnPropertyChanged(nameof(IsChangeButtonEnabled));
		}
	}

	/// <summary>
	/// The state of the delete button. If the button is enabled, the user can delete a schedule event from the list of schedule events.
	/// </summary>
	public bool IsDeleteButtonEnabled
	{
		get => _isDeleteButtonEnabled;
		set
		{
			_isDeleteButtonEnabled = value;
			OnPropertyChanged(nameof(IsDeleteButtonEnabled));
		}
	}

	/// <summary>
	/// The state of the save button. If the button is enabled, the user can save the food schedule.
	/// </summary>
	public bool IsSaveButtonEnabled
	{
		get => _isSaveButtonEnabled;
		set
		{
			_isSaveButtonEnabled = value;
			OnPropertyChanged(nameof(IsSaveButtonEnabled));
		}
	}

	/// <summary>
	/// The text that is entered in the entry for food schedule name.
	/// </summary>
	public string? FoodScheduleName
	{
		get => _foodScheduleName;
		set
		{
			if (_foodScheduleName != value)
			{
				_foodScheduleName = value;
				OnPropertyChanged(nameof(FoodScheduleName));
			}
		}
	}

	/// <summary>
	/// The text that is entered in the entry for schedule event.
	/// </summary>
	public string? ScheduleEvent
	{
		get => _scheduleEvent;
		set
		{
			if (_scheduleEvent != value)
			{
				_scheduleEvent = value;
				OnPropertyChanged(nameof(ScheduleEvent));
			}
		}
	}

	/// <summary>
	/// The schedule event currently selected in the list of schedule events.
	/// </summary>
	public string? SelectedScheduleEvent
	{
		get => _selectedScheduleEvent;
		set
		{
			ScheduleEvent = value;
			if (_selectedScheduleEvent != value)
			{
				_selectedScheduleEvent = value;
				OnPropertyChanged(nameof(SelectedScheduleEvent));
			}
		}
	}

	public FoodSchedulePageModel(FoodSchedule? foodSchedule = null)
	{
		_isEditing = foodSchedule != null;
		_foodSchedule = foodSchedule ?? new();

		InitializeCommands();
		MapSelectedFoodScheduleToUI();
	}

	public ICommand? OnAddButtonClickCommand { get; set; }
	public ICommand? OnChangeButtonClickCommand { get; set; }
	public ICommand? OnDeleteButtonClickCommand { get; set; }
	public ICommand? OnSaveButtonClickCommand { get; set; }


	public ICommand? OnEventListItemClickedCommand { get; set; }
	public ICommand? OnEventEntryChangedCommand { get; set; }
	public ICommand? OnNameEntryChangedCommand { get; set; }
	public ICommand? OnFoodScheduleItemClickCommand { get; set; }

	public Task<bool> ShowDialogueAsync()
	{
		_taskCompletionSource = new TaskCompletionSource<bool>();
		return _taskCompletionSource.Task;
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


	/// <summary>
	/// Populates the FoodSchedulePage - UI with the properties from the 
	/// selected FoodSchedule object. If FoodSchedule is null, the UI will be
	/// empty.
	/// </summary>
	private void MapSelectedFoodScheduleToUI()
	{
		FoodScheduleName = _foodSchedule.Name;
		FoodScheduleEvents = _foodSchedule.FoodScheduleEvents == null
			? []
			: [.. _foodSchedule.FoodScheduleEvents];
	}

	/// <summary>
	/// Initializes all the commands declared in the class, using
	/// lambda for direct implementation.
	/// </summary>
	private void InitializeCommands()
	{
		// When an item in the list of schedule events is clicked - set
		// the appropriate state for the buttons.
		OnEventListItemClickedCommand = new Command<object>((obj) =>
		{
			if (obj != null && obj is ItemTappedEventArgs e && e.Item != null && e.Item is string)
			{
				IsAddButtonEnabled = false;
				IsDeleteButtonEnabled = true;
				IsChangeButtonEnabled = false;
				IsSaveButtonEnabled = false;				
			}
		});

		// When the text in the entry for schedule event is changed, set the
		// appropriate state for the buttons.
		OnEventEntryChangedCommand = new Command(() =>
		{
			if (!string.IsNullOrEmpty(ScheduleEvent))
			{
				IsAddButtonEnabled = true;
				IsChangeButtonEnabled = true;
			}
		});

		// When the text in the entry for food schedule name is changed, set the
		// appropriate state for the buttons.
		OnNameEntryChangedCommand = new Command(() =>
		{
			if (!string.IsNullOrEmpty(FoodScheduleName))
			{
				IsAddButtonEnabled = true;
				IsChangeButtonEnabled = true;
				IsSaveButtonEnabled = true;
			}
		});

		// When the add-button is clicked, check whether the button is enabled, and if so,
		// add the schedule event to the list of schedule events, and set the
		// appropriate state for the buttons.
		OnAddButtonClickCommand = new Command(() =>
		{
			if (IsAddButtonEnabled) 
			{
				FoodScheduleEvents.Add(ScheduleEvent!);
				IsSaveButtonEnabled = true;
			}
		});

		// When the change-button is clicked, if the button is enabled, and the event entry is not empty,
		// and the selected schedule event is in the list of schedule events, change the selected schedule event
		// to the new schedule event entered in the schedule event entry.
		OnChangeButtonClickCommand = new Command(() =>
		{
			if (IsChangeButtonEnabled)
			{
				if (!string.IsNullOrEmpty(ScheduleEvent) && FoodScheduleEvents.IndexOf(SelectedScheduleEvent!) >= 0)
				{
					FoodScheduleEvents[FoodScheduleEvents.IndexOf(SelectedScheduleEvent!)] = ScheduleEvent;
					IsDeleteButtonEnabled = false;
					IsAddButtonEnabled = false;
					IsChangeButtonEnabled = false;
					IsSaveButtonEnabled = true;
				}
			}
		});

		// When the delete-button is clicked, if the button is enabled, remove the selected schedule event from the list of schedule events.
		OnDeleteButtonClickCommand = new Command(() =>
		{
			if (IsDeleteButtonEnabled)
				FoodScheduleEvents.Remove(SelectedScheduleEvent!);
		});

		// When the save button is clicked, if the button is enabled, set the the food schedeule object's name and
		// list of schedule events to the values in the UI, and send the appropriate message to the messenger with the food schedule object.
		OnSaveButtonClickCommand = new Command(() =>
		{
			if (IsSaveButtonEnabled)
			{
				_foodSchedule.Name = FoodScheduleName!;
				_foodSchedule.FoodScheduleEvents = [.. FoodScheduleEvents];

				if (_isEditing)
				{
					WeakReferenceMessenger.Default.Send(new EditingFoodScheduleMessage(_foodSchedule));
				}
				else
				{
					WeakReferenceMessenger.Default.Send(new AddingFoodScheduleMessage(_foodSchedule));
				}
			}
			IsSaveButtonEnabled = false;
		});
	}
}

/// <summary>
/// This class is used to send a message to the messenger when a food schedule is added.
/// </summary>
public class AddingFoodScheduleMessage : ValueChangedMessage<FoodSchedule>
{
	public AddingFoodScheduleMessage(FoodSchedule foodSchedule) : base(foodSchedule)
	{
	}
}

/// <summary>
/// This class is used to send a message to the messenger when a food schedule is edited.
/// </summary>
public class EditingFoodScheduleMessage : ValueChangedMessage<FoodSchedule>
{
	public EditingFoodScheduleMessage(FoodSchedule foodSchedule) : base(foodSchedule)
	{
	}
}




