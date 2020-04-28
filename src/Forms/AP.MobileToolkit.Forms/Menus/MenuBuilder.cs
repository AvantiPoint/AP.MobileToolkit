using System.Collections.Generic;
using System.Linq;
using AP.CrossPlatform.Collections;

namespace AP.MobileToolkit.Menus
{
    public class MenuBuilder : IMenuBuilder
    {
        private ObservableRangeCollection<MainMenuOption> Options { get; } =
            new ObservableRangeCollection<MainMenuOption>();

        void IMenuBuilder.RegisterOption(MainMenuOption option)
        {
            Options.Add(option);
            Options.ReplaceRange(Options.OrderBy(x => x.Priority));
        }

        IReadOnlyList<MainMenuOption> IMenuBuilder.GetOptions() => Options;
    }
}
