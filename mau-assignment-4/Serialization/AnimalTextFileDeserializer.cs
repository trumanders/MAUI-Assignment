namespace mau_assignment_4.Serialization;

public static class AnimalTextFileDeserializer
{
	static List<string?>? _speciesObjectLines;

	/// <summary>
	/// Reads text file containing animal collection and maps the text to a collection of Animal.
	/// Maps to the different sub classes of Animal.
	/// </summary>
	/// <param name="stream">The text file contents</param>
	/// <returns>Animal collection</returns>
	public static List<Animal> AnimalTextFileDeserializeAsync(Stream stream)
	{
		var animals = new List<Animal>();
		var text = new StreamReader(stream).ReadToEnd();

		foreach (var speciesObjectText in text.Split("Species: "))
		{
			_speciesObjectLines = [.. speciesObjectText.Split("\n")];
			var speciesName = _speciesObjectLines[0];
			var animal = GetAnimalInstanceFromString(speciesName);

			if (animal is not null)
				SetAnimalProperties(animal);
			
			SetCategoryProperties(animal);
			SetSpeciesProperties(animal);

			if (animal is not null)
				animals.Add(animal);
		}

		return animals;
	}

	/// <summary>
	/// Maps a string value to bool value.
	/// </summary>
	/// <param name="propertyRowIndex">The row number in the text file to read the value from</param>
	/// <returns>True if text is "Yes", otherwise false</returns>
	private static bool GetBoolValue(int propertyRowIndex)
	{
		return GetPropertyValueString(propertyRowIndex) == "Yes";
	}

	/// <summary>
	/// Maps string value to integer value.
	/// </summary>
	/// <param name="propertyRowIndex">The row number in the text file to read the value from</param>
	/// <returns>The integer representation of the read string value</returns>
	private static int? GetIntValue(int propertyRowIndex)
	{
		var value = new string([.. GetPropertyValueString(propertyRowIndex).TakeWhile(char.IsDigit)]);
		if (!int.TryParse(value, out int intValue))
			return null;
		return intValue;
	}

	/// <summary>
	/// Maps string value to Enum value.
	/// </summary>
	/// <typeparam name="TEnum">The enum type to convert to</typeparam>
	/// <param name="propertyRowIndex">The row number in the text file to read the value from</param>
	/// <returns>The enum value of the paseed type</returns>
	private static TEnum? GetEnumValue<TEnum>(int propertyRowIndex) where TEnum : struct, Enum
	{
		var propertyValue = GetPropertyValueString(propertyRowIndex);
		if (string.IsNullOrEmpty(propertyValue))
			return null;
		return Enum.Parse<TEnum>(propertyValue);
	}

	/// <summary>
	/// Maps string value to flagged enum value by extracting each enum value from the string
	/// and add them to an enum variable using bitwise operator.
	/// </summary>
	/// <returns>The enum value of type DietTypesEnum</returns>
	private static DietTypesEnum GetDietTypes()
	{
		var dietTypesString = GetPropertyValueString(7);
		DietTypesEnum dietTypes = 0;
		foreach (var type in dietTypesString.Split(", "))
		{
			dietTypes |= (DietTypesEnum)Enum.Parse(typeof(DietTypesEnum), type.Trim());
		}

		return dietTypes;
	}
	static private string? GetPropertyValueString(int speciesObjectLineIndex)
	{
		var propertyLine = _speciesObjectLines[speciesObjectLineIndex];
		var propertyValue = propertyLine.Substring(propertyLine.IndexOf(':') + 2);
		return propertyValue;
	}

	/// <summary>
	/// Creates an instance of base type Animal based on the passed species name 
	/// as a string
	/// </summary>
	/// <param name="speciesName">The string representation of the type to create an instance of</param>
	/// <returns>An instance of base type Animal</returns>
	private static Animal? GetAnimalInstanceFromString(string speciesName)
	{
		if (string.IsNullOrEmpty(speciesName))
			return null;
		 
		var typeName = GetTypeName(speciesName);

		return (Animal?)Activator.CreateInstance(Type.GetType(typeName)) ?? null;
	}

