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
> generate the token
  ![image](https://github.com/mehdy-dev/BFF_OcelotGW/assets/84580354/25ac94e4-1fe9-4b6a-820c-29b29774e3a3)

2-Call protected api sending the 
