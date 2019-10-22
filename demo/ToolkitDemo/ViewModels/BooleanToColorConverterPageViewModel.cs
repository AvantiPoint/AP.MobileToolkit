using System.Collections.Generic;
using System.ComponentModel;
using AP.CrossPlatform.Collections;
using AP.CrossPlatform.Extensions;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ToolkitDemo.Models;

namespace ToolkitDemo.ViewModels
{
    public class BooleanToColorConverterPageViewModel : DemoPageViewModelBase
    {
        public BooleanToColorConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            ProjectList = new ObservableRangeCollection<Project>()
            {
                new Project() { Name = "Project 1", Status = ProjectStatus.Completed.GetAttribute<DescriptionAttribute>().Description, IsCompleted = true },
                new Project() { Name = "Project 2", Status = ProjectStatus.InProgress.GetAttribute<DescriptionAttribute>().Description, IsCompleted = false },
                new Project() { Name = "Project 3", Status = ProjectStatus.Completed.GetAttribute<DescriptionAttribute>().Description, IsCompleted = true },
                new Project() { Name = "Project 4", Status = ProjectStatus.InProgress.GetAttribute<DescriptionAttribute>().Description, IsCompleted = false },
            };
        }

        public IEnumerable<Project> ProjectList { get; }

        public bool Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private bool _value;
    }
}
