using mau_assignment_4.Serialization;

namespace mau_assignment_4.Services;

public partial class ListService<T>(ISaveSettings _saveSettings, IAlertService _alertService) : ObservableObject, IListService<T>
{
	[ObservableProperty]
	private ObservableCollection<T> _items = [];
	public ISaveSettings SaveSettings { get; set; }
	public bool IsCollectionSaved { get; set; } = true;


	public string? XmlSaveLocation { get; set; } = null;

	/// <summary>
	/// Gets the number of items in Items.
	/// </summary>
	/// <returns>The number of items in Items.</returns>
	public int Count { get { return _items.Count; } }

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
		IsCollectionSaved = false;
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
		IsCollectionSaved = false;
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
		IsCollectionSaved = false;

		return true;
	}

	public void DeleteAll()
	{
		Items.Clear();
		IsCollectionSaved = false;
	}
	public string[] ToStringArray()
	{
		return [.. Items.Select(x => x?.ToString() ?? string.Empty)];
	}

	public async Task<bool> Save()
	{
		var fileFormat = _saveSettings.SaveFileFormat;
		try
		{
			if (fileFormat == SaveFileFormat.None)
			{
				await _alertService.ShowAlert("No file to save", "Please choose File -> Save as Json / Text file first", "Ok");
				IsCollectionSaved = false;
				return false;
			}

			if (fileFormat == SaveFileFormat.Json)
			{
				await SaveJson();
			}

			if (fileFormat == SaveFileFormat.Txt)
			{
				await SaveTextFile();
			}

			if (fileFormat == SaveFileFormat.Xml)
			{
				await SaveXml();
			}
			IsCollectionSaved = true;
			return true;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error saving file: ", ex);
		}
	}

	public async Task SaveAsJson()
	{
		try
		{
			if (typeof(T) != typeof(Animal))
				return;

			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
				Converters = { new AnimalJsonConverter() }
			};

			using var stream = new MemoryStream();

			await JsonSerializer.SerializeAsync(stream, Items, options);

			stream.Position = 0;

			var result = await FileSaver.SaveAsync("My animals.json", stream, CancellationToken.None);

			if (result.IsSuccessful)
			{
				await new AlertService().ShowAlert("Animals saved", "Animal collection successfully saved!", "Ok");
			}
			else
			{
				await new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
			}
			_saveSettings.SetSaveSettings(SaveFileFormat.Json, result.FilePath);
			IsCollectionSaved = true;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error saving json file.\n\n{ex.Message}");
		}
	}

	public async Task SaveAsTextFile()
	{
		try
		{
			var fileSaver = FileSaver.Default;
			var text = string.Join(Environment.NewLine, ToStringArray());
			var bytes = Encoding.UTF8.GetBytes(text);
			using var stream = new MemoryStream(bytes);
			var result = await fileSaver.SaveAsync("All animals demo.txt", stream, new CancellationToken());
			if (!result.IsSuccessful)
				return;

			_saveSettings.SetSaveSettings(SaveFileFormat.Txt, result.FilePath);
			IsCollectionSaved = true;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error saving text file.\n\n{ex.Message}");
		}
	}

	public async Task SaveAsXml()
	{
		try
		{
			if (typeof(T) != typeof(FoodSchedule))
				return;

			using var stream = new MemoryStream();
			var serializer = new XmlSerializer(typeof(List<FoodSchedule>));
			serializer.Serialize(stream, Items.ToList());
			stream.Position = 0;

			var result = await FileSaver.SaveAsync("My food schedules.xml", stream, CancellationToken.None);

			if (result.IsSuccessful)
			{
				await new AlertService().ShowAlert("Food schedules saved", "Food schedules successfully saved!", "Ok");
			}
			else
			{
				await new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
			}
			XmlSaveLocation = result.FilePath;
			IsCollectionSaved = true;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error saving xml file\n\n{ex.Message}");
		}
	}

	public async Task Open()
	{
		var performOpen = true;
		if (!IsCollectionSaved)
		{
			if (await _alertService.ShowAskSaveChangesInCollection(typeof(T)))
			{
				performOpen = await Save();
			}
		}
		if (!performOpen)
			return;

		try
		{
			if (typeof(T) == typeof(FoodSchedule))
			{
				OpenXml();
				return;
			}

			var result = await FilePicker.Default.PickAsync(new PickOptions
			{
				PickerTitle = "Pick a collection",
				FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.WinUI, new[] { ".json", ".txt" } },
				})
			});

			if (result == null) return;

			using var stream = await result.OpenReadAsync();
			if (stream == null) return;

			if (result.FileName.EndsWith(".txt"))
			{
				await OpenTextFile(stream);
			}

			if (result.FileName.EndsWith(".json"))
			{
				await OpenJson(stream);
			}

			_saveSettings.SaveLocation = result.FullPath;
			IsCollectionSaved = true;
		}
		catch (Exception ex)
		{
			throw new Exception("Error opening file: ", ex);
		}
	}

	public async Task New()
	{
		bool performNew = true;
		if (!IsCollectionSaved)
		{
			if (await _alertService.ShowAskSaveChangesInCollection(typeof(T)))
			{
				performNew = await Save();
			}
		}
		if (performNew)
		{
			DeleteAll();
			_saveSettings.SetSaveSettings(SaveFileFormat.None, saveLocation: null);
			IsCollectionSaved = true;
		}
	}

	private async Task OpenJson(Stream stream)
	{
		try
		{
			var options = new JsonSerializerOptions()
			{
				Converters = { new AnimalJsonConverter() }
			};
			var animals = await JsonSerializer.DeserializeAsync(stream, typeof(List<T>), options);
			if (animals is List<T> animalCollection)
			{
				DeleteAll();
				foreach (var item in animalCollection)
					Items.Add(item);
			}
			_saveSettings.SaveFileFormat = SaveFileFormat.Json;
		}
		catch (Exception ex)
		{
			throw new Exception($"Json file.\n\n{ex.Message}");
		}
	}
	private async Task OpenTextFile(Stream stream)
	{
		try
		{
			DeleteAll();
			var animals = AnimalTextFileDeserializer.AnimalTextFileDeserializeAsync(stream);

			if (animals is List<T> animalCollection)
			{
				foreach (var animal in animalCollection)
					Items.Add(animal);
			}
			_saveSettings.SaveFileFormat = SaveFileFormat.Txt;
		}
		catch (Exception ex)
		{
			throw new Exception($"Text file.\n\n{ex.Message}");
		}
	}

	private async Task OpenXml()
	{
		try
		{
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

			using var stream = await result.OpenReadAsync();
			var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

			if (serializer.Deserialize(stream) is List<FoodSchedule> schedules)
			{
				Items.Clear();
				foreach (var item in schedules.Cast<T>())
					Items.Add(item);
			}
			_saveSettings.SetSaveSettings(SaveFileFormat.Xml, result.FullPath);
		}
		catch (InvalidFoodScheduleXmlException ex)
		{
			throw new InvalidFoodScheduleXmlException(XmlSaveLocation!, $"Food schedule xml file.\n\n{ex.Message}");
		}
	}

	private async Task SaveJson()
	{
		try
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
		catch (Exception ex)
		{
			throw new Exception($"json file.\n\n{ex.Message}");
		}
	}


	private async Task SaveTextFile()
	{
		try
		{
			var text = string.Join(Environment.NewLine, ToStringArray());
			var bytes = Encoding.UTF8.GetBytes(text);

			using var stream = new FileStream(
				_saveSettings.SaveLocation,
				FileMode.Create,
				FileAccess.Write);

			await stream.WriteAsync(bytes);
		}
		catch (Exception ex)
		{
			throw new Exception($"Text file.\n\n{ex.Message}");
		}
	}

	private async Task SaveXml()
	{
		try
		{
			if (typeof(T) != typeof(FoodSchedule))
				return;

			if (string.IsNullOrEmpty(_saveSettings.SaveLocation))
			{
				await SaveAsXml();
			}
			else
			{
				var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

				using var stream = new FileStream(
					_saveSettings.SaveLocation,
					FileMode.Create,
					FileAccess.Write);

				serializer.Serialize(stream, Items.ToList());
			}
		}
		catch (Exception ex)
		{
			throw new Exception($"xml file\n\n{ex.Message}");
		}
	}


}