namespace LD_P
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.IO.Ports;                  // for serial port
    using System.Net;
    using System.Globalization;
    using System.Reflection;                // for version query
    using System.Collections;

    public partial class LD_ProtocolExample : Form
    {

        /// <summary>
        /// State of the state machine for receiving characters via serial port
        /// </summary>
        public enum RxStates
        {
            IDLE = 0,
            WAIT_STX,
            WAIT_LGE,
            WAIT_CRC
        };
        RxStates RxState = RxStates.IDLE;

        /// <summary>
        /// LD command error codes
        /// </summary>
        public enum ErrCode
        {
            CRC = 1,
            LEN = 2,
            CMD_ILLEGAL = 10,
            DATA_LENGTH = 11,
            NO_READ = 12,
            NO_WRITE = 13,
            ARRAY_INDEX = 14,
            CONTROL = 20,
            PASSWORD = 21,
            CMD_NOT_ALLOWED = 22,
            DATA = 30,
            NO_DATA = 31
        }

        /// <summary>
        /// LD command data types
        /// </summary>
        public enum DataType
        {
            BOOL = 0,
            SINT8 = 1,
            SINT16 = 2,
            SINT32 = 3,
            UINT8 = 4,
            UINT16 = 5,
            UINT32 = 6,
            CHAR = 7,
            SINT64 = 8,
            UINT64 = 9,
            FLOAT = 10,
            NODATA = 11,
            INDEXMAX = 12
        }

        /// <summary>
        /// LD command datatypes in plain text
        /// </summary>
        string[] DATA_TYPE = new string[(int)DataType.INDEXMAX]
        {
            "Boolean",
            "signed 8 bit",
            "signed 16 bit",
            "signed 32 bit",
            "unsigned 8 bit",
            "unsigned 16 bit",
            "unsigned 32 bit",
            "Character",
            "signed 64 bit",
            "unsigned 64 bit",
            "floating point",
            "no data"
        };

        /// <summary>
        /// "Start of Text" character
        /// </summary>
        const byte STX = 0x02;
        /// <summary>
        /// "Enquiry" character
        /// </summary>
        const byte ENQ = 0x05;
        /// <summary>
        /// Timeout for leak detector response
        /// </summary>
        int RECEIVE_TIMEOUT = 1000;
        int AUTO_SEND_INTERVAL = 100;
        /// <summary>
        /// Max. value of length byte
        /// </summary>
        const int RECEIVE_LGE_MAX = 255 - 7;
        /// <summary>
        /// Position of the first data byte in the buffer
        /// </summary>
        const int COM_S_DATA_I_LD = 6;

        // Definition of Bits 15...13 "Command specifier"
        const byte TX_READ_VAL = 0x00;       
        const byte TX_WRITE_VAL = 0x20;
        const byte TX_READ_VAL_MIN = 0x40;  // read lower limit value
        const byte TX_READ_VAL_MAX = 0x60;  // read upper limit value
        const byte TX_READ_VAL_DEF = 0x80;  // read default value
        const byte TX_READ_NAME = 0xA0;     // read command name in plain text
        const byte TX_READ_INFO = 0xC0;     // read command information

        public enum Command
        {
            CMD_READ_VALUE = 0,
            CMD_WRITE_VALUE = 1,
            CMD_READ_VAL_MIN = 2,
            CMD_READ_VAL_MAX = 3,
            CMD_READ_VAL_DEF = 4,
            CMD_READ_NAME = 5,
            CMD_READ_INFO = 6,
            INDEXMAX = 7
        }

        byte[] TX_CMD_ARRAY = new byte[(int)Command.INDEXMAX]
        {
            TX_READ_VAL,
            TX_WRITE_VAL,
            TX_READ_VAL_MIN,
            TX_READ_VAL_MAX,
            TX_READ_VAL_DEF,
            TX_READ_NAME,
            TX_READ_INFO
        };

        string[] RX_CMD_STRING = new string[(int)Command.INDEXMAX]
        {
            "Read value",
            "Write value",
            "Read lower limit",
            "Read upper limit",
            "Read default value",
            "Read name",
            "Read command info"
        };

        bool AutoSend = false;

        byte TxLge = 0;             // transmit length byte (without STX and LGE, but with CRC)
        byte RxLge = 0;             // receive length byte (without STX and LGE, but with CRC)
        UInt16 TxCmd = 0;           // Transmitted command number 0...11 bit; Bit 12 ununsed; Bit 15...13 for Function
        byte RxCmd = 0;
        UInt16 RxCmdNo = 0;         // Received command number 0...11 bit; Bit 12 ununsed; Bit 15...13 for Function
        byte TxDataLge = 0;         // Length of data part
        string TxDataString;        // Data to send (as string)
        bool TxDataCheck = false;   // Entered data valid?

        /// <summary>
        /// Buffer for data to transmit
        /// </summary>
        byte[] TxArray = new byte[512];
        /// <summary>
        /// Buffer for received data
        /// </summary>
        byte[] RxArray = new byte[256];

        bool bData;
        sbyte s8Data;
        Int16 s16Data;
        Int32 s32Data;
        Int64 s64Data;
        byte u8Data;
        UInt16 u16Data;
        UInt32 u32Data;
        UInt64 u64Data;
        float fData;

        /// <summary>
        /// Number of characters already received
        /// </summary>
        int RxBytesRead;                    
        /// <summary>
        /// Number of characters still missing to receive
        /// </summary>
        int RxBytesMissing;                 
        int RxNextByte;
        string RxString = "";
        string RxDataString = "";

        CultureInfo ci;

        public LD_ProtocolExample()
        {
            InitializeComponent();
        }

        private void LD_ProtoTerm_Load(object sender, EventArgs e)
        {
            Version myVersion;
            string programVersion = null;

            // Get the version of the current application.
            myVersion = Assembly.GetExecutingAssembly().GetName().Version;
            programVersion = "V" + myVersion.Major.ToString() + "." + myVersion.Minor.ToString() + " (Build " + myVersion.Revision.ToString() + ")";
            this.Text += (" " + programVersion);

            toolStripStatusLabel1.Text = null;

            foreach (string CmdItem in RX_CMD_STRING)
            {
                cbCommand.Items.Add(CmdItem);
            }
            cbCommand.SelectedIndex = (int)Command.CMD_READ_VALUE;
            numCommandNr.Value = 0;      // CommandNr

            foreach (string DataTypeItem in DATA_TYPE)
            {
                cbTxDataType.Items.Add(DataTypeItem);
                cbRxDataType.Items.Add(DataTypeItem);
            }
            cbTxDataType.SelectedIndex = (int)DataType.NODATA;
            cbRxDataType.SelectedIndex = (int)DataType.NODATA;

            string[] SerialPorts = SerialPort.GetPortNames();               // all in hardware existing serial ports
            foreach (string port in SerialPorts)                            // for each port
            {
                serialPortWire.PortName = port;                                 // selsect a serial port
                try
                {
                    serialPortWire.Open();                                      // Try to open the serial port
                }
                catch                                                       // Error opening serial port
                {
                    serialPortWire.Close();                                     // close as a precaution
                }
                if (serialPortWire.IsOpen == true)                              // Is port open?
                {
                    cbSerialPorts.Items.Add(port);                          // Add to the list of available ports
                    serialPortWire.Close();                                     // close port
                }
            }
            if (cbSerialPorts.Items.Count == 0)                             // No port avialable?
            {
                MessageBox.Show("No serial port available!", "Check ports");
            }
            else                                                            // There is at least one port available
            {
                cbSerialPorts.SelectedIndex = 0;                            // Offer the first one
            }
            cbBaudrate.SelectedIndex = 0;
            ci = new CultureInfo("en-US");
        }

        private void cbSerialPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPortWire.IsOpen)
            {
                serialPortWire.Close();
            }
            serialPortWire.PortName = cbSerialPorts.SelectedItem.ToString();
            try
            {
                serialPortWire.BaudRate = Convert.ToInt32(cbBaudrate.SelectedItem.ToString());
            }
            catch (Exception)
            {
                serialPortWire.BaudRate = 19200;
            }
            serialPortWire.ReceivedBytesThreshold = 1;  // DataReceived Event as soon as at least one character is received
            serialPortWire.ReadTimeout = -1;            // ReadTimeout does not affect DataReceived Event (would affect ReadBytes)

            serialPortWire.Open();
        }

        private void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPortWire.IsOpen)
            {
                serialPortWire.Close();
            }
            try
            {
                serialPortWire.PortName = cbSerialPorts.SelectedItem.ToString();
                serialPortWire.BaudRate = Convert.ToInt32(cbBaudrate.SelectedItem.ToString());
                serialPortWire.ReceivedBytesThreshold = 1;  // DataReceived Event as soon as at least one character is received
                serialPortWire.ReadTimeout = -1;            // ReadTimeout does not affect DataReceived Event (would affect ReadBytes)

                serialPortWire.Open();
            }
            catch
            { }
        }


        private void LD_ProtoTerm_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPortWire.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Create new data to transmit
            GenerateSendData();

            // Transmit buffer
            SendData();
        }

        private void GenerateSendData()
        {
            string[] TxDataStringSplit;
            int cnt;
            byte[] DataArray4 = new byte[4];

            // Initialize ComboBox for receiving
            switch ((Command)(cbCommand.SelectedIndex))
            {
                case Command.CMD_READ_NAME:
                    TxDataLge = 0;
                    cbRxDataType.SelectedIndex = (byte)DataType.CHAR;
                    break;
                case Command.CMD_WRITE_VALUE:
                    cbRxDataType.SelectedIndex = (byte)DataType.NODATA;
                    break;
                case Command.CMD_READ_INFO:
                    TxDataLge = 0;
                    cbRxDataType.SelectedIndex = (byte)DataType.NODATA;
                    break;
                case Command.CMD_READ_VALUE:
                case Command.CMD_READ_VAL_MIN:
                case Command.CMD_READ_VAL_MAX:
                case Command.CMD_READ_VAL_DEF:
                    break;
            }



            if (cbCommand.SelectedIndex == (int)Command.CMD_READ_VALUE)
            {
                TxDataLge = 0;
                tbData.BackColor = Color.White; // Reading command requires no data

                if (cbIsArray.Checked)
                {
                    // It is an array, first enter the index
                    string TxIndexString = tBArrayIndex.Text.ToLower();
                    if ((byte.TryParse(TxIndexString, out u8Data)) == true)
                    {
                        TxArray[5] = u8Data;
                        TxDataLge = 1;
                        tBArrayIndex.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        TxDataLge = 0;
                        tBArrayIndex.BackColor = Color.LightSalmon;
                    }
                }
            }
            else if (cbCommand.SelectedIndex == (int)Command.CMD_WRITE_VALUE)     // Write data: There must be data and/or array index
            {
                TxDataLge = 0;

                if (cbIsArray.Checked)
                {
                    // It is an array, first enter the index
                    string TxIndexString = tBArrayIndex.Text.ToLower();
                    if ((byte.TryParse(TxIndexString, out u8Data)) == true)
                    {
                        TxArray[5] = u8Data;
                        TxDataLge = 1;
                        tBArrayIndex.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        TxDataLge = 0;
                        tBArrayIndex.BackColor = Color.LightSalmon;
                    }
                }

                TxDataString = tbData.Text.ToLower();

                if (!string.IsNullOrEmpty(TxDataString))
                {
                    // Data are available
                    if ((cbIsArray.Checked == true) && (TxArray[5] == 255))
                    {
                        // Whole array (several values) are to be written
                        // Trim Text
                        TxDataString.Trim();
                        TxDataString.Trim(new Char[] { '{', '}' });
                        if (cbTxDataType.SelectedIndex != (int)DataType.CHAR) // For CHAR only one string in TxDataStringSplit[]
                            TxDataStringSplit = TxDataString.Split(new Char[] { ' ', ',' });
                        else
                            TxDataStringSplit = new string[] { TxDataString };
                    }
                    else
                    {
                        TxDataStringSplit = new string[] { TxDataString };
                    }

                    for (cnt = 0; cnt < TxDataStringSplit.Length; cnt++)
                    {
                        switch (cbTxDataType.SelectedIndex)
                        {
                            case (int)DataType.BOOL:
                                if ((TxDataStringSplit[cnt] == "false") || (TxDataStringSplit[cnt] == "0"))
                                {
                                    TxArray[5 + TxDataLge] = 0;
                                    TxDataLge += 1;
                                    TxDataCheck = true;
                                }
                                else if ((TxDataStringSplit[cnt] == "true") || (TxDataStringSplit[cnt] == "1"))
                                {
                                    TxArray[5 + TxDataLge] = 1;
                                    TxDataLge += 1;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.SINT8:
                                if (((sbyte.TryParse(TxDataStringSplit[cnt], out s8Data)) == true) &&
                                    (rbTxDecimal.Checked == true))
                                {
                                    TxArray[5 + TxDataLge] = (byte)s8Data;
                                    TxDataLge += 1;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.SINT16:
                                if (((Int16.TryParse(TxDataStringSplit[cnt], out s16Data)) == true) &&
                                    (rbTxDecimal.Checked == true))
                                {
                                    s16Data = IPAddress.HostToNetworkOrder(s16Data);        // swap byte order 
                                    Marshal.WriteInt16(TxArray, 5 + TxDataLge, s16Data);    // write to array from index 5 + TxDataLge
                                    TxDataLge += 2;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.SINT32:
                                if (((Int32.TryParse(TxDataStringSplit[cnt], out s32Data)) == true) &&
                                    (rbTxDecimal.Checked == true))
                                {
                                    s32Data = IPAddress.HostToNetworkOrder(s32Data);        // swap byte order 
                                    Marshal.WriteInt32(TxArray, 5 + TxDataLge, s32Data);    // write to array from index 5 + TxDataLge
                                    TxDataLge += 4;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.UINT8:
                                if (rbTxHexadecimal.Checked == true)
                                {
                                    bData = Int32.TryParse(TxDataStringSplit[cnt],
                                                           System.Globalization.NumberStyles.HexNumber,
                                                           CultureInfo.InvariantCulture,
                                                           out s32Data);
                                    if ((bData == true) && (s32Data <= 0xff))
                                    {
                                        TxArray[5 + TxDataLge] = (byte)s32Data;
                                        TxDataLge += 1;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                else        // dezimal
                                {
                                    if ((byte.TryParse(TxDataStringSplit[cnt], out u8Data)) == true)
                                    {
                                        TxArray[5 + TxDataLge] = u8Data;
                                        TxDataLge += 1;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                break;

                            case (int)DataType.UINT16:
                                if (rbTxHexadecimal.Checked == true)
                                {
                                    bData = Int32.TryParse(TxDataStringSplit[cnt],
                                                           System.Globalization.NumberStyles.HexNumber,
                                                           CultureInfo.InvariantCulture,
                                                           out s32Data);
                                    if ((bData == true) && (s32Data <= 0xffff))
                                    {
                                        TxArray[5 + TxDataLge] = (byte)((s32Data >> 8) & 0xff);
                                        TxArray[6 + TxDataLge] = (byte)(s32Data & 0xff);
                                        TxDataLge += 2;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                else        // dezimal
                                {
                                    if ((UInt16.TryParse(TxDataStringSplit[cnt], out u16Data)) == true)
                                    {
                                        s16Data = IPAddress.HostToNetworkOrder(unchecked((Int16)(UInt16)u16Data));   // swap byte order
                                        Marshal.WriteInt16(TxArray, 5 + TxDataLge, s16Data);    // write to array from index 5 + TxDataLge
                                        TxDataLge += 2;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                break;

                            case (int)DataType.UINT32:
                                if (rbTxHexadecimal.Checked == true)
                                {
                                    bData = Int32.TryParse(TxDataStringSplit[cnt],
                                                           System.Globalization.NumberStyles.HexNumber,
                                                           CultureInfo.InvariantCulture,
                                                           out s32Data);
                                    if (bData == true)
                                    {
                                        TxArray[5 + TxDataLge] = (byte)((s32Data >> 24) & 0xff);
                                        TxArray[6 + TxDataLge] = (byte)((s32Data >> 16) & 0xff);
                                        TxArray[7 + TxDataLge] = (byte)((s32Data >> 8) & 0xff);
                                        TxArray[8 + TxDataLge] = (byte)(s32Data & 0xff);
                                        TxDataLge += 4;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                else        // dezimal
                                {
                                    if ((UInt32.TryParse(TxDataStringSplit[cnt], out u32Data)) == true)
                                    {
                                        s32Data = IPAddress.HostToNetworkOrder(unchecked((Int32)(UInt32)u32Data));   // swap byte order
                                        Marshal.WriteInt32(TxArray, 5 + TxDataLge, s32Data);    // write to array from index 5 + TxDataLge
                                        TxDataLge += 4;
                                        TxDataCheck = true;
                                    }
                                    else
                                    {
                                        TxDataCheck = false;
                                    }
                                }
                                break;

                            case (int)DataType.SINT64:
                                if (((Int64.TryParse(TxDataStringSplit[cnt], out s64Data)) == true) &&
                                    (rbTxDecimal.Checked == true))
                                {
                                    s64Data = IPAddress.HostToNetworkOrder(s64Data);        // swap byte order
                                    Marshal.WriteInt64(TxArray, 5 + TxDataLge, s64Data);                // write to array from index 5 + TxDataLge
                                    TxDataLge += 8;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.UINT64:
                                if (((UInt64.TryParse(TxDataStringSplit[cnt], out u64Data)) == true) &&
                                    (rbTxDecimal.Checked == true))
                                {
                                    s64Data = IPAddress.HostToNetworkOrder(unchecked((Int64)(UInt64)u64Data));   // swap byte order
                                    Marshal.WriteInt64(TxArray, 5 + TxDataLge, s64Data);    // write to array from index 5 + TxDataLge
                                    TxDataLge += 8;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.FLOAT:                                
                                if (
                                    (float.TryParse(TxDataStringSplit[cnt], (NumberStyles.Float | NumberStyles.AllowThousands), ci, out fData) == true) &&
                                    (rbTxDecimal.Checked == true)
                                   )
                                {
                                    DataArray4 = BitConverter.GetBytes(fData);      // float ==> byte[]
                                    Array.Reverse(DataArray4);                      // swap byte order
                                    s32Data = BitConverter.ToInt32(DataArray4, 0);  // byte[] ==> Int32
                                    Marshal.WriteInt32(TxArray, 5 + TxDataLge, s32Data);        // write to array from index 5 + TxDataLge
                                    TxDataLge += 4;
                                    TxDataCheck = true;
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.CHAR:
                                // With CHAR only work with a string with content
                                if ((TxDataStringSplit.Length == 1) && (TxDataStringSplit[cnt].Length > 0))
                                {
                                    if ((TxArray[5] < 255) && (TxDataStringSplit[cnt].Length > 1))
                                    {
                                        TxDataCheck = false;
                                    }
                                    else
                                    {
                                        for (int Index = 0; Index < TxDataStringSplit[cnt].Length; Index++)
                                        {
                                            TxArray[5 + TxDataLge] = Convert.ToByte(TxDataStringSplit[cnt][Index]);
                                            TxDataLge += 1;
                                            TxDataCheck = true;
                                        }
                                    }
                                }
                                else
                                {
                                    TxDataCheck = false;
                                }
                                break;

                            case (int)DataType.NODATA:
                            default:
                                TxDataCheck = false;
                                break;
                        }
                    } // end of for

                    if (TxDataCheck == false)
                    {
                        tbData.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        tbData.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    // Keine Daten vorhanden
                    if (cbTxDataType.SelectedIndex == (int)DataType.NODATA)
                    {
                        // Keine Daten gewünscht
                        tbData.BackColor = Color.White;
                    }
                    else
                    {
                        // Daten sollten vorhanden sein
                        tbData.BackColor = Color.LightSalmon;
                    }
                }
            }
            else
            {
                tbData.BackColor = Color.White;
            }

            CompleteTransmitData();
        }

        private void SendData()
        {
            if (RxState == RxStates.IDLE)
            {
                try
                {
                    ClearReceivedData();
                    serialPortWire.DiscardInBuffer();               // receive buffer löschen
                    RxState = RxStates.WAIT_STX;
                    SetRxError(RxErrors.UNKNOWN);
                    serialPortWire.Write(TxArray, 0, TxLge + 2);    // String senden
                    timerReceive.Interval = RECEIVE_TIMEOUT;
                    timerReceive.Start();
                }
                catch { }
            }
        }

        private string DataToString(byte[] data)
        {
            string DataValue;
            byte[] DataArray2 = new byte[2];
            byte[] DataArray4 = new byte[4];
            byte[] DataArray8 = new byte[8];

            if (RxLge > 5)  // is there a data section?
            {
                switch (cbRxDataType.SelectedIndex)
                {
                    case (int)DataType.BOOL:
                        if ((data[COM_S_DATA_I_LD] == 0) && (RxLge == 6))
                        {
                            DataValue = "false";
                        }
                        else if ((data[COM_S_DATA_I_LD] == 1) && (RxLge == 6))
                        {
                            DataValue = "true";
                        }
                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.UINT8:
                    case (int)DataType.SINT8:
                        if (RxLge == 6)
                        {
                            if (rbRxDecimal.Checked == true)
                            {
                                DataValue = (cbRxDataType.SelectedIndex == (int)DataType.UINT8)
                                    ? (Convert.ToString(data[COM_S_DATA_I_LD]))
                                    : (Convert.ToString((sbyte)(data[COM_S_DATA_I_LD])));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue = "0x" + BitConverter.ToString(data, COM_S_DATA_I_LD, 1);
                            }
                            else
                            {
                                DataValue = (Convert.ToString(data[COM_S_DATA_I_LD], 2).PadLeft(8, '0'));
                            }
                        }
                        else if (RxLge == 7)
                        {
                            // Probably answer to an array query (single element)
                            DataValue = "[" + Convert.ToString(data[COM_S_DATA_I_LD]) + "]: ";
                            if (rbRxDecimal.Checked == true)
                            {
                                DataValue += (cbRxDataType.SelectedIndex == (int)DataType.UINT8)
                                    ? (Convert.ToString(data[COM_S_DATA_I_LD + 1]))
                                    : (Convert.ToString((sbyte)(data[COM_S_DATA_I_LD + 1])));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue += "0x" + BitConverter.ToString(data, COM_S_DATA_I_LD + 1, 1);
                            }
                            else
                            {
                                DataValue += (Convert.ToString(data[COM_S_DATA_I_LD + 1], 2).PadLeft(8, '0'));
                            }
                        }
                        else if ((RxLge > 7) && (data[COM_S_DATA_I_LD] == 255))
                        {
                            // Byte array with multiple elements
                            DataValue = "{";
                            for (int Anzahl = 0; Anzahl < (RxLge - 6); Anzahl++)
                            {
                                if (Anzahl != 0) DataValue += " , ";
                                if (rbRxDecimal.Checked == true)
                                {
                                    DataValue += (cbRxDataType.SelectedIndex == (int)DataType.UINT8)
                                        ? (Convert.ToString(data[COM_S_DATA_I_LD + 1 + Anzahl]))
                                        : (Convert.ToString((sbyte)(data[COM_S_DATA_I_LD + 1 + Anzahl])));
                                }
                                else if (rbRxHexadecimal.Checked == true)
                                {
                                    DataValue += "0x" + BitConverter.ToString(data, COM_S_DATA_I_LD + 1 + Anzahl, 1);
                                }
                                else
                                {
                                    DataValue += (Convert.ToString(data[COM_S_DATA_I_LD + 1 + Anzahl], 2).PadLeft(8, '0'));
                                }
                            }
                            DataValue += "}";
                        }
                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.UINT16:
                    case (int)DataType.SINT16:
                        if (RxLge == 7)
                        {
                            Array.Copy(data, 6, DataArray2, 0, 2);
                            if (rbRxDecimal.Checked == true)
                            {
                                Array.Reverse(DataArray2);
                                DataValue = ((cbRxDataType.SelectedIndex == (int)DataType.UINT16))
                                    ? Convert.ToString(BitConverter.ToUInt16(DataArray2, 0))
                                    : Convert.ToString(BitConverter.ToInt16(DataArray2, 0));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue = "0x" + BitConverter.ToString(DataArray2).Replace("-", "");
                            }
                            else
                            {
                                DataValue = Convert.ToString(DataArray2[0], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray2[1], 2).PadLeft(8, '0');
                            }
                        }
                        else if (RxLge == 8)
                        {
                            // Probably answer to an array query (single element)
                            DataValue = "[" + Convert.ToString(data[COM_S_DATA_I_LD]) + "]: ";
                            Array.Copy(data, 7, DataArray2, 0, 2);
                            if (rbRxDecimal.Checked == true)
                            {
                                Array.Reverse(DataArray2);
                                DataValue += ((cbRxDataType.SelectedIndex == (int)DataType.UINT16))
                                    ? Convert.ToString(BitConverter.ToUInt16(DataArray2, 0))
                                    : Convert.ToString(BitConverter.ToInt16(DataArray2, 0));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue += "0x" + BitConverter.ToString(DataArray2).Replace("-", "");
                            }
                            else
                            {
                                DataValue += Convert.ToString(DataArray2[0], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray2[1], 2).PadLeft(8, '0');
                            }
                        }
                        else if ((RxLge > 8) && (data[COM_S_DATA_I_LD] == 255) && (((RxLge - 6) % 2) == 0))
                        {
                            // 16 bit value array with several elements
                            DataValue = "{";
                            for (int Anzahl = 0; Anzahl < ((RxLge - 6) / 2); Anzahl++)
                            {
                                Array.Copy(data, 7 + Anzahl * 2, DataArray2, 0, 2);
                                if (Anzahl != 0) DataValue += " , ";
                                if (rbRxDecimal.Checked == true)
                                {
                                    Array.Reverse(DataArray2);
                                    DataValue += ((cbRxDataType.SelectedIndex == (int)DataType.UINT16))
                                        ? Convert.ToString(BitConverter.ToUInt16(DataArray2, 0))
                                        : Convert.ToString(BitConverter.ToInt16(DataArray2, 0));
                                }
                                else if (rbRxHexadecimal.Checked == true)
                                {
                                    DataValue += "0x" + BitConverter.ToString(DataArray2).Replace("-", "");
                                }
                                else
                                {
                                    DataValue += Convert.ToString(DataArray2[0], 2).PadLeft(8, '0');
                                    DataValue += " - ";
                                    DataValue += Convert.ToString(DataArray2[1], 2).PadLeft(8, '0');
                                }
                            }
                            DataValue += "}";
                        }
                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.UINT32:
                    case (int)DataType.SINT32:
                        if (RxLge == 9)
                        {
                            Array.Copy(data, 6, DataArray4, 0, 4);
                            if (rbRxDecimal.Checked == true)
                            {
                                Array.Reverse(DataArray4);
                                DataValue = ((cbRxDataType.SelectedIndex == (int)DataType.UINT32))
                                    ? Convert.ToString(BitConverter.ToUInt32(DataArray4, 0))
                                    : Convert.ToString(BitConverter.ToInt32(DataArray4, 0));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue = "0x" + BitConverter.ToString(DataArray4).Replace("-", "");
                            }
                            else
                            {
                                DataValue = Convert.ToString(DataArray4[0], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[1], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[2], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[3], 2).PadLeft(8, '0');
                            }
                        }
                        else if (RxLge == 10)
                        {
                            // Probably answer to an array query(single element)
                            DataValue = "[" + Convert.ToString(data[COM_S_DATA_I_LD]) + "]: ";
                            Array.Copy(data, 7, DataArray4, 0, 4);
                            if (rbRxDecimal.Checked == true)
                            {
                                Array.Reverse(DataArray4);
                                DataValue += ((cbRxDataType.SelectedIndex == (int)DataType.UINT32))
                                    ? Convert.ToString(BitConverter.ToUInt32(DataArray4, 0))
                                    : Convert.ToString(BitConverter.ToInt32(DataArray4, 0));
                            }
                            else if (rbRxHexadecimal.Checked == true)
                            {
                                DataValue += "0x" + BitConverter.ToString(DataArray4).Replace("-", "");
                            }
                            else
                            {
                                DataValue += Convert.ToString(DataArray4[0], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[1], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[2], 2).PadLeft(8, '0');
                                DataValue += " - ";
                                DataValue += Convert.ToString(DataArray4[3], 2).PadLeft(8, '0');
                            }
                        }
                        else if ((RxLge > 10) && (data[COM_S_DATA_I_LD] == 255) && (((RxLge - 6) % 4) == 0))
                        {
                            // 32 bit array with multiple elements
                            DataValue = "{";
                            for (int Anzahl = 0; Anzahl < ((RxLge - 6) / 4); Anzahl++)
                            {
                                Array.Copy(data, 7 + Anzahl * 4, DataArray4, 0, 4);
                                if (Anzahl != 0) DataValue += " , ";
                                if (rbRxDecimal.Checked == true)
                                {
                                    Array.Reverse(DataArray4);
                                    DataValue += ((cbRxDataType.SelectedIndex == (int)DataType.UINT32))
                                        ? Convert.ToString(BitConverter.ToUInt32(DataArray4, 0))
                                        : Convert.ToString(BitConverter.ToInt32(DataArray4, 0));
                                }
                                else if (rbRxHexadecimal.Checked == true)
                                {
                                    DataValue += "0x" + BitConverter.ToString(DataArray4).Replace("-", "");
                                }
                                else
                                {
                                    DataValue += Convert.ToString(DataArray4[0], 2).PadLeft(8, '0');
                                    DataValue += " - ";
                                    DataValue += Convert.ToString(DataArray4[1], 2).PadLeft(8, '0');
                                    DataValue += " - ";
                                    DataValue += Convert.ToString(DataArray4[2], 2).PadLeft(8, '0');
                                    DataValue += " - ";
                                    DataValue += Convert.ToString(DataArray4[3], 2).PadLeft(8, '0');
                                }
                            }
                            DataValue += "}";
                        }
                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.CHAR:
                        if (RxLge == 6)
                        {
                            DataValue = "\"" + (char)data[COM_S_DATA_I_LD] + "\"";
                        }
                        else if (RxLge == 7)
                        {
                            // Probably answer to an array query (single element)
                            DataValue = "[" + Convert.ToString((sbyte)data[COM_S_DATA_I_LD]) + "]: ";
                            DataValue = "\"" + (char)data[COM_S_DATA_I_LD + 1] + "\"";
                        }
                        else if ((RxLge > 7) && (data[COM_S_DATA_I_LD] == 255))
                        {
                            // Byte array with multiple elements
                            DataValue = "\"";
                            for (int Anzahl = 0; Anzahl < (RxLge - 6); Anzahl++)
                            {
                                DataValue += (char)data[COM_S_DATA_I_LD + 1 + Anzahl];
                            }
                            DataValue += "\"";
                        }
                        else
                        {
                            DataValue = "";
                            for (int i = 6; i <= RxLge; i++)
                            {
                                if ((data[i] >= 0x20) && (data[i] <= 0x7d))
                                {
                                    DataValue += (char)data[i];
                                }
                                else
                                {
                                    DataValue += "?";
                                }
                            }
                        }
                        break;

                    case (int)DataType.UINT64:
                    case (int)DataType.SINT64:
                        if (RxLge == 13)
                        {
                            Array.Copy(data, 6, DataArray8, 0, 8);
                            if (rbRxDecimal.Checked == true)
                            {
                                Array.Reverse(DataArray8);
                                DataValue = ((cbRxDataType.SelectedIndex == (int)DataType.UINT32))
                                    ? Convert.ToString(BitConverter.ToUInt64(DataArray8, 0))
                                    : Convert.ToString(BitConverter.ToInt64(DataArray8, 0));
                            }
                            else
                            {
                                DataValue = "0x" + BitConverter.ToString(DataArray8).Replace("-", "");
                            }
                        }
                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.FLOAT:
                        if (RxLge == 9)
                        {
                            Array.Copy(data, 6, DataArray4, 0, 4);
                            Array.Reverse(DataArray4);                        // swap byte order
                            DataValue = Convert.ToString(BitConverter.ToSingle(DataArray4, 0), ci);
                        }
                        else if (RxLge == 10)
                        {
                            // Probably answer to an array query (single element)
                            Array.Copy(data, 7, DataArray4, 0, 4);
                            Array.Reverse(DataArray4);                        // drehen
                            DataValue = "[" + Convert.ToString(data[COM_S_DATA_I_LD]) + "]: ";
                            DataValue = DataValue + Convert.ToString(BitConverter.ToSingle(DataArray4, 0), ci);
                        }
                        else if ((RxLge > 10) && (data[COM_S_DATA_I_LD] == 255) && (((RxLge - 6) % 4) == 0))
                        {
                            // Float array with several elements
                            DataValue = "{";
                            for (int Anzahl = 0; Anzahl < ((RxLge - 6) / 4); Anzahl++)
                            {
                                Array.Copy(data, 7 + Anzahl * 4, DataArray4, 0, 4);
                                Array.Reverse(DataArray4);                        // drehen
                                if (Anzahl != 0) DataValue += " , ";
                                DataValue += Convert.ToString(BitConverter.ToSingle(DataArray4, 0), ci);
                            }
                            DataValue += "}";
                        }

                        else
                        {
                            DataValue = "?????";
                        }
                        break;

                    case (int)DataType.NODATA:
                        DataValue = "";
                        for (int i = 6; i <= RxLge; i++)
                        {
                            if ((data[i] >= 0x20) && (data[i] <= 0x7d))
                            {
                                DataValue += (char)data[i];
                            }
                            else
                            {
                                DataValue += "?";
                            }
                        }
                        break;

                    default:
                        DataValue = "?????";
                        break;
                }
            }
            else
            {
                DataValue = String.Empty;
            }
            return (DataValue);
        }

        // This event occurs if response was not received completely
        private void timerReceive_Tick(object sender, EventArgs e)
        {
            timerReceive.Stop();
            switch (RxState)
            {
                default:
                case RxStates.IDLE:
                    break;

                case RxStates.WAIT_STX:
                    SetRxError(RxErrors.TIMEOUT_STX);
                    break;

                case RxStates.WAIT_LGE:
                    SetRxError(RxErrors.TIMEOUT_LENGTH);
                    break;

                case RxStates.WAIT_CRC:
                    SetRxError(RxErrors.TIMEOUT_CRC);
                    break;
            }
            RxState = RxStates.IDLE;

            ClearReceivedData();
            tbResponse.BackColor = Color.LightSalmon;
            toolStripStatusLabel1.Text = "No communication, timeout!";
            tbResponse.Text = Enum.GetName(typeof(RxErrors), RxError);
        }

        /// <summary>
        /// Representation of the received string changed (data type or dec/hex/bin)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RxDataValueSelectionChanged(object sender, EventArgs e)
        {
            tbRxDataValue.Text = (cbRxDataType.SelectedIndex == (int)DataType.NODATA) ? "" : DataToString(RxArray);
        }

        public enum RxErrors
        {
            UNKNOWN = 0,
            OK,
            FRAMING,
            NO_STX,
            LENGTH_INVALID,
            LENGTH_EXCEEDED,
            TIMEOUT_STX,
            TIMEOUT_LENGTH,
            TIMEOUT_CRC,
            CRC_INVALID
        }
        RxErrors RxError = RxErrors.UNKNOWN;

        private void SetRxError(RxErrors value)
        {
            RxError = value;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPortWire.BytesToRead > 0)
            {
                switch (RxState)
                {
                    default:        
                    case RxStates.IDLE: // should not occur: unsolicited characters are rejected
                        serialPortWire.DiscardInBuffer();
                        break;

                    case RxStates.WAIT_STX:
                        if (serialPortWire.BytesToRead > 0)
                        {
                            RxBytesRead = serialPortWire.Read(RxArray, 0, 1);       // start character
                            if ((RxArray[0] == STX))
                            {
                                RxState = RxStates.WAIT_LGE;
                            }
                            else    // ERROR: no start character
                            {
                                SetRxError(RxErrors.NO_STX);
                            }
                        }
                        break;

                    case RxStates.WAIT_LGE:
                        if (serialPortWire.BytesToRead > 0)
                        {
                            RxBytesRead = serialPortWire.Read(RxArray, 1, 1);       // length byte
                            RxLge = RxArray[1];
                            if (RxLge > RECEIVE_LGE_MAX)    // ERROR: invalid length
                            {
                                SetRxError(RxErrors.LENGTH_INVALID);
                            }
                            RxBytesMissing = RxLge;
                            RxNextByte = 2;
                            RxState = RxStates.WAIT_CRC;
                        }
                        break;

                    case RxStates.WAIT_CRC:
                        if (serialPortWire.BytesToRead > 0)
                        {
                            RxBytesRead = serialPortWire.Read(RxArray, RxNextByte, serialPortWire.BytesToRead);
                            RxBytesMissing -= RxBytesRead;
                            RxNextByte += RxBytesRead;
                            if (RxBytesMissing == 0)        // already received all characters?
                            {
                                timerReceive.Stop();
                                this.Invoke(new EventHandler(DoUpdate));
                                RxState = RxStates.IDLE;
                            }
                            else if (RxBytesMissing < 0)    // ERROR: more characters than expected
                            {
                                SetRxError(RxErrors.LENGTH_EXCEEDED);
                            }
                            else    // still all ok, keep waiting for the missing characters
                            {
                                ;
                            }
                        }
                        break;
                }   // END "switch (RxState)"
                if (RxError != RxErrors.UNKNOWN)
                {
                    RxState = RxStates.IDLE;
                }
            }   // END "while (RxBytesAvail > 0)"
        }

        private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SetRxError(RxErrors.FRAMING);   // ERROR: for example framing
        }

        private void DoUpdate(object s, EventArgs e)
        {
            byte RxChecksum;

            RxChecksum = CalculateCRC(RxArray, Convert.ToByte(RxLge + 1));
            if (RxChecksum == RxArray[RxLge + 1])
            {
                tbRxChecksum.BackColor = Color.LightGreen;
                SetRxError(RxErrors.OK);
            }
            else
            {
                tbRxChecksum.BackColor = Color.Salmon;
                SetRxError(RxErrors.CRC_INVALID);
            }

            tbRxChecksum.Text = String.Format("{0:X02}", RxChecksum);

            RxString = BitConverter.ToString(RxArray, 0, 1);
            for (int i = 1; i < RxLge + 2; i++)
            {
                RxString += "-" + BitConverter.ToString(RxArray, i, 1);
            }
            tbResponse.Text = RxString;

            tbRxLge.Text = Convert.ToString(RxLge);

            tbStatusWord.Text = (Convert.ToString(RxArray[2], 2).PadLeft(8, '0'))   // Statusword
                + "-" + (Convert.ToString(RxArray[3], 2).PadLeft(8, '0'));

            if ((RxArray[2] & 0x80) == 0x80)
            {
                cbSyntax.Checked = true;
            }
            else
            {
                cbSyntax.Checked = false;
                tbRxData.BackColor = SystemColors.Control;
            }

            cbError.Checked = ((RxArray[2] & 0x40) != 0) ? true : false;            // Bit 14: Device error
            cbWarning.Checked = ((RxArray[2] & 0x20) != 0) ? true : false;          // Bit 13: Unconfirmed device warning
            cbStillWarning.Checked = ((RxArray[3] & 0x20) != 0) ? true : false;     // Bit 5: Still pending device warning
            cbZero.Checked = ((RxArray[3] & 0x10) != 0) ? true : false;             // Bit 4: Zero

            RxCmdNo = Convert.ToUInt16((RxArray[4] << 8) + RxArray[5]); // Command
            RxCmd = Convert.ToByte(RxCmdNo >> 13);
            RxCmdNo &= 0x0fff;

            tbRxCommand.Text = RX_CMD_STRING[RxCmd];
            tbRxCommandNo.Text = Convert.ToString(RxCmdNo);

            if (RxLge > 5)
            {
                if ((RxArray[2] & 0x80) == 0x80)    // Syntax error?
                {
                    RxDataString = GetSyntaxError();

                    tbRxDataValue.Text = "";
                    tbRxData.BackColor = Color.LightSalmon;
                }
                else    // kein Syntax error
                {
                    RxDataString = BitConverter.ToString(RxArray, 6, 1);
                    for (int i = 0; i < (RxLge - 6); i++)
                    {
                        RxDataString += "-" + BitConverter.ToString(RxArray, 7 + i, 1);
                    }
                    tbRxDataValue.Text = (cbRxDataType.SelectedIndex == (int)DataType.NODATA) ? "" : DataToString(RxArray);
                    tbRxData.BackColor = SystemColors.Control;
                }
                tbRxData.Text = RxDataString;
            }
        }

        private void timerAutoSend_Tick(object sender, EventArgs e)
        {
            // Transmit transmit buffer
            SendData();
        }

        private void AutoSendToggle()
        {
            if (AutoSend == true)
            {
                btnSend.Enabled = true;
                SetControls(true);

                AutoSend = false;
                toolStripStatusLabel1.Text = "Autosend stopped";
                timerAutoSend.Stop();
            }
            else
            {
                btnSend.Enabled = false;
                SetControls(false);
                
                AutoSend = true;
                toolStripStatusLabel1.Text = "Autosend active ...    Cancel with Esc key!";

                // Create new transmit string in transmit buffer
                GenerateSendData();

                timerAutoSend.Interval = AUTO_SEND_INTERVAL;
                timerAutoSend.Start();
            }
        }

        private void LD_ProtoTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) && (AutoSend == true))
            {
                AutoSendToggle();
            }
        }

        private void numCommandNr_ValueChanged(object sender, EventArgs e)
        {
            // Get command number of the selected command:
            Int16 value = Convert.ToInt16(numCommandNr.Value);

            // Clear Array Index Checkbox
            cbIsArray.Checked = false;
            tBArrayIndex.Text = "0";

            if (cbCommand.SelectedIndex == (int)Command.CMD_READ_VALUE) // Read command?
            {
                // Hide data areas
                gBData.Visible = false;
            }
            tbData.BackColor = Color.White;             // Reset colors from last write
            tBArrayIndex.BackColor = Color.White;       // Reset colors from last write         
        }

        private void cbIsArray_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsArray.Checked)
            {
                tBArrayIndex.Enabled = true;
            }
            else
            {
                tBArrayIndex.Enabled = false;
            }
        }

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbCommand.SelectedIndex == (int)Command.CMD_WRITE_VALUE) || (cbCommand.SelectedIndex == (int)Command.CMD_READ_VALUE))     // Daten schreiben oder lesen 
            {
                // Array Index possible
                gBArrayIndex.Visible = true;
            }
            else
            {
                // Array Index not possible - hide
                gBArrayIndex.Visible = false;
            }

            try
            {
                if (cbCommand.SelectedIndex == (int)Command.CMD_WRITE_VALUE)
                {
                    // Make the corresponding GroupBox visible 
                    gBData.Visible = true;
                }
                else
                {
                    // For all other values there is no data.
                    // Make the corresponding GroupBox invisible
                    gBData.Visible = false;
                }
            }
            catch
            { }

        }

        private void ClearReceivedData()
        {
            tbRxLge.Text = "";
            tbResponse.Text = "";
            tbResponse.BackColor = SystemColors.Control;
            tbStatusWord.Text = "";
            tbRxCommand.Text = "";
            tbRxCommandNo.Text = "";
            tbRxChecksum.Text = "";
            tbRxChecksum.BackColor = Color.White;
            tbRxData.Text = "";
            tbRxData.BackColor = SystemColors.Control;
            tbRxDataValue.Text = "";
            cbSyntax.Checked = false;
            cbError.Checked = false;
            cbWarning.Checked = false;
            cbStillWarning.Checked = false;
            cbZero.Checked = false;
        }

        private void SetControls(bool SetControl)
        {
            cbSerialPorts.Enabled = SetControl;
            cbBaudrate.Enabled = SetControl;

            gbSend.Enabled = SetControl;
            gbReceive.Enabled = SetControl;

            lbComPort.Enabled = SetControl;
            lbBaudrate.Enabled = SetControl;

        }

        private void LD_ProtoTerm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerReceive.Enabled = false;
        }

        private void btnAutoSend_Click(object sender, EventArgs e)
        {
            AutoSendToggle();
        }

    }

}