namespace mau_assignment_4.Interfaces;

public interface IListService<T>
{
	public ObservableCollection<T> Items { get; set; }
	public int Count { get; set; }
	public bool Add(T type);
	public bool ChangeAt(T type, int index);
	public bool CheckIndex(int index);
	public bool DeleteAt(int index);
	public void DeleteAll();
	public Task Open();
	public Task Save();
	public Task SaveAsJson();
	public Task SaveAsTextFile();
	public Task SaveAsXml();
}