namespace FFPRSaveEditor.Common.Models {
    // Compared to BaseModel.GpsData: Add transportationId property
    public class FF4GpsData : GpsData {
        public int transportationId { get; set; }
        public override int mapId { get; set; }
        public override int areaId { get; set; }
        public override int gpsId { get; set; }
        public override int width { get; set; }
        public override int height { get; set; }
    }

    // Compared to BaseModel.MapData: Add gpsData property
    public class FF4MapData : MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override PlayerEntity playerEntity { get; set; }
        public override string companionEntity { get; set; }
        public FF4GpsData gpsData { get; set; }
        public override int moveCount { get; set; }
        public override int subtractSteps { get; set; }
        public override string telepoCacheData { get; set; }
        public override int playableCharacterCorpsId { get; set; }
        public override string timerData { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class FF4SaveGame : BaseSaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public FF4UserData userData { get; set; }
        public override ConfigData configData { get; set; }
        public override DataStorage dataStorage { get; set; }
        public FF4MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }

    // Compared to BaseModel.UserData: Add totalGil property
    public class FF4UserData : UserData  {
        public override CorpsList corpsList { get; set; }
        public override CorpsSlots corpsSlots { get; set; }
        public override OwnedCharacterList ownedCharacterList { get; set; }
        public override ReleasedJobs releasedJobs { get; set; }
        public override int owendGil { get; set; }
        public override decimal playTime { get; set; }
        public override NormalOwnedItemList normalOwnedItemList { get; set; }
        public override ImportantOwendItemList importantOwendItemList { get; set; }
        public override NormalOwnedItemSortIdList normalOwnedItemSortIdList { get; set; }
        public override string currentArea { get; set; }
        public override string currentLocation { get; set; }
        public override OwnedTransportationList ownedTransportationList { get; set; }
        public override OwendCrystalFlags owendCrystalFlags { get; set; }
        public override string configData { get; set; }
        public override WarehouseItemList warehouseItemList { get; set; }
        public override OwnedKeyWaordList ownedKeyWaordList { get; set; }
        public override OwnedMagicList ownedMagicList { get; set; }
        public override LearnedAbilityList learnedAbilityList { get; set; }
        public override int escapeCount { get; set; }
        public override int battleCount { get; set; }
        public override int corpsSlotIndex { get; set; }
        public override int openChestCount { get; set; }
        public override OwnedMagicStoneList ownedMagicStoneList { get; set; }
        public override int steps { get; set; }
        public override int saveCompleteCount { get; set; }
        public override int monstersKilledCount { get; set; }
        public int totalGil { get; set; }
    }
}
