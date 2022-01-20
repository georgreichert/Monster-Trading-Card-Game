@echo off

REM --------------------------------------------------
REM Monster Trading Cards Game Integration Tests
REM --------------------------------------------------
title Monster Trading Cards Game
echo CURL Testing for Monster Trading Cards Game
echo.

REM --------------------------------------------------
echo 1) Create Users (Registration)
REM Create User
curl -X POST http://localhost:10001/users --header "Content-Type: application/json" -d "{\"Username\":\"testuser\", \"Password\":\"testpw\"}"
echo.
curl -X POST http://localhost:10001/users --header "Content-Type: application/json" -d "{\"Username\":\"integrationuser\", \"Password\":\"integrationpw\"}"
echo.
echo.
echo should fail wrong json
curl -X POST http://localhost:10001/users --header "Content-Type: application/json" -d "{\"Username\":\"integrationuser\", \"Password\":\"integrationpw\}"
echo.

REM --------------------------------------------------
echo 2) Login Users
curl -X POST http://localhost:10001/sessions --header "Content-Type: application/json" -d "{\"Username\":\"testuser\", \"Password\":\"testpw\"}"
echo.
curl -X POST http://localhost:10001/sessions --header "Content-Type: application/json" -d "{\"Username\":\"integrationuser\", \"Password\":\"integrationpw\"}"
echo.
echo.
echo should fail wrong json
curl -X POST http://localhost:10001/sessions --header "Content-Type: application/json" -d "{\"Usernme\":\"integrationuser\", \"Password\":\"integrationpw\"}"
echo.

echo should fail:
curl -X POST http://localhost:10001/sessions --header "Content-Type: application/json" -d "{\"Username\":\"testuser\", \"Password\":\"integrationpw\"}"
echo.
echo.

