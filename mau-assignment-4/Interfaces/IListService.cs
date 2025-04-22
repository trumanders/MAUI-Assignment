namespace mau_assignment_4.Interfaces;

public interface IListService<T>
{
	public ObservableCollection<T> Items { get; set; }
	public int Count { get; set; }
	public bool Add(T type); // mandatory
	public bool ChangeAt(T type, int index); // mandatory
	public bool CheckIndex(int index);
	public bool DeleteAt(int index);
	public Task SaveJson();
	public Task SaveAsJson();
	public Task OpenJson();
	public Task SaveXml();
	public Task SaveAsXml();
	public Task OpenXml();
}