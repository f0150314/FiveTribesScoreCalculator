using System;
using System.Collections.Generic;
using System.Linq;

namespace Five_Tribes_Score_Calculator.Helpers
{
    public class PlayerCountGenerator
    {
        public Dictionary<string, int> PlayerCountDic { get; }
            = new Dictionary<string, int>();

        /// <summary>
        /// Populate a list of play count in the picker control
        /// </summary>
        /// <param name="maximumPlayers"></param>
        /// <returns>A list of keys in PlayCountDic</returns>
        public List<string> PopulatePickerItems(int maximumPlayers)
        {
            // Clear dictionary
            PlayerCountDic.Clear();

            string itemKey = string.Empty;

            // Add items
            for (int i = 0; i <= maximumPlayers; i++)
            {
                // No one player option
                if (i == 1)
                {
                    continue;
                }
                // If player more than 1, set item key
                else if (i != 0)
                {
                    itemKey = string.Format("{0} Players", i);
                }

                PlayerCountDic.Add(itemKey, i);
            }

            return PlayerCountDic.Keys.ToList();
        }
    }
}
