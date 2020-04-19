# MiBocata - Ejemplos de Aplicaciones de Referencia

### Este repositorio contiene una serie de proyectos que representan todo el sofware de una empresa ficticia tipo JustEat, Glovo u otras.
### Contiene el backend, aplicación de gestion de pedidos y store enfocada al empresario y aplicación enfocada a los clientes que realizarán pedidos a las tiendas.

## Tecnologías
* El backend usa Asp .net core Api con base de datos MySql
* El acceso a datos se realiza con Entity Framework Core y los modelos son compartidos en todos los proyectos
* Ambas apps (Businnes y Client) estan realizadas en Xamarin Forms
* Bussines implementa UWP , IOS y Android
* Client implementa IOS y Android
------

|CI Tool                    |Build Status|
|---------------------------|---|
| App Center, Android Client    |  [![Build status](https://build.appcenter.ms/v0.1/apps/10197ac5-31b7-420c-8192-1c21b668a542/branches/master/badge)](https://appcenter.ms)|
| App Center, Android Businnes  | [![Build status](https://build.appcenter.ms/v0.1/apps/4111f84e-a2af-47b0-a910-6d3b202a37be/branches/master/badge)](https://appcenter.ms)  |

Puedes descargar la app apuntando al backend en mi repositorio publicado

* [Android Client](https://install.appcenter.ms/orgs/mibocata/apps/mibocata.android.client/distribution_groups/public)
* [Android Businnes](https://install.appcenter.ms/orgs/mibocata/apps/mibocata.android.busnnes/distribution_groups/public)

------

### Screenshots

#### Businnes
![Sign In](images/businnes/signin.png)
![Orders](images/businnes/orders.png)
![Store](images/businnes/store.png)
![Products](images/businnes/products.png)

#### Client
![Stores](images/client/stores.png)
![Profile](images/client/profile.png)
![Store](images/client/store.png)
![Cart](images/client/cart.png)

------

### TODO
- [ ] CI/CD en todas las plataformas
- [ ] Pasar CI/CD a los pipelines de DevOps
- [ ] Poner al mismo nivel los proyectos IOS
- [ ] Añadir unit test
- [ ] Añadir visual test
- [ ] Añadir modelo de la base de datos al repo

# Contributing
Este proyecto acepta cualquier tipo de aporte de todos los usuarios. Solo tienes que hacer tu Pull Request.

O también puedes contribuir ayudandome con un coffee

<a href='https://ko-fi.com/Y8Y41MNBQ' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://cdn.ko-fi.com/cdn/kofi1.png?v=2' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>