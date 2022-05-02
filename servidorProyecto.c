#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <string.h>
#include <pthread.h>

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

typedef struct 
{
	char nombre [20];
	int socket;
} Conectado;

typedef struct 
{
	Conectado conectados [100];
	int num;
} ListaConectados;



	int PonConectado (char nombre[20], int socket, ListaConectados MiLista)
	{
		if (MiLista.num == 100)
			return -1;
		else
		{
			pthread_mutex_lock(&mutex);
			strcpy (MiLista.conectados[MiLista.num].nombre, nombre);
			MiLista.conectados[MiLista.num].socket = socket;
			MiLista.num++;
			printf("loggeado\n");
			pthread_mutex_unlock(&mutex);
			return 0;
		
		}
	}


	int DesconectarUsuario (char nombre[20], ListaConectados *MiLista)
	{
		int found=0;
		int i=0;
		while((found==0)&&(i<MiLista->num))
		{
			if(strcmp(nombre,MiLista->conectados[i].nombre)==0)
			{
				found==1;
				for (int j=0;j<MiLista->num;j++)
				{
					strcpy(MiLista->conectados[j].nombre,MiLista->conectados[j+1].nombre);
					MiLista->conectados[j].socket=MiLista->conectados[j+1].socket;
				}
				MiLista->num--;
			}
			i++;
		}
	
	}

	void ShowConectados(char respuesta[512], ListaConectados *MiLista)
	{
		pthread_mutex_lock(&mutex);
		char respuestaAUX[200];
		sprintf(respuesta,"%d/",MiLista->num);
		int i; 
		for (i=0; i<MiLista->num; i++)
		{
			sprintf(respuestaAUX,"%s%s/",respuestaAUX,MiLista->conectados[i].nombre);
		}
		respuestaAUX[strlen(respuestaAUX)-1] = '\0';
		strcat(respuesta,respuestaAUX);
		printf("%s <-respuesta\n",respuesta);
		pthread_mutex_unlock(&mutex);
	}
	
	
	void JugadorConMasVictorias(MYSQL *conn, char respuesta[512])
	{
		pthread_mutex_lock( &mutex ); //No me interrumpas ahora
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		int victorias;
		int err=mysql_query (conn, "SELECT distinct jugadores.username,jugadores.victorias FROM (jugadores) WHERE jugadores.victorias = (SELECT MAX(jugadores.victorias) FROM (jugadores))");
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		
		
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
		{
			printf ("Jugador que ha conseguido el mayor número de victorias:\n");	
			victorias = atoi(row[1]);
			
			sprintf (respuesta,"%s con un total de %d victorias.\n", row[0],victorias);
			row = mysql_fetch_row (resultado);
		}
		pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	}
	
	
	void RankingUpperLower(MYSQL *conn, char respuesta[512])
	{
		pthread_mutex_lock( &mutex ); //No me interrumpas ahora
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		int puntos;
		memset(respuesta, 0, 512);
		
		// consulta SQL para obtener una tabla con todos los datos
		// de la base de datos
		int err=mysql_query (conn, "SELECT jugadores.username,jugadores.puntos FROM (jugadores)ORDER  BY puntos DESC");
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
			printf("Ranking de los jugadores \n");
		while (row !=NULL)
			
		{
			printf ("Username: %s, puntos: %s\n", row[0], row[1]);
			sprintf(respuesta,"%s%s %s/",respuesta,row[0],row[1]);
			// obtenemos la siguiente fila
			
			row = mysql_fetch_row (resultado);
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
		}
		respuesta[strlen(respuesta)-1] = '\0';
	}
	
	
	void WinRate(MYSQL *conn, char respuesta[512], char nombre[20])
	{
		pthread_mutex_lock( &mutex ); //No me interrumpas ahora
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		int partidas;
		int ganadas;
		
		char consulta [80];
		char consulta2 [80];
		
		printf("\n");
		
		// consulta SQL para obtener una tabla con todos los datos
		// de la base de datos
		strcpy (consulta,"SELECT COUNT(participacion.partidaid) FROM (jugadores,participacion) WHERE jugadores.username = '"); 
		strcat (consulta, nombre);
		strcat (consulta,"' AND jugadores.id = participacion.jugadorid;");
		
		int err=mysql_query (conn, consulta);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
			while (row !=NULL){
				
				partidas = atoi (row[0]);
				
				row = mysql_fetch_row (resultado);
		}
			
			// consulta2 SQL para obtener una tabla con todos los datos
			// de la base de datos
			strcpy (consulta2,"SELECT COUNT(partidas.ganador) FROM partidas WHERE partidas.ganador='"); 
			strcat (consulta2, nombre);
			strcat (consulta2,"';");
			
			err=mysql_query (conn, consulta2);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn);
			
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
				if (row !=NULL)
			{
					ganadas = atoi (row[0]);
					row = mysql_fetch_row (resultado);
			}
				
			float winrate = ((float)ganadas / (float)partidas)*100;
				
			printf ("El usuario %s ha jugado un total de %d partidas ganando %d.\n", nombre, partidas, ganadas);
			printf("El WINRATE de %s es del %.2f%\n",nombre,winrate);
				
			sprintf(respuesta,"%.2f%\n",winrate);	
			printf ("Respuesta: %s\n",respuesta);
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	}
	
	void LogIn(MYSQL *conn, char respuesta[512], char nombre[20], char password[20], int sock_conn, ListaConectados MiLista)
	{
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		printf("\n");
		char *p;
		p = strtok(NULL, "/");
		strcpy (password, p);
		
		char consulta [100];
		sprintf (consulta,"SELECT jugadores.username FROM (jugadores) WHERE jugadores.username = '%s' AND jugadores.password = '%s';", nombre, password); 
		
		int err=mysql_query (conn, consulta);
		printf("\n");			
		if (err!=0) {
			printf ("Error, nombre o contraseña incorrectos %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		
		if (row == NULL)
		{
			printf ("Error, nombre o contraseña incorrectos\n");
			strcpy(respuesta,"Nombre o contraseña incorrectos\n");
		}
		else
		{
			printf ("Nombre del jugador\n");
			printf ("Username: %s\n", row[0]);
			int res = PonConectado(nombre,sock_conn,MiLista);
			printf("1\n");
			if (res==-1)
			{
				sprintf(respuesta, "Log in failed, tabla llena\n");
			}
			else
			{
				printf("2\n");
				sprintf(respuesta,"Logueado correctamente");
				printf("3\n");
			}
		}
		printf("\n");
	}
	
	void SignIn(MYSQL *conn, char respuesta[512], char nombre[20], char password[20], int sock_conn, ListaConectados *MiLista)
	{
		pthread_mutex_lock( &mutex ); //No me interrumpas ahora
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		char *p;
		p = strtok( NULL, "/");
		strcpy (password, p);
		char consulta [100];
		char consulta2 [100];
		
		
		int ID;			
		
		sprintf(consulta2,"SELECT (COUNT(jugadores.username))+1 FROM (jugadores)");
		
		int err=mysql_query (conn, consulta2);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
			while (row !=NULL){
				
				ID = atoi (row[0]);
				
				row = mysql_fetch_row (resultado);
		}
			
			sprintf(consulta, "INSERT INTO jugadores VALUES(%d,'%s','%s',0,0)",ID,nombre,password);
			
			err = mysql_query(conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
				pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			}
			
			sprintf(respuesta,"Registrado\n");
	}
	

void *AtenderCliente (void *socket, ListaConectados MiLista)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	memset(respuesta, 0, 512);
	
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T3_BBDDJuego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos su peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibida una petición\n");
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		//Escribimos la peticion en la consola
		
		printf ("La petición es: %s\n",peticion);
		char *p = strtok(peticion, "/");
		int codigo =  atoi (p);
		char nombre[20];
		char password[20];
		if ((codigo !=0)&&(codigo !=6))
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
			
		}
		switch(codigo)
		{
		case 0:
			DesconectarUsuario(nombre, &MiLista);
			terminar=1;
			break;
		case 1:
			JugadorConMasVictorias(conn,respuesta);
			break;
		case 2:
			RankingUpperLower(conn,respuesta);
			break;
		case 3:
			WinRate(conn,respuesta,nombre);
			break;
		case 4:
			LogIn(conn,respuesta,nombre,password,sock_conn, MiLista);
			printf("1\n");
			break;
		case 5:
			SignIn(conn,respuesta,nombre,password,sock_conn, &MiLista);
			break;
		case 6:
			ShowConectados(respuesta,&MiLista);
			break;
		default:
			break;
		}
		printf ("final?\n");
		write (sock_conn,respuesta,strlen(respuesta));
	}
	mysql_close (conn);
	
	close(sock_conn); 

}
	


int main(int argc, char *argv[])
{
			
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9050
	serv_adr.sin_port = htons(50056);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 15) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	pthread_t thread;
	i=0;
	
	// Atenderemos infinitas peticiones
	
	for(;;)
	{
		
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		printf("\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] =sock_conn;
		// sock_conn es el socket que usaremos para este cliente
		//crear thread y decirle lo que tiene que hacer
		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);
		i=i+1;
	}	
		
		
		
}		
		
