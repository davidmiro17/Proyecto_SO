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
        public Form1()
        {
            InitializeComponent();
        }

        private void PonConectados(string trozo)
        {
            conectadosGrid.Rows.Clear();
            conectadosGrid.Refresh();
            if (trozo != null && trozo != "")
            {
                string[] usuarios = trozo.Split('/');
                int numero = Convert.ToInt32(usuarios[0]);

                for (int i = 0; i < numero; i++)
                {
                    conectadosGrid.Rows[conectadosGrid.Rows.Add()].Cells[0].Value = usuarios[i + 1];
                }
            }
            else
                MessageBox.Show("Ha ocurrido un error");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9052);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            this.BackColor = Color.Green;

            ThreadStart ts = delegate { atender_mensajes_servidor(); };
            atender = new Thread(ts);
            atender.Start();
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
                byte[] msg2 = new byte[80];
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
                            groupBox1.Enabled = true;
                            groupBox3.Enabled = true;
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
                }
            }
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.BackColor = Color.Gray;
        }


        private void button2_Click(object sender, EventArgs e)
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


        private void button1_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            atender.Abort();

        }

        private void registro_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string password = textBox2.Text;

            string mensaje = "5/" + nombre + "/" + password;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }

        private void Login_click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string password = textBox2.Text;
            string mensaje = "4/" + nombre + "/" + password;
            // Enviamos al servidor el nombre tecleado

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Jugar_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}


