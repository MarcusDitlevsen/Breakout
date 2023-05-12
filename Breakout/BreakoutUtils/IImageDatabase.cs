using DIKUArcade.Graphics;

namespace Breakout.BreakoutUtils
{

public interface IImageDatabase {
// Retrieves an image corresponding to a filename.
// Precondition: filename != null, filename is a string without path separator
// Postcondition: if image corresponding to filename has been previously loaded, return it;
// if file exists but hasn't been loaded yet, cache and return its contents;
// otherwise (file does not exist), raise FileNotFoundException
public IBaseImage GetImage(string filename);
// Checks to see if an image with given filename has been cached.
// Precondition: filename != null, filename is a string without path separator
// Postcondition: if file is in the cache of previously loaded files, return true;
// otherwise return false
public bool HasImage(string filename);
}
}