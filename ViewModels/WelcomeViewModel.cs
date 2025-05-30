using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiPuntos.uy_mobile.Models;
using ServiPuntos.uy_mobile.Models.Enums;
using ServiPuntos.uy_mobile.Views;

namespace ServiPuntos.uy_mobile.ViewModels;

public partial class WelcomeViewModel(IConfiguration _configs) : ObservableObject
{

  [ObservableProperty] private string? _TenantName;
  [RelayCommand]
  private async Task GoToLoginPage()
  {
    await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
  }
  [RelayCommand]
  private async Task GoToSignUpPage()
  {
    await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
  }

  public async Task CheckUserSession()
  {
    var sessionData =
        JsonConvert.DeserializeObject<SessionData>(await SecureStorage.GetAsync(SecureStorageType.Session.ToString()) ??
                                                   string.Empty);
    if (sessionData != null /* && sessionData.Expiration > DateTime.Now.AddMinutes(15) */)
    {
      await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
  }
  public void GetTenantName()
  {
    TenantName = _configs["TENANT_NAME"] ?? "interno";
  }
}

