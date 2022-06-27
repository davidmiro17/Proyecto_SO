namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WinRate = new System.Windows.Forms.RadioButton();
            this.Ranking = new System.Windows.Forms.RadioButton();
            this.Victorias = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Eliminar = new System.Windows.Forms.Button();
            this.registro = new System.Windows.Forms.Button();
            this.LogIn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.conectadosGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invitar = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.ChatPrivado = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chatBTN = new System.Windows.Forms.Button();
            this.ChatGlobal = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.Conectar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGrid)).BeginInit();
            this.ChatPrivado.SuspendLayout();
            this.ChatGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(114, 23);
            this.nombre.Margin = new System.Windows.Forms.Padding(2);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(86, 20);
            this.nombre.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.WinRate);
            this.groupBox1.Controls.Add(this.Ranking);
            this.groupBox1.Controls.Add(this.Victorias);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Location = new System.Drawing.Point(338, 278);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(234, 207);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // WinRate
            // 
            this.WinRate.AutoSize = true;
            this.WinRate.Location = new System.Drawing.Point(22, 87);
            this.WinRate.Margin = new System.Windows.Forms.Padding(2);
            this.WinRate.Name = "WinRate";
            this.WinRate.Size = new System.Drawing.Size(142, 17);
            this.WinRate.TabIndex = 11;
            this.WinRate.TabStop = true;
            this.WinRate.Text = "Dime mi ratio de victorias";
            this.WinRate.UseVisualStyleBackColor = true;
            // 
            // Ranking
            // 
            this.Ranking.AutoSize = true;
            this.Ranking.Location = new System.Drawing.Point(22, 69);
            this.Ranking.Margin = new System.Windows.Forms.Padding(2);
            this.Ranking.Name = "Ranking";
            this.Ranking.Size = new System.Drawing.Size(178, 17);
            this.Ranking.TabIndex = 10;
            this.Ranking.TabStop = true;
            this.Ranking.Text = "Dime el ranking de los jugadores";
            this.Ranking.UseVisualStyleBackColor = true;
            // 
            // Victorias
            // 
            this.Victorias.AutoSize = true;
            this.Victorias.Location = new System.Drawing.Point(22, 50);
            this.Victorias.Margin = new System.Windows.Forms.Padding(2);
            this.Victorias.Name = "Victorias";
            this.Victorias.Size = new System.Drawing.Size(172, 17);
            this.Victorias.TabIndex = 9;
            this.Victorias.TabStop = true;
            this.Victorias.Text = "Dime jugador con mas victorias";
            this.Victorias.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = global::WindowsFormsApplication1.Properties.Resources.Pngtreesend_icon_35815351;
            this.button2.Location = new System.Drawing.Point(62, 130);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 42);
            this.button2.TabIndex = 5;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Contraseña:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(130, 64);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 20);
            this.textBox2.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Eliminar);
            this.groupBox2.Controls.Add(this.registro);
            this.groupBox2.Controls.Add(this.LogIn);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(46, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(226, 238);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Iniciar sesion";
            // 
            // Eliminar
            // 
            this.Eliminar.Image = global::WindowsFormsApplication1.Properties.Resources._1783979;
            this.Eliminar.Location = new System.Drawing.Point(69, 176);
            this.Eliminar.Margin = new System.Windows.Forms.Padding(2);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(94, 51);
            this.Eliminar.TabIndex = 14;
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // registro
            // 
            this.registro.Image = global::WindowsFormsApplication1.Properties.Resources.png_transparent_computer_icons_button_presentation_register_button_blue_text_presentation;
            this.registro.Location = new System.Drawing.Point(118, 108);
            this.registro.Margin = new System.Windows.Forms.Padding(2);
            this.registro.Name = "registro";
            this.registro.Size = new System.Drawing.Size(82, 48);
            this.registro.TabIndex = 13;
            this.registro.UseVisualStyleBackColor = true;
            this.registro.Click += new System.EventHandler(this.registro_Click);
            // 
            // LogIn
            // 
            this.LogIn.Image = global::WindowsFormsApplication1.Properties.Resources.login_button_png_15;
            this.LogIn.Location = new System.Drawing.Point(8, 108);
            this.LogIn.Margin = new System.Windows.Forms.Padding(2);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(98, 48);
            this.LogIn.TabIndex = 12;
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.Login_click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.conectadosGrid);
            this.groupBox3.Controls.Add(this.invitar);
            this.groupBox3.Location = new System.Drawing.Point(338, 40);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(219, 214);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // conectadosGrid
            // 
            this.conectadosGrid.AllowUserToAddRows = false;
            this.conectadosGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.conectadosGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.conectadosGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.conectadosGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.conectadosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.conectadosGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.conectadosGrid.Location = new System.Drawing.Point(65, 13);
            this.conectadosGrid.Margin = new System.Windows.Forms.Padding(2);
            this.conectadosGrid.Name = "conectadosGrid";
            this.conectadosGrid.RowHeadersVisible = false;
            this.conectadosGrid.RowHeadersWidth = 51;
            this.conectadosGrid.Size = new System.Drawing.Size(84, 129);
            this.conectadosGrid.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Conectados";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // invitar
            // 
            this.invitar.Image = global::WindowsFormsApplication1.Properties.Resources.invite_friends_8;
            this.invitar.Location = new System.Drawing.Point(66, 150);
            this.invitar.Margin = new System.Windows.Forms.Padding(2);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(80, 44);
            this.invitar.TabIndex = 15;
            this.invitar.UseVisualStyleBackColor = true;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(41, 71);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(193, 303);
            this.listBox1.TabIndex = 16;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(41, 402);
            this.chatBox.Margin = new System.Windows.Forms.Padding(2);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.chatBox.Size = new System.Drawing.Size(113, 20);
            this.chatBox.TabIndex = 17;
            // 
            // ChatPrivado
            // 
            this.ChatPrivado.AccessibleName = "";
            this.ChatPrivado.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.wp_132;
            this.ChatPrivado.Controls.Add(this.button6);
            this.ChatPrivado.Controls.Add(this.button5);
            this.ChatPrivado.Controls.Add(this.button4);
            this.ChatPrivado.Controls.Add(this.listBox1);
            this.ChatPrivado.Controls.Add(this.chatBox);
            this.ChatPrivado.Controls.Add(this.chatBTN);
            this.ChatPrivado.Location = new System.Drawing.Point(715, 37);
            this.ChatPrivado.Margin = new System.Windows.Forms.Padding(2);
            this.ChatPrivado.Name = "ChatPrivado";
            this.ChatPrivado.Padding = new System.Windows.Forms.Padding(2);
            this.ChatPrivado.Size = new System.Drawing.Size(272, 456);
            this.ChatPrivado.TabIndex = 16;
            this.ChatPrivado.TabStop = false;
            this.ChatPrivado.Text = "Chat Privado";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(95, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(91, 23);
            this.button6.TabIndex = 21;
            this.button6.Text = "Eliminar chat";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(192, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "Salirse";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 25);
            this.button4.TabIndex = 19;
            this.button4.Text = "Invitar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // chatBTN
            // 
            this.chatBTN.Image = global::WindowsFormsApplication1.Properties.Resources.Pngtreesend_icon_35815351;
            this.chatBTN.Location = new System.Drawing.Point(167, 385);
            this.chatBTN.Margin = new System.Windows.Forms.Padding(2);
            this.chatBTN.Name = "chatBTN";
            this.chatBTN.Size = new System.Drawing.Size(67, 53);
            this.chatBTN.TabIndex = 18;
            this.chatBTN.UseVisualStyleBackColor = true;
            this.chatBTN.Click += new System.EventHandler(this.chatBTN_Click);
            // 
            // ChatGlobal
            // 
            this.ChatGlobal.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.wp_132;
            this.ChatGlobal.Controls.Add(this.button3);
            this.ChatGlobal.Controls.Add(this.textBox3);
            this.ChatGlobal.Controls.Add(this.listBox2);
            this.ChatGlobal.Location = new System.Drawing.Point(46, 278);
            this.ChatGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.ChatGlobal.Name = "ChatGlobal";
            this.ChatGlobal.Padding = new System.Windows.Forms.Padding(2);
            this.ChatGlobal.Size = new System.Drawing.Size(218, 207);
            this.ChatGlobal.TabIndex = 18;
            this.ChatGlobal.TabStop = false;
            this.ChatGlobal.Text = "ChatGlobal";
            // 
            // button3
            // 
            this.button3.Image = global::WindowsFormsApplication1.Properties.Resources.Pngtreesend_icon_35815351;
            this.button3.Location = new System.Drawing.Point(148, 144);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 45);
            this.button3.TabIndex = 19;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(16, 159);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(111, 20);
            this.textBox3.TabIndex = 18;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.Location = new System.Drawing.Point(25, 25);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(149, 108);
            this.listBox2.TabIndex = 19;
            // 
            // Conectar
            // 
            this.Conectar.Image = global::WindowsFormsApplication1.Properties.Resources.vecteezy_green_play_button_1186943;
            this.Conectar.Location = new System.Drawing.Point(357, 223);
            this.Conectar.Margin = new System.Windows.Forms.Padding(2);
            this.Conectar.Name = "Conectar";
            this.Conectar.Size = new System.Drawing.Size(211, 169);
            this.Conectar.TabIndex = 0;
            this.Conectar.UseVisualStyleBackColor = true;
            this.Conectar.Click += new System.EventHandler(this.Jugar_Click);
            // 
            // button1
            // 
            this.button1.Image = global::WindowsFormsApplication1.Properties.Resources.disconnect_plug;
            this.button1.Location = new System.Drawing.Point(581, 187);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 41);
            this.button1.TabIndex = 12;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1024, 526);
            this.Controls.Add(this.ChatGlobal);
            this.Controls.Add(this.ChatPrivado);
            this.Controls.Add(this.Conectar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conectadosGrid)).EndInit();
            this.ChatPrivado.ResumeLayout(false);
            this.ChatPrivado.PerformLayout();
            this.ChatGlobal.ResumeLayout(false);
            this.ChatGlobal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Victorias;
        private System.Windows.Forms.RadioButton Ranking;
        private System.Windows.Forms.RadioButton WinRate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.Button registro;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView conectadosGrid;
        private System.Windows.Forms.Button Conectar;
        private System.Windows.Forms.Button invitar;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Button chatBTN;
        private System.Windows.Forms.Button Eliminar;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox ChatPrivado;
        private System.Windows.Forms.GroupBox ChatGlobal;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
    }
}

