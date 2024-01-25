# BFF_OcelotGW
Prototype Pattern BFF Indenity Server Ocelot Orchestration

1-protect upstrean gateway call using jwt token 
>need a jwt issuer as during the validation ocelot will search for "Issuer" and if missing it fails

project : https://github.com/ricardodemauro/Labs.JwtAuthentication
  
  `````{
  "sub": "johndoe",
  "name": "johndoe",
  "aud": [
    "https://localhost:7004",
    "https://localhost:7004"
  ],
  "role": [
    "read_todo",
    "create_todo"
  ],
  "exp": 1706143497,
  "iss": "https://localhost:7004"
}
 `````
> generate the token

![image](https://github.com/mehdy-dev/BFF_OcelotGW/assets/84580354/2daf4fbd-3105-48c5-97ab-74603d8407cc)


2-Call protected api 

 > http://localhost:5000/apigateway/categories

 > Authentication type = bearer Token

 > Token : "eyJhbGciOiJIUzUxMiIsI"

3- response should look like this 

![image](https://github.com/mehdy-dev/BFF_OcelotGW/assets/84580354/5bfe2719-abb2-4937-b0ac-9eb915455d80)


