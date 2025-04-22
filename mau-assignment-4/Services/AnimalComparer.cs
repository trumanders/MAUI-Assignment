namespace mau_assignment_4.Services;

public class AnimalComparer(SortOption sortOption, bool isReverseOrder) : IComparer<Animal>
{
	private readonly SortOption _sortOption = sortOption;

	/// <summary>
	/// Compares two animals based on the specified sort option
	/// </summary>
	/// <param name="a">The first animal</param>
	/// <param name="b">The second animal</param>
	/// <returns></returns>
	public int Compare(Animal? a, Animal? b)
	{
		int result = _sortOption switch
		{
			SortOption.Name => string.Compare(a?.PersonalName, b?.PersonalName, StringComparison.Ordinal),
			SortOption.Species => string.Compare(a?.Species?.ToString(), b?.Species?.ToString(), StringComparison.Ordinal),
			_ => 0
		};

		return isReverseOrder ? -result : result;
	}
}


