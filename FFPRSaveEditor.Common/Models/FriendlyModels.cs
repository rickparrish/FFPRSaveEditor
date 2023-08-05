using Newtonsoft.Json;
using System.Drawing;

namespace FFPRSaveEditor.Common.Models {
    public partial class BaseSaveGame {
        /// <summary>
        /// FF5: Used to add magical abilities (black, blue, white, summon, etc) by adding the `AbilityId` to this list.
        /// </summary>
        [JsonIgnore]
        public List<int> Abilities {
            get {
                return userData.learnedAbilityList.target;
            }
        }

        /// <summary>
        /// A list of characters.  Filter to `IsInParty` for current party members, or use alternative `PartyMembers` property.
        /// </summary>
        [JsonIgnore]
        public List<OwnedCharacterList_Target> Characters {
            get {
                return userData.ownedCharacterList.target;
            }
        }

        /// <summary>
        /// The amount of gold the party currently has.
        /// </summary>
        [JsonIgnore]
        public int Gold {
            get {
                return userData.owendGil;
            }
            set {
                userData.owendGil = value;
            }
        }

        /// <summary>
        /// Indicates whether the current save file is an Autosave.
        /// </summary>
        [JsonIgnore]
        public bool IsAutosave {
            get {
                return id == 21;
            }
        }

        /// <summary>
        /// Indicates whether the current save file is a Quick Save.
        /// </summary>
        [JsonIgnore]
        public bool IsQuickSave {
            get {
                return id == 22;
            }
        }

        /// <summary>
        /// Standard items/weapons/armour/etc.  Use `KeyItems` to access important quest items.
        /// </summary>
        [JsonIgnore]
        public List<OwnedItemList_Target> Items {
            get {
                return userData.normalOwnedItemList.target;
            }
        }

        /// <summary>
        /// Key items, aka important quest items.  Use `Items` to access standard items/weapons/armour/etc.
        /// </summary>
        [JsonIgnore]
        public List<OwnedItemList_Target> KeyItems {
            get {
                return userData.importantOwendItemList.target;
            }
        }

        /// <summary>
        /// FF5: Used to add magical abilities (black, blue, white, summon, etc) by adding the `ContentId` to this list.
        /// NB:  Only seems to be used in FF5 and FF6
        /// </summary>
        [JsonIgnore]
        public List<int> Magics {
            get {
                return userData.ownedMagicList.target;
            }
        }

        /// <summary>
        /// A list of characters in the current party (ie IsInPart == true).  Use `Characters` for a full list of characters.
        /// </summary>
        [JsonIgnore]
        public List<OwnedCharacterList_Target> PartyMembers {
            get {
                return Characters.Where(x => x.IsInParty).ToList();
            }
        }

        /// <summary>
        /// The number of seconds the game has been played.
        /// </summary>
        [JsonIgnore]
        public decimal PlayTimeInSeconds {
            get {
                return userData.playTime;
            }
        }

        /// <summary>
        /// Contains the `ContentId` values for the `Items` the party currently has, in the order they should be displayed on the item list.
        /// </summary>
        [JsonIgnore]
        public List<int> SortedItemIds {
            get {
                return userData.normalOwnedItemSortIdList.target;
            }
        }

        /// <summary>
        /// A thumbnail of the screen where the save was taken.
        /// </summary>
        [JsonIgnore]
        public Image Thumbnail {
            get {
                if (_Thumbnail == null) {
                    _Thumbnail = Image.FromStream(new MemoryStream(Convert.FromBase64String(pictureData)));
                }

                return _Thumbnail;
            }
        }
        private Image? _Thumbnail = null;

        /// <summary>
        /// The version of the game the save is for (ie 1-6 for FF1 to FF6)
        /// </summary>
        [JsonIgnore]
        public int Version {
            get {
                if (!_Version.HasValue) {
                    string ownedTransportationList = string.Join(",", userData.ownedTransportationList.target.Select(x => x.id).OrderBy(x => x));
                    switch (ownedTransportationList) {
                        case "1,2,3,4":
                            _Version = 1;
                            break;
                        case "1,2,3,4,5,6,7":
                            _Version = 2;
                            break;
                        case "1,2,3,5,6,8,9,10,11,12,13,14,15,16":
                            _Version = 3;
                            break;
                        case "1,2,3,4,5,6,7,8,9,10,11":
                            _Version = 4;
                            break;
                        case "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,17,18,19,20":
                            _Version = 5;
                            break;
                        case "1,2,3,4,5,6,7,8":
                            _Version = 6;
                            break;
                        default:
                            throw new InvalidDataException("Version detection failed");
                    }
                }

                return _Version.Value;
            }
        }
        private int? _Version = null;
    }

    public partial class OwnedCharacterList_Target {
        /// <summary>
        /// FF5: Used to add magical abilities to a player (black, blue, white, summon, etc) by adding the class to this list.
        /// </summary>
        [JsonIgnore]
        public List<AbilityList_Target> Abilities {
            get {
                return abilityList.target;
            }
        }

