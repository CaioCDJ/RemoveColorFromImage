open System
open System.IO
open System.Drawing
open System.Drawing.Imaging

let files = 
    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*",SearchOption.TopDirectoryOnly)

let pictures = query{
    for file in files do
    where ( file.EndsWith(".jpg",StringComparison.CurrentCultureIgnoreCase) ||
         file.EndsWith(".png",StringComparison.CurrentCultureIgnoreCase))
    select file
    }

for path in pictures do
    
    let img = new Bitmap(path)

    let height:int = img.Size.Height
    let width:int = img.Size.Width

    // usando como   
    let colorComp = Color.FromArgb(150,150,150)

    for w = 0 to width - 1 do
        for h = 0 to height - 1  do
            let pixelColor = img.GetPixel(w,h);

            if ((pixelColor.R >= colorComp.R && pixelColor.B >= colorComp.B) 
                || (pixelColor.R >= colorComp.R && pixelColor.G >= colorComp.G)
                || (pixelColor.G >= colorComp.G && pixelColor.B >= colorComp.B)
                ) then
                img.SetPixel(w,h,Color.Transparent) 

    img.MakeTransparent(Color.White)

    let fileName = Path.GetFileName(path);

    File.Delete(path);

    img.Save(fileName,ImageFormat.Png)

    printf $"Conversao finalizada: {path}\n"