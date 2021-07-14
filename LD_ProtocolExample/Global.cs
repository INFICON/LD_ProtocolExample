namespace LD_P
{
    using System;

    public partial class LD_ProtocolExample
    {
        /// <summary>
        /// Calculate 8-Bit CRC
        /// </summary>
        /// <param name="CRCData">ByteArray with input data</param>
        /// <param name="CRCLen">Number of data bytess</param>
        /// <returns>Calculated CRC</returns>
        public static byte CalculateCRC(byte[] CRCData, byte CRCLen)
        {
            Byte[] u8TableCRC =         // lookup table for CRC calculations (CRC value (X^8 + X^5 + X^4 + 1))
            {                           // CRC-8-Dallas/Maxim, DOWCRC, 0x98
                0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
                157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
                35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
                190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
                70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
                219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
                101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
                248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
                140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
                17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
                175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
                50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
                202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
                87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
                233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
                116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53
            };

            byte CRC = 0;
            for (byte u8Idx = 0; u8Idx < CRCLen; u8Idx++)
            {
                CRC = u8TableCRC[CRC ^ CRCData[u8Idx]];
            }

            return (CRC);
        }

        /// <summary>
        /// Complete transmission data
        /// TxDataLge contains the length of the data section
        /// The data (and possibly also the array index) are available in the send buffer starting with TxArray[5]
        /// </summary>
        private void CompleteTransmitData()
        {

            TxLge = (byte)(4 + TxDataLge);
            tbTxLge.Text = Convert.ToString(TxLge);

            TxCmd = Convert.ToUInt16(numCommandNr.Value + (TX_CMD_ARRAY[cbCommand.SelectedIndex] << 8));    // Command No + Type

            TxArray[0] = ENQ;
            TxArray[1] = TxLge;
            TxArray[2] = 1; // always 1
            TxArray[3] = Convert.ToByte((TxCmd >> 8) & 0xff);   // Command high byte
            TxArray[4] = Convert.ToByte(TxCmd & 0xff);          // Command low byte

            TxArray[TxLge + 1] = CalculateCRC(TxArray, Convert.ToByte(TxLge + 1));
            string TxString = BitConverter.ToString(TxArray, 0, 1);
            for (int i = 1; i <= (TxLge + 1); i++)
            {
                TxString += ("-" + BitConverter.ToString(TxArray, i, 1));
            }

            tbSendData.Text = TxString;
        }


        /// <summary>
        /// get the error code in plain text
        /// </summary>
        /// <returns>Error-Text</returns>
        private string GetSyntaxError()
        {
            string Result;

            if (cbSyntax.Checked == true)   // Syntax Fehler
            {
                switch (RxArray[6])         // contains the error code
                {
                    case (byte)ErrCode.CRC:
                        Result = "CRC";
                        break;

                    case (byte)ErrCode.CMD_ILLEGAL:
                        Result = "CMD_ILLEGAL";
                        break;

                    case (byte)ErrCode.LEN:
                        Result = "LEN";
                        break;

                    case (byte)ErrCode.DATA_LENGTH:
                        Result = "DATA_LENGTH";
                        break;

                    case (byte)ErrCode.NO_READ:
                        Result = "NO_READ";
                        break;

                    case (byte)ErrCode.NO_WRITE:
                        Result = "NO_WRITE";
                        break;

                    case (byte)ErrCode.ARRAY_INDEX:
                        Result = "ARRAY_INDEX";
                        break;

                    case (byte)ErrCode.CONTROL:
                        Result = "CONTROL";
                        break;

                    case (byte)ErrCode.PASSWORD:
                        Result = "PASSWORD";
                        break;

                    case (byte)ErrCode.CMD_NOT_ALLOWED:
                        Result = "CMD_NOT_ALLOWED";
                        break;

                    case (byte)ErrCode.DATA:
                        Result = "DATA";
                        break;

                    case (byte)ErrCode.NO_DATA:
                        Result = "NO_DATA";
                        break;

                    default:
                        Result = "??? " + BitConverter.ToString(RxArray, 6, 1);
                        break;
                }
            }
            else
            {
                Result = null;
            }

            return (Result);
        }

    }
}