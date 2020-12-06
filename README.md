# Welcome to Twitter Desktop Manager
<img>![.NET Core](https://img.shields.io/github/workflow/status/petersteele111/TwitterDesktopManager/.NET%20Core)</img>

Welcome to the Twitter Desktop Manager. This application was built for my CIT255 Object Oriented Programming class at NMC during the Fall 2020 Semester. This project was built using a custom MVVM pattern and N-Tier design. I also implemented the Twitter API via TweetInvi as well to simplify the process since this was a short 4 week project. I plan to keep working on an updating this project for the future to include many more features to make the application useful and unique. Thanks for stopping by and I hope you enjoy what I have created!

## Application Description

### Purpose and Function
The purpose of this application is to allow users to manage their Twitter account via the desktop application without the need to login to the web application. They will be able to sign in and authorize their account to work with the application and view profile tweets, add tweets, and delete tweets. Additional functionality will be implemented to allow searching, filtering, and sorting of the users tweets as well. All tweets will be saved to a database allowing for easy management of the tweets and a historical record of their tweets as well, even if the twitter account is disabled or removed. The user will also be able to view their profile information such as their join date, name, screen name, followers, who they follow, and profile image. At this time, this information will not be editable in the application, but I may continue to flesh this program out over time to include the features and functionality to have a fully working Twitter Desktop Manager application on the desktop. There are other offerings out there that already do this, but it may be a nice project for future employment. 

### Technology and Design Patterns
The technology that will be used with this application will primarily be C# and WPF. I will be using .NET Core and C# 8.0 or 9.0 along with WPF which will use the MVVM. The overall structure will utilize an N-tier design. For the database, I will be using MySQL and MongoDB that can be switched back and forth and Entity Framework Core will handle the connections and CRUD operations. For my N-tier structure, I will have multiple projects under one solution. One will handle the Entity Models (two in this instance) for the application. The next tier will be the Entity Framework tier that will handle database connections and logic. The next tier after that will be my business logic for things such as calls to the repositories and defining the DBContext. I will be using a generic repository pattern like I did in the last project to handle grabbing entity data from the database. The final tier will be the presentation layer which will hold the views and view models. This tier will be designed with MVVM in mind and will work much like my previous project for the Movie Database. 

### Data Set
The data set for this application will be mixed. I will have two entities, a User and a Tweet. Each entity will house the information that is returned to me based on my research and digging through the JSON responses that are given from Twitter 1.1 API. Please see the entity diagrams for the fields that will be used and the relationships that have been defined. These entities will work with both MySQL and MongoDB. 

Quick side note regarding the Twitter API, I have the option to use the v2 API but they have limited endpoints available at the moment and are in a beta stage for testing. At some point in the future I will need to migrate everything over, but for now, v1.1 should work just fine for the purposes of this application. 

The other data set that I will be getting is the JSON objects from Twitter itself. These will be parsed and the information I need to create the Tweet objects and User object for the application will be stored in the databases. This data will be brought into the main window from the database directly, rather than directly from the API. This is a design decision that I have made to limit API calls as there are rate limits that could be exceeded if the API calls are used directly in the main window. I will include a way for the user to force an API update of the database if it is needed, but I would rather it only call the endpoint to get all the tweets once per application instance as they will not be pulling in live updates from other users in their timeline and only their tweets for the time being. If they are creating tweets and deleting tweets from the application and updating the database with this information, then using the database information should suffice as it will be current with the live endpoint as well. If they opt to tweet from their phone or on the web application at some point and want to manually force a pull down of the data from the endpoint, I will have that option available, but by default, I will have the application do this once the user logs in and opens the application each time to bring everything current. 

## User Documentation
User Documentation can be found on the Wiki or by clicking the following link: https://github.com/petersteele111/TwitterDesktopManager/wiki/User-Documentation

## Technical Documentation
Technical Documentation can be found on the Wiki or by clicking the following link: https://github.com/petersteele111/TwitterDesktopManager/wiki/Technical-Documentation