REM --------------------------------------------------
echo 3) create packages (done by "admin")
curl -X POST http://localhost:10001/packages --header "Content-Type: application/json" --header "Authorization: Basic admin-mtcgToken" -d "[{\"Id\":\"e561880c-cb4d-4531-9418-1515bb5b29cc\", \"Name\":\"FireElf\", \"Damage\": 10.0}, {\"Id\":\"34f54800-ec67-46e7-8470-5ef89b909ca0\", \"Name\":\"Kraken\", \"Damage\": 50.0}, {\"Id\":\"5dbd3376-ca48-4186-96e0-16e30a490efe\", \"Name\":\"RegularSpell\", \"Damage\": 20.0}, {\"Id\":\"82142195-a9ec-44af-b551-45888e7b1b79\", \"Name\":\"Ork\", \"Damage\": 45.0}, {\"Id\":\"caf843b4-3df2-4a79-b811-a5bdbc5bc8cf\", \"Name\":\"FireSpell\",    \"Damage\": 25.0}]"
echo.																																																																																		 				    
curl -X POST http://localhost:10001/packages --header "Content-Type: application/json" --header "Authorization: Basic admin-mtcgToken" -d "[{\"Id\":\"a519fad2-57cb-4c09-baab-06f45b03849e\", \"Name\":\"WaterGoblin\", \"Damage\":  9.0}, {\"Id\":\"7f33ddc3-96ad-41e7-802a-50e5c6b91b2a\", \"Name\":\"Dragon\", \"Damage\": 55.0}, {\"Id\":\"3bbdd841-9038-4f29-90bd-3cdb172f86ba\", \"Name\":\"WaterSpell\", \"Damage\": 50.0}, {\"Id\":\"c38ed7d1-c690-41bd-858b-95572753f9e7\", \"Name\":\"Wizzard\", \"Damage\": 55.0}, {\"Id\":\"9d274931-99d1-4401-8182-67924866ea1f\", \"Name\":\"WaterSpell\",   \"Damage\": 23.0}]"
echo.							 				    
curl -X POST http://localhost:10001/packages --header "Content-Type: application/json" --header "Authorization: Basic admin-mtcgToken" -d "[{\"Id\":\"e0a5ba87-6bb7-4e63-93fe-a79b71866af8\", \"Name\":\"WaterGoblin\", \"Damage\":  9.0}, {\"Id\":\"7252017d-3c30-48b4-a3ad-3deb19e4f637\", \"Name\":\"Dragon\", \"Damage\": 55.0}, {\"Id\":\"061fac5e-eac4-450e-bc94-21a7ef920c4a\", \"Name\":\"WaterSpell\", \"Damage\": 50.0}, {\"Id\":\"9b78970a-1803-42cd-ba99-e908cff2dedb\", \"Name\":\"Wizzard\", \"Damage\": 55.0}, {\"Id\":\"84dc86d3-6bff-4377-b847-33c0221c62fc\", \"Name\":\"WaterSpell\",   \"Damage\": 23.0}]"
echo.	
echo.
echo should fail wrong json																																																																																	 				    
curl -X POST http://localhost:10001/packages --header "Content-Type: application/json" --header "Authorization: Basic admin-mtcgToken" -d "[{\"Id\"\"a519fad2-57cb-4c09-baab-06f45b03849e\", \"Name\":\"WaterGoblin\", \"Damage\":  9.0}, {\"Id\":\"7f33ddc3-96ad-41e7-802a-50e5c6b91b2a\", \"Name\":\"Dragon\", \"Damage\": 55.0}, {\"Id\":\"3bbdd841-9038-4f29-90bd-3cdb172f86ba\", \"Name\":\"WaterSpell\", \"Damage\": 50.0}, {\"Id\":\"c38ed7d1-c690-41bd-858b-95572753f9e7\", \"Name\":\"Wizzard\", \"Damage\": 55.0}, {\"Id\":\"9d274931-99d1-4401-8182-67924866ea1f\", \"Name\":\"WaterSpell\",   \"Damage\": 23.0}]"
echo.	
echo.
echo should fail: 1 card UUID already in db																																																																																	 				    
curl -X POST http://localhost:10001/packages --header "Content-Type: application/json" --header "Authorization: Basic admin-mtcgToken" -d "[{\"Id\":\"f8aa5270-0c19-4e6c-a8cf-ce5b58a132e1\", \"Name\":\"WaterGoblin\", \"Damage\":  9.0}, {\"Id\":\"02d23bd1-d472-4c4d-815a-09c4f2625161\", \"Name\":\"Dragon\", \"Damage\": 55.0}, {\"Id\":\"796ea9b7-7902-4eb2-b1ec-8bb806729a2f\", \"Name\":\"WaterSpell\", \"Damage\": 21.0}, {\"Id\":\"4a13aff2-de2a-4e44-84e5-c0c49ad61998\", \"Name\":\"Ork\", \"Damage\": 55.0}, {\"Id\":\"9d274931-99d1-4401-8182-67924866ea1f\", \"Name\":\"WaterSpell\",   \"Damage\": 23.0}]"
echo.																																																																												
echo.
echo.

REM --------------------------------------------------
echo 4) acquire packages
curl -X POST http://localhost:10001/transactions/packages --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d ""
echo.
curl -X POST http://localhost:10001/transactions/packages --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d ""
echo.
curl -X POST http://localhost:10001/transactions/packages --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d ""
echo.
curl -X POST http://localhost:10001/transactions/packages --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d ""
echo.

REM --------------------------------------------------
echo 8) show all acquired cards testuser
curl -X GET http://localhost:10001/cards --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.
echo should fail (no token)
curl -X GET http://localhost:10001/cards 
echo.
echo.

