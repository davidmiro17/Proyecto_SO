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

ListaConectados MiLista;


int PonConectado (ListaConectados *lista, char nombre[50], int socket)
{
	if (lista->num == 100)
		return -1;
	
	else
	{
		pthread_mutex_lock(&mutex);//no se escribe asi, mirar en la leccion 3, tmb se debe añadir en la funcion eliminar
		strcpy (lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		pthread_mutex_unlock(&mutex);//no se escribe asi, mirar en la leccion 3, tmb se debe añadir en la funcion eliminar
		return 0;
		
	}
}





	int DesconectarUsuario (char nombre, ListaConectados *lista)
	{
		int found=0;
		int i=0;
		while((found==0)&&(i<lista->num))
		{
			if(strcmp(nombre,lista->conectados[i].nombre)==0)
			{
				found==1;
				for (int j=0;j<lista->num;j++)
				{
					strcpy(lista->conectados[j].nombre,lista->conectados[j].nombre);
					lista->conectados[j].socket=lista->conectados[j+1].socket;
				}
				lista->num--;
			}
			i++;
		}
	
	}

	void ShowConectados(ListaConectados *lista, char *respuesta[512])
	{
		pthread_mutex_lock(&mutex);
		sprintf(&respuesta, "%d/",lista->num);
		int i; 
		for (i=0; i<lista->num; i++)
		{
			sprintf(respuesta,"%s%s/",respuesta,lista->conectados[i].nombre);
		}
		respuesta[strlen(respuesta)-1] = '\0';
		printf("%s\n",&respuesta);
		pthread_mutex_lock(&mutex);
	}





void *AtenderCliente (void *socket, ListaConectados *miLista)
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
	
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego",0, NULL, 0);
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
		
		if (codigo ==0)//petici?n de desconexi?n
		{ 
			DesconectarUsuario(nombre,&miLista);
			terminar=1;
		}
		//Ahora escribimos las consultas de la BBDD, cada una con un codigo distinto
		
		else if (codigo ==1) 
		{ 
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			int victorias;
			err=mysql_query (conn, "SELECT distinct jugadores.username,jugadores.victorias FROM (jugadores) WHERE jugadores.victorias = (SELECT MAX(jugadores.victorias) FROM (jugadores))");
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
			//printf ("Respuesta: %s %d\n",row[0],victorias);
			printf ("%s\n",respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			
		}	
		
		else if (codigo == 2)
		{   
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			int puntos;
			memset(respuesta, 0, 512);
			
			// consulta SQL para obtener una tabla con todos los datos
			// de la base de datos
			err=mysql_query (conn, "SELECT jugadores.username,jugadores.puntos FROM (jugadores)ORDER  BY puntos DESC");
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
			printf("\n");
			printf ("%s",respuesta);
			printf("\n");
			
			
			write  (sock_conn, respuesta,strlen(respuesta));
			//write (sock_conn,resultado,strlen(resultado));
			
		}
		else if (codigo==3)
		{   
			
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			
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
			
			err=mysql_query (conn, consulta);
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
					// Enviamos la respuesta
					write (sock_conn,respuesta,strlen(respuesta));
					
					pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
					
		}
		
		else if (codigo==4)
		{
			
			printf("\n");
			p = strtok(NULL, "/");
			strcpy (password, p);
			printf ("Codigo: %d, Nombre: %s, password: %s\n", codigo, nombre, password);
			char consulta [100];
			sprintf (consulta,"SELECT jugadores.username FROM (jugadores) WHERE jugadores.username = '%s' AND jugadores.password = '%s';", nombre, password); 
			
			err=mysql_query (conn, consulta);
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
				int res = PonConectado(&miLista,&nombre,sock_conn);
				if (res==-1)
					printf("Log in failed, tabla llena\n");
				strcpy(respuesta,"Logueado correctamente");
			}
			printf("\n");
			write (sock_conn,respuesta, strlen(respuesta));
			
		}
		
		else if (codigo ==5)
		{
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			
			p = strtok( NULL, "/");
			strcpy (password, p);
			char consulta [100];
			char consulta2 [100];
			
			
			int ID;			
			
			sprintf(consulta2,"SELECT (COUNT(jugadores.username))+1 FROM (jugadores)");
			
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
		
			sprintf(respuesta,"Registrado\n",respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		
		
		else if (codigo == 6)
		{
			ShowConectados(&miLista, &respuesta[512]);
			printf("1\n");
			write (sock_conn,&respuesta, strlen(respuesta));
		}
		
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
	serv_adr.sin_port = htons(9050);
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
		
