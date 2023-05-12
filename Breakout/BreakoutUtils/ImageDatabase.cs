using DIKUArcade.Graphics;

 
namespace Breakout.BreakoutUtils
{
    public class ImageDatabase : IImageDatabase
    {

        private readonly string folder;

 
        public ImageDatabase(string folder) {
            this.folder = folder;
        }

        //There are better descriptions of these methods in the interface class

        ///<Summary> Retrieves an image corresponding to a filename </summary>
        ///<param name = "filename"> The name of the file to retrieve </param>
        ///<returns> A retrieved file </returns>
        public IBaseImage GetImage(string filename) {

            return new Image(Path.Combine(folder, filename));

        }


        ///<summary> Checks if an image has been cached </summary>
        ///<param name = "filename"> The name of the file to check</param>
        ///<returns> A path to the existing file </returns>
        public bool HasImage(string filename) {
            var dir = new DirectoryInfo(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin")

            {
                dir = dir.Parent;
            }
    
            dir = dir.Parent;

            var path = Path.Combine(Path.Combine(dir.FullName.ToString(), folder, filename));

            return File.Exists(path);

            //Taken from DIKUArcade
        }
    }
}