namespace FFPRSaveEditor.Common.Models.FF4Models {
    // Compared to BaseModel.GpsData: Add transportationId property
    public class GpsData : BaseModels.GpsData {
        public int transportationId { get; set; }
        public override int mapId { get; set; }
        public override int areaId { get; set; }
        public override int gpsId { get; set; }
        public override int width { get; set; }
        public override int height { get; set; }
    }

    // Compared to BaseModel.MapData: Add gpsData property
    public class MapData : BaseModels.MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override BaseModels.PlayerEntity playerEntity { get; set; }
        public override string companionEntity { get; set; }
        public GpsData gpsData { get; set; }
        public override int moveCount { get; set; }
        public override int subtractSteps { get; set; }
        public override string telepoCacheData { get; set; }
        public override int playableCharacterCorpsId { get; set; }
        public override string timerData { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class SaveGame : BaseModels.SaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public UserData userData { get; set; }
        public override BaseModels.ConfigData configData { get; set; }
        public override BaseModels.DataStorage dataStorage { get; set; }
        public MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }

    // Compared to BaseModel.UserData: Add totalGil property
    public class UserData : BaseModels.UserData  {
        public override BaseModels.CorpsList corpsList { get; set; }
        public override BaseModels.CorpsSlots corpsSlots { get; set; }
        public override BaseModels.OwnedCharacterList ownedCharacterList { get; set; }
        public override BaseModels.ReleasedJobs releasedJobs { get; set; }
        public override int owendGil { get; set; }
        public override decimal playTime { get; set; }
        public override BaseModels.NormalOwnedItemList normalOwnedItemList { get; set; }
        public override BaseModels.ImportantOwendItemList importantOwendItemList { get; set; }
        public override BaseModels.NormalOwnedItemSortIdList normalOwnedItemSortIdList { get; set; }
        public override string currentArea { get; set; }
        public override string currentLocation { get; set; }
        public override BaseModels.OwnedTransportationList ownedTransportationList { get; set; }
        public override BaseModels.OwendCrystalFlags owendCrystalFlags { get; set; }
        public override string configData { get; set; }
        public override BaseModels.WarehouseItemList warehouseItemList { get; set; }
        public override BaseModels.OwnedKeyWaordList ownedKeyWaordList { get; set; }
        public override BaseModels.OwnedMagicList ownedMagicList { get; set; }
        public override BaseModels.LearnedAbilityList learnedAbilityList { get; set; }
        public override int escapeCount { get; set; }
        public override int battleCount { get; set; }
        public override int corpsSlotIndex { get; set; }
        public override int openChestCount { get; set; }
        public override BaseModels.OwnedMagicStoneList ownedMagicStoneList { get; set; }
        public override int steps { get; set; }
        public override int saveCompleteCount { get; set; }
        public override int monstersKilledCount { get; set; }
        public int totalGil { get; set; }
    }
}
