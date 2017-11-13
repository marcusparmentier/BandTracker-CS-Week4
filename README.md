#### A MVC app with a database to keep track of different venues and bands. 11/03/17

#### By **Marcus Parmentier**

## Description

A MVC app created with C Sharp and use of Razor and .NET framework with a database focusing on creating objects with a custom class and constructor, using RESTful route conventions using HttpGet and HttpPost, and routes to specific instance of the object. Also with a focus on BDD, use of MS Testing, and using MySQL databases with the use of join statements.


## Setup/Installation Requirements

* Clone project from GitHub
* Have .NET Core 1.1.4 downloaded
* Restore and run project while in the project in your terminal



* Database setup
  * > CREATE DATABASE band_tracker;
  * > USE band_tracker;
  * > CREATE TABLE venues (id serial PRIMARY KEY, venue_name VARCHAR(255));
  * > CREATE TABLE bands (id serial PRIMARY KEY, band_name VARCHAR(255), genre VARCHAR(255));
  * > CREATE TABLE venues_bands (id serial PRIMARY KEY, venue_id INT, band_id INT);

  * Repeat Instructions above except new database name is "band_tracker_test"

## Known Bugs

* N/A

## Technologies Used

* C Sharp
 * .Net Core
 * MySQL database
 * Razor

## Support and contact details

_Email me at marcusjparmentier@gmail.com with any questions, comments, or concerns._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2017 **_{Marcus Parmentier}_**
