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
		static string GetSpecies(Animal? animal) =>
			animal switch
			{
				Eagle eagle => eagle.Species.ToString(),
				Parrot parrot => parrot.Species.ToString(),
				Penguin penguin => penguin.Species.ToString(),
				Alligator alligator => alligator.Species.ToString(),
				Chameleon chameleon => chameleon.Species.ToString(),
				Komodo komodo => komodo.Species.ToString(),
				Bat bat => bat.Species.ToString(),
				Dolphin dolphin => dolphin.Species.ToString(),
				Gorilla gorilla => gorilla.Species.ToString(),
				_ => animal?.Species?.ToString() ?? string.Empty,
			};

		int result = _sortOption switch
		{
			SortOption.Name => string.Compare(a?.PersonalName, b?.PersonalName, StringComparison.Ordinal),
			SortOption.Species => string.Compare(GetSpecies(a), GetSpecies(b), StringComparison.Ordinal),
			_ => 0
		};

		return isReverseOrder ? -result : result;
	}
}


