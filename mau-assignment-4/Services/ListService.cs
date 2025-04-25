namespace mau_assignment_4.Services;

public partial class ListService<T>(ISaveSettings _saveSettings) : ObservableObject, IListService<T>
{
	[ObservableProperty]
	private ObservableCollection<T> _items = [];
	protected ISaveSettings SaveSettings
	{
		get => _saveSettings;
	}

	public string? XmlSaveLocation { get; set; } = null;

	public int Count
	{
		get => throw new NotImplementedException();
		set => throw new NotImplementedException();
	}

	/// <summary>
	/// Adds the passed object to the list.
	/// </summary>
	/// <param name="objectToAdd">The object that as added</param>
	/// <returns>False if object to add is null, the Items collection is null, or if
	/// the object to add is already in the Items collection.
	/// </returns>
	public bool Add(T objectToAdd)
	{
		if (objectToAdd == null || Items == null || Items.Contains(objectToAdd))
			return false;

		Items.Add(objectToAdd);
		return true;
	}

	/// <summary>
	/// Changes the object at the specified index to the new object.
	/// </summary>
	/// <param name="newItem">The objecct to replace the old object</param>
	/// <param name="index">The index of the object being changed</param>
	/// <returns></returns>
	public bool ChangeAt(T newItem, int index)
	{
		if (index < 0 || index >= Items.Count)
			return false;

		Items[index] = newItem;
		return true;
	}

	/// <summary>
	/// Get an item in the list by index
	/// </summary>
	/// <param name="index">The index to get the item from</param>
	/// <returns>The item at the specified item, or default if index does not exist</returns>
	public T GetAt(int index) // Not needed
	{
		var item = CheckIndex(index) ? Items[index] : default;
		return item!;
	}

	/// <summary>
	/// Checks if the index is valid for the Items collection.
	/// </summary>
	/// <param name="index">The index being checked</param>
	/// <returns>True is index in within the collection, otherwise false</returns>
	public bool CheckIndex(int index)
	{
		return index >= 0 && index < Items.Count;
	}

	/// <summary>
	/// Deletes the object at the specified index.
	/// </summary>
	/// <param name="index">The index of the object being deleted</param>
	/// <returns>False if index is out of bounds, otherwise true</returns>
	public bool DeleteAt(int index)
	{
		if (!CheckIndex(index))
			return false;

		Items.RemoveAt(index);

		return true;
	}

	public void DeleteAll()
	{
		Items.Clear();
	}
	public string[] ToStringArray()
	{
		return [.. Items.Select(x => x?.ToString() ?? string.Empty)];
	}

	public async Task SaveJson()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			Converters = { new AnimalJsonConverter() }
		};

		using var stream = new FileStream(
			_saveSettings.SaveLocation,
			FileMode.Create,
			FileAccess.Write);

		await JsonSerializer.SerializeAsync(stream, Items, options);
	}

	public async Task SaveAsJson()
	{
		if (typeof(T) != typeof(Animal))
			return;

		_saveSettings.SaveFileFormat = SaveFileFormat.Json;

		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			Converters = { new AnimalJsonConverter() }
		};

		using var stream = new MemoryStream();

		await JsonSerializer.SerializeAsync(stream, Items, options);

		stream.Position = 0;

		var result = await FileSaver.SaveAsync("My animals.json", stream, CancellationToken.None);
		_saveSettings.SaveLocation = result.FilePath;

		if (result.IsSuccessful)
		{
			await new AlertService().ShowAlert("Animals saved", "Animal collection successfully saved!", "Ok");
		}
		else
		{
			await new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
		}
	}

	public async Task OpenJson()
	{
		if (typeof(T) != typeof(Animal))
			return;

		var result = await FilePicker.Default.PickAsync(new PickOptions
		{
			PickerTitle = "Pick a JSON file",
			FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{ DevicePlatform.WinUI, new[] { ".json" } },
				{ DevicePlatform.MacCatalyst, new[] { ".json" } },
				{ DevicePlatform.iOS, new[] { "public.json" } },
				{ DevicePlatform.Android, new[] { "application/json" } }
			})
		});

		if (result != null)
		{
			using var stream = await result.OpenReadAsync();
			var options = new JsonSerializerOptions()
			{
				Converters = { new AnimalJsonConverter() }
			};

			var animals = await JsonSerializer.DeserializeAsync(stream, typeof(List<T>), options);
			if (animals is List<T> animalCollection)
			{
				Items.Clear();
				foreach (var item in animalCollection)
					Items.Add(item);
			}
		}
		if (result != null)
			_saveSettings.SaveLocation = result.FullPath;
	}

	public async Task SaveAsTextFile()
	{		
		var fileSaver = FileSaver.Default;
		var text = string.Join(Environment.NewLine, ToStringArray());
		var bytes = Encoding.UTF8.GetBytes(text);
		using var stream = new MemoryStream(bytes);
		var result = await fileSaver.SaveAsync("All animals demo.txt", stream, new CancellationToken());
		if (!result.IsSuccessful)
			return;

		_saveSettings.SaveFileFormat = SaveFileFormat.Txt;
		_saveSettings.SaveLocation = result.FilePath;
	}

	//public Task SaveTextFile()
	//{
	//	var 
	//}

	public async Task OpenXml()
	{
		try
		{
			if (typeof(T) != typeof(FoodSchedule))
				return;

			var result = await FilePicker.Default.PickAsync(new PickOptions
			{
				PickerTitle = "Pick an XML file",
				FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.WinUI, new[] { ".xml" } },
					{ DevicePlatform.MacCatalyst, new[] { ".xml" } },
					{ DevicePlatform.iOS, new[] { "public.xml" } },
					{ DevicePlatform.Android, new[] { "application/xml" } }
				})
			});

			if (result == null) return;

			XmlSaveLocation = result.FullPath;
			using var stream = await result.OpenReadAsync();
			var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

			if (serializer.Deserialize(stream) is List<FoodSchedule> schedules)
			{
				Items.Clear();
				foreach (var item in schedules.Cast<T>())
					Items.Add(item);
			}
		}
		catch (InvalidFoodScheduleXmlException ex)
		{
			throw new InvalidFoodScheduleXmlException(XmlSaveLocation!, "Error while reading food schedule xml file.", ex);
		}
	}

	public async Task SaveXml()
	{
		if (typeof(T) != typeof(FoodSchedule))
			return;

		if (string.IsNullOrEmpty(XmlSaveLocation))
		{
			await SaveAsXml();
		}
		else
		{
			var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

			using var stream = new FileStream(
				XmlSaveLocation,
				FileMode.Create,
				FileAccess.Write);

			serializer.Serialize(stream, Items.ToList());
		}
	}


	public async Task SaveAsXml()
	{
		if (typeof(T) != typeof(FoodSchedule))
			return;

		using var stream = new MemoryStream();
		var serializer = new XmlSerializer(typeof(List<FoodSchedule>));
		serializer.Serialize(stream, Items.ToList());
		stream.Position = 0;

		var result = await FileSaver.SaveAsync("My food schedules.xml", stream, CancellationToken.None);
		XmlSaveLocation = result.FilePath;

		if (result.IsSuccessful)
		{
			await new AlertService().ShowAlert("Food schedules saved", "Food schedules successfully saved!", "Ok");
		}
		else
		{
			await new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
		}
	}
}
