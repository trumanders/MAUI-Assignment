namespace mau_assignment_4.Interfaces;

public interface IListService<T>
{
	public ObservableCollection<T> Items { get; set; }
	public int Count { get; set; }
	public bool Add(T type); // mandatory

	public bool ChangeAt(T type, int index); // mandatory

	public bool CheckIndex(int index);

	public void DeleteAll();

	public bool DeleteAt(int index);

	public T GetAt(int index);

	public string[] ToStringArray(); // mandatory

	public List<string> ToStringList();

	public Task SaveAsJson();
	public Task OpenFromJson();
	public bool XMLSeralize(string fileName);
}