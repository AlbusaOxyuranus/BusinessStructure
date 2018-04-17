using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BS.Vms.JShoping.ImExPrice.BAL;
using BS.Vms.ViewModels.price;
using Microsoft.Office.Interop.Excel;

namespace BS.Vms.ViewModels
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
        public string NameRow { get; set; }
        public TypeExp TypeExp { get; set; }
        public List<string> ValueList { get; set; }
        public int Width { get; set; }
        public HorizontalAlignmentText HorizontalAlignmentText { get; set; }
        public VerticalAlignmentText VerticalAlignmentText { get; set; }
        
        public Exp()
        {
            TypeExp = TypeExp.STRING;
            ValueList = new List<string>();

        }
    }

    public class PriceViewModel : ViewModelBase
    {
        private ObservableCollection<ProductViewModel> _list;
        public IAsyncCommand CreatePriceExcelCommand { get; }

        public PriceViewModel()
        {
            _list = new ObservableCollection<ProductViewModel>();
            CreatePriceExcelCommand = AsyncCommand.Create(CreatePriceExcel);
        }

        private async Task CreatePriceExcel()
        {
            Store.CreateOrGet<PriceViewModel>().BussinessProcess = true;
            await TaskHelper.RunAsync(() =>
            {
                
                List<Exp> expList = new List<Exp>();

                List<Exp> list = new List<Exp>();
                var ids = new Exp() { NameRow = "№", TypeExp = TypeExp.INT, VerticalAlignmentText = VerticalAlignmentText.Center };
                var names = new Exp() { NameRow = "Наименование товара",VerticalAlignmentText = VerticalAlignmentText.Center};
                var images = new Exp() { NameRow = "Изображение", TypeExp = TypeExp.IMAGE};
                var WeightVolumeUnits = new Exp() { NameRow = "Количество в упаковке", TypeExp = TypeExp.INT, HorizontalAlignmentText = HorizontalAlignmentText.Center, VerticalAlignmentText = VerticalAlignmentText.Center };
                var Price_Byn_units = new Exp() { NameRow = "Цена за единицу товара BYN", TypeExp = TypeExp.DOUBLE, HorizontalAlignmentText = HorizontalAlignmentText.Center, VerticalAlignmentText = VerticalAlignmentText.Center };
                var Price_Byn = new Exp()
                {
                    NameRow = "Цена за упаковку BYN", TypeExp = TypeExp.DOUBLE , HorizontalAlignmentText = HorizontalAlignmentText.Center
                    ,VerticalAlignmentText = VerticalAlignmentText.Center
                };
                //var priceUSD = new Exp() { NameRow = "Цена в USD", TypeExp = TypeExp.DOUBLE };

                list.Add(ids);
                list.Add(names);
                list.Add(images);
                list.Add(WeightVolumeUnits);
                list.Add(Price_Byn_units);
                list.Add(Price_Byn);
                //list.Add(priceUSD);
                foreach (var prod in this.List)
                {
                    ids.ValueList.Add(prod.Id.ToString());
                    names.ValueList.Add(prod.Name);
                    images.ValueList.Add(prod.ImageBit);
                    //priceUSD.ValueList.Add(prod.PriceUsd);
                   
                    WeightVolumeUnits.ValueList.Add(prod.WeightVolumeUnits.ToString());
                    Price_Byn_units.ValueList.Add(prod.Price_Byn_unit);
                    Price_Byn.ValueList.Add(prod.Price_Byn);
                }
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "price.xls"))
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "price.xls");
                ImportExport.CreateExcelDocument(System.AppDomain.CurrentDomain.BaseDirectory, list);
            });
            Store.CreateOrGet<PriceViewModel>().BussinessProcess = false;
        }

        public ObservableCollection<ProductViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
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
                var res = await bc.GetProducts();
                await TaskHelper.RunAsync(() =>
                {
                    var itemNumber = 0;

                    if (!Directory.Exists("Cache"))
                    {
                        Directory.CreateDirectory("Cache");

                        
           
                    }

                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Cache");

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }

                    var items = res.FindAll(x => x.ProductPublish == 1);// && x.ImageUrl.Contains("k"));
                    foreach (var r in items) //.FindAll(x=>x.ImageUrl.Contains("")))
                    {
                        itemNumber++;
                        Store.CreateOrGet<PriceViewModel>().IsPercent = false;
                        Store.CreateOrGet<PriceViewModel>().PercentProcess =  100 * itemNumber/ items.Count;
                        Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                            "Чтение файла с изображением " +
                            r.ImageUrl + " ( " + itemNumber + " из " +
                            items.Count + " )";// + ". Процент выполнения " +
                            //Store.CreateOrGet<PriceViewModel>().PercentProcess;
                        Bitmap bitmap = null;
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

                                        Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage =
                                            "Уменьшение изображения по пропорции " +
                                            r.ImageUrl;

                                        bitmap = new Bitmap(bitmap, bitmap.Width/4, bitmap.Height/4);

                                        if (bitmap != null)
                                            bitmap.Save("Cache\\" + r.ImageUrl, ImageFormat.Jpeg);

                                        stream.Flush();
                                    }

                                    stream.Close();
                                    resultLIst.Add(new ProductViewModel
                                    {
                                        Id = r.Id,
                                        Name = r.Name,
                                        ImageBit = System.AppDomain.CurrentDomain.BaseDirectory + "Cache\\" + r.ImageUrl,
                                        PriceUsd = r.ProductPrice.ToString(),
                                        WeightVolumeUnits = r.WeightVolumeUnits
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

        public static bool CreateExcelDocument(string directoryPath, List<Exp> dictionary)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int emptyLine = 2;
            int countImage = 0;
            for (int i = 0; i < dictionary.Count; i++)
            {
                int j = 2;
                xlWorkSheet.Cells[j - 1, i + 1] = dictionary[i].NameRow;
                foreach (var value in dictionary[i].ValueList)
                {
                    countImage++;
                    Store.CreateOrGet<PriceViewModel>().IsPercent = false;
                    Store.CreateOrGet<PriceViewModel>().PercentProcess =( 100 * countImage) / (dictionary.Count * dictionary[i].ValueList.Count);
                    Store.CreateOrGet<PriceViewModel>().BussinessProcessMessage = "Обработка  " + countImage +"  из "+ dictionary.Count* dictionary[i].ValueList.Count;
                    if (dictionary[i].TypeExp == TypeExp.STRING)
                    {
                        Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[j, i + 1];
                        oRange.ColumnWidth = 30;

                        if (dictionary[i].HorizontalAlignmentText==HorizontalAlignmentText.Center)
                            oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                            oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        xlWorkSheet.Cells[j, i + 1] = value;
                       
                    }
                    else if (dictionary[i].TypeExp == TypeExp.DOUBLE)
                    {
                        Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[j, i + 1];
                        if (dictionary[i].HorizontalAlignmentText == HorizontalAlignmentText.Center)
                            oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                            oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        xlWorkSheet.Cells[j, i + 1] = double.Parse(value);
                    }
                    else if (dictionary[i].TypeExp == TypeExp.INT)
                    {
                        Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[j, i + 1];
                        if (dictionary[i].HorizontalAlignmentText == HorizontalAlignmentText.Center)
                            oRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (dictionary[i].VerticalAlignmentText == VerticalAlignmentText.Center)
                            oRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                        Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[j, i + 1];
                        System.Drawing.Image img = System.Drawing.Image.FromFile(value);
                        float Left = (float)((double)oRange.Left);
                        float Top = (float)((double)oRange.Top);
                        //const float ImageSize = 32;
                        oRange.RowHeight = img.Height / 2;
                        oRange.ColumnWidth = 30;// (img.Width / 10 - 12 + 5) / 7d + 1;//From pixels to inches.
                        xlWorkSheet.Shapes.AddPicture(value, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, img.Width/2, img.Height/2);
                    }
                    j++;
                }
            }

            //worksheet.AllocatedRange.AutoFitColumns();
            
            //xlWorkSheet.Columns.AutoFit();
            //xlWorkSheet.Rows.AutoFit();

            xlWorkSheet.Name = "Прайс";
            //xlWorkSheet.Shapes.AddPicture("C:\\csharp-xl-picture.JPG", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);
            xlWorkBook.SaveAs(directoryPath + "price.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            return true;
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