        /// <summary>
        /// FF5: Used to add magical abilities to a player (black, blue, white, summon, etc) by adding the `ContentId` to this list.
        /// NB:  It accesses a property called additionOrderOwnedAbilityIds, implying it contains a list of `AbilityId`, but observing
        ///      the behaviour of learning magic in FF5 showed that it is actually `ContentId` values that are stored here, hence why
        ///      the extension property name is AbilityContentIds.
        /// </summary>
        [JsonIgnore]
        public List<int> AbilityContentIds {
            get {
                return additionOrderOwnedAbilityIds.target;
            }
        }

        /// <summary>
        /// NB: Only appears to be used in FF1.
        /// </summary>
        [JsonIgnore]
        public int Accuracy {
            get {
                return parameter.addtionalAccuracyRate;
            }
            set {
                parameter.addtionalAccuracyRate = value;
            }
        }

        /// <summary>
        /// NB: Does not appear to be used in FF6.
        /// </summary>
        [JsonIgnore]
        public int Agility {
            get {
                return parameter.addtionalAgility;
            }
            set {
                parameter.addtionalAgility = value;
            }
        }

        /// <summary>
        /// NB: Does not appear to be used at all
        /// TODOX: Confirm whether this is used by FF1, which includes it on the character editor form
        /// </summary>
        [JsonIgnore]
        public int Evasion {
            get {
                return parameter.addtionalEvasionRate;
            }
            set {
                parameter.addtionalEvasionRate = value;
            }
        }

        [JsonIgnore]
        public int Experience {
            get {
                return currentExp;
            }
            set {
                currentExp = value;
            }
        }

        [JsonIgnore]
        public int HitPoints {
            get {
                return parameter.currentHP;
            }
            set {
                parameter.currentHP = value;
            }
        }

        [JsonIgnore]
        public int HitPointsMax {
            get {
                return parameter.addtionalMaxHp;
            }
            set {
                parameter.addtionalMaxHp = value;
            }
        }

        /// <summary>
        /// NB: Does not appear to be used in FF5 or FF6
        /// </summary>
        [JsonIgnore]
        public int Intellect {
            get {
                return parameter.addtionalIntelligence;
            }
            set {
                parameter.addtionalIntelligence = value;
            }
        }

        /// <summary>
        /// Indicates whether the character is in the current party.
        /// </summary>
        [JsonIgnore]
        public bool IsInParty {
            get {
                return isEnableCorps;
            }
        }

        /// <summary>
        /// FF5: Used to add jobs to a player, or edit jobs they have already learned.
        /// </summary>
        [JsonIgnore]
        public List<JobList_Target> Jobs {
            get {
                return jobList.target;
            }
        }

        /// <summary>
        /// FF5: Used to add job abilities to a player by adding the `ContentId` to this list.
        /// NB:  It accesses a property called learningAbilitys, implying it contains a list of `AbilityId`, but observing
        ///      the behaviour of learning job abilities in FF5 showed that it is actually `ContentId` values that are stored here, hence why
        ///      the extension property name is JobAbilityContentIds.
        /// </summary>
        [JsonIgnore]
        public List<int> JobAbilityContentIds {
            get {
                return learningAbilitys.target;
            }
        }

        [JsonIgnore]
        public int Level {
            get {
                return parameter.addtionalLevel;
            }
        }

        /// <summary>
        /// Only appears to be used in FF1.
        /// </summary>
        [JsonIgnore]
        public int Luck {
            get {
                return parameter.addtionalLuck;
            }
            set {
                parameter.addtionalLuck = value;
            }
        }

        /// <summary>
        /// Does not appear to be used by FF3 or FF4
        /// </summary>
        [JsonIgnore]
        public int Magic {
            get {
                return parameter.addtionalMagic;
            }
            set {
                parameter.addtionalMagic = value;
            }
        }

        /// <summary>
        /// NB: Doesn't appear to be used in FF1, FF3, or FF4.
        /// </summary>
        [JsonIgnore]
        public int MagicPoints {
            get {
                return parameter.currentMP;
            }
            set {
                parameter.currentMP = value;
            }
        }

        /// <summary>
        /// NB: Doesn't appear to be used in FF1, FF3, or FF4.
        /// </summary>
        [JsonIgnore]
        public int MagicPointsMax {
            get {
                return parameter.addtionalMaxMp;
            }
            set {
                parameter.addtionalMaxMp = value;
            }
        }

        /// <summary>
        /// Doesn't appear to be used in FF5 or FF6.
        /// </summary>
        [JsonIgnore]
        public int Spirit {
            get {
                return parameter.addtionalSpirit;
            }
            set {
                parameter.addtionalSpirit = value;
            }
        }

        [JsonIgnore]
        public int Stamina {
            get {
                return parameter.addtionalVitality;
            }
            set {
                parameter.addtionalVitality = value;
            }
        }

        [JsonIgnore]
        public int Strength {
            get {
                return parameter.addtionalPower;
            }
            set {
                parameter.addtionalPower = value;
            }
        }
    }
}
