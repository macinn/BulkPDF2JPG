using ImageMagick;
class Program
{
    static void Main(String[] args)
    {
        //string dirPath = @"C:\Users\marci\OneDrive - Politechnika Warszawska\Pulpit\arkusze";
        //string outputPath = @"C:\Users\marci\OneDrive - Politechnika Warszawska\Pulpit";

        string dirPath = args[0];       // Directory with PDFs
        string outputPath = args[1];    // Directory, where outupt directory will be created

        var settings = new MagickReadSettings();
        settings.Density = new Density(300, 300);

        using (var images = new MagickImageCollection())
        {
            // Add all the pages of the pdf file to the collection
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            DirectoryInfo outputDir = Directory.CreateDirectory($"{outputPath}\\PDFtoJPG");
            foreach (var file in directory.GetFiles())
            {
                if (!file.Extension.Equals(".pdf")) continue;
                Console.WriteLine(file.Name);
                DirectoryInfo currentDir = Directory.CreateDirectory($"{outputDir.FullName}\\{Path.GetFileNameWithoutExtension(file.Name)}");

                images.Read(file.FullName, settings);
                var page = 1;
                foreach (var image in images)
                {

                    image.Format = MagickFormat.Jpg;
                    image.Write($"{currentDir.FullName}\\Page{page}.jpg");
                    page++;
                }
            }
        }
    }
}

    