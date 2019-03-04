using System;
using System.Collections.Generic;
using System.Text;

namespace FormUI
{
    public interface ICommand
    {
        //bool Enable { get; set; }

        void Run();
    }

    public interface ISelectChange
    {
        void Change(string item);
    }
}
