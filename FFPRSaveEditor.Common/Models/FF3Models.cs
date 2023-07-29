using System.Runtime.Serialization;

namespace FFPRSaveEditor.Common.Models {
    // Compared to BaseModel.GpsData: Add new transportationId property
    public class FF3GpsData : GpsData {
        public int transportationId { get; set; }
        public override int mapId { get; set; }
        public override int areaId { get; set; }
        public override int gpsId { get; set; }
        public override int width { get; set; }
        public override int height { get; set; }
    }

    // Compared to BaseModel.MapData: Add custom gpsData and new viewType properties
    public class FF3MapData : MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override PlayerEntity playerEntity { get; set; }
        public override object companionEntity { get; set; }
        public new FF3GpsData gpsData { get; set; }
        public override int moveCount { get; set; }
        public override int subtractSteps { get; set; }
        public override string telepoCacheData { get; set; }
        public override int playableCharacterCorpsId { get; set; }
        public override object timerData { get; set; }
        public int viewType { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context) {
            base.gpsData = gpsData;
        }
    }

    // Compared to BaseModel.SaveGame: Add custom mapData property
    public class FF3SaveGame : BaseSaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public override UserData userData { get; set; }
        public override ConfigData configData { get; set; }
        public override DataStorage dataStorage { get; set; }
        public new FF3MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context) {
            base.mapData = mapData;
        }
    }
}
