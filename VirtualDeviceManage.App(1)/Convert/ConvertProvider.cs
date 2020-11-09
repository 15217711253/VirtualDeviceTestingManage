using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App
{
    public static class ConvertProvider
    {
        /// <summary>
        /// 16位bool数组转为16进制字符串
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public static byte[] ConvertBoolListToByte(List<bool> bo)
        {
            int len = bo.Count;
            int value = 0; 
            
            if (len <= 16 && len > 8)
            {
                byte[] bytes = new byte[2];
                foreach (bool b in bo)
                {
                    value = (value << 1) + (b ? 1 : 0);
                }
                if ((byte)((value >> 8) & 0xFF) != 0)
                    bytes[0] = ((byte)((value >> 8) & 0xFF));
                if ((byte)((value) & 0xFF) != 0)
                    bytes[1] = ((byte)((value) & 0xFF));
                return bytes;

            }
            else  
            {
                byte[] bytes = new byte[1];
                foreach (bool b in bo)
                {
                    value = (value << 1) + (b ? 1 : 0);
                }
                if ((byte)((value) & 0xFF) != 0)
                    bytes[0] = ((byte)((value) & 0xFF));
                return bytes;

            }
            
        }

        /// <summary>
        /// 将0~FFFF内的整数转为Bytes字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ConvertIntToByte(int value)
        {
            if (value > 255 && value < 65535)
            {
                byte[] bytes = new byte[2];
                if ((byte)((value >> 8) & 0xFF) != 0)
                    bytes[0] = ((byte)((value >> 8) & 0xFF));
                if ((byte)((value) & 0xFF) != 0)
                    bytes[1] = ((byte)((value) & 0xFF));
                return bytes;
            }
            else
            {
                byte[] bytes = new byte[1];
                if ((byte)((value) & 0xFF) != 0)
                    bytes[0] = ((byte)((value) & 0xFF));
                return bytes;
            }
            
        }
    }

}
