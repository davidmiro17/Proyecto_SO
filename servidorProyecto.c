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

//
//Estructura para crear la lista de conectados y la tabla de partidas
//
typedef struct 
{
	Conectado conectados [250];
	int num;
} ListaConectados;

typedef struct
{
	Conectado jugadores [4];
	int id;
	int libre;
} TMulti;

typedef TMulti TPartidas[100];


ListaConectados MiLista;
TPartidas MiTabla;

//
//Funcion para inciar la tabla
//

void IniciarTabla()
{
	for (int i=0; i<100; i++)
	{
		MiTabla[i].libre=0;
		MiTabla[i].id=i;
	}
}

int AddToGame(int partida_id,char listajugadores[200], char invitador[20])
{
	char invitado[20];
	char copia[200];
	char copia2[200];
	strcpy(copia,listajugadores);
	strcpy(copia2,listajugadores);
	char *p = strtok(copia, "-");
	int i=0;
	int encontrado = 0;
	while((i<4)&&(encontrado==0))//encuentra el primer sitio vacio
	{
		if(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
			encontrado=1;
		else
			i++;
	}
	if (encontrado==1)
	{
		int huecos=4-i;//calcula los huecos libres
		int k=0;
		while(p!=NULL)//cuenta los invitados que hay
		{
			strcpy(invitado,p);
			k++;
			p=strtok(NULL,"-");
		}
		if(huecos<k)
		{
			return huecos; //mas invitados que huecos, vuelve otra vez a invitar
		}
		else{
			char *l = strtok(copia2,"-");
			while(l!=NULL)
			{
				strcpy(MiTabla[partida_id].jugadores[i].nombre,l);
				int j=0;
				encontrado=0;
				while(j<MiLista.num && encontrado==0)
				{
					if(strcmp(MiLista.conectados[j].nombre,l)==0)
					{
						encontrado=1;
					}
					else
					   j++;
				}
				MiTabla[partida_id].jugadores[i].socket=MiLista.conectados[j].socket;
				char notificacion[512];
				char respuesta[512];
				int m=0;
				char integrantes[200];
				while(strcmp(MiTabla[partida_id].jugadores[m].nombre,"")!=0)
				{
					sprintf(integrantes,"%s%s-",integrantes,MiTabla[partida_id].jugadores[m].nombre);
					m++;
				}
				integrantes[strlen(integrantes)-1]='\0';
				
				sprintf(respuesta,"7|%d/%s/%s",partida_id,invitador,integrantes);
				write(MiTabla[partida_id].jugadores[i].socket, respuesta, strlen(respuesta));
				printf("Enviada invitacion\n");
				/*sprintf(notificacion,"12|%d/%s",partida_id,MiTabla[partida_id].jugadores[i].nombre);
				int f=0;
				while(strcmp(MiTabla[partida_id].jugadores[f].nombre,"")!=0)
				{
				if(strcmp(MiTabla[partida_id].jugadores[f].nombre,MiTabla[partida_id].jugadores[i].nombre)!=0)
				write(MiTabla[partida_id].jugadores[f].socket, respuesta, strlen(respuesta));
				f++;
				}*/
				
				i++;
				l=strtok(NULL,"-");
			}
			return 0;
		}
	}
}

//
//Funcion para crear una partida, que devuelve el id de la partida o un -1 si no hay sitio
//

int CrearPartida (char listajugadores[200], int socket) //devuelve -1 si no hay hueco para mas partidas, o el id de la partida
{
	int i=0;
	int encontrado=0;
	while(i<100 && encontrado==0)
	{
		if(MiTabla[i].libre==0)
		{
			encontrado=1;
			MiTabla[i].libre=1;
		}
		else
		   i++;
	}
	if(encontrado=0)
		  return -1;
	else
	{
		int j=0;
		int encontrado=0;
		while(j<MiLista.num && encontrado==0)
		{
			if(MiLista.conectados[j].socket==socket)
			{
				encontrado=1;
			}
			else
			   j++;
		}
		strcpy(MiTabla[i].jugadores[0].nombre,MiLista.conectados[j].nombre);
		MiTabla[i].jugadores[0].socket=socket;
		printf("Host socket: %d\n",MiTabla[i].jugadores[0].socket);
		char copia[200];
		strcpy(copia,listajugadores);
		char *p = strtok(copia, "-");
		int k=1;
		while(p!=NULL)
		{
			strcpy(MiTabla[i].jugadores[k].nombre,p);
			j=0;
			encontrado=0;
			while(j<MiLista.num && encontrado==0)
			{
				if(strcmp(MiLista.conectados[j].nombre,p)==0)
				{
					encontrado=1;
				}
				else
				   j++;
			}	
			MiTabla[i].jugadores[k].socket=MiLista.conectados[j].socket;
			k++;
			p=strtok(NULL,"-");
		}
		return MiTabla[i].id;
	}
}

//
//Funcion de eliminar un usuario de una partida
//



int EliminarDePartida(char nombre[20], int partida_id)
{
	int encontrado=0;
	int i =0;
	while ((strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)&&(encontrado==0))
	{
		if(strcmp(MiTabla[partida_id].jugadores[i].nombre,nombre)==0)
			encontrado=1;
		else
			i++;
	}
	if (encontrado==1)
	{
		while ((strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0))
		{
			strcpy(MiTabla[partida_id].jugadores[i].nombre,MiTabla[partida_id].jugadores[i+1].nombre);
			MiTabla[partida_id].jugadores[i].socket=MiTabla[partida_id].jugadores[i+1].socket;
			i++;
		}
		return 0;
	}
	else 
		return -1;
}

//
//Funcion para eliminar una partida
//



void EliminarPartida(int id)
{
	int i=0;
	while(i<4)
	{
		strcpy(MiTabla[id].jugadores[i].nombre, "");
		MiTabla[id].jugadores[i].socket = -1;
		i++;
	}
}

//
//Funcion para poner conectado a la lista de conectados
//



int PonConectado (ListaConectados *MiLista, char nombre[20], int socket)
	///jjjjj
{
	if (MiLista->num == 100)
		return -1;
	else
	{
		strcpy (MiLista->conectados[MiLista->num].nombre, nombre);
		MiLista->conectados[MiLista->num].socket = socket;
		MiLista->num++;
		return 0;
		
	}
}

//
//Funcion que devuelve el socket del usuario que le pasamos
//



int DameSocket (ListaConectados *MiLista, char nombre[20])
{
	//Funcion que devuelve el socket
	int i=0;
	int encontrado = 0;
	while ((i < MiLista->num) && (encontrado==0))
	{
		if (strcmp(MiLista->conectados[i].nombre, nombre) == 0)
			encontrado =1;
		else
			i=i+1;
	}
	if (encontrado==1)
		return MiLista->conectados[i].socket;
	else
		return -1;
}











//
//Funcion que desconecta el usuario 
//



int DesconectarUsuario (ListaConectados *MiLista, char nombre[20])
{
	printf("%s\n",nombre);
	int found=0;
	int i=0;
	while((found==0)&&(i<MiLista->num))
	{
		if(strcmp(nombre,MiLista->conectados[i].nombre)==0)
		{
			found==1;
			for (int j=i;j<MiLista->num;j++)
			{
				strcpy(MiLista->conectados[j].nombre,MiLista->conectados[j+1].nombre);
				MiLista->conectados[j].socket=MiLista->conectados[j+1].socket;
			}
			MiLista->num--;
		}
		i++;
	}
	
}
//
//Funcion que en muestra conectados
//
void ShowConectados(char respuesta[512])
{
	pthread_mutex_lock(&mutex);
	char respuestaAUX[512];
	memset(respuestaAUX, 0, 512);
	sprintf(respuesta,"6|%d/",MiLista.num);
	int i; 
	for (i=0; i<MiLista.num; i++)
	{
		sprintf(respuestaAUX,"%s%s/",respuestaAUX,MiLista.conectados[i].nombre);
	}
	respuestaAUX[strlen(respuestaAUX)-1] = '\0';
	strcat(respuesta,respuestaAUX);
	printf("%s\n",respuesta);
	printf("\n");
	pthread_mutex_unlock(&mutex);
}

/*funcion que envia la lista de conectados a todos los conectados cada vez que alguien se conecta o DesconectarUsuario
EN el apartado LogIn y DesconectarUsuario hay que meter una llamada a esta funcion
Esta funcion ha de recorrer toda la lista de conectados e imprimir un mensaje tipo "NUMERO/usuario1/usuario2/..." como respuesta (LLAMADA A ShowConectados)
*/



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
		sprintf (respuesta,"3|No se han obtenido datos en la consulta\n");
	else
	{
		victorias = atoi(row[1]);
		
		sprintf (respuesta,"3|%s   de %d victorias.\n", row[0],victorias);
		row = mysql_fetch_row (resultado);
	}
	printf("%s\n",respuesta);
	printf("\n");
	pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	
}

