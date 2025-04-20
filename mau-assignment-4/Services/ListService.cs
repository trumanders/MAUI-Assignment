using CommunityToolkit.Mvvm.ComponentModel;

namespace mau_assignment_4.Services;

public partial class ListService<T> : ObservableObject,	IListService<T>
{
	[ObservableProperty]
	private ObservableCollection<T> _items =[];

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
}
