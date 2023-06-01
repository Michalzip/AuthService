  <h1 align="center">Authentication Service</h1>

<!-- ABOUT THE PROJECT -->

## About The Project

the project uses the modular monolith design pattern and this project is intended for user authorization using google, facebook and its own authorization system :smile:

<br/>

### Prerequisites

follow a few steps to run the project

<br>Integration With Providers</br>

first, go to the websites https://developers.facebook.com/apps, https://console.cloud.google.com, create an applications and add a redirect address named: </br>
for facebook : https://localhost:7151/signin-callback </br>
for google : http://localhost:7151/signin-callback </br>
then take the required authorization keys and add them to the src/Bootstrapper/AuthService.Bootstrapper/module.users.json file

#### Startup Options

<br>Dotnet </br>

        dotnet restore
        dotnet run

<br>Docker</br>

        docker-compose build
        docker-compose up
