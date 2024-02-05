//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Microsoft.Extensions.DependencyInjection;
//using Shared_Catalogs.Entities.Customers;
//using Shared_Catalogs.Interfaces;
//using Shared_Catalogs.Models;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Threading;

//namespace Catalog_App.Mvvm.ViewModels;

//public partial class CustomerListViewModel : ObservableObject
//{
//    private readonly ICustomerService _customerService;

//    private readonly IServiceProvider _sp;

//    [ObservableProperty]
//    public ObservableCollection<ICustomerDto> _customerList = [];

//    //[ObservableProperty]
//    //private ICustomerModel _customer = new CustomerModel();



//    public CustomerListViewModel(IServiceProvider sp, ICustomerService customerService)
//    {
//        _sp = sp;
//        _customerService = customerService;

//        //_customerService.GetAllCustomersAsync().ConfigureAwait(false);

//        Task.Run(async () =>
//        {
//            var result = await _customerService.GetAllCustomersAsync();
//            if (result != null)
//            {
//                Application.Current.Dispatcher.Invoke(() =>
//                {
//                    CustomerList = new ObservableCollection<ICustomerDto>(result);
//                });
//            }


//        });




//        //_customer = _customerService.CurrentContact;

//    }



//    [RelayCommand]
//    public void NavigateToAddCustomer()
//    {
//        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
//        mainViewModel.CurrentViewModel = _sp.GetRequiredService<AddCustomerViewModel>();

//    }


//    [RelayCommand]
//    private async Task NavigateToProfileView(CustomersEntity customer)
//    {
//        await _customerService.GetCustomerAsync(customer);
//        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
//        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerProfileViewModel>();
//    }
//}
