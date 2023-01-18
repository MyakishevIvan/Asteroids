using Zenject;

namespace BaseLogic.Installers
{
    public class ZenjectInjectionSystem
    {
        public static DiContainer Container { get; private set; }

        public static void UpdateContext(SceneContext context)
        {
            if (context == null)
            {
                Container = ProjectContext.Instance.Container;
                return;
            }

            Container = context.Container;
        }
    }
}