	/// <summary>
	/// Gets the full type name for each species subclass of Animal, including the namespace.
	/// </summary>
	/// <param name="speciesName">The string representation of the species type name</param>
	/// <returns>A string representing the full type name including namepace.</returns>
	private static string GetTypeName(string? speciesName)
	{
		if (speciesName == "Eagle" || speciesName == "Parrot" || speciesName == "Penguin")
			return "mau_assignment_4.Models.Birds." + speciesName;
		if (speciesName == "Bat" || speciesName == "Dolphin" || speciesName == "Gorilla")
			return "mau_assignment_4.Models.Mammals." + speciesName;
		if (speciesName == "Alligator" || speciesName == "Chameleon" || speciesName == "Komodo")
			return "mau_assignment_4.Models.Reptiles." + speciesName;
		return string.Empty;
	}

	/// <summary>
	/// Calls methods to set the properties of animal category subclasses.
	/// </summary>
	/// <param name="animal">The Animal instance to set the properties of.</param>
	private static void SetCategoryProperties(Animal? animal)
	{
		switch (animal)
		{
			case Mammal mammal:
				SetMammalProperties(mammal);
				break;
				case Bird bird:
				SetBirdProperties(bird);
				break;
			case Reptile reptile:
				SetReptileProperties(reptile);
				break;
		}
	}

	/// <summary>
	/// Sets the properties of the animal species subclasses.
	/// </summary>
	/// <param name="animal">The Animal instance to set the properties of.</param>
	private static void SetSpeciesProperties(Animal? animal)
	{
		switch (animal)
		{
			case Bat bat:
				bat.WingAreaInCm2 = GetIntValue(12);
				break;

			case Dolphin dolphin:
				dolphin.JumpHeightInCentimeters = GetIntValue(12) ?? null;
				dolphin.HasUniqueMarkings = GetBoolValue(13);
				break;

			case Gorilla gorilla:
				gorilla.ArmSpanInCentimeters = GetIntValue(12);
				gorilla.IsAlphaMale = GetBoolValue(13);
				break;

			case Eagle eagle:
				eagle.TalonsLengthInMillimeters = GetIntValue(10);
				eagle.HasPermanentInjury = GetBoolValue(11);
				break;

			case Parrot parrot:
				parrot.VocabularySize = GetIntValue(10);
				parrot.IsTrainedForPerformance = GetBoolValue(11);
				break;

			case Penguin penguin:
				penguin.DivingDepthInMeters = GetIntValue(10);
				penguin.IsHandFed = GetBoolValue(11);
				break;

			case Alligator alligator:
				alligator.JawStrengthPSI = GetIntValue(9);

				break;

			case Chameleon chameleon:
				chameleon.TongueLengthInMillimeters = GetIntValue(9);
				chameleon.HasRegrownTail = GetBoolValue(10);
				break;

			case Komodo komodo:
				komodo.IsPartOfBreedingProgram = GetBoolValue(9);
				break;
		}
	}

	/// <summary>
	/// Sets the properties of the base class Animal
	/// </summary>
	/// <param name="animal">The Animal instance to set the properies of</param>
	private static void SetAnimalProperties(Animal? animal)
	{
		animal.Id = GetIntValue(1) ?? 0;
		animal.PersonalName = GetPropertyValueString(2) ?? null;
		animal.AgeInYears = GetIntValue(3) ?? null;
		animal.Gender = GetEnumValue<Gender>(4) ?? null;
		animal.IsVenomous = GetBoolValue(5);
		animal.IsEndangered = GetBoolValue(6);
		animal.DietTypes = GetDietTypes();
	}

	/// <summary>
	/// Sets the properties of the animal category type Mammal
	/// </summary>
	/// <param name="mammal">The Mammal instance to set the properties of.</param>
	private static void SetMammalProperties(Mammal? mammal)
	{
		mammal.WeightInGrams = GetIntValue(8);
		mammal.LactationPeriodInWeeks = GetIntValue(9);
		mammal.IsCurrentlyNursing = GetBoolValue(10);
		mammal.IsPregnant = GetBoolValue(11);
	}

	/// <summary>
	/// Sets the properties of the animal category type Bird
	/// </summary>
	/// <param name="bird">The Bird instance to set the properties of</param>
	private static void SetBirdProperties(Bird? bird)
	{
		bird.MigratoryPattern = GetEnumValue<MigratoryPattern>(8);
		bird.EggIncubationTemperature = GetIntValue(9);
	}

	/// <summary>
	/// Sets the properties of the animal category type Reptile.
	/// </summary>
	/// <param name="reptile">The Reptile instance to set the properties of</param>
	private static void SetReptileProperties(Reptile? reptile)
	{
		reptile.TypicalNumberOfEggsLaid = GetIntValue(8);
	}
}