//
//Funcion que devuelve el ranking de jugadores con mas victorias
//

void RankingUpperLower(MYSQL *conn, char respuesta[512])
{
	pthread_mutex_lock( &mutex ); //No me interrumpas ahora
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int puntos;
	char respuestaAUX[200];
	memset(respuestaAUX, 0, 200);
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
		sprintf(respuestaAUX,"%s%s %s/",respuestaAUX,row[0],row[1]);
		// obtenemos la siguiente fila
		
		row = mysql_fetch_row (resultado);
	}
	respuestaAUX[strlen(respuestaAUX)-1] = '\0';
	printf("%s\n",respuestaAUX);
	sprintf(respuesta,"4|%s",respuestaAUX);
	printf("%s\n",respuesta);
	printf("\n");
	pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
}


//
//Funcion que devuelve el winrate del jugador
//



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
			
			sprintf(respuesta,"5|%.2f%\n",winrate);	
			printf("%s\n",respuesta);
			printf("\n");
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
}

//
//Funcion que loguea al jugador, que se identifica con un nombre de usuario y una contrase�a. Si el nombre y la contrase�a se encuentran en la base de datos
//se loguea correctamente
//


void LogIn(MYSQL *conn, char respuesta[512], char nombre[20], char password[20], int sock_conn)
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char *p;
	p = strtok(NULL, "/");
	strcpy (password, p);
	
	char consulta [512];
	sprintf (consulta,"SELECT jugadores.username FROM (jugadores) WHERE jugadores.username = '%s' AND jugadores.password = '%s';", nombre, password); 
	
	int err=mysql_query (conn, consulta);
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
		strcpy(respuesta,"1|Nombre o contraseña incorrectos\n");
	}
	else
	{			
		pthread_mutex_lock(&mutex);
		int res = PonConectado(&MiLista,nombre,sock_conn);
		pthread_mutex_unlock(&mutex);
		if (res==-1)
		{
			sprintf(respuesta, "1|Log in failed, tabla llena\n");
		}
		else
		{
			sprintf(respuesta,"1|Logueado correctamente");
		}
	}
	printf("%s\n",respuesta);
	printf("\n");
}

