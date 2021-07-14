namespace LD_P
{
    partial class LD_ProtocolExample
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LD_ProtocolExample));
            this.serialPortWire = new System.IO.Ports.SerialPort(this.components);
            this.cbSerialPorts = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbResponse = new System.Windows.Forms.TextBox();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.numCommandNr = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbComPort = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rbTxDecimal = new System.Windows.Forms.RadioButton();
            this.rbTxHexadecimal = new System.Windows.Forms.RadioButton();
            this.cbTxDataType = new System.Windows.Forms.ComboBox();
            this.tbData = new System.Windows.Forms.TextBox();
            this.gBData = new System.Windows.Forms.GroupBox();
            this.tbTxLge = new System.Windows.Forms.TextBox();
            this.tbSendData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbSend = new System.Windows.Forms.GroupBox();
            this.btnAutoSend = new System.Windows.Forms.Button();
            this.gBArrayIndex = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbIsArray = new System.Windows.Forms.CheckBox();
            this.tBArrayIndex = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelCommandPlainText = new System.Windows.Forms.Label();
            this.lbCmdNo = new System.Windows.Forms.Label();
            this.gbReceive = new System.Windows.Forms.GroupBox();
            this.cbZero = new System.Windows.Forms.CheckBox();
            this.cbStillWarning = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbParseAs = new System.Windows.Forms.Label();
            this.rbRxBinary = new System.Windows.Forms.RadioButton();
            this.tbRxDataValue = new System.Windows.Forms.TextBox();
            this.rbRxHexadecimal = new System.Windows.Forms.RadioButton();
            this.cbRxDataType = new System.Windows.Forms.ComboBox();
            this.rbRxDecimal = new System.Windows.Forms.RadioButton();
            this.tbRxData = new System.Windows.Forms.TextBox();
            this.cbWarning = new System.Windows.Forms.CheckBox();
            this.cbError = new System.Windows.Forms.CheckBox();
            this.cbSyntax = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRxChecksum = new System.Windows.Forms.TextBox();
            this.tbRxCommandNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRxLge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbRxCommand = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStatusWord = new System.Windows.Forms.TextBox();
            this.timerReceive = new System.Windows.Forms.Timer(this.components);
            this.timerAutoSend = new System.Windows.Forms.Timer(this.components);
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.lbBaudrate = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numCommandNr)).BeginInit();
            this.gBData.SuspendLayout();
            this.gbSend.SuspendLayout();
            this.gBArrayIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbReceive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortWire
            // 
            this.serialPortWire.BaudRate = 19200;
            this.serialPortWire.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
            this.serialPortWire.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // cbSerialPorts
            // 
            this.cbSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerialPorts.FormattingEnabled = true;
            this.cbSerialPorts.Location = new System.Drawing.Point(10, 47);
            this.cbSerialPorts.Name = "cbSerialPorts";
            this.cbSerialPorts.Size = new System.Drawing.Size(65, 21);
            this.cbSerialPorts.TabIndex = 0;
            this.cbSerialPorts.SelectedIndexChanged += new System.EventHandler(this.cbSerialPorts_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(6, 292);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(185, 34);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbResponse
            // 
            this.tbResponse.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbResponse.Location = new System.Drawing.Point(6, 42);
            this.tbResponse.Name = "tbResponse";
            this.tbResponse.ReadOnly = true;
            this.tbResponse.Size = new System.Drawing.Size(303, 22);
            this.tbResponse.TabIndex = 2;
            // 
            // cbCommand
            // 
            this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Location = new System.Drawing.Point(6, 92);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbCommand.Size = new System.Drawing.Size(145, 24);
            this.cbCommand.TabIndex = 3;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // numCommandNr
            // 
            this.numCommandNr.Location = new System.Drawing.Point(6, 42);
            this.numCommandNr.Maximum = new decimal(new int[] {
            3315,
            0,
            0,
            0});
            this.numCommandNr.Name = "numCommandNr";
            this.numCommandNr.Size = new System.Drawing.Size(58, 22);
            this.numCommandNr.TabIndex = 4;
            this.numCommandNr.ValueChanged += new System.EventHandler(this.numCommandNr_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Command specifier";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Len";
            // 
            // lbComPort
            // 
            this.lbComPort.AutoSize = true;
            this.lbComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbComPort.Location = new System.Drawing.Point(10, 28);
            this.lbComPort.Name = "lbComPort";
            this.lbComPort.Size = new System.Drawing.Size(66, 16);
            this.lbComPort.TabIndex = 11;
            this.lbComPort.Text = "COM-Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Received data";
            // 
            // rbTxDecimal
            // 
            this.rbTxDecimal.AutoSize = true;
            this.rbTxDecimal.Checked = true;
            this.rbTxDecimal.Location = new System.Drawing.Point(162, 21);
            this.rbTxDecimal.Name = "rbTxDecimal";
            this.rbTxDecimal.Size = new System.Drawing.Size(49, 20);
            this.rbTxDecimal.TabIndex = 16;
            this.rbTxDecimal.TabStop = true;
            this.rbTxDecimal.Text = "dec";
            this.rbTxDecimal.UseVisualStyleBackColor = true;
            // 
            // rbTxHexadecimal
            // 
            this.rbTxHexadecimal.AutoSize = true;
            this.rbTxHexadecimal.Location = new System.Drawing.Point(217, 21);
            this.rbTxHexadecimal.Name = "rbTxHexadecimal";
            this.rbTxHexadecimal.Size = new System.Drawing.Size(47, 20);
            this.rbTxHexadecimal.TabIndex = 17;
            this.rbTxHexadecimal.Text = "hex";
            this.rbTxHexadecimal.UseVisualStyleBackColor = true;
            // 
            // cbTxDataType
            // 
            this.cbTxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTxDataType.FormattingEnabled = true;
            this.cbTxDataType.Location = new System.Drawing.Point(6, 19);
            this.cbTxDataType.Name = "cbTxDataType";
            this.cbTxDataType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbTxDataType.Size = new System.Drawing.Size(120, 24);
            this.cbTxDataType.TabIndex = 18;
            // 
            // tbData
            // 
            this.tbData.BackColor = System.Drawing.SystemColors.Window;
            this.tbData.Location = new System.Drawing.Point(6, 47);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(339, 22);
            this.tbData.TabIndex = 19;
            // 
            // gBData
            // 
            this.gBData.Controls.Add(this.tbData);
            this.gBData.Controls.Add(this.rbTxHexadecimal);
            this.gBData.Controls.Add(this.cbTxDataType);
            this.gBData.Controls.Add(this.rbTxDecimal);
            this.gBData.Location = new System.Drawing.Point(6, 213);
            this.gBData.Name = "gBData";
            this.gBData.Size = new System.Drawing.Size(360, 73);
            this.gBData.TabIndex = 20;
            this.gBData.TabStop = false;
            this.gBData.Text = "Data";
            // 
            // tbTxLge
            // 
            this.tbTxLge.Location = new System.Drawing.Point(305, 365);
            this.tbTxLge.Name = "tbTxLge";
            this.tbTxLge.ReadOnly = true;
            this.tbTxLge.Size = new System.Drawing.Size(46, 22);
            this.tbTxLge.TabIndex = 21;
            // 
            // tbSendData
            // 
            this.tbSendData.Location = new System.Drawing.Point(3, 365);
            this.tbSendData.Name = "tbSendData";
            this.tbSendData.ReadOnly = true;
            this.tbSendData.Size = new System.Drawing.Size(273, 22);
            this.tbSendData.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Transmitted  data";
            // 
            // gbSend
            // 
            this.gbSend.Controls.Add(this.btnAutoSend);
            this.gbSend.Controls.Add(this.gBArrayIndex);
            this.gbSend.Controls.Add(this.pictureBox1);
            this.gbSend.Controls.Add(this.labelCommandPlainText);
            this.gbSend.Controls.Add(this.lbCmdNo);
            this.gbSend.Controls.Add(this.gBData);
            this.gbSend.Controls.Add(this.label5);
            this.gbSend.Controls.Add(this.btnSend);
            this.gbSend.Controls.Add(this.tbSendData);
            this.gbSend.Controls.Add(this.cbCommand);
            this.gbSend.Controls.Add(this.tbTxLge);
            this.gbSend.Controls.Add(this.numCommandNr);
            this.gbSend.Controls.Add(this.label1);
            this.gbSend.Controls.Add(this.label3);
            this.gbSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSend.Location = new System.Drawing.Point(12, 84);
            this.gbSend.Name = "gbSend";
            this.gbSend.Size = new System.Drawing.Size(372, 438);
            this.gbSend.TabIndex = 24;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "Master sends...";
            // 
            // btnAutoSend
            // 
            this.btnAutoSend.Location = new System.Drawing.Point(204, 293);
            this.btnAutoSend.Name = "btnAutoSend";
            this.btnAutoSend.Size = new System.Drawing.Size(66, 34);
            this.btnAutoSend.TabIndex = 37;
            this.btnAutoSend.Text = "Auto\r\n";
            this.toolTip.SetToolTip(this.btnAutoSend, "Auto sends last command repetitively");
            this.btnAutoSend.UseVisualStyleBackColor = true;
            this.btnAutoSend.Click += new System.EventHandler(this.btnAutoSend_Click);
            // 
            // gBArrayIndex
            // 
            this.gBArrayIndex.Controls.Add(this.label14);
            this.gBArrayIndex.Controls.Add(this.cbIsArray);
            this.gBArrayIndex.Controls.Add(this.tBArrayIndex);
            this.gBArrayIndex.Location = new System.Drawing.Point(6, 130);
            this.gBArrayIndex.Name = "gBArrayIndex";
            this.gBArrayIndex.Size = new System.Drawing.Size(360, 77);
            this.gBArrayIndex.TabIndex = 29;
            this.gBArrayIndex.TabStop = false;
            this.gBArrayIndex.Text = "Array-Index";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(102, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "Index (255=All)";
            // 
            // cbIsArray
            // 
            this.cbIsArray.AutoSize = true;
            this.cbIsArray.Location = new System.Drawing.Point(7, 35);
            this.cbIsArray.Name = "cbIsArray";
            this.cbIsArray.Size = new System.Drawing.Size(72, 20);
            this.cbIsArray.TabIndex = 20;
            this.cbIsArray.Text = "Is Array";
            this.cbIsArray.UseVisualStyleBackColor = true;
            this.cbIsArray.CheckedChanged += new System.EventHandler(this.cbIsArray_CheckedChanged);
            // 
            // tBArrayIndex
            // 
            this.tBArrayIndex.BackColor = System.Drawing.SystemColors.Window;
            this.tBArrayIndex.Enabled = false;
            this.tBArrayIndex.Location = new System.Drawing.Point(103, 35);
            this.tBArrayIndex.Name = "tBArrayIndex";
            this.tBArrayIndex.Size = new System.Drawing.Size(159, 22);
            this.tBArrayIndex.TabIndex = 19;
            this.tBArrayIndex.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 393);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(275, 28);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // labelCommandPlainText
            // 
            this.labelCommandPlainText.AutoSize = true;
            this.labelCommandPlainText.Location = new System.Drawing.Point(6, 24);
            this.labelCommandPlainText.Name = "labelCommandPlainText";
            this.labelCommandPlainText.Size = new System.Drawing.Size(70, 16);
            this.labelCommandPlainText.TabIndex = 27;
            this.labelCommandPlainText.Text = "Command";
            this.labelCommandPlainText.Visible = false;
            // 
            // lbCmdNo
            // 
            this.lbCmdNo.AutoSize = true;
            this.lbCmdNo.Location = new System.Drawing.Point(6, 24);
            this.lbCmdNo.Name = "lbCmdNo";
            this.lbCmdNo.Size = new System.Drawing.Size(63, 16);
            this.lbCmdNo.TabIndex = 25;
            this.lbCmdNo.Text = "Cmd. No.";
            // 
            // gbReceive
            // 
            this.gbReceive.Controls.Add(this.cbZero);
            this.gbReceive.Controls.Add(this.cbStillWarning);
            this.gbReceive.Controls.Add(this.pictureBox2);
            this.gbReceive.Controls.Add(this.groupBox2);
            this.gbReceive.Controls.Add(this.cbWarning);
            this.gbReceive.Controls.Add(this.cbError);
            this.gbReceive.Controls.Add(this.cbSyntax);
            this.gbReceive.Controls.Add(this.tbResponse);
            this.gbReceive.Controls.Add(this.label10);
            this.gbReceive.Controls.Add(this.tbRxChecksum);
            this.gbReceive.Controls.Add(this.tbRxCommandNo);
            this.gbReceive.Controls.Add(this.label9);
            this.gbReceive.Controls.Add(this.tbRxLge);
            this.gbReceive.Controls.Add(this.label8);
            this.gbReceive.Controls.Add(this.tbRxCommand);
            this.gbReceive.Controls.Add(this.label6);
            this.gbReceive.Controls.Add(this.tbStatusWord);
            this.gbReceive.Controls.Add(this.label7);
            this.gbReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbReceive.Location = new System.Drawing.Point(401, 84);
            this.gbReceive.Name = "gbReceive";
            this.gbReceive.Size = new System.Drawing.Size(318, 438);
            this.gbReceive.TabIndex = 25;
            this.gbReceive.TabStop = false;
            this.gbReceive.Text = "Received from slave...";
            // 
            // cbZero
            // 
            this.cbZero.AutoCheck = false;
            this.cbZero.AutoSize = true;
            this.cbZero.Location = new System.Drawing.Point(215, 197);
            this.cbZero.Name = "cbZero";
            this.cbZero.Size = new System.Drawing.Size(55, 20);
            this.cbZero.TabIndex = 38;
            this.cbZero.Text = "Zero";
            this.cbZero.UseVisualStyleBackColor = true;
            // 
            // cbStillWarning
            // 
            this.cbStillWarning.AutoCheck = false;
            this.cbStillWarning.AutoSize = true;
            this.cbStillWarning.Location = new System.Drawing.Point(215, 177);
            this.cbStillWarning.Name = "cbStillWarning";
            this.cbStillWarning.Size = new System.Drawing.Size(97, 20);
            this.cbStillWarning.TabIndex = 37;
            this.cbStillWarning.Text = "Still warning";
            this.cbStillWarning.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 77);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(306, 29);
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbParseAs);
            this.groupBox2.Controls.Add(this.rbRxBinary);
            this.groupBox2.Controls.Add(this.tbRxDataValue);
            this.groupBox2.Controls.Add(this.rbRxHexadecimal);
            this.groupBox2.Controls.Add(this.cbRxDataType);
            this.groupBox2.Controls.Add(this.rbRxDecimal);
            this.groupBox2.Controls.Add(this.tbRxData);
            this.groupBox2.Location = new System.Drawing.Point(10, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 190);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // lbParseAs
            // 
            this.lbParseAs.AutoSize = true;
            this.lbParseAs.Location = new System.Drawing.Point(0, 61);
            this.lbParseAs.Name = "lbParseAs";
            this.lbParseAs.Size = new System.Drawing.Size(101, 16);
            this.lbParseAs.TabIndex = 28;
            this.lbParseAs.Text = "Parse data as...";
            // 
            // rbRxBinary
            // 
            this.rbRxBinary.AutoSize = true;
            this.rbRxBinary.Location = new System.Drawing.Point(243, 81);
            this.rbRxBinary.Name = "rbRxBinary";
            this.rbRxBinary.Size = new System.Drawing.Size(44, 20);
            this.rbRxBinary.TabIndex = 26;
            this.rbRxBinary.Text = "bin";
            this.rbRxBinary.UseVisualStyleBackColor = true;
            this.rbRxBinary.CheckedChanged += new System.EventHandler(this.RxDataValueSelectionChanged);
            // 
            // tbRxDataValue
            // 
            this.tbRxDataValue.BackColor = System.Drawing.SystemColors.Control;
            this.tbRxDataValue.Location = new System.Drawing.Point(3, 110);
            this.tbRxDataValue.Multiline = true;
            this.tbRxDataValue.Name = "tbRxDataValue";
            this.tbRxDataValue.ReadOnly = true;
            this.tbRxDataValue.Size = new System.Drawing.Size(287, 74);
            this.tbRxDataValue.TabIndex = 19;
            // 
            // rbRxHexadecimal
            // 
            this.rbRxHexadecimal.AutoSize = true;
            this.rbRxHexadecimal.Location = new System.Drawing.Point(190, 81);
            this.rbRxHexadecimal.Name = "rbRxHexadecimal";
            this.rbRxHexadecimal.Size = new System.Drawing.Size(47, 20);
            this.rbRxHexadecimal.TabIndex = 17;
            this.rbRxHexadecimal.Text = "hex";
            this.rbRxHexadecimal.UseVisualStyleBackColor = true;
            this.rbRxHexadecimal.CheckedChanged += new System.EventHandler(this.RxDataValueSelectionChanged);
            // 
            // cbRxDataType
            // 
            this.cbRxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRxDataType.FormattingEnabled = true;
            this.cbRxDataType.Location = new System.Drawing.Point(3, 80);
            this.cbRxDataType.Name = "cbRxDataType";
            this.cbRxDataType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRxDataType.Size = new System.Drawing.Size(120, 24);
            this.cbRxDataType.TabIndex = 18;
            this.cbRxDataType.SelectedIndexChanged += new System.EventHandler(this.RxDataValueSelectionChanged);
            // 
            // rbRxDecimal
            // 
            this.rbRxDecimal.AutoSize = true;
            this.rbRxDecimal.Checked = true;
            this.rbRxDecimal.Location = new System.Drawing.Point(135, 81);
            this.rbRxDecimal.Name = "rbRxDecimal";
            this.rbRxDecimal.Size = new System.Drawing.Size(49, 20);
            this.rbRxDecimal.TabIndex = 16;
            this.rbRxDecimal.TabStop = true;
            this.rbRxDecimal.Text = "dec";
            this.rbRxDecimal.UseVisualStyleBackColor = true;
            this.rbRxDecimal.CheckedChanged += new System.EventHandler(this.RxDataValueSelectionChanged);
            // 
            // tbRxData
            // 
            this.tbRxData.Location = new System.Drawing.Point(6, 29);
            this.tbRxData.Name = "tbRxData";
            this.tbRxData.ReadOnly = true;
            this.tbRxData.Size = new System.Drawing.Size(287, 22);
            this.tbRxData.TabIndex = 24;
            // 
            // cbWarning
            // 
            this.cbWarning.AutoCheck = false;
            this.cbWarning.AutoSize = true;
            this.cbWarning.Location = new System.Drawing.Point(215, 157);
            this.cbWarning.Name = "cbWarning";
            this.cbWarning.Size = new System.Drawing.Size(77, 20);
            this.cbWarning.TabIndex = 32;
            this.cbWarning.Text = "Warning";
            this.cbWarning.UseVisualStyleBackColor = true;
            // 
            // cbError
            // 
            this.cbError.AutoCheck = false;
            this.cbError.AutoSize = true;
            this.cbError.Location = new System.Drawing.Point(215, 137);
            this.cbError.Name = "cbError";
            this.cbError.Size = new System.Drawing.Size(56, 20);
            this.cbError.TabIndex = 31;
            this.cbError.Text = "Error";
            this.cbError.UseVisualStyleBackColor = true;
            // 
            // cbSyntax
            // 
            this.cbSyntax.AutoCheck = false;
            this.cbSyntax.AutoSize = true;
            this.cbSyntax.Location = new System.Drawing.Point(215, 117);
            this.cbSyntax.Name = "cbSyntax";
            this.cbSyntax.Size = new System.Drawing.Size(67, 20);
            this.cbSyntax.TabIndex = 30;
            this.cbSyntax.Text = "Syntax";
            this.cbSyntax.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(93, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "CRC";
            // 
            // tbRxChecksum
            // 
            this.tbRxChecksum.Location = new System.Drawing.Point(96, 134);
            this.tbRxChecksum.Name = "tbRxChecksum";
            this.tbRxChecksum.ReadOnly = true;
            this.tbRxChecksum.Size = new System.Drawing.Size(54, 22);
            this.tbRxChecksum.TabIndex = 27;
            // 
            // tbRxCommandNo
            // 
            this.tbRxCommandNo.Location = new System.Drawing.Point(10, 225);
            this.tbRxCommandNo.Name = "tbRxCommandNo";
            this.tbRxCommandNo.ReadOnly = true;
            this.tbRxCommandNo.Size = new System.Drawing.Size(62, 22);
            this.tbRxCommandNo.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 16);
            this.label9.TabIndex = 24;
            this.label9.Text = "Len";
            // 
            // tbRxLge
            // 
            this.tbRxLge.Location = new System.Drawing.Point(10, 134);
            this.tbRxLge.Name = "tbRxLge";
            this.tbRxLge.ReadOnly = true;
            this.tbRxLge.Size = new System.Drawing.Size(54, 22);
            this.tbRxLge.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Command";
            // 
            // tbRxCommand
            // 
            this.tbRxCommand.Location = new System.Drawing.Point(78, 225);
            this.tbRxCommand.Name = "tbRxCommand";
            this.tbRxCommand.ReadOnly = true;
            this.tbRxCommand.Size = new System.Drawing.Size(186, 22);
            this.tbRxCommand.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Statuswort";
            // 
            // tbStatusWord
            // 
            this.tbStatusWord.Location = new System.Drawing.Point(10, 177);
            this.tbStatusWord.Name = "tbStatusWord";
            this.tbStatusWord.ReadOnly = true;
            this.tbStatusWord.Size = new System.Drawing.Size(186, 22);
            this.tbStatusWord.TabIndex = 22;
            // 
            // timerReceive
            // 
            this.timerReceive.Tick += new System.EventHandler(this.timerReceive_Tick);
            // 
            // timerAutoSend
            // 
            this.timerAutoSend.Tick += new System.EventHandler(this.timerAutoSend_Tick);
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Items.AddRange(new object[] {
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudrate.Location = new System.Drawing.Point(88, 47);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbBaudrate.Size = new System.Drawing.Size(65, 21);
            this.cbBaudrate.TabIndex = 30;
            this.cbBaudrate.SelectedIndexChanged += new System.EventHandler(this.cbBaudrate_SelectedIndexChanged);
            // 
            // lbBaudrate
            // 
            this.lbBaudrate.AutoSize = true;
            this.lbBaudrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBaudrate.Location = new System.Drawing.Point(95, 28);
            this.lbBaudrate.Name = "lbBaudrate";
            this.lbBaudrate.Size = new System.Drawing.Size(63, 16);
            this.lbBaudrate.TabIndex = 36;
            this.lbBaudrate.Text = "Baudrate";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(731, 22);
            this.statusStrip1.TabIndex = 40;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // LD_ProtocolExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 551);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbBaudrate);
            this.Controls.Add(this.cbBaudrate);
            this.Controls.Add(this.gbSend);
            this.Controls.Add(this.lbComPort);
            this.Controls.Add(this.cbSerialPorts);
            this.Controls.Add(this.gbReceive);
            this.KeyPreview = true;
            this.Name = "LD_ProtocolExample";
            this.Text = "LD_ProtocolExample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LD_ProtoTerm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LD_ProtoTerm_FormClosed);
            this.Load += new System.EventHandler(this.LD_ProtoTerm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LD_ProtoTerm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numCommandNr)).EndInit();
            this.gBData.ResumeLayout(false);
            this.gBData.PerformLayout();
            this.gbSend.ResumeLayout(false);
            this.gbSend.PerformLayout();
            this.gBArrayIndex.ResumeLayout(false);
            this.gBArrayIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbReceive.ResumeLayout(false);
            this.gbReceive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortWire;
        private System.Windows.Forms.ComboBox cbSerialPorts;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbResponse;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.NumericUpDown numCommandNr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbComPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbTxDecimal;
        private System.Windows.Forms.RadioButton rbTxHexadecimal;
        private System.Windows.Forms.ComboBox cbTxDataType;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.GroupBox gBData;
        private System.Windows.Forms.TextBox tbTxLge;
        private System.Windows.Forms.TextBox tbSendData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbSend;
        private System.Windows.Forms.GroupBox gbReceive;
        private System.Windows.Forms.TextBox tbStatusWord;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRxLge;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRxCommand;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRxCommandNo;
        private System.Windows.Forms.TextBox tbRxChecksum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRxData;
        private System.Windows.Forms.CheckBox cbError;
        private System.Windows.Forms.CheckBox cbSyntax;
        private System.Windows.Forms.CheckBox cbWarning;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbRxDataValue;
        private System.Windows.Forms.RadioButton rbRxHexadecimal;
        private System.Windows.Forms.ComboBox cbRxDataType;
        private System.Windows.Forms.RadioButton rbRxDecimal;
        private System.Windows.Forms.Timer timerReceive;
        private System.Windows.Forms.Timer timerAutoSend;
        private System.Windows.Forms.Label lbCmdNo;
        private System.Windows.Forms.Label labelCommandPlainText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox gBArrayIndex;
        private System.Windows.Forms.TextBox tBArrayIndex;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbIsArray;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.Label lbBaudrate;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox cbStillWarning;
        private System.Windows.Forms.CheckBox cbZero;
        private System.Windows.Forms.Button btnAutoSend;
        private System.Windows.Forms.RadioButton rbRxBinary;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lbParseAs;
    }
}

