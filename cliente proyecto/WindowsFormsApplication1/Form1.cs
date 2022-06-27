using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        Socket server;
        Thread atender;
        string message;
        int id_partida;

        public Form1()
        {
            InitializeComponent();
        }

       
        private void PonConectados(string trozo)
        {
            
            conectadosGrid.ColumnCount = 1;
            conectadosGrid.Rows.Clear();
            conectadosGrid.Refresh();
            conectadosGrid.AutoResizeColumns();
            if (trozo != null && trozo != "")
            {
                string[] usuarios = trozo.Split('/');
                int numero = Convert.ToInt32(usuarios[0]);

                for (int i = 0; i < numero; i++)
                {
                    conectadosGrid.Rows[conectadosGrid.Rows.Add()].Cells[0].Value = usuarios[i + 1];
                    conectadosGrid.ClearSelection();
                    
                }
            }
            else
                MessageBox.Show("Ha ocurrido un error");

        }








        private void Form1_Load(object sender, EventArgs e)
        {
            conectadosGrid.BackgroundColor = Color.Green;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            ChatPrivado.Enabled = false;
            ChatGlobal.Visible = false;
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            ChatPrivado.Visible = false;
            Conectar.Visible = false;
            groupBox2.Visible = true;


            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50056);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;

                ThreadStart ts = delegate { atender_mensajes_servidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            
        }




        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/" + textBox1.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            conectadosGrid.BackgroundColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            atender.Abort();
        }


        private void atender_mensajes_servidor()
        {
            /* este thread atiende los mensajes del servidor. Los tipos de mensajes:
            * 1: Anterior conectado
            * 2: Numero de conectados
            * 3: Nombre del que se acaba de desconectar
            */

            while (true)
            {
                int a;
                byte[] msg2 = new byte[250];
                // recibo mensaje del servidor
                server.Receive(msg2);

                string[] trozos = Encoding.ASCII.GetString(msg2).Split('|');
                string mensaje2 = trozos[1].Split('\0')[0];
                a = Convert.ToInt32(trozos[0]);
                // Averiguo el tipo de mensaje
                switch (a)
                {
                    case 1:
                        if (mensaje2 == "Logueado correctamente")
                        {
                            
                            MessageBox.Show(mensaje2);
                            this.Invoke(new Action(() =>
                            {
                                ChatGlobal.Visible = true;
                                groupBox1.Visible = true;
                                groupBox3.Visible = true;
                                groupBox1.Enabled = true;
                                groupBox3.Enabled = true;
                            }));
                            
                        }
                        else
                            MessageBox.Show(mensaje2);
                        break;
                    case 2:
                        MessageBox.Show(mensaje2);
                        break;
                    case 3:
                        if (mensaje2  == "No se han obtenido datos en la consulta")
                            MessageBox.Show(mensaje2);
                        else
                            MessageBox.Show("El jugador con mas victorias es: " + mensaje2);
                        break;
                    case 4:
                        MessageBox.Show("Éste es el ranking de los jugadores: " + mensaje2);
                        break;
                    case 5:
                        MessageBox.Show("El WinRate de " + nombre.Text + " es del: " + mensaje2);
                        break;
                    case 6:
                        this.Invoke(new Action(() =>
                        {
                            PonConectados(mensaje2);
                        }));
                        //PonConectados(mensaje2);
                    break;
                    case 7:
                        DialogResult respuesta;
                        string[] parte = mensaje2.Split('/');
                        int id_partida = Convert.ToInt32(parte[0]);
                        
                        respuesta = MessageBox.Show(parte[1] + " te ha invitado a jugar con:" + parte[2].Replace('-',' '), "Invitación", MessageBoxButtons.OKCancel);
                        if (respuesta == DialogResult.OK)
                        {
                            string mensaje = "8/" + id_partida + "/" + textBox1.Text + "/0";
                            // Enviamos al servidor el nombrepor teclado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                        else
                        {
                            string mensaje = "8/" + id_partida + "/" + textBox1.Text + "/1";
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }

                    break;
                    case 8:
                        string[] piezas = mensaje2.Split('/');
                        int acepta = Convert.ToInt32(piezas[2]);
                        if (acepta == 0)
                        {
                            MessageBox.Show(piezas[1] + " ha aceptado la invitación");
                            this.Invoke(new Action(() =>
                            {
                                ChatPrivado.Visible = true;
                                ChatPrivado.Enabled = true;
                            }));
                        }
                        else
                            MessageBox.Show(piezas[1] + " ha rechazado la invitación");
                        break;
                    case 9:
                        string[] partes = mensaje2.Split('/');
                        string texto = partes[0] +": " + partes[1];
                        this.Invoke(new Action(() =>
                        {
                            listBox2.Items.Add(texto);
                        }));
                        break;
                    case 10:
                        if (mensaje2 == "Usuario eliminado")
                        {
                            MessageBox.Show(mensaje2);
                        
                        }
                        else
                            MessageBox.Show(mensaje2);
                        break;
                    case 11:
                        partes = mensaje2.Split('/');
                        texto = partes[1] + ": " + partes[2];
                        this.Invoke(new Action(() =>
                        {
                            listBox1.Items.Add(texto);
                        }));
                        break;
                    case 12:
                        piezas = mensaje2.Split('/');
                        this.Invoke(new Action(() =>
                        {
                            listBox1.Items.Add("        " + piezas[0] + " se ha unido al lobby.");
                        }));
                        break;
                    case 13:
                        piezas = mensaje2.Split('/');
                        MessageBox.Show(piezas[1]);
                        if (piezas[1] == "Saliste correctamente")
                            this.Invoke(new Action(() =>
                            {
                                listBox1.Items.Clear();
                            }));
                        break;
                    case 14:
                        piezas = mensaje2.Split('/');
                        this.Invoke(new Action(() =>
                        {
                            listBox1.Items.Add("        " + piezas[1] + " salió del chat.");
                        }));
                        break;
                    case 15:
                        MessageBox.Show(mensaje2);
                        break;
                    case 16:
                        piezas = mensaje2.Split('/');
                        MessageBox.Show(piezas[1]);
                        if (piezas[1] == "El chat ha finalizado")
                            this.Invoke(new Action(() =>
                            {
                                listBox1.Items.Clear();
                            }));
                        break;
                }
            }
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.BackColor = Color.Gray;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (nombre.Text == "")
            {

                MessageBox.Show("Escribe un nombre sobre el que realizar la consulta");

            }
            else
            {
                if (Victorias.Checked)
                {
                    // Quiere el jugador con mas victorias
                    string mensaje = "1/" + nombre.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }

                if (Ranking.Checked)
                {

                    string mensaje = "2/" + nombre.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }
                else if (WinRate.Checked)
                {
                    string mensaje = "3/" + nombre.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            
            
        }


        private void button1_Click(object sender, EventArgs e)
        {

            ChatGlobal.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            ChatPrivado.Visible = false;
            button1.Visible = false;
            Conectar.Visible = true;



            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            conectadosGrid.BackgroundColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            atender.Abort();

        }

        private void registro_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string password = textBox2.Text;
            if ((textBox2.Text != "") && (textBox1.Text != ""))
            {
                string mensaje = "5/" + nombre + "/" + password;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            else
            {
                MessageBox.Show("Introduce un nombre y una contraseña");

            }
        }

        private void Login_click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string password = textBox2.Text;
            if ((textBox2.Text != "") && (textBox1.Text != ""))
            {
                
                string mensaje = "4/" + nombre + "/" + password;
                // Enviamos al servidor el nombre tecleado

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }

            else
            {

                MessageBox.Show("Introduce un nombre y una contraseña");

            }
                

        }

        private void Jugar_Click(object sender, EventArgs e)
        {
            conectadosGrid.BackgroundColor = Color.Green;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            ChatPrivado.Enabled = false;
            ChatGlobal.Visible = false;
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            button1.Visible = true;
            ChatPrivado.Visible = false;
            Conectar.Visible = false;



            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50056);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;

                ThreadStart ts = delegate { atender_mensajes_servidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }



        private void invitar_Click(object sender, EventArgs e)
        {
            ChatPrivado.Enabled = true;
            ChatPrivado.Visible = true;
            string invitados="";
            bool FirstValue = true;
            
            if (conectadosGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario al que invitar");
            }
            else
            {
                foreach (DataGridViewCell cell in conectadosGrid.SelectedCells)
                {
                    
                    if (!FirstValue)
                        invitados += "-";
                    invitados += cell.Value.ToString();
                    FirstValue = false;
                }

                string mensaje = "7/" + invitados;
                MessageBox.Show(mensaje);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            
        }


        private void chatBTN_Click(object sender, EventArgs e)
        {
            if (chatBox.Text != "")
            {
                string mensaje = "11/" + id_partida + "/" + textBox1.Text + "/" + chatBox.Text;
                chatBox.Clear();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            else
            {

                MessageBox.Show("Escribe un mensaje que enviar");

            }



        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string password = textBox2.Text;

            if ((textBox2.Text != "") && (textBox1.Text != ""))
            {
                string mensaje = "10/" + nombre + "/" + password;
                // Enviamos al servidor el nombre tecleado

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                nombre = null;
                password = null;
            }


            else
            {

                MessageBox.Show("Introduce un nombre y una contraseña");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                string mensaje = "9/" + textBox1.Text + "/" + textBox3.Text;
                textBox3.Clear();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else
            {

                MessageBox.Show("Escribe un mensaje que enviar");

            }
        
        }


        private void button6_Click_1(object sender, EventArgs e)
        {
            string mensaje = "14/" + id_partida;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string invitados = "";
            bool FirstValue = true;

            if (conectadosGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario al que invitar");
            }
            else
            {
                foreach (DataGridViewCell cell in conectadosGrid.SelectedCells)
                {
                    if (!FirstValue)
                        invitados += "-";
                    invitados += cell.Value.ToString();
                    FirstValue = false;
                }

                string mensaje = "13/" + id_partida + "/" + textBox1.Text + "/" + invitados;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string mensaje = "12/" + id_partida + "/" + textBox1.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}


