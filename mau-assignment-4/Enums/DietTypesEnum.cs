namespace mau_assignment_4.Enums;

[Flags]
public enum DietTypesEnum
{
	None = 0,
	Herbivore = 1,
	Carnivore = 2,
	Omnivore = 4,
	Insectivore = 8,
	Frugivore = 16,
	Piscivore = 32,
	Nectarivore = 64,
	Folivore = 128
}
