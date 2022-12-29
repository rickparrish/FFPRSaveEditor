namespace FFPRSaveEditor.Common.Models.FF1Models {
    // Compared to BaseModel.MapData: Add gpsData property
    public class MapData : BaseModels.MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override BaseModels.PlayerEntity playerEntity { get; set; }
        public override string companionEntity { get; set; }
        public BaseModels.GpsData gpsData { get; set; }
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
        public BaseModels.UserData userData { get; set; }
        public override BaseModels.ConfigData configData { get; set; }
        public override BaseModels.DataStorage dataStorage { get; set; }
        public MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }
}
