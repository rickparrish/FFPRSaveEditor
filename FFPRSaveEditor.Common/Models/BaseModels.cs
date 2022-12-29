namespace FFPRSaveEditor.Common.Models.BaseModels {
    public class AbilityDictionary {
        public List<int> keys { get; set; }
        public List<AbilityDictionary_Value> values { get; set; }
    }

    public class AbilityDictionary_Value {
        public List<AbilityDictionary_Value_Target> target { get; set; }
    }

    public class AbilityDictionary_Value_Target {
        public int abilityId { get; set; }
        public int contentId { get; set; }
        public int skillLevel { get; set; }
    }

    public class AbilityList {
        public List<AbilityList_Target> target { get; set; }
    }

    public class AbilityList_Target {
        public int abilityId { get; set; }
        public int contentId { get; set; }
        public int skillLevel { get; set; }
    }

    public class AbilitySlotDataList {
        public List<AbilitySlotDataList_Target> target { get; set; }
    }

    public class AbilitySlotDataList_Target {
        public int level { get; set; }
        public AbilitySlotDataList_Target_SlotInfo slotInfo { get; set; }
    }

    public class AbilitySlotDataList_Target_SlotInfo {
        public List<int> keys { get; set; }
        // TODOX In FF1 the values property is a List<AbilitySlotDataList_Target_SlotInfo_Values> for magic
        //       characters, but for non-magic characters and for FF4+FF6 characters the values property
        //       is a List<string> where all values are "" (ie string.Empty).
        //       So for now List<object> is used to ensure things deserialize for all characters in all games,
        //       but it means hoops will need to be jumped through to edit spells for magic characters in FF1.
        // public List<AbilitySlotDataList_Target_SlotInfo_Values> values { get; set; }
        public List<object> values { get; set; }
    }

    public class AbilitySlotDataList_Target_SlotInfo_Values {
        public int abilityId { get; set; }
        public int contentId { get; set; }
        public int skillLevel { get; set; }
    }


    public class AdditionOrderOwnedAbilityIds {
        public List<int> target { get; set; }
    }

    public class AddtionalMaxMpCountList {
        public List<int> keys { get; set; }
        public List<int> values { get; set; }
    }

    public class CommandList {
        public List<int> target { get; set; }
    }

    public class ConfigData {
        public int buttonType { get; set; }
        public int battleModeIndex { get; set; }
        public int battleSpeedIndex { get; set; }
        public int battleMessageSpeedIndex { get; set; }
        public bool isCursorMemory { get; set; }
        public bool isKeepAutoBattle { get; set; }
        public int messageSpeed { get; set; }
        public double brightness { get; set; }
        public double masterVolume { get; set; }
        public double bgmVolume { get; set; }
        public double seVolume { get; set; }
        public bool isLeftActionIcon { get; set; }
        public bool isLeftVirtualPad { get; set; }
        public bool isLeftMenuCommand { get; set; }
        public bool isLeftBattleCommand { get; set; }
        public int reequipIndex { get; set; }
        public int analogModeIndex { get; set; }
    }

    public class CorpsList {
        public List<CorpsList_Target> target { get; set; }
    }

    public class CorpsList_Target {
        public int id { get; set; }
        public int characterId { get; set; }
    }

    public class CorpsSlots {
        public List<int> keys { get; set; }
        public List<CorpsSlots_Value> values { get; set; }
    }

    public class CorpsSlots_Value {
        public List<CorpsSlots_Value_Target> target { get; set; }
    }

    public class CorpsSlots_Value_Target {
        public int id { get; set; }
        public int characterId { get; set; }
    }

    public class CurrentConditionList {
        public List<int> target { get; set; }
    }

    public class CurrentMpCountList {
        public List<int> keys { get; set; }
        public List<int> values { get; set; }
    }

    public class DataStorage {
        public List<uint> scenario { get; set; }
        public List<uint> treasure { get; set; }
        public List<int> global { get; set; }
        public List<int> area { get; set; }
        public List<int> map { get; set; }
        public int selected { get; set; }
        public int itemSelected { get; set; }
        public List<uint> transportation { get; set; }
    }

    public class EquipmentAbilitys {
        public List<int> target { get; set; }
    }

    public class EquipmentList {
        public List<int> keys { get; set; }
        public List<EquipmentList_Value> values { get; set; }
    }

    public class EquipmentList_Value {
        public int contentId { get; set; }
        public int count { get; set; }
    }

    public class GpsData {
        public virtual int mapId { get; set; }
        public virtual int areaId { get; set; }
        public virtual int gpsId { get; set; }
        public virtual int width { get; set; }
        public virtual int height { get; set; }
    }

    public class ImportantOwendItemList {
        public List<ImportantOwendItemList_Target> target { get; set; }
    }

    public class ImportantOwendItemList_Target {
        public int contentId { get; set; }
        public int count { get; set; }
    }

    public class JobList {
        public List<JobList_Target> target { get; set; }
    }

    public class JobList_Target {
        public int id { get; set; }
        public int level { get; set; }
        public int currentProficiency { get; set; }
    }

    public class LearnedAbilityList {
        public List<int> target { get; set; }
    }

    public class LearningAbilitys {
        public List<int> target { get; set; }
    }

    public class MapData {
        public virtual int mapId { get; set; }
        public virtual int pointIn { get; set; }
        public virtual int transportationId { get; set; }
        public virtual bool carryingHoverShip { get; set; }
        public virtual PlayerEntity playerEntity { get; set; }
        public virtual string companionEntity { get; set; }
        // public virtual GpsData gpsData { get; set; }
        public virtual int moveCount { get; set; }
        public virtual int subtractSteps { get; set; }
        public virtual string telepoCacheData { get; set; }
        public virtual int playableCharacterCorpsId { get; set; }
        public virtual string timerData { get; set; }
    }

    public class MapData2 {
        public virtual int mapId { get; set; }
        public virtual int pointIn { get; set; }
        public virtual int transportationId { get; set; }
        public virtual bool carryingHoverShip { get; set; }
        public virtual PlayerEntity playerEntity { get; set; }
        public virtual string companionEntity { get; set; }
        public virtual GpsData gpsData { get; set; }
        public virtual int moveCount { get; set; }
        public virtual int subtractSteps { get; set; }
        public virtual string telepoCacheData { get; set; }
        public virtual int playableCharacterCorpsId { get; set; }
        public virtual string timerData { get; set; }
    }

    public class NormalOwnedItemList {
        public List<NormalOwnedItemList_Target> target { get; set; }
    }

    public class NormalOwnedItemList_Target {
        public int contentId { get; set; }
        public int count { get; set; }
    }

    public class NormalOwnedItemSortIdList {
        public List<int> target { get; set; }
    }

    public class OwendCrystalFlags {
        public List<bool> target { get; set; }
    }

    public class OwnedCharacterList {
        public List<OwnedCharacterList_Target> target { get; set; }
    }

    public class OwnedCharacterList_Target {
        public int id { get; set; }
        public int characterStatusId { get; set; }
        public bool isEnableCorps { get; set; }
        public int jobId { get; set; }
        public string name { get; set; }
        public int currentExp { get; set; }
        public Parameter parameter { get; set; }
        public CommandList commandList { get; set; }
        public AbilityList abilityList { get; set; }
        public AbilitySlotDataList abilitySlotDataList { get; set; }
        public JobList jobList { get; set; }
        public EquipmentList equipmentList { get; set; }
        public AdditionOrderOwnedAbilityIds additionOrderOwnedAbilityIds { get; set; }
        public SortOrderOwnedAbilityIds sortOrderOwnedAbilityIds { get; set; }
        public AbilityDictionary abilityDictionary { get; set; }
        public SkillLevelTargets skillLevelTargets { get; set; }
        public LearningAbilitys learningAbilitys { get; set; }
        public EquipmentAbilitys equipmentAbilitys { get; set; }
        public int numberOfButtles { get; set; }
        public int ownedMonsterId { get; set; }
        public int magicStoneId { get; set; }
        public int magicLearningValue { get; set; }
    }

    public class OwnedKeyWaordList {
        public List<int> target { get; set; }
    }

    public class OwnedMagicList {
        public List<int> target { get; set; }
    }

    public class OwnedMagicStoneList {
        public List<int> target { get; set; }
    }

    public class OwnedTransportationList {
        public List<OwnedTransportationList_Target> target { get; set; }
    }

    public class OwnedTransportationList_Target {
        public Position position { get; set; }
        public int direction { get; set; }
        public int id { get; set; }
        public int mapId { get; set; }
        public bool enable { get; set; }
        public long timeStampTicks { get; set; }
    }

    public class Parameter {
        public int currentHP { get; set; }
        public int currentMP { get; set; }
        public CurrentMpCountList currentMpCountList { get; set; }
        public AddtionalMaxMpCountList addtionalMaxMpCountList { get; set; }
        public int addtionalLevel { get; set; }
        public int addtionalMaxHp { get; set; }
        public int addtionalMaxMp { get; set; }
        public int addtionalPower { get; set; }
        public int addtionalVitality { get; set; }
        public int addtionalAgility { get; set; }
        public int addionalWeight { get; set; }
        public int addtionalIntelligence { get; set; }
        public int addtionalSpirit { get; set; }
        public int addtionalAttack { get; set; }
        public int addtionalDefense { get; set; }
        public int addtionalAbilityDefense { get; set; }
        public int addtionalAbilityEvasionRate { get; set; }
        public int addtionalMagic { get; set; }
        public int addtionalLuck { get; set; }
        public int addtionalAccuracyRate { get; set; }
        public int addtionalEvasionRate { get; set; }
        public int addtionalAbilityDisturbedRate { get; set; }
        public int addtionalCriticalRate { get; set; }
        public int addtionalDamageDirmeter { get; set; }
        public int addtionalAbilityDefenseRate { get; set; }
        public int addtionalAccuracyCount { get; set; }
        public int addtionalEvasionCount { get; set; }
        public int addtionalDefenseCount { get; set; }
        public int addtionalMagicDefenseCount { get; set; }
        public CurrentConditionList currentConditionList { get; set; }
    }

    public class PlayerEntity {
        public Position position { get; set; }
        public int direction { get; set; }
    }

    public class Position {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class ReleasedJobs {
        public List<int> target { get; set; }
    }

    public class SaveGame {
        public virtual int id { get; set; }
        public virtual string pictureData { get; set; }
        // public virtual UserData userData { get; set; }
        public virtual ConfigData configData { get; set; }
        public virtual DataStorage dataStorage { get; set; }
        // public virtual MapData mapData { get; set; }
        public virtual string timeStamp { get; set; }
        public virtual decimal playTime { get; set; }
        public virtual int clearFlag { get; set; }
        public virtual int isCompleteFlag { get; set; }
    }

    public class SaveGame2 {
        public virtual int id { get; set; }
        public virtual string pictureData { get; set; }
        public virtual UserData userData { get; set; }
        public virtual ConfigData configData { get; set; }
        public virtual DataStorage dataStorage { get; set; }
        public virtual MapData2 mapData { get; set; }
        public virtual string timeStamp { get; set; }
        public virtual decimal playTime { get; set; }
        public virtual int clearFlag { get; set; }
        public virtual int isCompleteFlag { get; set; }
    }

    public class SkillLevelTargets {
        public List<int> keys { get; set; }
        public List<int> values { get; set; }
    }

    public class SortOrderOwnedAbilityIds {
        public List<int> target { get; set; }
    }

    public class UserData {
        public virtual CorpsList corpsList { get; set; }
        public virtual CorpsSlots corpsSlots { get; set; }
        public virtual OwnedCharacterList ownedCharacterList { get; set; }
        public virtual ReleasedJobs releasedJobs { get; set; }
        public virtual int owendGil { get; set; }
        public virtual decimal playTime { get; set; }
        public virtual NormalOwnedItemList normalOwnedItemList { get; set; }
        public virtual ImportantOwendItemList importantOwendItemList { get; set; }
        public virtual NormalOwnedItemSortIdList normalOwnedItemSortIdList { get; set; }
        public virtual string currentArea { get; set; }
        public virtual string currentLocation { get; set; }
        public virtual OwnedTransportationList ownedTransportationList { get; set; }
        public virtual OwendCrystalFlags owendCrystalFlags { get; set; }
        public virtual string configData { get; set; }
        public virtual WarehouseItemList warehouseItemList { get; set; }
        public virtual OwnedKeyWaordList ownedKeyWaordList { get; set; }
        public virtual OwnedMagicList ownedMagicList { get; set; }
        public virtual LearnedAbilityList learnedAbilityList { get; set; }
        public virtual int escapeCount { get; set; }
        public virtual int battleCount { get; set; }
        public virtual int corpsSlotIndex { get; set; }
        public virtual int openChestCount { get; set; }
        public virtual OwnedMagicStoneList ownedMagicStoneList { get; set; }
        public virtual int steps { get; set; }
        public virtual int saveCompleteCount { get; set; }
        public virtual int monstersKilledCount { get; set; }
    }

    public class WarehouseItemList {
        public List<WarehouseItemList_Target> target { get; set; }
    }

    public class WarehouseItemList_Target {
        public int contentId { get; set; }
        public int count { get; set; }
    }
}
