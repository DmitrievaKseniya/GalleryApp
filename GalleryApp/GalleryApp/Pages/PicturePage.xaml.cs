using GalleryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicturePage : ContentPage
	{
        public static string PageName { get; set; }
        public Picture Picture { get; set; }

		public PicturePage (Picture picture = null)
		{
			Picture = picture;
            PageName = Picture.Name;

			InitializeComponent ();
            OpenEditor();
        }

		public void OpenEditor()
		{
            // Создание поля для отображения картинки
            var selectedPicture = new Image
            {
                Source = Picture.Image,
                Margin = new Thickness(0, 25, 0, 0)
            };

            stackLayout.Children.Add(selectedPicture);

            // Создание поля для подписи
            var dataCreation = new Label 
            { 
                Text = "Дата создания изображения: " + Picture.DateTime, 
                HorizontalOptions = LayoutOptions.Center, 
                Margin = new Thickness(20, 25, 0, 0) 
            };
            stackLayout.Children.Add(dataCreation);
        }
    }
}