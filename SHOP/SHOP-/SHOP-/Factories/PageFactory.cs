using System;
using SHOP.Data;
using SHOP.ViewModels;

namespace SHOP.Factories;

public class PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
{
    public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => factory.Invoke(pageName);
}