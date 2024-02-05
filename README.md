# BFF OcelotGW OidCProxy Microsoft Entra Id
Prototype Pattern BFF Indenity Server Ocelot Orchestration
# Credits to 
`` 1- OidcProxy : https://github.com/thecloudnativewebapp/OidcProxy.Net
   2- Ocelot : https://github.com/ThreeMammals/Ocelot


this sample/prototype contains sevral component and pattern 
  `````
1- Ocelot APi Gateway
2- OidcProxy
3- BFF Pattern
4- Razor Page FE
5- Random Resources API
  `````

![image](https://github.com/mehdy-dev/BFF_OcelotGW/assets/84580354/5f19d9fa-d0b2-4a56-a435-b97d5e5bff17)


> how to run the project
  ```````
  1- need app registration for Microsoft Entra Id Authentication 
  2- dont forget admin consent 
  3- callback Url is important 
  4- the following component should run as a whole microservices architecture 
      a- FE Razor App
      b- Ocelot API Gateway
      c- Identity Server OidcProxy
  5- set the config file oidcProxy "Microsoft Entra Id"
  6- run the projects 
    a- \OcelotGW dotnet run
    b- \FE.Endpoint dotnet run
    c- \IdentityServer.EntraId dotnet run

  7-navigate to the FE page and start by login 
  ```````
  ![image](https://github.com/mehdy-dev/BFF_OcelotGW/assets/84580354/7c32d425-2a3a-4e20-80f3-16e51452449c)

    
  