//
//Funcion que nos registra como usuarios
//

void SignIn(MYSQL *conn, char respuesta[512], char nombre[20], char password[20], int sock_conn)
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
		if (row !=NULL){
			ID = atoi (row[0]);
			row = mysql_fetch_row (resultado);
	}
		
		sprintf(consulta, "INSERT INTO jugadores VALUES(%d,'%s','%s',0,0)",ID,nombre,password);
		
		err = mysql_query(conn, consulta);
		
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		sprintf(respuesta,"2|Registrado\n");
		printf("\n");
		pthread_mutex_unlock(&mutex); //ya puedes interrumpirme
}


//
//El usuario envia el nombre al cliente  del invitador a la que quiere invitar. Ejemplo: David invita a Cesar (7/Cesar)
//

void Invitacion (char nombre[512], int sock_inv, char respuesta[512]) 	
{
	sprintf(respuesta,"7|%s\n", nombre);
	printf("%s\n",respuesta);
}



//
//El invitado le envia al usuario que crea la invitacion su respuesta de si acepta o no. Ejemplo: Cesar acepta la invitacion de David (8/David)
//


void RespuestaAInvitacion(ListaConectados *MiLista, char usrinvitado [20], int sock_conn, char respuesta [512], int partida_id)
{
	
	char respuesta2[512];
	char *p;
	int res;
	int acepta;
	p=strtok(NULL, "/");
	acepta = atoi(p);
	printf("acepta: %d\n",acepta);
	sprintf(respuesta2, "12|%s", usrinvitado);
	if (acepta == 0)
	{
		sprintf(respuesta,"8|%d/%s/0",partida_id, usrinvitado);
		int i=0;
		while(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
		{
			write(MiTabla[partida_id].jugadores[i].socket, respuesta2, strlen(respuesta2));
			i++;
		}
	}
	else
	{
		sprintf(respuesta,"8|%d/%s/1",partida_id, usrinvitado);
		res = EliminarDePartida(usrinvitado,partida_id);
		if (res==0)
			printf("ok\n");
		else 
			printf("didn't work\n");
	}
}

//
//Funcion que da de baja el usuario identificado con nombre y contrase�a
//




void EliminarUsuario (MYSQL *conn, char respuesta[512], char nombre[20], char password[20], int sock_conn)
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char *p;
	p = strtok(NULL, "/");
	strcpy (password, p);
	char consulta [512];
	
	
	
	
	
	strcpy (consulta, "SELECT jugadores.username FROM (jugadores) WHERE jugadores.username='");
	strcat (consulta, nombre); 
	strcat (consulta, "'");
	strcat (consulta, " AND jugadores.password='"); 
	strcat (consulta, password); 
	strcat (consulta, "';");
	
	printf("consulta = %s\n", consulta);
	
	
	
	
	
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("El username y el password no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("El USERNAME y el PASSWORD no coinciden\n");
		strcpy(respuesta,"10|El usuario no ha podido loguearse, revise si el username y la password coinciden.");
		return -1;
		
	}
	
	else
	{
		
		while (row != NULL)
		{
			
			strcpy (consulta, "DELETE FROM jugadores WHERE jugadores.username='");
			strcat (consulta, nombre); 
			strcat (consulta, "';");
			
			printf("consulta = %s\n", consulta);
			
			strcpy(respuesta,"10|El usuario ha sido eliminado de la base de datos ");
			
			
			err = mysql_query(conn, consulta);
			
			
			err = mysql_query(conn, consulta);
			if (err!=0)
			{
				printf ("Error al modificar datos la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"10|El usuario no ha sido eliminado de la base de datos ");
				return -1;
				exit (1);
			}
			
			printf("\n");
			printf("Despues de dar baja al jugador deseado la BBDD queda de la siguiente forma:\n");
			err=mysql_query (conn, "SELECT * FROM jugadores");
			if (err!=0) 
			{
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			
			else
				while (row !=NULL) 
			{
					printf ("Username: %s\n", row[1]);
					row = mysql_fetch_row (resultado);							
			}
				
		}
		
	}
}







void *AtenderCliente (void *socket)
{
	int sock_conn;
	int sock_inv;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	char peticion[512];
	char respuesta[512];
	char notificacion[512];
	int ret;
	char invitado;
	
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
		memset(respuesta, 0, 512);
		memset(notificacion, 0, 512);
		// Ahora recibimos su peticion
		printf("\n");
		printf ("Esperando peticion\n");
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibida una petición\n");
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		//Escribimos la peticion en la consola
		
		printf ("La petición es: %s\n",peticion);
		char *p = strtok(peticion, "/");
		int codigo =  atoi (p);
		int res;
		char nombre[20];
		char password[20];
		char usrinvitado [20];
		char usuario[20];
		char mensaje[200];
		char listajugadores[200];
		char invitador[20];
		if ((codigo !=0)&&(codigo !=6)&&(codigo !=7)&&(codigo !=8)&&(codigo !=9)&&(codigo !=11)&&(codigo !=12)&&(codigo !=13)&&(codigo !=14))
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			printf ("Codigo: %d, Nombre: %s, socket: %d\n", codigo, nombre, sock_conn);
			
		}
		switch(codigo)
		{
		case 0:
			DesconectarUsuario(&MiLista,nombre);
			
			ShowConectados(notificacion);
			for (int j = 0; j < MiLista.num; j++)
				write(MiLista.conectados[j].socket,notificacion,strlen(notificacion));
			
			terminar=1;
			break;
		case 1:
			JugadorConMasVictorias(conn,respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 2:
			RankingUpperLower(conn,respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 3:
			WinRate(conn,respuesta,nombre);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 4:
			LogIn(conn,respuesta,nombre,password,sock_conn);
			write (sock_conn,respuesta,strlen(respuesta));
			
			ShowConectados(notificacion);
			for (int j = 0; j < MiLista.num; j++)
				write(MiLista.conectados[j].socket,notificacion,strlen(notificacion));
			break;
		case 5:
			SignIn(conn,respuesta,nombre,password,sock_conn);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 6:
			ShowConectados(respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 7:			
			p=strtok(NULL,"/");
			sprintf(listajugadores,"%s",p);
			printf("%s\n",listajugadores);
			
			int partida_id = CrearPartida(listajugadores,sock_conn);
			sprintf(respuesta,"7|%d/%s/%s",partida_id,MiTabla[partida_id].jugadores[0].nombre,listajugadores);
			printf("%s\n",respuesta);
			int i=1;
			while(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
			{
				write(MiTabla[partida_id].jugadores[i].socket,respuesta,strlen(respuesta));
				i++;
			}
			break;
		case 8:
			p=strtok(NULL,"/");
			partida_id=atoi(p);
			p=strtok(NULL,"/");
			strcpy(usrinvitado,p);
			RespuestaAInvitacion(&MiLista,usrinvitado,sock_conn,respuesta,partida_id);
			write (sock_conn,respuesta,strlen(respuesta));
			
			break;
		case 9:
			p=strtok(NULL, "/");
			strcpy(usuario,p);
			
			p=strtok(NULL, "/");
			strcpy(mensaje,p);
			
			sprintf(respuesta, "9|%s/%s", usuario, mensaje);
			
			for (int i=0; i<MiLista.num ; i++)
			{
				write (MiLista.conectados[i].socket, respuesta , strlen(respuesta));
			}
			break;
		case 10:
			EliminarUsuario(conn,respuesta,nombre,password,sock_conn);
			write (sock_conn,respuesta,strlen(respuesta));
			break;
		case 11:
			p=strtok(NULL, "/");
			partida_id=atoi(p);
			p=strtok(NULL, "/");
			strcpy(usuario,p);
			
			p=strtok(NULL, "/");
			strcpy(mensaje,p);
			
			sprintf(respuesta, "11|%d/%s/%s",partida_id, usuario, mensaje);
			printf("%s\n", respuesta);
			i=0;
			while(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
			{
				write(MiTabla[partida_id].jugadores[i].socket, respuesta, strlen(respuesta));
				i++;
			}
			break;
		case 12:
			p=strtok(NULL,"/");
			partida_id=atoi(p);
			p=strtok(NULL,"/");
			strcpy(usuario,p);
			res=EliminarDePartida(usuario,partida_id);
			if (res==0)
			{
				sprintf(respuesta,"13|%d/Saliste correctamente",partida_id);
				sprintf(notificacion,"14|%d/%s/salio del chat",partida_id,usuario);
			}
			else
				sprintf(respuesta,"13|%d/Algo ha fallado",partida_id);
			write (sock_conn,respuesta,strlen(respuesta));
			i=0;
			while(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
			{
				if(strcmp(MiTabla[partida_id].jugadores[i].nombre,usuario)!=0)
					write(MiTabla[partida_id].jugadores[i].socket, notificacion, strlen(notificacion));
				i++;
			}
			break;
		case 13:
			p=strtok(NULL,"/");
			partida_id=atoi(p);
			p=strtok(NULL,"/");
			sprintf(invitador,"%s",p);
			p=strtok(NULL,"/");
			sprintf(listajugadores,"%s",p);
			printf("%s\n",listajugadores);
			
			res = AddToGame(partida_id,listajugadores,invitador);
			printf("Sale de la funcion\n");
			if (res!=0)
			{
				printf("Envia la info de que hay mucha gente invitada\n");
				sprintf(respuesta,"15|%d/Demasiadas invitaciones, solo puedes invitar a %d",partida_id,res);
				write (sock_conn,respuesta,strlen(respuesta));
			}
			break;
		case 14:
			p=strtok(NULL,"/");
			partida_id=atoi(p);
			EliminarPartida(partida_id);
			sprintf(respuesta,"15|El chat ha finalizado");
			while(strcmp(MiTabla[partida_id].jugadores[i].nombre,"")!=0)
			{
				write(MiTabla[partida_id].jugadores[i].socket, respuesta, strlen(respuesta));
				i++;
			}
			break;
		default:
			break;
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
	// escucharemos en el port 50056
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
	IniciarTabla();
	
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

