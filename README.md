1. CRUD USUARIO
1.1 Buscar un usuario :
dev/api/User
Method: GET
-	Devolver el usuario por parámetros.
  
Using Curl:
````
curl -X 'GET' \
  'https://localhost:7208/User?name=Chemaria&passw=1192&email=chemaria%40email.com' \
  -H 'accept: text/plain'
````
JSON:
````
Response body
{
  "idUser": null,
  "users": "Chemaria                                          ",
  "pass": "1192                                              ",
  "email": "chemaria@email.com                                ",
  "administrador": 1,
  "manage": 1,
  "idNegocio": null,
  "validated": 1,
  "usuarioPostGre": null
}
}
````

1.2. Crear un usuario.
Dev/api/user
Method: POST.
-	Introducir los datos y crear el usuario.
Using Curl:
````
curl -X 'POST' \
  'https://localhost:7208/User?name=Ignacio&password=password&email=ignacion%40email.com' \
  -H 'accept: */*' \
  -d ''
````

1.3. Editar usuario
Dev/api/user
Method: PUT
-	Editar el usuario que coincide con el nombre.
Using Curl:
````
curl -X 'PUT' \
  'https://localhost:7208/User?name=Ignacio&password=cesjuan&email=ignacio%40email.com' \
  -H 'accept: */*'
````

1.4. Eliminar usuario
Dev/api/user
Method: DELETE
-	Eliminar el usuario que coincida con el nombre introducido.
Using Curl:
````
curl -X 'DELETE' \
  'https://localhost:7208/User?name=Chemari' \
  -H 'accept: */*'
````

2. CRUD LIBRO
2.1 Buscar un LIBRO :
dev/api/Libro
Method: GET
-	Devolver el libro por año.
Using Curl:
````
curl -X 'GET' \
  'https://localhost:7208/Libro?year=2008' \
  -H 'accept: text/plain'
````

JSON:

````
Response body
{
  "id": null,
  "titulo": "El ultimo caton                                                                                                                                                                                                                                                                                             ",
  "autor": "Matilde Asensi                                                                                                                                                                                                                                                                                              ",
  "year": 2008
}
````

2.2. Crear un libro.
Dev/api/Libro
Method: POST.
-	Introducir los datos y crear el libro.
Using Curl:
````
curl -X 'POST' \
  'https://localhost:7208/Libro?titulo=H%C3%A1bitos%20at%C3%B3micos&autor=Juan%20J%C3%ADmenez&year=2012' \
  -H 'accept: */*' \
  -d ''
````

2.3. Editar un libro
Dev/api/Libro
Method: PUT
-	Editar el libro que coincide con el título.
Using Curl:
````
curl -X 'PUT' \
  'https://localhost:7208/Libro?titulo=H%C3%A1bitos%20At%C3%B3micos&autor=Vetusta%20Morla&year=2013' \
  -H 'accept: */*'
````

2.4. Eliminar libro
Dev/api/user
Method: DELETE
-	Eliminar el libro que coincida con el título introducido.
Using Curl:
````
curl -X 'DELETE' \
  'https://localhost:7208/Libro?titulo=H%C3%A1bitos%20At%C3%B3micos' \
  -H 'accept: */*'
````
