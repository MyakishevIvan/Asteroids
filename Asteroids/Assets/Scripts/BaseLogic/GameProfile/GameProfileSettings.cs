using Asteroids.Helper;
using UnityEngine;
using Zenject;

namespace Asteroids.GameProfile
{
    public class GameProfileSettings : IInitializable
    {
        public static bool IsPointOverUIElements { get; set; }
        public void SaveFirstGameStart() => PlayerPrefs.SetInt(TextNameHelper.IS_USER_PLAYED_BEFORE, 1);
        public void RemoveSave() =>  PlayerPrefs.DeleteAll();
        
        public bool IsUserPlayedBefore()
        {
            var hasKey = PlayerPrefs.HasKey(TextNameHelper.IS_USER_PLAYED_BEFORE);
            var isPlayerPlayedBefore = PlayerPrefs.GetInt(TextNameHelper.IS_USER_PLAYED_BEFORE) == 1;
            return hasKey && isPlayerPlayedBefore;
        }

        public void Initialize()
        {
            IsPointOverUIElements = false;
        }
    }
}