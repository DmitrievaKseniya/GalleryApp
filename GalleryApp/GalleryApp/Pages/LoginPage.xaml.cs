using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        // Константа для текста кнопки
        public const string BUTTON_TEXT = "Войти";

        public string savePIN;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Проверяем, есть ли в словаре значение
            savePIN = Preferences.Get("PIN", null);
            if (String.IsNullOrEmpty(savePIN))
            {
                loginButton.Text = "Установить новый PIN";
            }

            base.OnAppearing();
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            infoMessage.Text = "";
            if (string.IsNullOrEmpty(loginEntry.Text))
            {
                infoMessage.Text = "Вы не ввели PIN";
            }
            else if (loginEntry.Text.Length < 4)
            {
                infoMessage.Text = "PIN должен содержать 4 символа";
            }
            else if ((!string.IsNullOrEmpty(savePIN)) && (loginEntry.Text != savePIN))
            {
                infoMessage.Text = "Вы указали не верный PIN";
            }
            else
            {
                Preferences.Set("PIN", loginEntry.Text);
                loginButton.Text = $"Выполняется вход..";
                // Имитация задержки (приложение загружает данные с сервера)
                await Task.Delay(150);

                // Переход на следующую страницу - страницу списка устройств
                await Navigation.PushAsync(new PicturesListPage());
                // Восстановим первоначальный текст на кнопке (на случай, если пользователь вернется на этот экран чтобы выполнить вход снова)
                loginButton.Text = BUTTON_TEXT;
                loginEntry.Text = "";
            }
        }
    }
}