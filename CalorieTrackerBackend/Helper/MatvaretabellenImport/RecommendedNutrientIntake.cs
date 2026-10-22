namespace CalorieTracker.Helper.MatvaretabellenImport;

using CalorieTracker.Models.Detailed;
using CalorieTracker.DTO.Detailed;

public static class NutrientRecommendationSeed
{
    private static readonly List<NutrientRecommendation> Recommendations = new()
    {
        // https://www.helsedirektoratet.no/rapporter/referanseverdier-for-energi-og-naeringsstoffer/anbefalinger-om-energi-og-naeringsstoffer-ved-planlegging-av-kosthold/vitaminer-og-mineraler#anbefalt-inntak-ri-av-mineraler
        // ==========================================
        // 1. PROXIMATES & MACRO TOTALS Macro nutrient quantity shuld be percentage based
        // ==========================================
        new NutrientRecommendation { NutrientId = "Vann", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 2500, Unit = "g" },
        new NutrientRecommendation { NutrientId = "Vann", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 2000, Unit = "g" },

        new NutrientRecommendation { NutrientId = "Alko", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 0, Unit = "g" },
        new NutrientRecommendation { NutrientId = "Alko", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 0, Unit = "g" },

        new NutrientRecommendation { NutrientId = "Protein", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 90, Unit = "g" },
        new NutrientRecommendation { NutrientId = "Protein", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 70, Unit = "g" },

        new NutrientRecommendation { NutrientId = "Karbo", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 300, Unit = "g" },
        new NutrientRecommendation { NutrientId = "Karbo", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 230, Unit = "g" },

        new NutrientRecommendation { NutrientId = "Fett", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 80, Unit = "g" },
        new NutrientRecommendation { NutrientId = "Fett", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 65, Unit = "g" },

        // ==========================================
        // 2. MACRO MINERALS: Fact checked
        // ==========================================
        new NutrientRecommendation { NutrientId = "Ca", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 950, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Mg", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 350, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Mg", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 300, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "K", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 3500, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "P", MinAge = 18, MaxAge = 24, Gender = "Any", Quantity = 550, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "P", MinAge = 25, MaxAge = 120, Gender = "Any", Quantity = 520, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Na", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 2300, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "NaCl", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 5.75, Unit = "g" },

        // ==========================================
        // 3. TRACE MINERALS: Fact checked
        // ==========================================
        new NutrientRecommendation { NutrientId = "Fe", MinAge = 18, MaxAge = 50, Gender = "Female", Quantity = 15, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Fe", MinAge = 51, MaxAge = 70, Gender = "Female", Quantity = 8, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Fe", MinAge = 71, MaxAge = 120, Gender = "Female", Quantity = 7, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Fe", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 9, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Cu", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 0.9, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "I", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 150, Unit = "µg" }, // Jodd

        new NutrientRecommendation { NutrientId = "Se", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 75, Unit = "µg" },
        new NutrientRecommendation { NutrientId = "Se", MinAge = 18, MaxAge = 70, Gender = "Male", Quantity = 90, Unit = "µg" },
        new NutrientRecommendation { NutrientId = "Se", MinAge = 71, MaxAge = 120, Gender = "Male", Quantity = 85, Unit = "µg" },

        new NutrientRecommendation { NutrientId = "Zn", MinAge = 18, MaxAge = 50, Gender = "Female", Quantity = 9.7, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Zn", MinAge = 51, MaxAge = 70, Gender = "Female", Quantity = 9.5, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Zn", MinAge = 71, MaxAge = 120, Gender = "Female", Quantity = 9.3, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Zn", MinAge = 18, MaxAge = 70, Gender = "Male", Quantity = 12, Unit = "mg" },

        // ==========================================
        // 4. WATER-SOLUBLE VITAMINS Fact checked
        // ==========================================
        new NutrientRecommendation { NutrientId = "Folat", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 330, Unit = "µg" },
        new NutrientRecommendation { NutrientId = "BIOT", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 40, Unit = "µg" },

        new NutrientRecommendation { NutrientId = "Vit B1", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 1.2, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Vit B1", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 1.0, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Vit B2", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 1.6, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Vit B2", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 1.6, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Vit B6", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 1.8, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Vit B6", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 1.6, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Vit B12", MinAge = 18, MaxAge = 120, Gender = "Any", Quantity = 4.0, Unit = "µg" },

        new NutrientRecommendation { NutrientId = "Vit C", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 110, Unit = "mg" },
        new NutrientRecommendation { NutrientId = "Vit C", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 95, Unit = "mg" },

        new NutrientRecommendation { NutrientId = "Niacin", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 18, Unit = "mg NE" },
        new NutrientRecommendation { NutrientId = "Niacin", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 15, Unit = "mg NE" },

        new NutrientRecommendation { NutrientId = "NIAEQ", MinAge = 18, MaxAge = 120, Gender = "Male", Quantity = 18, Unit = "mg NE" },
        new NutrientRecommendation { NutrientId = "NIAEQ", MinAge = 18, MaxAge = 120, Gender = "Female", Quantity = 15, Unit = "mg NE" },
    };

    public static Dictionary<string, NutrientRecommendationDTO> GetRecommendationDTOs(string gender, int age)
    {
        return Recommendations
            .Where(x => x.Gender == gender && age >= x.MinAge && age <= x.MaxAge)
            .ToDictionary(x => x.NutrientId, x =>  new NutrientRecommendationDTO
            {
                NutrientId = x.NutrientId,
                Gender = x.Gender,
                MinAge = x.MinAge,
                MaxAge = x.MaxAge,
                Quantity = x.Quantity,
                Unit = x.Unit,
            });
    }
}
