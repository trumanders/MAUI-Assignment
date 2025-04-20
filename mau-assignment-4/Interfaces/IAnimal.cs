namespace mau_assignment_4.Interfaces;

public interface IAnimal
{
	public int Id { get; set; }
	public string PersonalName { get; set; }
	public Gender? Gender { get; set; }
	public string ToString();
}
