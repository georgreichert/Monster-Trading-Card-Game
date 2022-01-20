# Monster Trading Card Game

## Design
* UML diagram of battle logic is in root directory of this repository. 
* Server architecture is not different from the server provided in the 
course, all new RouteCommands are based on ProtectedRouteCommand.   
* RouteParser was extended to be able to parse an arbitrary number of 
url-parameters in the form of ?param1=value1&param2=value2&...
* Export of database schema can be found in root directory
* Added BattleManager for handling of battle queue and battle results. 
* Battles run fully concurrent, all relevant code passages are secured 
by locks
* Added several custom exceptions for handling of different errors
* Added several models for json parsing
* DatabaseRepository implements both repository interfaces because 
I wanted all database access in one place
* All HTTP requests are handled concurrently
* All database access is secured by locks to prevent concurrent access  
to Postgres-DB

## Lessons learned
* Named capturing groups in regexes and how they are used
* PostgreSQL does not know the @parameter syntax for named parameters, only 
$1, $2 and so on are used as positional placeholders, and Npgsql translates 
them before submitting the query. This leads to unhelpful and confusing error 
messages when one or more parameters are accidentally not filled with a 
value before querying.
* Sometimes it's hard to decide if it is better to use Exceptions or return 
values (or both) and where to catch them. If the decision is made inconsistently 
it can be hard to find the right spot in your code quickly after a few days 
have passed since writing it :-D

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
custom curl script MonsterTradingCards.integrationtests.curl.bat for details.

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
