namespace BreakoutEntities {
    public class BlockTypes {
        public enum BlockType {
            Default,
            Hardened,
            Moving,
            Unbreakable,
            Invisible,
            Switch,
            Teleporting,
            Sequence,
            Healing,
            Hungry,
            PowerUp
        }
        
        ///<summary> Fetches the list of block types </summary>
        ///<returns> The enum of block types </returns>
        public static Array GetEnumList() {
            Array enums;
            enums = Enum.GetValues(typeof(BlockTypes.BlockType));

            return enums;
        }

        ///<summary> Prints the full array of the given enum list </summary>
        ///<returns> A string array </returns>
        public static string[] GetEnumStringArray() {
            var tmpLst = GetEnumList();
            string[] returnArr = new string[tmpLst.Length];

            int i = 0;
            foreach (var type in tmpLst) {
                returnArr[i] = type.ToString();
                i++;
            }

            return returnArr;
        }
    }
}