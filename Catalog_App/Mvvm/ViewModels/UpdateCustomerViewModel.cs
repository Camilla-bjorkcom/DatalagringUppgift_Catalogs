using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Interfaces;

namespace Catalog_App.Mvvm.ViewModels;

public partial class UpdateCustomerViewModel : ObservableObject
{
    private readonly IServiceProvider _sp;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private IUpdateCustomerDto _updateCustomerForm = new UpdateCustomerDto();

    public UpdateCustomerViewModel(IServiceProvider sp, ICustomerService customerService)
    {
        _sp = sp;
        _customerService = customerService;

        UpdateCustomerForm = _customerService.CurrentCustomer;
    }

    [RelayCommand]
    private void Update()
    {
        _customerService.UpdateCustomerAsync(UpdateCustomerForm);
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }
}
