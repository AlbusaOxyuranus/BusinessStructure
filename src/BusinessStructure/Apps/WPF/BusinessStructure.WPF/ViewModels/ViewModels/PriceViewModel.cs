using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BusinessStructure.Vms.JShoping.ImExPrice.BAL;
using BusinessStructure.Vms.ViewModels.price;
using BusinessStructure.WPF.Views.Pages;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Application = Microsoft.Office.Interop.Excel.Application;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;

namespace BusinessStructure.Vms.ViewModels
{
    public enum TypeExp
    {
        INT,
        STRING,
        DOUBLE,
        IMAGE
    }

    public enum HorizontalAlignmentText
    {
        NoAlign,
        Center
    }

    public enum VerticalAlignmentText
    {
        NoAlign,
        Center
    }

    //workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
    public class Exp
    {
        public Exp()
        {
            TypeExp = TypeExp.STRING;
            ValueList = new List<string>();
        }

        public string NameRow { get; set; }
        public TypeExp TypeExp { get; set; }
        public List<string> ValueList { get; set; }
        public int Width { get; set; }
        public HorizontalAlignmentText HorizontalAlignmentText { get; set; }
        public VerticalAlignmentText VerticalAlignmentText { get; set; }
    }

    public class PriceViewModel : ViewModelBase
    {
        private ObservableCollection<ProductViewModel> _list;

        public PriceViewModel()
        {
            _list = new ObservableCollection<ProductViewModel>();
            CreatePriceExcelCommand = AsyncCommand.Create(CreatePriceExcel);
            NavigateMainCommand = AsyncCommand.Create(NavigateMain);
        }

        public IAsyncCommand NavigateMainCommand { get; }

        private async Task NavigateMain()
        {
            Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
        }

        public IAsyncCommand CreatePriceExcelCommand { get; }

