using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace VastraIndiaDAL
{
    public class SaveImageDAL
    {


        public async Task SaveImagesAsync(IFormFile formFile, string FileName, string FolderName)
        {
            try
            {
                var file = formFile;

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                if (file != null)
                {
                    var fileName = FileName;
                     var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(FolderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }

            catch (Exception ex)
            {
               
            }
        }

        public async Task SaveProductImagesAsync(IFormFile formFile, IFormFile File1, IFormFile File2, string FileName, string SidephotoName, string BackphotoName, string FolderName)
        {
            try
            {
            
                var file = formFile;

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                if (file != null)
                {
                    var fileName = FileName;
                   var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(FolderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                var file1 = File1;

                if (file1 != null)
                {
                    var fullPath = Path.Combine(pathToSave, SidephotoName);
                    var dbPath = Path.Combine(FolderName, SidephotoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file1.CopyTo(stream);
                    }
                }


                var file2 = File2;

                if (file2 != null)
                {
                     var fullPath = Path.Combine(pathToSave, BackphotoName);
                    var dbPath = Path.Combine(FolderName, BackphotoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file2.CopyTo(stream);
                    }
                }
            }

            catch (Exception ex)
            {
                
            }
        }

        public async Task SaveSizeChartImageAsync(IFormFile formFile, IFormFile File1,  string Mensizechartr, string Womensizechart,string FolderName)
        {
            try
            {
               var file = formFile;

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                if (file != null)
                {
                    var fileName = Mensizechartr;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(FolderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                var file1 = File1;

                if (file1 != null)
                {
                   var fullPath = Path.Combine(pathToSave, Womensizechart);
                    var dbPath = Path.Combine(FolderName, Womensizechart);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file1.CopyTo(stream);
                    }
                }


            }

            catch (Exception ex)
            {
                
            }
        }

        public async Task SaveProductImageAsync(IFormFile FrontImgFile, IFormFile MenFrontImage, /*IFormFile MenSideImage, IFormFile MenBackImage,*/ /*IFormFile MenSizeChartImage,*/ string FrontPhoto, string MenFrontImageFile, /*string MenSideImageFile, string MenBackImageFile,*/ /*string MenSizeChartImageFile,*/ IFormFile WomenFrontImage, /*IFormFile WomenSideImage, IFormFile WomenBackImage,*/ /*IFormFile WomenSizeChartImage,*/ string WomenFrontImageFile, /*string WomenSideImageFile, string WomenBakImageFile,*/ /*string WomenSizeChartImageFile,*/ string FolderName)
        {
            try
            {

               
                var file = MenFrontImage;

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
              
                
                if (file != null)
                {
                    var fullPath = Path.Combine(pathToSave, MenFrontImageFile);
                    var dbPath1 = Path.Combine(FolderName, MenFrontImageFile);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                   
                }

                var file1 = FrontImgFile;

                if (file1.Length > 0)
                {
                    var fullPath = Path.Combine(pathToSave, FrontPhoto);
                    var dbPath = Path.Combine(FolderName, FrontPhoto);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file1.CopyTo(stream);
                    }
                }


               

                var file4 = WomenFrontImage;

                if (file4 != null)
                {
                    var fullPath = Path.Combine(pathToSave, WomenFrontImageFile);
                    var dbPath = Path.Combine(FolderName, WomenFrontImageFile);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file4.CopyTo(stream);
                    }
                }

                
               
            }

            catch (Exception ex)
            {
                
            }
        }

        public async Task SaveMultiProductImagesAsync(IFormFile[] MenImages, IFormFile[] WomenImages, IFormFile MenFrontImage, /*IFormFile MenSizeChartImage,*/ IFormFile WomenFrontImage, /*IFormFile WomenSizeChartImage,*/ string MenFrontImageFile, /*string MenSizeChartImageFile,*/ string WomenFrontImageFile,/* string WomenSizeChartImageFile,*/ string FolderName)
        {
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);

                var files = MenImages;

                
                if (files!=null)
                {


                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {

                            var Ext = System.IO.Path.GetExtension(file.FileName);

                            var name = System.IO.Path.GetFileNameWithoutExtension(file.FileName);


                            var MenFrontPhoto = name + DateTime.Now.ToString("-dd-MM-yyyy-HH") + Ext;



                            var fullPath = Path.Combine(pathToSave, MenFrontPhoto);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }


                        }
                    }

                }

                var Womenfiles = WomenImages;

     
                if (Womenfiles != null)
                {


                    foreach (var file in Womenfiles)
                    {
                        if (file.Length > 0)
                        {

                            var Ext = System.IO.Path.GetExtension(file.FileName);

                            var name = System.IO.Path.GetFileNameWithoutExtension(file.FileName);


                            var WomenFrontPhoto = name + DateTime.Now.ToString("-dd-MM-yyyy-HH") + Ext;



                            var fullPath = Path.Combine(pathToSave, WomenFrontPhoto);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }


                        }
                    }

                }

                var file1 = MenFrontImage;


                if (file1 != null)
                {
                    var fullPath = Path.Combine(pathToSave, MenFrontImageFile);
                    var dbPath = Path.Combine(FolderName, MenFrontImageFile);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file1.CopyTo(stream);
                    }
                }

               


                var file3 = WomenFrontImage;

                if (file3 != null)
                {
                    var fullPath = Path.Combine(pathToSave, WomenFrontImageFile);
                    var dbPath = Path.Combine(FolderName, WomenFrontImageFile);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file3.CopyTo(stream);
                    }
                }

               
            }

            catch (Exception ex)
            {
                
            }
        }
    }
}