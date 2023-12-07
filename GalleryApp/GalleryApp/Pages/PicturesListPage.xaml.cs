using GalleryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicturesListPage : ContentPage
    {

        string folderPath = "/storage/emulated/0/DCIM/Camera";
        Picture SelectedPicture;

        public PicturesListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateFileList();
        }

        void UpdateFileList()
        {

            // получаем все файлы
            picturesList.ItemsSource = Directory.GetFiles(folderPath).Select(f => new Picture
            {
                Image = f,
                Name = Path.GetFileName(f),
                DateTime = File.GetCreationTime(f).ToString("dd.MM.yyyy"),
            });
        }

        private void PictureSelect(object sender, SelectedItemChangedEventArgs e)
        {
            // распаковка модели из объекта
            //SelectedPicture = e.SelectedItem.GetType().GetProperty("Name").GetValue(e.SelectedItem) as string;
            SelectedPicture = (Picture)e.SelectedItem;
        }

        private async void OpenButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedPicture == null)
            {
                await DisplayAlert(null, $"Пожалуйста, выберите картинку!", "OK");
                return;
            }

            await Navigation.PushAsync(new PicturePage(SelectedPicture));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedPicture == null)
            {
                await DisplayAlert(null, $"Пожалуйста, выберите картинку!", "OK");
                return;
            }
            // удаляем файл из списка
            File.Delete(Path.Combine(folderPath, SelectedPicture.Image));
            // обновляем список файлов
            UpdateFileList();
            SelectedPicture = null;
        }
    }
}