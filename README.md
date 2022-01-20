# Monster Trading Card Game

## Design
UML diagram of battle logic is in root directory of this repository. 
Server architecture is not different from the server provided in the 
course, all new RouteCommands are based on ProtectedRouteCommand.

## Lessons learned
* Named capturing groups in regexes and how they are used
* PostgreSQL does not know the @parameter syntax for named parameters, only 
$1, $2 and so on are used as positional placeholders, and Npgsql translates 
them before submitting the query. This leads to unhelpful and confusing error 
messages when one or more parameters are accidentally not filled with a 
value before querying.

## Testing decisions
Battlelogic tested with 42 unit tests. Server only tested with integration 
tests because of the high amount of exception handling involved. Unit tests 
seemed impractical in most cases.   
Integration tests for server, in addition to the provided curl-script, focused 
on the different problems that can occur when trading cards and when 
syntactically incorrect or incomplete json strings are transmitted. Also 
tested problems occuring by trying to get unauthorized access to someone elses 
cards or tradings.   
Also tested implemented bonus feature for selling cards with curl.
IMPORTANT: the custom testscript MonsterTradingCards.integrationtests.curl.bat 
only works properly if the originally provided script MonsterTradingCards.exercise.curl.bat 
runs first, because of the used UIDs

## Unique feature
Before each game one card of both contesting players' decks is chosen randomly 
and marked as a _guarded_ card. If a _guarded_ card is beaten by the opponent's 
card, the mark is removed and the card is returned to the owners deck. The 
round counts as a draw.

## Bonus feature
It is possible to post and buy cards for sale via /sales and /sales/{id}, see 
custom curl script for details.

## Time spent
|              |        |
| ------------ | ------ |
| Battlelogic: |  ~  7h |   
| Server:      |  ~ 60h |  
| Unit Tests:  |  ~  3h |   
|              |        |
| Sum:         |  ~ 70h |  

## Link to Git
https://github.com/georgreichert/Monster-Trading-Card-Game
