using CalorieTracker.Models.Detailed;


namespace CalorieTracker.Helper.MatvaretabellenImport;

public static class GetNutrientCategory
{
    public static NutrientCategory GetCategory(string nutrientId)
    {
        return nutrientId switch
        {
            // Proximates / General
            "Vann" or "Alko" => NutrientCategory.Proximates,
            "Fett" or "Karbo" or "Protein" => NutrientCategory.MacroTotals,

            // Minerals & Trace Elements
            "Ca" or "Mg" or "K" or "P" or "Na" or "NaCl" => NutrientCategory.MacroMineral,
            "Fe" or "Cu" or "I" or "Se" or "Zn" => NutrientCategory.TraceMineral,

            // Water-Soluble Vitamins & B-Complex
            "Folat" or "BIOT" or "Vit B1" or "Vit B2" or "Vit B6" or "Vit B12" or "Vit C" or "Niacin" or "NIAEQ" => NutrientCategory.WaterSolubleVitamin,

            // Fat-Soluble Vitamins & Carotenoids (Including MK4-MK13 variants)
            "B-karo" or "CAROTENS" or "CARTA" or "CRYPXA" or "CRYPXB"
                or "CHOCAL" or "CHOCALOH" or "ERGCAL" or "ERGESTR" or "ERGSTROH"
                or "Retinol" or "Vit A" or "Vit A RE" or "Vit D" or "Vit E"
                or "MK4" or "MK5" or "MK6" or "MK7" or "MK8" or "MK9"
                or "MK10" or "MK11" or "MK12" or "MK13" => NutrientCategory.FatSolubleVitamin,

            // Lipids, Fatty Acids & Sterols
            var id when id.StartsWith("C1") || id.StartsWith("C2")
                || id == "Mettet" || id == "Enumet" || id == "Flerum"
                || id == "Trans" || id == "Kolest" || id == "LACS"
                || id == "Omega-3" || id == "Omega-6" => NutrientCategory.LipidProfile,

            // Carbohydrates, Sugars & Fibers
            "Fiber" or "Stivel" or "SUCS" or "SUGAN" or "Sukker"
                or "Mono+Di" or "FRUS" or "GALS" or "GLUS" or "ISOMALT"
                or "MALS" or "MANTL" or "POLYLP" or "POLY" => NutrientCategory.CarbProfile,

            // Catch-all if any remain
            _ => NutrientCategory.Other

        };
    }
}
