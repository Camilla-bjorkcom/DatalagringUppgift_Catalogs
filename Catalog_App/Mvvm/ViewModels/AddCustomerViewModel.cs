using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;

namespace Catalog_App.Mvvm.ViewModels;

//public partial class AddCustomerViewModel : ObservableObject
//{
//    private readonly IServiceProvider _sp;
//    private readonly ICustomerService _customerService;



//    public AddCustomerViewModel(IServiceProvider sp, ICustomerService customerService)
//    {
//        _sp = sp;
//        _customerService = customerService;
//    }

//    /// <summary>
//    /// Navigates to the StartPage view.
//    /// </summary>
//    [RelayCommand]
//    private void NavigateToListView()
//    {
//        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
//        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
//    }

//    /// <summary>
//    /// Customer form for adding a new customer.
//    /// </summary>
//    [ObservableProperty]
//    private ICustomerModel _customerModel = new CustomerModel();

//    /// <summary>
//    /// Adds a contact and navigates to the contact list view.
//    /// </summary>
//    [RelayCommand]
//    private async Task Add()
//    {
//        var customerDto = new CustomerRegistrationDto
//        {
//            FirstName = CustomerModel.FirstName,
//            LastName = CustomerModel.LastName,
//            StreetName = CustomerModel.StreetName,
//            City = CustomerModel.City,
//            PostalCode = CustomerModel.PostalCode,
//            Email = CustomerModel.Email,
//            CustomerType = CustomerModel.CustomerType,
//        };
//        await _customerService.CreateCustomerAsync(customerDto);

//        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
//        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
//    }
//}