        public ObservableCollection<ProductViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        private async Task CreatePriceExcel()
        {
            var saveFileDialog1 = new SaveFileDialog();
            string filename = "Price" + "-" + Convert.ToDateTime(DateTime.Now).ToString("yyMMdd") + ".xls";
            saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = filename;
            if (saveFileDialog1.ShowDialog() == true)
            {
                Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage = "Подготовка к выполнению процесса";
                Store.CreateOrGet<PriceViewModel>().PercentProcess = 0;
                Store.CreateOrGet<PriceViewModel>().BussinessProcess = true;


                await TaskHelper.RunAsync(() =>
                {
                    var expList = new List<Exp>();

                    var list = new List<Exp>();
                    var ids = new Exp
                    {
                        NameRow = "№",
                        TypeExp = TypeExp.INT,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var names = new Exp
                    {
                        NameRow = "Наименование товара",
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var images = new Exp {NameRow = "Изображение", TypeExp = TypeExp.IMAGE};
                    var WeightVolumeUnits = new Exp
                    {
                        NameRow = "Количество в упаковке",
                        TypeExp = TypeExp.INT,
                        HorizontalAlignmentText = HorizontalAlignmentText.Center,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var Price_Byn_units = new Exp
                    {
                        NameRow = "Цена за единицу товара BYN",
                        TypeExp = TypeExp.DOUBLE,
                        HorizontalAlignmentText = HorizontalAlignmentText.Center,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var Price_Byn = new Exp
                    {
                        NameRow = "Цена за упаковку BYN",
                        TypeExp = TypeExp.DOUBLE,
                        HorizontalAlignmentText = HorizontalAlignmentText.Center,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var Sostav = new Exp
                    {
                        NameRow = "Состав",
                        TypeExp = TypeExp.STRING,
                        HorizontalAlignmentText = HorizontalAlignmentText.Center,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    var Sizes = new Exp
                    {
                        NameRow = "Размеры",
                        TypeExp = TypeExp.STRING,
                        HorizontalAlignmentText = HorizontalAlignmentText.Center,
                        VerticalAlignmentText = VerticalAlignmentText.Center
                    };
                    //var priceUSD = new Exp() { NameRow = "Цена в USD", TypeExp = TypeExp.DOUBLE };

                    list.Add(ids);
                    list.Add(names);
                    list.Add(images);
                    list.Add(WeightVolumeUnits);
                    list.Add(Price_Byn_units);
                    list.Add(Price_Byn);
                    list.Add(Sostav);
                    list.Add(Sizes);
                    foreach (var prod in List)
                    {
                        ids.ValueList.Add(prod.Id.ToString());
                        names.ValueList.Add(prod.Name);
                        images.ValueList.Add(prod.ImageBit);
                        Price_Byn.ValueList.Add(prod.Price_Byn);
                        WeightVolumeUnits.ValueList.Add(prod.WeightVolumeUnits.ToString());
                        Price_Byn_units.ValueList.Add(prod.Price_Byn_unit);
                        Sostav.ValueList.Add(prod.ExtraField2);
                        Sizes.ValueList.Add(prod.ExtraField1);
                    }
                    
                    if (saveFileDialog1.FileName != String.Empty)
                    {
                        var pathDirectory=System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                        ImportExport.CreateExcelDocument(pathDirectory,System.IO.Path.GetFileName(saveFileDialog1.FileName), list);
                    }
                    else
                    {
                        
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "price.xls"))
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "price.xls");
                        ImportExport.CreateExcelDocument(AppDomain.CurrentDomain.BaseDirectory, filename, list);
                    }

                    MessageBox.Show("Прайс лист успешно создан", "Задача успешно выполнена", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                });
                Store.CreateOrGet<PriceViewModel>().BussinessProcess = false;
            }

        }

        public static async Task<ObservableCollection<ProductViewModel>> GetProducts()
        {
            //await Task.Delay(999);
            var resultLIst = new ObservableCollection<ProductViewModel>();
            //{
            //    new ProductViewModel {Name = "dsdsa"},new ProductViewModel {Name = "dsdsa2"}
            //};

            using (var bc = new ImExPriceBusinessContext(ModeApp.Instance))
            {
                Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                    "Установка соединения с веб-сайтом:  http://monnaanna.by";
                var res = await bc.GetProducts();
                var resFieldValues = await bc.GetExtraFieldsValuesAsync();
                await TaskHelper.RunAsync(() =>
                {
                    var itemNumber = 0;

                    if (!Directory.Exists("Cache"))
                    {
                        Directory.CreateDirectory("Cache");
                    }

#if !DEBUG
                    var items = res.FindAll(x => x.ProductPublish == 1);// && x.ImageUrl.Contains("R"));
                    #else
                    var items = res.FindAll(x => x.ProductPublish == 1 && x.ImageUrl.Contains("R"));
#endif
                    Bitmap bitmap = null;
                    using (var clientLogo = new WebClient())
                    {
                        using (var stream =
                            clientLogo.OpenRead(
                                "http://monnaanna.by/images/MonnaAnna/logo.png"))
                        {
                            if (stream != null || stream.Length != 0)
                            {
                                bitmap = new Bitmap(stream);

                                Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                                    "Загрузка логотипа компании с веб-сайта:  http://monnaanna.by" +
                                    "http://monnaanna.by/images/MonnaAnna/logo.png";

                                //bitmap = new Bitmap(bitmap, bitmap.Width / 4, bitmap.Height / 4);

                                if (bitmap != null)
                                    bitmap.Save("Cache\\" + "logo.png", ImageFormat.Png);

                                stream.Flush();
                            }

                            stream.Close();
                        }
                    }

                    foreach (var r in items) //.FindAll(x=>x.ImageUrl.Contains("")))
                    {
                        itemNumber++;
                        Store.CreateOrGet<PriceViewModel>().IsPercent = false;
                        Store.CreateOrGet<PriceViewModel>().PercentProcess = 100 * itemNumber / items.Count;
                        Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                            "Скачивание и уменьшение размера файла изображения " + r.ImageUrl +
                            " c веб-сайта:  http://monnaanna.by" +
                            " ( " + itemNumber + " из " +
                            items.Count + " )"; // + ". Процент выполнения " +
                        //Store.CreateOrGet<PriceViewModel>().PercentProcess;

                        try
                        {
                            using (var client = new WebClient())
                            {
                                using (var stream =
                                    client.OpenRead(
                                        "http://monnaanna.by/components/com_jshopping/files/img_products/" +
                                        r.ImageUrl))
                                {
                                    if (stream != null || stream.Length != 0)
                                    {
                                        bitmap = new Bitmap(stream);

                                        //Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                                        //    "Уменьшение изображения по пропорции " +
                                        //    r.ImageUrl;

                                        bitmap = new Bitmap(bitmap, bitmap.Width / 4, bitmap.Height / 4);

                                        if (bitmap != null)
                                            bitmap.Save("Cache\\" + r.ImageUrl, ImageFormat.Jpeg);

                                        stream.Flush();
                                    }

                                    stream.Close();
                                    var sostav = resFieldValues.Find(x => x.Id == int.Parse(r.ExtraField2)).NameRu;
                                    var size = r.ExtraField1.Trim().Split(',');
                                    var sizeResStr = "";
                                    foreach (var val in size)
                                        if (sizeResStr == string.Empty)
                                            sizeResStr = resFieldValues.Find(x => x.Id == int.Parse(val)).NameRu.Trim();
                                        else
                                            sizeResStr = sizeResStr + ", " +
                                                         resFieldValues.Find(x => x.Id == int.Parse(val)).NameRu.Trim();
                                    resultLIst.Add(new ProductViewModel
                                    {
                                        Id = r.Id,
                                        Name = r.Name,
                                        ImageBit = AppDomain.CurrentDomain.BaseDirectory + "Cache\\" + r.ImageUrl,
                                        PriceUsd = r.ProductPrice.ToString(),
                                        WeightVolumeUnits = r.WeightVolumeUnits,
                                        ExtraField1 = sizeResStr,
                                        ExtraField2 = sostav
                                    });
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                                "Ошибка при чтение файла с изображением " +
                                r.ImageUrl + " ( " + itemNumber + " из " +
                                items.Count + " )";
                        }
                    }


                    //var c = new WebClient();
                    //    var bytes = c.DownloadData(("http://monnaanna.by/components/com_jshopping/files/img_products/" +
                    //                                                   r.ImageUrl));
                    //    var ms = new MemoryStream(bytes);

                    //    var bi = new BitmapImage();
                    //    bi.BeginInit();
                    //    bi.StreamSource = ms;
                    //    bi.EndInit();

                    //c.Dispose();
                });
                /*
                 * var res=await bc.GetProducts();
                    foreach (var r in res)
                    {
                        if(r.ProductPublish>0)
                        resultLIst.Add(new ProductViewModel(){Id=r.Id, ProductEan =r.ProductEan, Name=r.Name,PriceUsd= r.ProductPrice.ToString(), ProductQuantity = r.ProductQuantity.ToString(), WeightVolumeUnits= r.WeightVolumeUnits, ProductPublish = r.ProductPublish });
                    }
                 */
            }

            return resultLIst;
        }

        public Image ResizeOrigImg(Image image, int nWidth, int nHeight, out int newSizeImage, out bool isWidth)
        {
            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = nWidth;
                newHeight = image.Height * nWidth / image.Width;
                newSizeImage = newHeight;
                isWidth = true;
            }
            else
            {
                newHeight = nHeight;
                newWidth = image.Width * nWidth / image.Height;
                newSizeImage = newWidth;
                isWidth = false;
            }

            Image result = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.Dispose();
            }

            return result;
        }

        public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public async Task LoadData()
        {
            Store.CreateOrGet<PriceViewModel>().BussinessProcess = true;
            Store.CreateOrGet<PriceViewModel>().List = await GetProducts();
            Store.CreateOrGet<PriceViewModel>().BussinessProcess = false;
        }
    }

    public class ImportExport
    {
        public static bool CreateExcelDocument(string directoryPath, string filename, List<Exp> dictionary)
        {
            try
            {
                var xlApp = new Application();
                Workbook xlWorkBook;
                Worksheet xlWorkSheet;
                object misValue = Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Worksheet) xlWorkBook.Worksheets.get_Item(1);

                var emptyLine = 4;
                var countImage = 0;

                // Выделяем диапазон ячеек от H1 до K1         
                var _excelCells1 = xlWorkSheet.get_Range("A1", "B1").Cells;
                // Производим объединение
                _excelCells1.Merge(Type.Missing);
                var oRangeLogo = (Range) xlWorkSheet.Cells[1, 1];
                var img2 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Cache\\" + "Logo.png");
                var Left2 = (float) (double) oRangeLogo.Left;
                var Top2 = (float) (double) oRangeLogo.Top;
                //const float ImageSize = 32;
                oRangeLogo.RowHeight = img2.Height + 10;
                oRangeLogo.ColumnWidth = 30; // (img.Width / 10 - 12 + 5) / 7d + 1;//From pixels to inches.
                xlWorkSheet.Shapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "Cache\\" + "Logo.png",
                    MsoTriState.msoFalse, MsoTriState.msoCTrue, Left2, Top2, img2.Width, img2.Height);

                // Выделяем диапазон ячеек от H1 до K1         
                var _excelCells2 = xlWorkSheet.get_Range("A2", "F2").Cells;
                // Производим объединение
                _excelCells2.Merge(Type.Missing);
                var str = "Наименование: Индивидуальный предприниматель Сугак Надежда Анатольевна" + "\r\n" +
                          "УНП: 190230096" +
                          "Наш адрес: г.Минск, ул.Некрасова, дом 35 корпус 1" + "\r\n" +
                          "График работы: Без выходных: c 9:00 до 21:00" + "\r\n" +
                          "Телефоны: +375 29 6663173(VELCOM) + 375 29 1970178(VELCOM)" + "\r\n" +
                          "nadski @yandex.ru";
                _excelCells2.RowHeight = 78;
                xlWorkSheet.Cells[2, 1] = str;
                //_excelCells2.AutoFit();

                // Выделяем диапазон ячеек от H1 до K1         
                var _excelCells3 = xlWorkSheet.get_Range("A3", "F3").Cells;
                // Производим объединение
                _excelCells3.Merge(Type.Missing);
                // xlWorkSheet.Cells[3, 1] = "Общие";

                // Выделяем диапазон ячеек от H1 до K1         
                var _excelCells4 = xlWorkSheet.get_Range("A4", "F4").Cells;
                // Производим объединение
                _excelCells4.Merge(Type.Missing);
                xlWorkSheet.Cells[4, 1] = "Официальный сайт: http://monnaanna.by";

                //// Выделяем диапазон ячеек от O1 до Q1         
                //Microsoft.Office.Interop.Excel.Range _excelCells2 = (Microsoft.Office.Interop.Excel..Range)xlWorkSheet.get_Range("O1", "Q1").Cells;
                //// Производим объединение
                //_excelCells2.Merge(Type.Missing);
                //xlWorkSheet.Cells[1, 15] = "Общие";

                for (var i = 0; i < dictionary.Count; i++)
                {
                    var j = 2 + emptyLine;
                    xlWorkSheet.Cells[j - 1, i + 1] = dictionary[i].NameRow;
                    foreach (var value in dictionary[i].ValueList)
                    {
                        countImage++;
                        Store.CreateOrGet<PriceViewModel>().IsPercent = false;
                        Store.CreateOrGet<PriceViewModel>().PercentProcess =
                            100 * countImage / (dictionary.Count * dictionary[i].ValueList.Count);
                        Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                            "Обработка строк документа " + countImage + "  из " +
                            dictionary.Count * dictionary[i].ValueList.Count;
                        if (dictionary[i].TypeExp == TypeExp.STRING)
                        {
                            var oRange = (Range) xlWorkSheet.Cells[j, i + 1];
                            oRange.ColumnWidth = 30;

                            if (dictionary[i].HorizontalAlignmentText == HorizontalAlignmentText.Center)
                                oRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                                oRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            oRange.ColumnWidth = 30;
                            xlWorkSheet.Cells[j, i + 1] = value;
                        }
                        else if (dictionary[i].TypeExp == TypeExp.DOUBLE)
                        {
                            var oRange = (Range) xlWorkSheet.Cells[j, i + 1];
                            if (dictionary[i].HorizontalAlignmentText == HorizontalAlignmentText.Center)
                                oRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                                oRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            oRange.ColumnWidth = 30;
                            xlWorkSheet.Cells[j, i + 1] = double.Parse(value);
                        }
                        else if (dictionary[i].TypeExp == TypeExp.INT)
                        {
                            var oRange = (Range) xlWorkSheet.Cells[j, i + 1];
                            if (dictionary[i].HorizontalAlignmentText == HorizontalAlignmentText.Center)
                                oRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                                oRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            oRange.ColumnWidth = 30;
                            xlWorkSheet.Cells[j, i + 1] = int.Parse(value);
                        }
                        else if (dictionary[i].TypeExp == TypeExp.IMAGE)
                        {
                            ////Image oImage = Image.FromFile(value);
                            ////System.Windows.Forms.Clipboard.SetDataObject(oImage, true);
                            ////ThisSheet.Paste(oRange, _stLOGO);
                            //object missing = System.Reflection.Missing.Value;
                            //Microsoft.Office.Interop.Excel.Pictures p = xlWorkSheet.Pictures(missing) as Microsoft.Office.Interop.Excel.Pictures;
                            //Microsoft.Office.Interop.Excel.Picture pic = null;
                            ////Microsoft.Office.Interop.Excel.Range picPosition = GetPicturePosition(); // retrieve the range for picture insert
                            //pic = p.Insert(value, missing);
                            ////pic.Left = Convert.ToDouble(picPosition.Left);
                            ////pic.Top = Convert.ToDouble(picPosition.Top);

                            //pic = p.Insert(value, missing);
                            //xlWorkSheet.Cells[j, i + 1] = pic;
                            ////xlWorkSheet.Cells[j, i + 1].Shapes.AddPicture(value, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);
                            ////xlWorkSheet.Cells[j, i + 1] = int.Parse(value);
                            var oRange = (Range) xlWorkSheet.Cells[j, i + 1];
                            var img = Image.FromFile(value);
                            var Left = (float) (double) oRange.Left;
                            var Top = (float) (double) oRange.Top;
                            //const float ImageSize = 32;
                            oRange.RowHeight = img.Height / 3;
                            oRange.ColumnWidth = 30; // (img.Width / 10 - 12 + 5) / 7d + 1;//From pixels to inches.
                            xlWorkSheet.Shapes.AddPicture(value, MsoTriState.msoFalse, MsoTriState.msoCTrue, Left, Top,
                                img.Width / 3, img.Height / 3);
                        }

                        j++;
                    }
                }

                //worksheet.AllocatedRange.AutoFitColumns();

                //xlWorkSheet.Columns.AutoFit();
                //xlWorkSheet.Rows.AutoFit();

                xlWorkSheet.Name = "Прайс";
                //xlWorkSheet.Shapes.AddPicture("C:\\csharp-xl-picture.JPG", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);
                xlWorkBook.SaveAs(
                    directoryPath + filename,
                    XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                    misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        //private void PlacePicture(Image picture, Range destination)
        //{
        //    Worksheet ws = destination.Worksheet;
        //    Clipboard.SetImage(picture);
        //    ws.Paste(destination, false);
        //    Pictures p = ws.Pictures(System.Reflection.Missing.Value) as Pictures;
        //    Picture pic = p.Item(p.Count) as Picture;
        //    ScalePicture(pic, (double)destination.Width, (double)destination.Height);
        //}

        //private void ScalePicture(Picture pic, double width, double height)
        //{
        //    double fX = width / pic.Width;
        //    double fY = height / pic.Height;
        //    double oldH = pic.Height;
        //    if (fX < fY)
        //    {
        //        pic.Width *= fX;
        //        if (pic.Height == oldH) // no change if aspect ratio is locked
        //            pic.Height *= fX;
        //        pic.Top += (height - pic.Height) / 2;
        //    }
        //    else
        //    {
        //        pic.Width *= fY;
        //        if (pic.Height == oldH) // no change if aspect ratio is locked
        //            pic.Height *= fY;
        //        pic.Left += (width - pic.Width) / 2;
        //    }
        //}
    }
}