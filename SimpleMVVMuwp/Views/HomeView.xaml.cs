﻿using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using SimpleMVVM.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        internal HomeViewModel ViewModel = Ioc.Default.GetService<HomeViewModel>();
    }
}