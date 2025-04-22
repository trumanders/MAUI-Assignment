using CommunityToolkit.Mvvm.ComponentModel;
using mau_assignment_4.Exceptions;
using mau_assignment_4.JsonConverters;
using System.Xml;
using System.Xml.Serialization;


namespace mau_assignment_4.Services;

public partial class ListService<T> : ObservableObject, IListService<T>
{
	[ObservableProperty]
	private ObservableCollection<T> _items = [];
	private JsonSerializerOptions _options = new();

	public static string SaveLocation { get; set; }

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

	public async Task SaveJson()
	{
		if (string.IsNullOrEmpty(SaveLocation))
		{
			SaveAsJson();
		}
		else
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			options.Converters.Add(new AnimalJsonConverter());


			using var stream = new FileStream(
				SaveLocation,
				FileMode.Create,
				FileAccess.Write);

			await JsonSerializer.SerializeAsync(stream, Items, options);
		}
	}


	public async Task SaveAsJson()
	{
		if (typeof(T) != typeof(Animal))
			return;

		var options = new JsonSerializerOptions()
		{
			WriteIndented = true
		};
		options.Converters.Add(new AnimalJsonConverter());

		using var stream = new MemoryStream();

		await JsonSerializer.SerializeAsync(stream, Items, options);

		stream.Position = 0;

		var result = await FileSaver.SaveAsync("My animals.json", stream, CancellationToken.None);
		SaveLocation = result.FilePath;

		if (result.IsSuccessful)
		{
			new AlertService().ShowAlert("Animals saved", "Animal collection successfully saved!", "Ok");
		}
		else
		{
			new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
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
			_options = new JsonSerializerOptions();
			_options.Converters.Add(new AnimalJsonConverter());
			var animals = await JsonSerializer.DeserializeAsync(stream, typeof(List<T>), _options);
			if (animals is List<T> animalCollection)
			{
				Items.Clear();
				foreach (var item in animalCollection)
					Items.Add(item);
			}
		}
		if (result != null)
			SaveLocation = result.FullPath;
	}

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

			if (result != null)
			{
				using var stream = await result.OpenReadAsync();
				var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

				try
				{
					if (serializer.Deserialize(stream) is List<FoodSchedule> schedules)
					{
						Items.Clear();
						foreach (var item in schedules.Cast<T>())
							Items.Add(item);
					}
					SaveLocation = result.FullPath;
				}
				catch (InvalidFoodScheduleXmlException ex)
				{
					throw ex;
				}
				catch (Exception ex)
				{
					throw new InvalidFoodScheduleXmlException(SaveLocation, "Error while reading food schedule xml file.", ex);
				}				
			}
		}
		catch (InvalidFoodScheduleXmlException ex)
		{
			throw ex;
		}
	}

	public async Task SaveXml()
	{
		if (typeof(T) != typeof(FoodSchedule))
			return;

		if (string.IsNullOrEmpty(SaveLocation))
		{
			await SaveAsXml();
		}
		else
		{
			var serializer = new XmlSerializer(typeof(List<FoodSchedule>));

			using var stream = new FileStream(
				SaveLocation,
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
		SaveLocation = result.FilePath;

		if (result.IsSuccessful)
		{
			new AlertService().ShowAlert("Food schedules saved", "Food schedules successfully saved!", "Ok");
		}
		else
		{
			new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
		}
	}
}
