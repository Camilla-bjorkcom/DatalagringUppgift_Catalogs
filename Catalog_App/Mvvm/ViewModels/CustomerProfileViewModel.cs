using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Interfaces;

namespace Catalog_App.Mvvm.ViewModels;

public partial class CustomerProfileViewModel : ObservableObject
{

    private readonly IServiceProvider _sp;
    private readonly ICustomerService _customerService;

    public CustomerProfileViewModel(IServiceProvider sp, ICustomerService customerService)
    {
        _sp = sp;
        _customerService = customerService;
    }

    [RelayCommand]
    private void NavigateToUpdateCustomer()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<UpdateCustomerViewModel>();
    }
}
