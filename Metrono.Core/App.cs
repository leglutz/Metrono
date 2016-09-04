using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace DiodeCompany.Metrono.Core
{
    public class App : MvxApplication
    {
        public override void Initialize ()
        {
            // Register services
            Mvx.ConstructAndRegisterSingleton<ISettingsService, SettingsService>();

            // Registers ViewModels
            Mvx.LazyConstructAndRegisterSingleton<SettingsViewModel, SettingsViewModel>();
            Mvx.LazyConstructAndRegisterSingleton<MetronomeViewModel, MetronomeViewModel>();
            Mvx.LazyConstructAndRegisterSingleton<MeasureViewModel, MeasureViewModel>();

            // Register app start
            RegisterAppStart(new AppStart());
        }
    }
}