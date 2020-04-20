# MegaCinema
My first web project for ASP.Net Core Course in SoftUni 02.2020.

This is a simple, basic application for cinema company, that allows users to get information about every projection in every cinema of the company, to view details about the movies and for registered users - to book a ticket by choosing desired seat (row and place) in the cinema hall for the chosen projection.
The application also keeps track about all of the tickets the users have booked and each user have access to his tickets history.
It has all needed functionalities for users with administrator role - to create, edit or delete movie to the database and create, edit or delete projections for each available movie in the database, following the required data validation for each property.
Initialy, the seeding methods populate the database with the following models:
User - with role administrator,
Cinema,
Hall,
Movie,
Projection,
Seat.

The application is deployed in Azure on the following address: https://megacinema.azurewebsites.net/
Registered user: peter@mail.com, password: 123321
