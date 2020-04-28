using System.Collections.Generic;

namespace AP.MobileToolkit.Menus
{
    public interface IMenuBuilder
    {
        void RegisterOption(MainMenuOption option);

        IReadOnlyList<MainMenuOption> GetOptions();
    }
}
