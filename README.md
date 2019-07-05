# Airline
Airline Database with CRUD methods

VS 2019 ASP.net mvc project is the projection of Airline website with the possibility to add Races to the database with Raceteams that have
Stuardesses/Radioman/Pilot/Navigator

There are 3 different roles for authentication and authorization:
1)user (login - user pw - user) - can see Races and find the required one on the main page(sort by number and name are available as well)

2)dispatcher (login - dispatcher pw - dispatcher) - has user's privilegy and also has 2 additional pages on navigation bar:
-Raceteams - edition and removing of the raceteams;
-Dispatcher - adding new raceteams to the races; edition of race date/status/raceteam; sending of the request to add raceteam to the
corresponding race

3)admin (login - admin pw - admin) - has dispatcher's privilegy of "Raceteams" page and CRUD functionality for available Races and 
People in "Admin" section. Also, can accept or decline queries of dispatcher to assign some team for a race.
