using CommunityToolkit.Mvvm.ComponentModel;
using mau_assignment_4.JsonConverters;


namespace mau_assignment_4.Services;

public partial class ListService<T> : ObservableObject, IListService<T>
{
	[ObservableProperty]
	private ObservableCollection<T> _items = [];
	private JsonSerializerOptions _options = new();

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
	public void DeleteAll()
	{
		throw new NotImplementedException();
	}

	public T GetAt(int index)
	{
		throw new NotImplementedException();
	}

	public string[] ToStringArray()
	{
		throw new NotImplementedException();
	}

	public List<string> ToStringList()
	{
		throw new NotImplementedException();
	}

	public async Task SaveAsJson()
	{
		var options = new JsonSerializerOptions()
		{
			WriteIndented = true
		};
		options.Converters.Add(new AnimalJsonConverter());

		using var stream = new MemoryStream();

		await JsonSerializer.SerializeAsync(stream, _items, options);
		stream.Position = 0;

		var result = await FileSaver.SaveAsync("My animals.json", stream, CancellationToken.None);

		if (result.IsSuccessful)
		{
			new AlertService().ShowAlert("Animals saved", "Animal collection successfully saved!", "Ok");
		}
		else
		{
			new AlertService().ShowAlert("Saving failed", result.Exception?.Message, "Ok");
		}
	}

	public async Task OpenFromJson()
	{
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
			var animals = await JsonSerializer.DeserializeAsync(stream,typeof(List<T>), _options);
			if (animals is List<T> animalCollection)
			{
				_items.Clear();
				foreach (var item in animalCollection)
					_items.Add(item);
			}
		}
	}

	public bool XMLSeralize(string fileName)
	{
		throw new NotImplementedException();
	}
}
