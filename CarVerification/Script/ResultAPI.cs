using System.Collections.Generic;

namespace ApiResult
{

    public partial class Welcome
    {
        public Result Result { get; set; }
        public object TargetUrl { get; set; }
        public bool Success { get; set; }
        public object Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool Abp { get; set; }
    }

    public partial class Result
    {
        public List<Product> Products { get; set; }
    }

    public partial class Product
    {
        public string Id { get; set; }
        public LanguageCodeEnum LanguageCodeEnum { get; set; }
        public string ProductId { get; set; }
        public object ProductionTypeEnum { get; set; }
        public object ProductMappingKey { get; set; }
        public ConsumerTypeEnum ConsumerTypeEnum { get; set; }
        public ModelGroupEnum ModelGroupEnum { get; set; }
        public ModelGroupDisplayName ModelGroupDisplayName { get; set; }
        public string ModelEnum { get; set; }
        public string ModelDisplayName { get; set; }
        public long ModelDisplayOrder { get; set; }
        public string ModelMappingKey { get; set; }
        public long ModelYearEnum { get; set; }
        public long ModelYearDisplayName { get; set; }
        public long ModelYearDisplayOrder { get; set; }
        public long ModelYearMappingKey { get; set; }
        public string ModelYearSlogan { get; set; }
        public string ModelYearDescription { get; set; }
        public string TrimEnum { get; set; }
        public string TrimDisplayName { get; set; }
        public long TrimDisplayOrder { get; set; }
        public string TrimMappingKey { get; set; }
        public string VariantEnum { get; set; }
        public string VariantDisplayName { get; set; }
        public string VariantLongDisplayName { get; set; }
        public long VariantDisplayOrder { get; set; }
        public string VariantMappingKey { get; set; }
        public bool IsModelRepresentative { get; set; }
        public bool IsTrimRepresentative { get; set; }
        public bool IsRepresentative { get; set; }
        public bool IsElectric { get; set; }
        public bool IsHybrid { get; set; }
        public bool IsEco { get; set; }
        public bool IsPrebooking { get; set; }
        public bool IsPriceConfirmed { get; set; }
        public object ModelRepresentativeExteriorColorCode { get; set; }
        public object TrimRepresentativeExteriorColorCode { get; set; }
        public object VariantRepresentativeExteriorColorCode { get; set; }
        public List<FscDependency> FscDependencies { get; set; }
        public object ModelPopups { get; set; }
        public List<object> ModelBadges { get; set; }
        public List<object> VariantBadges { get; set; }
        public List<object> AdditionalFields { get; set; }
        public object CoeVehicleCategoryCode { get; set; }
        public object CoeVehicleCategoryDisplayName { get; set; }
        public bool IsActive { get; set; }
    }

    public partial class FscDependency
    {
        public string Fsc { get; set; }
        public string FscMappingKey { get; set; }
        public double Msrp { get; set; }
        public string MsrpDisclaimer { get; set; }
        public bool IsTrimRepresentativeFsc { get; set; }
        public bool IsRepresentative { get; set; }
        public List<object> PriceItems { get; set; }
        public List<object> ExtendedWarrantyItems { get; set; }
        public List<AccessoryItem> AccessoryItems { get; set; }
        public List<SpecOption> SpecOptions { get; set; }
        public List<ColorCombination> ColorCombinations { get; set; }
        public List<Spec> Specs { get; set; }
        public List<Spec> CompareSpecs { get; set; }
        public object PromotionItems { get; set; }
        public List<string> KspItemIds { get; set; }
    }

    public partial class AccessoryItem
    {
        public string Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemMappingKey { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }
        public bool Has3DContent { get; set; }
        public List<Category> Categories { get; set; }
        public DependencyGroup DependencyGroup { get; set; }
        public List<PriceItem> PriceItems { get; set; }
    }

    public partial class DependencyGroup
    {
        public string GroupId { get; set; }
    }

    public partial class PriceItem
    {
        public object PriceItemTypeEnum { get; set; }
        public object PriceItemCode { get; set; }
        public PriceItemName PriceItemName { get; set; }
        public double PriceItemValue { get; set; }
    }

    public partial class ColorCombination
    {
        public string ColorCombinationId { get; set; }
        public List<Color> Colors { get; set; }
        public bool IsActive { get; set; }
    }

    public partial class Color
    {
        public Category ColorTypeEnum { get; set; }
        public object ColorTypeDisplayName { get; set; }
        public string ColorId { get; set; }
        public string ColorCode { get; set; }
        public string ColorMappingKey { get; set; }
        public string ColorDisplayName { get; set; }
        public long ColorPrice { get; set; }
        public ColorAdditionalTypeEnum ColorAdditionalTypeEnum { get; set; }
        public ColorAdditionalTypeDisplayName ColorAdditionalTypeDisplayName { get; set; }
        public bool IsHot { get; set; }
    }

    public partial class Spec
    {
        public CategoryType CategoryTypeEnum { get; set; }
        public CategoryType CategoryTypeDisplayName { get; set; }
        public List<SpecItem> SpecItems { get; set; }
    }

    public partial class SpecItem
    {
        public string Id { get; set; }
        public SpecCode SpecCode { get; set; }
        public string SpecMappingKey { get; set; }
        public string SpecTitle { get; set; }
        public string SpecDescription { get; set; }
        public List<AdditionalField> AdditionalFields { get; set; }
    }

