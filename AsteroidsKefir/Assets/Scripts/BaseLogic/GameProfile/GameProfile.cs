using Asteroids.Helper;
using UnityEngine;

namespace Asteroids.GameProfile
{
    public class GameProfile
    {
        public void SaveFirstGameStart()
        {
            PlayerPrefs.SetInt(TextNameHelper.IS_USER_PLAYED_BEFORE, 1);
        }

        public bool IsUserPlayedBefore()
        {
            var hasKey = PlayerPrefs.HasKey(TextNameHelper.IS_USER_PLAYED_BEFORE);
            var isPlayerPlayedBefore = PlayerPrefs.GetInt(TextNameHelper.IS_USER_PLAYED_BEFORE) == 1;
            return hasKey && isPlayerPlayedBefore;
        }
    }
}