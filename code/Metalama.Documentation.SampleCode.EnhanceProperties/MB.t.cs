using Doc.MB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.MB
{
    public class Storage
    {
        private long _mediaSize;
        [MB]
        public long MediaSize
        {
            get
            {
                return _mediaSize;
            }
            set
            {
                this._mediaSize = value;
                this._mediaSize = Convert.ToInt64(this._mediaSize / 1000000);
            }
        }
        public long PhotoSize { get; set; }
    }
    public class DefaultStringDemo
    {
        public static void Main(string[] args)
        {
            Storage s = new Storage();
            s.MediaSize = 6000000;
            Console.WriteLine($"Media size is {s.MediaSize} MB");
        }
    }
}