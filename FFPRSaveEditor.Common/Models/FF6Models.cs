// Note: To eliminate redundancy some properties inherit from the FF4 models instead of the Base models

namespace FFPRSaveEditor.Common.Models.FF6Models {
    // Compared to BaseModel.MapData: Add gpsData, currentSelectedPartyId, viewType, otherPartyDataList, partyPlayableCharacterCorpsId,
    //                                    fieldDefenseNpcEntityIDList, beastFieldEncountExchangeFlags, beastFieldEncountSeekGroupId, rtsData properties
    public class MapData : BaseModels.MapData {
        public override int mapId { get; set; }
        public override int pointIn { get; set; }
        public override int transportationId { get; set; }
        public override bool carryingHoverShip { get; set; }
        public override BaseModels.PlayerEntity playerEntity { get; set; }
        public override string companionEntity { get; set; }
        public FF4Models.GpsData gpsData { get; set; }
        public override int moveCount { get; set; }
        public override int subtractSteps { get; set; }
        public override string telepoCacheData { get; set; }
        public override int playableCharacterCorpsId { get; set; }
        public int currentSelectedPartyId { get; set; }
        public override string timerData { get; set; }
        public int viewType { get; set; }
        public List<OtherPartyDataList> otherPartyDataList { get; set; }
        public List<int> partyPlayableCharacterCorpsId { get; set; }
        public List<int> fieldDefenseNpcEntityIDList { get; set; }
        public List<int> beastFieldEncountExchangeFlags { get; set; }
        public int beastFieldEncountSeekGroupId { get; set; }
        public string rtsData { get; set; }

    }

    // New class for FF6
    public class OtherPartyDataList {
        public int mapId { get; set; }
        public int pointIn { get; set; }
        public string playerEntity { get; set; }
        public int playableCharacterCorpsId { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class SaveGame : BaseModels.SaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public FF4Models.UserData userData { get; set; }
        public override BaseModels.ConfigData configData { get; set; }
        public override BaseModels.DataStorage dataStorage { get; set; }
        public MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }
}
