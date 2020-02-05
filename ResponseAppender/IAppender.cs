using System;
using System.Collections.Generic;
using System.Text;

namespace ResponseAppender
{
   public interface IAppender
    {
        void AddtoFile(Type t, string Message,string path);
    }
}
