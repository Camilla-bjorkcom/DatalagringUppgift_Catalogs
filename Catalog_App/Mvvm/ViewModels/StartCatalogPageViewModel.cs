//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Microsoft.Extensions.DependencyInjection;
//using Shared_Catalogs.Interfaces;

//namespace Catalog_App.Mvvm.ViewModels;

//public partial class StartCatalogPageViewModel : ObservableObject
//{
//    private readonly IServiceProvider _sp;
//    private readonly ICustomerService _customerService;

//    public StartCatalogPageViewModel(IServiceProvider sp, ICustomerService customerService)
//    {
//        _sp = sp;
//        _customerService = customerService;
//    }

//    [RelayCommand]
//    private void NavigateToCustomerList()
//    {
//        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
//        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
//    }
//}
