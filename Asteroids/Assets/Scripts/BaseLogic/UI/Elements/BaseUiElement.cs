using Asteroids.GameProfile;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Asteroids.UI.Elements
{
    public class BaseUiElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            GameProfileSettings.IsPointOverUIElements = true;
            Debug.Log("IsPointOverUIElements " +  GameProfileSettings.IsPointOverUIElements);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameProfileSettings.IsPointOverUIElements = false;
            Debug.Log("IsPointOverUIElements " +  GameProfileSettings.IsPointOverUIElements);
        }
    }
    
}