REM --------------------------------------------------
echo 9) show all acquired cards integrationuser
curl -X GET http://localhost:10001/cards --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 10) show unconfigured deck
curl -X GET http://localhost:10001/deck --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/deck --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 11) configure deck
curl -X PUT http://localhost:10001/deck --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "[\"e561880c-cb4d-4531-9418-1515bb5b29cc\", \"34f54800-ec67-46e7-8470-5ef89b909ca0\", \"5dbd3376-ca48-4186-96e0-16e30a490efe\", \"82142195-a9ec-44af-b551-45888e7b1b79\"]"
echo.
curl -X GET http://localhost:10001/deck --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X PUT http://localhost:10001/deck --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "[\"a519fad2-57cb-4c09-baab-06f45b03849e\", \"7f33ddc3-96ad-41e7-802a-50e5c6b91b2a\", \"3bbdd841-9038-4f29-90bd-3cdb172f86ba\", \"c38ed7d1-c690-41bd-858b-95572753f9e7\"]"
echo.
echo.
echo. should fail wrong json
curl -X PUT http://localhost:10001/deck --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "[\"a519fad2-57cb-4c09-baab-06f45b03849e\"\"7f33ddc3-96ad-41e7-802a-50e5c6b91b2a\", \"3bbdd841-9038-4f29-90bd-3cdb172f86ba\", \"c38ed7d1-c690-41bd-858b-95572753f9e7\"]"
echo.
curl -X GET http://localhost:10001/deck --header "Authorization: Basic integrationuser-mtcgToken"
echo.

REM --------------------------------------------------
echo 13) show configured deck different representation
echo testuser
curl -X GET http://localhost:10001/deck?format=plain --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.
echo integrationuser
curl -X GET http://localhost:10001/deck?format=plain --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 14) edit user data
echo.
curl -X GET http://localhost:10001/users/testuser --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/users/integrationuser --header "Authorization: Basic integrationuser-mtcgToken"
echo.
curl -X PUT http://localhost:10001/users/testuser --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Name\": \"Testi Testersson\",  \"Bio\": \"me playin...\", \"Image\": \":-)\"}"
echo.
curl -X PUT http://localhost:10001/users/integrationuser --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "{\"Name\": \"Integratio Testersson\", \"Bio\": \"me codin...\",  \"Image\": \":-D\"}"
echo.
echo.
echo should fail wrong json
curl -X PUT http://localhost:10001/users/integrationuser --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "{\"Name\"\"Integratio Testersson\", \"Bio\": \"me codin...\",  \"Image\": \":-D\"}"
echo.
curl -X GET http://localhost:10001/users/testuser --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/users/integrationuser --header "Authorization: Basic integrationuser-mtcgToken"
echo.

REM --------------------------------------------------
echo 15) stats
curl -X GET http://localhost:10001/stats --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/stats --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 16) scoreboard
curl -X GET http://localhost:10001/score --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 17) battle
start /b "testuser battle" curl -X POST http://localhost:10001/battles --header "Authorization: Basic testuser-mtcgToken"
start /b "integrationuser battle" curl -X POST http://localhost:10001/battles --header "Authorization: Basic integrationuser-mtcgToken"
ping localhost -n 10 >NUL 2>NUL

REM --------------------------------------------------
echo 18) Stats 
echo.
echo testuser
curl -X GET http://localhost:10001/stats --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.
echo integrationuser
curl -X GET http://localhost:10001/stats --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 19) scoreboard
curl -X GET http://localhost:10001/score --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 20) trade
echo check trading deals
curl -X GET http://localhost:10001/tradings --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.
echo create trading deal
curl -X POST http://localhost:10001/tradings --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\": \"35cd5e4b-67ee-4c61-b56b-1d9aabc213b7\", \"CardToTrade\": \"caf843b4-3df2-4a79-b811-a5bdbc5bc8cf\", \"Type\": \"spell\", \"MinimumDamage\": 15}"
echo.
echo.
echo should fail wrong json
curl -X POST http://localhost:10001/tradings --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\" \"35cd5e4b-67ee-4c61-b56b-1d9aabc213b7\", \"CardToTrade\": \"caf843b4-3df2-4a79-b811-a5bdbc5bc8cf\", \"Type\": \"spell\", \"MinimumDamage\": 15}"
echo.
echo.
echo check trading deals
curl -X GET http://localhost:10001/tradings --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/tradings --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.
echo delete trading deals
echo should fail because trading was not posted by integrationuser
curl -X DELETE http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Authorization: Basic integrationuser-mtcgToken"
echo.
curl -X DELETE http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.