    public partial class AdditionalField
    {
        public AdditionalFieldName AdditionalFieldName { get; set; }
        public AdditionalFieldValue AdditionalFieldValue { get; set; }
    }

    public partial class SpecOption
    {
        public CategoryTypeEnum CategoryTypeEnum { get; set; }
        public CategoryTypeDisplayName CategoryTypeDisplayName { get; set; }
        public List<SpecOptionItem> SpecOptionItems { get; set; }
    }

    public partial class SpecOptionItem
    {
        public string Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemMappingKey { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public long ItemPrice { get; set; }
    }

    public enum ConsumerTypeEnum { Candidate };

    public enum Category { Comfort, Exterior, Interior, Roofracksandlifestyle, Safety, SportsStyling, Style, Styling, Technology };

    public enum PriceItemName { ItemPriceExcludingFitment };

    public enum ColorAdditionalTypeDisplayName { AlcantaraLeather, Cloth, ClothLeatherAppointed, ClothSeats, Leather, LeatherAppointed, MatteMatte, Metallic, MetallicPremium, Mica, MicaMetallic, MicaPremium, NappaLeatherAppointed, RedStitchingAndPiping, Solid, SolidStandard, SuedeEffectLeatherAppointed, Suedeandnappaleather };

    public enum ColorAdditionalTypeEnum { AlcantaraLeather, Cloth, ClothandLeatherAppointed, Clothseats, ColorAdditionalTypeEnumCloth, Leather, Leatherappointed, Mattematte, Metallic, Metallicpremium, Mica, Micametallic, Micapremium, Nappaleatherappointed, Redstitchingandpiping, Solid, Solidstandard, SuedeEffectLeatherAppointed, Suedeandnappaleather };

    public enum CategoryType { Convenience, Dimensions, Exterior, Fuel, Interior, Performance, Safety, Specification };

    public enum AdditionalFieldName { DisplayTypeEnum };

    public enum AdditionalFieldValue { CheckType, DescriptionType };

    public enum SpecCode { Abs, Ac, Airbag, Ambient, BatteryCapacity, Bluetooth, Bsd, Bsm, Bumper, CargoSpace, Cluster, Cruise, Cupholder, Daw, Defog, DimensionsHeight, DimensionsLength, DimensionsWheelbase, DimensionsWidth, Drivemode, Drivetrain, Drl, Dusksensing, ElectricEfficiency, ElectricRange, Engine, EngineDisplacement, EnginePower, EngineTorque, EngineType, Epb, Esc, Fca, Foglamp, FuelCapacity, FuelCo2, FuelConsumption, FuelType, Glovebox, Grille, Hac, Headlamp, HeadlampDrl, Hmsl, Hud, Key, Lfa, Lka, LuggageNet, Maplamp, Mdps, Media, Mudguard, Muffler, Paddleshifter, Parkingsensor, PositioningLight, Rainsensor, Rearlamp, Rearspolier, Roa, Roofrack, Sbw, Sea, SeatCapacity, SeatOption, SeatType, SidemirrorColor, SidemirrorFolding, SidemirrorHeated, SidemirrorPuddle, SidemirrorRepeater, Speaker, SpecCodeSeatCapacity, Steercontrol, Sunroof, Tpms, Transmission, TrunkOption, Twotone, TyreSize, Usb, Wheel, WheelSize, WheelType, Wiper, Wireless };

    public enum CategoryTypeDisplayName { Anthena, BeltLine, BodyType, DoorGarnish, DoorHandle, DoorSill, Drivetrain, Drl, Engine, FrontBumper, FrontFogLamp, FrontMudGuard, FrontParkingSensor, FrontSkidPlate, FrontWiper, Fuel, Grille, HeadLamp, Hmsl, Muffler, RearBumper, RearCamera, RearEmblem, RearFogLamp, RearLamp, RearMudGuard, RearParkingSensor, RearSkidPlate, RearSpoiler, RearWiper, RoofRails, Seater, SideMirrorColor, SideMirrorType, SkidPlate, Sunroof, The2NdWindowType, Transmission, TwoToneRoof, Wheel, Wiper };

    public enum CategoryTypeEnum { Anthena, Beltline, Bodytype, BumperFront, BumperRear, CamRear, CategoryTypeEnumHmsl, DoorGanish, DoorHandle, DoorSill, Drivetrain, Drl, EmblemRear, Engine, FoglampFront, FoglampRear, Fuel, Grille, Headlamp, Hmsl, MudguardFront, MudguardRear, Muffler, ParkingsensorFront, ParkingsensorRear, Rearlamp, Rearspoiler, Roofrack, Seater, SidemirrorColor, SidemirrorType, Skidplate, SkidplateFront, SkidplateRear, Sunroof, The2Ndwinows, Transmission, Twotone, Wheel, Wiper, WiperFront, WiperRear };

    public enum LanguageCodeEnum { EnUs };

    public enum ModelGroupDisplayName { Commercial, CompactHatchback, Hybrid, Peoplemover, Sedan, Suv };

    public enum ModelGroupEnum { Commercial, Compacthatchback, Hybrid, Peoplemover, Sedan, Suv };
}
