using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<data> datas;
            datas = new List<data>() {
            new data (){
               id="101010101",
                date="[00:30],[01:30]",
                mode="mode1"

            },
            new data (){
               id="101010101",
                date="[00:30],[01:30]",
                mode="mode1"

            },
            new data (){
               id="101010101",
                date="[00:30],[01:30]",
                mode="mode1"

            },new data (){
               id="101010101",
                date="[00:30],[01:30]",
                mode="mode1"

            }
            };
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            var Json = jsonSerialize.Serialize(datas);
        } 

    }
    public class data
   {
        public string id;
        public string date;
        public string mode;

    }

}