REM --------------------------------------------------
echo 21) check trading deals
curl -X GET http://localhost:10001/tradings  --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X POST http://localhost:10001/tradings --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\": \"35cd5e4b-67ee-4c61-b56b-1d9aabc213b7\", \"CardToTrade\": \"caf843b4-3df2-4a79-b811-a5bdbc5bc8cf\", \"Type\": \"spell\", \"MinimumDamage\": 35}"
echo check trading deals
curl -X GET http://localhost:10001/tradings  --header "Authorization: Basic testuser-mtcgToken"
echo.
echo.
echo should fail configure deck with card in trading
curl -X PUT http://localhost:10001/deck --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "[\"caf843b4-3df2-4a79-b811-a5bdbc5bc8cf\", \"34f54800-ec67-46e7-8470-5ef89b909ca0\", \"5dbd3376-ca48-4186-96e0-16e30a490efe\", \"82142195-a9ec-44af-b551-45888e7b1b79\"]"
echo.
echo.
echo try to trade 
echo should fail wrong json
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "a519fad2-57cb-4c09-baab-06f45b03849e\""
echo.
echo.
echo should fail cards not eligible
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"a519fad2-57cb-4c09-baab-06f45b03849e\""
echo.
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"7f33ddc3-96ad-41e7-802a-50e5c6b91b2a\""
echo.
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"9d274931-99d1-4401-8182-67924866ea1f\""
echo.
echo.
echo should work
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"3bbdd841-9038-4f29-90bd-3cdb172f86ba\""
echo.
echo.
echo should fail, trading already done/card already traded
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"3bbdd841-9038-4f29-90bd-3cdb172f86ba\""
echo.
curl -X POST http://localhost:10001/tradings/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d "\"a519fad2-57cb-4c09-baab-06f45b03849e\""
echo.
curl -X GET http://localhost:10001/tradings --header "Authorization: Basic testuser-mtcgToken"
echo.
curl -X GET http://localhost:10001/tradings --header "Authorization: Basic integrationuser-mtcgToken"
echo.
echo.
echo should fail card is in deck
curl -X POST http://localhost:10001/tradings --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\": \"35cd5e4b-67ee-4c61-b56b-1d9aabc213b7\", \"CardToTrade\": \"34f54800-ec67-46e7-8470-5ef89b909ca0\", \"Type\": \"spell\", \"MinimumDamage\": 35}"
echo.
echo.
echo try to sell
curl -X POST http://localhost:10001/sales --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\": \"35cd5e4b-67ee-4c61-b56b-1d9aabc213b7\", \"CardToSell\": \"061fac5e-eac4-450e-bc94-21a7ef920c4a\", \"Coins\": 3}"
echo.
echo.
echo should fail card is in deck
curl -X POST http://localhost:10001/sales --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "{\"Id\": \"36cd5e4b-AAAA-4c61-b56b-1d9aabc213b7\", \"CardToSell\": \"34f54800-ec67-46e7-8470-5ef89b909ca0\", \"Coins\": 35}"
echo.
echo.
echo should fail configure deck with card in sale
curl -X PUT http://localhost:10001/deck --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d "[\"e561880c-cb4d-4531-9418-1515bb5b29cc\", \"061fac5e-eac4-450e-bc94-21a7ef920c4a\", \"5dbd3376-ca48-4186-96e0-16e30a490efe\", \"82142195-a9ec-44af-b551-45888e7b1b79\"]"
echo.
echo.
echo should fail (can't buy from yourself)
curl -X POST http://localhost:10001/sales/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic testuser-mtcgToken" -d ""
echo.
echo.
echo should fail (not enough money)
curl -X POST http://localhost:10001/sales/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic kienboec-mtcgToken" -d ""
echo.
echo.
curl -X GET http://localhost:10001/sales --header "Authorization: Basic testuser-mtcgToken"
echo.
echo should work
curl -X POST http://localhost:10001/sales/35cd5e4b-67ee-4c61-b56b-1d9aabc213b7 --header "Content-Type: application/json" --header "Authorization: Basic integrationuser-mtcgToken" -d ""
echo.
curl -X GET http://localhost:10001/sales --header "Authorization: Basic testuser-mtcgToken"
echo.

REM --------------------------------------------------
echo end...

REM this is approx a sleep 
ping localhost -n 100 >NUL 2>NUL
@echo on
