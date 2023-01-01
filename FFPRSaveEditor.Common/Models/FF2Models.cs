namespace FFPRSaveEditor.Common.Models {
    // Compared to BaseModel.MapData: Add gpsData property
    public class FF2MapData : MapData {
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
        public override object timerData { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class FF2SaveGame : BaseSaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public UserData userData { get; set; }
        public override ConfigData configData { get; set; }
        public override DataStorage dataStorage { get; set; }
        public FF2MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }
}
