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
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WinRate = new System.Windows.Forms.RadioButton();
            this.Ranking = new System.Windows.Forms.RadioButton();
            this.Victorias = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
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
            this.Jugar = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.chatBTN = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.ChatPrivado = new System.Windows.Forms.GroupBox();
            this.ChatGlobal = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
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
            this.label2.Location = new System.Drawing.Point(22, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(140, 38);
            this.nombre.Margin = new System.Windows.Forms.Padding(4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(141, 22);
            this.nombre.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(55, 173);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(345, 300);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(312, 224);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // WinRate
            // 
            this.WinRate.AutoSize = true;
            this.WinRate.Location = new System.Drawing.Point(37, 134);
            this.WinRate.Margin = new System.Windows.Forms.Padding(4);
            this.WinRate.Name = "WinRate";
            this.WinRate.Size = new System.Drawing.Size(187, 21);
            this.WinRate.TabIndex = 11;
            this.WinRate.TabStop = true;
            this.WinRate.Text = "Dime mi ratio de victorias";
            this.WinRate.UseVisualStyleBackColor = true;
            // 
            // Ranking
            // 
            this.Ranking.AutoSize = true;
            this.Ranking.Location = new System.Drawing.Point(37, 106);
            this.Ranking.Margin = new System.Windows.Forms.Padding(4);
            this.Ranking.Name = "Ranking";
            this.Ranking.Size = new System.Drawing.Size(236, 21);
            this.Ranking.TabIndex = 10;
            this.Ranking.TabStop = true;
            this.Ranking.Text = "Dime el ranking de los jugadores";
            this.Ranking.UseVisualStyleBackColor = true;
            // 
            // Victorias
            // 
            this.Victorias.AutoSize = true;
            this.Victorias.Location = new System.Drawing.Point(37, 77);
            this.Victorias.Margin = new System.Windows.Forms.Padding(4);
            this.Victorias.Name = "Victorias";
            this.Victorias.Size = new System.Drawing.Size(226, 21);
            this.Victorias.TabIndex = 9;
            this.Victorias.TabStop = true;
            this.Victorias.Text = "Dime jugador con mas victorias";
            this.Victorias.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 254);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Desconectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 48);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(105, 22);
            this.textBox1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Contraseña:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(152, 98);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(105, 22);
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
            this.groupBox2.Location = new System.Drawing.Point(49, 36);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(267, 223);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Iniciar sesion";
            // 
            // Eliminar
            // 
            this.Eliminar.Location = new System.Drawing.Point(72, 180);
            this.Eliminar.Margin = new System.Windows.Forms.Padding(4);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(100, 28);
            this.Eliminar.TabIndex = 14;
            this.Eliminar.Text = "Dar de baja";
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // registro
            // 
            this.registro.Location = new System.Drawing.Point(137, 144);
            this.registro.Margin = new System.Windows.Forms.Padding(4);
            this.registro.Name = "registro";
            this.registro.Size = new System.Drawing.Size(100, 28);
            this.registro.TabIndex = 13;
            this.registro.Text = "Registrarse";
            this.registro.UseVisualStyleBackColor = true;
            this.registro.Click += new System.EventHandler(this.registro_Click);
            // 
            // LogIn
            // 
            this.LogIn.Location = new System.Drawing.Point(29, 144);
            this.LogIn.Margin = new System.Windows.Forms.Padding(4);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(100, 28);
            this.LogIn.TabIndex = 12;
            this.LogIn.Text = "Log In";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.Login_click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.conectadosGrid);
            this.groupBox3.Controls.Add(this.invitar);
            this.groupBox3.Location = new System.Drawing.Point(345, 36);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(202, 256);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // conectadosGrid
            // 
            this.conectadosGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.conectadosGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.conectadosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectadosGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.conectadosGrid.Location = new System.Drawing.Point(18, 23);
            this.conectadosGrid.Margin = new System.Windows.Forms.Padding(4);
            this.conectadosGrid.Name = "conectadosGrid";
            this.conectadosGrid.RowHeadersWidth = 51;
            this.conectadosGrid.Size = new System.Drawing.Size(165, 186);
            this.conectadosGrid.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Conectados";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 112;
            // 
            // invitar
            // 
            this.invitar.Location = new System.Drawing.Point(59, 218);
            this.invitar.Margin = new System.Windows.Forms.Padding(4);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(73, 30);
            this.invitar.TabIndex = 15;
            this.invitar.Text = "Invitar";
            this.invitar.UseVisualStyleBackColor = true;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            // 
            // Jugar
            // 
            this.Jugar.Location = new System.Drawing.Point(557, 84);
            this.Jugar.Margin = new System.Windows.Forms.Padding(4);
            this.Jugar.Name = "Jugar";
            this.Jugar.Size = new System.Drawing.Size(100, 28);
            this.Jugar.TabIndex = 0;
            this.Jugar.Text = "Jugar";
            this.Jugar.UseVisualStyleBackColor = true;
            this.Jugar.Click += new System.EventHandler(this.Jugar_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(17, 33);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(260, 404);
            this.listBox1.TabIndex = 16;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(17, 448);
            this.chatBox.Margin = new System.Windows.Forms.Padding(4);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(185, 22);
            this.chatBox.TabIndex = 17;
            // 
            // chatBTN
            // 
            this.chatBTN.Location = new System.Drawing.Point(210, 443);
            this.chatBTN.Margin = new System.Windows.Forms.Padding(4);
            this.chatBTN.Name = "chatBTN";
            this.chatBTN.Size = new System.Drawing.Size(67, 32);
            this.chatBTN.TabIndex = 18;
            this.chatBTN.Text = "Enviar";
            this.chatBTN.UseVisualStyleBackColor = true;
            this.chatBTN.Click += new System.EventHandler(this.chatBTN_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(12, 27);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(245, 164);
            this.listBox2.TabIndex = 19;
            // 
            // ChatPrivado
            // 
            this.ChatPrivado.AccessibleName = "";
            this.ChatPrivado.Controls.Add(this.listBox1);
            this.ChatPrivado.Controls.Add(this.chatBox);
            this.ChatPrivado.Controls.Add(this.chatBTN);
            this.ChatPrivado.Location = new System.Drawing.Point(678, 36);
            this.ChatPrivado.Name = "ChatPrivado";
            this.ChatPrivado.Size = new System.Drawing.Size(288, 488);
            this.ChatPrivado.TabIndex = 16;
            this.ChatPrivado.TabStop = false;
            this.ChatPrivado.Text = "Chat Privado";
            // 
            // ChatGlobal
            // 
            this.ChatGlobal.Controls.Add(this.button3);
            this.ChatGlobal.Controls.Add(this.textBox3);
            this.ChatGlobal.Controls.Add(this.listBox2);
            this.ChatGlobal.Location = new System.Drawing.Point(49, 282);
            this.ChatGlobal.Name = "ChatGlobal";
            this.ChatGlobal.Size = new System.Drawing.Size(267, 242);
            this.ChatGlobal.TabIndex = 18;
            this.ChatGlobal.TabStop = false;
            this.ChatGlobal.Text = "ChatGlobal";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 202);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 22);
            this.textBox3.TabIndex = 18;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(201, 198);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 32);
            this.button3.TabIndex = 19;
            this.button3.Text = "Enviar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 590);
            this.Controls.Add(this.ChatGlobal);
            this.Controls.Add(this.ChatPrivado);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Jugar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button Jugar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
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
    }
}

