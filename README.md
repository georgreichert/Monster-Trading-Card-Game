# Monster Trading Card Game

## Design


## Lessons learned
* Named capturing groups in regexes and how they are used
* PostgreSQL does not know the @parameter syntax for named parameters, only 
$1, $2 and so on are used as positional placeholders, and Npgsql translates 
them before submitting the query. This leads to unhelpful and confusing error 
messages when one or more parameters are accidentally not filled with a 
value before querying.
* 

## Unit testing decisions
Battlelogic tested with 42 unit tests. Server only tested with integration 
tests because of the high amount of exception handling involved. Unit tests 
seemed impractical in most cases.

## Unique feature
Before each game one card of both contesting players' decks is chosen randomly 
and marked as a _guarded_ card. If a _guarded_ card is beaten by the opponent's 
card, the mark is removed and the card is returned to the owners deck. The 
round counts as a draw.

## Time spent
| ------------ | ------ |
| Battlelogic: |  ~  7h |   
| Server:      |  ~ 50h |  
| Unit Tests:  |  ~  3h |   
| Sum:         |  ~ 60h |  

## Link to Git
https://github.com/georgreichert/Monster-Trading-Card-Game
