using System.Reflection;
using UnityEngine;

namespace Asteroids.Windows
{
    public class PlayerHudParams
    {
        private static PlayerHudParams _instance;
        public static PlayerHudParams Instance => _instance ?? new PlayerHudParams();
        
        public int rayCount;
        public float angel;
        public float speed;
        public int rayReloadTime;
        public Vector2 coordinates;
        private FieldInfo[] _fields;
        public int Score { get; set; }

        private PlayerHudParams()
        {
            var type = this.GetType();
            _fields = type.GetFields();
            _instance = this;
        }

        public override string ToString()
        {
            var result = string.Empty;
            
            foreach (var field  in _fields)
            {
                result += $"\n {field.Name} - {field.GetValue(this)}";
            }

            return result;
        }
    }
}