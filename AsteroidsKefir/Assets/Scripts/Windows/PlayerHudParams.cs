using System.Linq;
using System.Reflection;
using Asteroids.Configs;
using UnityEngine;
using Zenject;

namespace Asteroids.Windows
{
    public class PlayerHudParams : IInitializable
    {
        [Inject] private BalanceStorage _balanceStorage;
        public int rayCount;
        public float angel;
        public float speed;
        public int rayReloadTime;
        public Vector2 coordinates;
        private FieldInfo[] _fields;
        public int Score { get; set; }
        
        public void Initialize()
        {
            var type = this.GetType();
           _fields = type.GetFields().Where(x => x.IsPublic).ToArray();
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