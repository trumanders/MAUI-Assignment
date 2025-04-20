namespace mau_assignment_4.Services;

public class FoodScheduleService : ListService<FoodSchedule>, IFoodScheduleService
{
	public FoodScheduleService()
	{
		InitializeFoodSchedules();		
	}

	/// <summary>
	/// Initializes the food schedules for various animals.
	/// </summary>
	private void InitializeFoodSchedules()
	{
		Add(new FoodSchedule()
		{
			Name = "Komodo",
			FoodScheduleEvents =
			{
				"Morning: Whole prey (rats, rabbits, chickens)",
				"Lunch: Whole rodents, eggs, small goats",
				"Evening: Larger mammals (deer, wild boar), occasional fish"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Chameleon",
			FoodScheduleEvents =
			{
				"Morning: Insects (crickets, roaches, locusts)",
				"Lunch: Leafy greens (dandelion greens, collard greens)",
				"Evening: Small insects, occasional fruit (berries, mangoes)",
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Alligator",
			FoodScheduleEvents =
			{
				"Morning: Whole fish (catfish, carp, tilapia)",
				"Lunch: Chicken or turkey (whole or parts, like wings, necks)",
				"Evening: Occasional mammal meat (rabbits, rodents)"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Gorilla",
			FoodScheduleEvents =
			{
				"Morning: Fresh fruits (bananas, apples, pineapple), leafy greens",
				"Lunch: Vegetables (carrots, corn, yams), nuts, seeds",
				"Evening: Leafy greens (kale, spinach), protein-rich items (tofu, beans)"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Dolphin",
			FoodScheduleEvents =
			{
				"Morning: Fish (sardines, mackerel, anchovies)",
				"Lunch: Squid, shrimp, or other shellfish",
				"Evening: Fish or squid, occasional enrichment feeding with larger prey"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Bat",
			FoodScheduleEvents =
			{
				"Morning: Fruit (bananas, mangoes, berries)",
				"Lunch: Insects (moths, beetles, crickets)",
				"Evening: Mealworms, nectar, and other small fruits"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Penguin",
			FoodScheduleEvents =
			{
				"Morning: Herring and capelin (5-8 whole fish)",
				"Lunch: Vitamins hidden in fish",
				"Evening: Smaller fish (smelt, sardines), optional vitamin supplementation"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Parrot",
			FoodScheduleEvents =
			{
				"Morning: Fresh fruits (bananas, mango, apples) and nuts",
				"Lunch: Vegetables (carrots, leafy greens) and cooked grains",
				"Evening: Pellets and seeds, occasional boiled egg for protein"
			}
		});

		Add(new FoodSchedule()
		{
			Name = "Eagle",
			FoodScheduleEvents =
			{
				"Lunch: Small bone-in meats (chicken necks, rabbit parts)",
				"Evening: Occasional fasting day to mimic natural hunting",
				"Morning: Whole prey (rats, quail, or fish)"
			}
		});
	}
}
