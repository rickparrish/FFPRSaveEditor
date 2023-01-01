namespace FFPRSaveEditor.Common.Models {
    // Compared to BaseModel.MapData: Add gpsData and viewType properties
    public class FF5MapData : MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override PlayerEntity playerEntity { get; set; }
        public override string companionEntity { get; set; }
        public GpsData gpsData { get; set; }
        public override int moveCount { get; set; }
        public override int subtractSteps { get; set; }
        public override string telepoCacheData { get; set; }
        public override int playableCharacterCorpsId { get; set; }
        // TODOX See note in BaseModels regarding timerData type.
        // public override TimerData timerData { get; set; }
        public override object timerData { get; set; }
        public int viewType { get; set; }
    }

    // New class for FF5, although not currently used because other games have the timerData property as a string.
    // See note in BaseModels for more information.
    public class TimerData {
        public double requiredSeconds { get; set; }
        public double elapsedSeconds { get; set; }
        public List<int> updateStateList { get; set; }
        public List<string> scriptNameList { get; set; }
        public bool isDisplayUI { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class FF5SaveGame : BaseSaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public FF5UserData userData { get; set; }
        public override ConfigData configData { get; set; }
        public override DataStorage dataStorage { get; set; }
        public FF5MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }

    // Compared to BaseModel.UserData: Add winCount, totalGil, wonderWandIndex, braveBladeReliefCount,
    //                                 braveBladeEscapeCount, and isTakeOverBraveBladeEscapeCount properties
    public class FF5UserData : UserData {
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
        public int winCount { get; set; }
        public override int corpsSlotIndex { get; set; }
        public override int openChestCount { get; set; }
        public override OwnedMagicStoneList ownedMagicStoneList { get; set; }
        public override int steps { get; set; }
        public override int saveCompleteCount { get; set; }
        public override int monstersKilledCount { get; set; }
        public int totalGil { get; set; }
        public int wonderWandIndex { get; set; }
        public int braveBladeReliefCount { get; set; }
        public int braveBladeEscapeCount { get; set; }
        public bool isTakeOverBraveBladeEscapeCount { get; set; }
    }
}
