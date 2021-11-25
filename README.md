# Tic-Tac-Toe

# Introduction
This app was built for the Launchpad By Vog code challenge.
It uses .NET Web Api and a SQL DB to run a REST Server for a Tic Tac Toe games. 

# Set Up
1. Make sure you have Docker Compose downloaded on your computer.
2. Clone the repository and cd into the project
3. run the following commands:
```
docker-compose build
docker-compose up
```
4. The container should now be up and running. Look at the API documentation below to see what http requests you can send.

# API Documentation
READ BEFORE TESTING ENDPOINTS:
1. The server is only configured for http. Since it is only in development, it has not been configured with SSL yet.
Therefore when sending a request to an endpoint use http://localhost:8000/<Rest-of-URL>.
2. The SQL DB starts up with data pre seeded. Therefore, the database contains 1 game object, two player objects, and 2 move objects at the starting before any endpoints are called. This is purely for development purposes and will be removed in production.
3. To easily test all endpoints, download Postman and follow the API documentation in the next section.

# Endpoint 1
## Purpose
This endpoint allows user to create a new game

## URL
http://localhost:8000/Game

## Method
POST Request
  
## URL Params
None
  
## Data Params
JSON Body:
```
{
    "Player1Name": "John",
    "Player2Name": "Jacob"
}
```
  
## Success Response
Code: 201 CREATED  
Content:
```
{
    "game": {
        "id": 1006
    },
    "player1": {
        "id": 1011,
        "name": "John"
    },
    "player2": {
        "id": 1012,
        "name": "Jacob"
    },
    "message": "Player 1's turn",
    "row0": "[0, 0, 0]",
    "row1": "[0, 0, 0]",
    "row2": "[0, 0, 0]"
}
```
  
## Error Response 
Code: 400 BAD REQUEST
```
// User tries to send request without Player 1 Name
{
    "errors": {
        "Player1Name": [
            "The Player1Name field is required."
        ]
    },
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-e3d180b5d972604398c5bf08c249d3c8-031f512820293d46-00"
}
```
  
## Notes
Make sure you note down both player IDs after getting the response back from the server. You will be using these player IDs to register moves in the 2nd endpoint.
  
# Endpoint 2
## Purpose
This endpoint allows players to register a move

## URL
http://localhost:8000/Move

## Method
POST Request
  
## URL Params
None

## Data Params
JSON Body:
```
{
  "RowNumber": 0,    // Value from 0 to 2
  "ColumnNumber": 1, // Value from 0 to 2
  "PlayerId": 6
}
```
  
## Success Response
Code: 201 CREATED  
Content:
```
{
    "message": "Player 1 has registered their move",
    "row0": "[0, 1, 0]",
    "row1": "[0, 2, 1]",
    "row2": "[0, 0, 0]"
}
```
  
## Error Response 
Code: 400 BAD REQUEST
```
// Player tries to enter a column number less than 0 or greater than 2
{
    "errors": {
        "ColumnNumber": [
            "The field ColumnNumber must be between 0 and 2."
        ]
    },
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-ae40bbcb8ecc634bb0bd8a62543bcd8d-eef240c42231ce43-00"
}
```
```
// Player tries to register two moves in a row without waiting for opponent
Wait for opponent to make a move before you play
```
  
# Endpoint 3
## Purpose
This endpoint return a list of all the active games with their player names and the number of registered moves.

## URL
http://localhost:8000/Game

## Method
GET Request

## URL Params
None

## Data Params
None

## Success Response
Code: 200 OK  
Content: 
```
[
    {
        "gameId": 3,
        "status": -1,
        "numberOfMoves": 0,
        "player1Name": "Brian",
        "player2Name": "Jacob"
    }
]
```

## Error Response 
Code: 500 INTERNAL_SERVER_ERROR
```
{ error: "internal server error" }
```
## Notes
If this is the first endpoint you call, you might expect it to return an empty array as you have not created a game yet. However, for testing purposes some data is seeded into the database when the container starts up. Therefore, you will see one active game (created during seeding) in the array
  

