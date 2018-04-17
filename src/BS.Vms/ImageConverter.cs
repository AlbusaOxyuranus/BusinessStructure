using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BS.Vms
{
    public class ImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if ((value as VESFlib.Manager.Photo).IsVisible)
            //{
            var path = value as string;
            try
            {
                if (!string.IsNullOrEmpty(path))
                {

                    var info = new FileInfo(path);

                    if (info.Exists && info.Length > 0)
                    {
                        var bi = new BitmapImage();

                        bi.BeginInit();
                        bi.DecodePixelWidth = 50;
                        //bi.DecodePixelHeight = 50;
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.UriSource = new Uri(info.FullName);
                        bi.EndInit();

                        return bi;
                    }
                }
                else return null;
            }
            catch
            {
                return null;
            }
            //}
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
