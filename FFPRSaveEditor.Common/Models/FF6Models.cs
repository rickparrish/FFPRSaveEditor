// Note: To eliminate redundancy some properties inherit from the FF4 models instead of the Base models

namespace FFPRSaveEditor.Common.Models {
    // Compared to BaseModel.MapData: Add gpsData, currentSelectedPartyId, viewType, otherPartyDataList, partyPlayableCharacterCorpsId,
    //                                    fieldDefenseNpcEntityIDList, beastFieldEncountExchangeFlags, beastFieldEncountSeekGroupId, rtsData properties
    public class FF6MapData : MapData {
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
        public int currentSelectedPartyId { get; set; }
        public override string timerData { get; set; }
        public int viewType { get; set; }
        public List<FF6OtherPartyDataList> otherPartyDataList { get; set; }
        public List<int> partyPlayableCharacterCorpsId { get; set; }
        public List<int> fieldDefenseNpcEntityIDList { get; set; }
        public List<int> beastFieldEncountExchangeFlags { get; set; }
        public int beastFieldEncountSeekGroupId { get; set; }
        public string rtsData { get; set; }

    }

    // New class for FF6
    public class FF6OtherPartyDataList {
        public int mapId { get; set; }
        public int pointIn { get; set; }
        public string playerEntity { get; set; }
        public int playableCharacterCorpsId { get; set; }
    }

    // Compared to BaseModel.SaveGame: Add userData and mapData properties
    public class FF6SaveGame : BaseSaveGame {
        public override int id { get; set; }
        public override string pictureData { get; set; }
        public FF4UserData userData { get; set; }
        public override ConfigData configData { get; set; }
        public override DataStorage dataStorage { get; set; }
        public FF6MapData mapData { get; set; }
        public override string timeStamp { get; set; }
        public override decimal playTime { get; set; }
        public override int clearFlag { get; set; }
        public override int isCompleteFlag { get; set; }
    }
}
