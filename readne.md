# Description
This is a simple PoC user API using .Net for test purpose.
## Original constraint
- Create databse for user with with structure
	- tbl_user
		- userid int
		- namalengkap varchar
		- username varchar
		- password varchar
		- status char
- Create API with these basic functionality
	- ```getDataUser(userid)```
		- this will be used to query information on user data based on userid
		- return list of all user if 'all' is used as parameter
	- ```setDataUser```
		- store user data in database using json/xml payload
		```
		{
		  "userId": 0,
		  "namaLengkap": "string",
		  "username": "string",
		  "password": "string",
		  "status": "string"
		}
		```
	- ```delDataUser(userid)```
		- delete / remove user data based on user id
## API Implementation
### Technology / Component used
- .Net 7 with C#
- Entity Framework
- MS SQL Database
### API functionality checklist
- [x] ```getDataUser```
	- [x] return spesific user data if userid is passed as parameter
	- [x] return list of every user data if `all` is passed as parameter
	- [x] return empty list if no user is found
	- [x] return error message if userid is invalid (should be in int format)
- [x] ```setDataUser```
	- [x] create new user data in database if userid not found or not included in payload
	- [x] update user data if userid found on database
	- [x] check to make sure username is unique before register / update user data
- [x] ```delDataUser```
	- [x] delete user data and return success message if userid found on database
	- [x] return error message if user not found
### What i would change
This is quite a barebone API serve as proof of concept for a test. on real development i would change few things.
- User management should be locked behind authorization. These functions should not be freely accessible by anyone without proper role. I would utilize JWT Token for this.
- i would use GUID for userid instead of int. this will provide unique id with consistent length and not easily guessed (unlike integer/number)
- i would seperate user registration and user data update logic and endpoint instead of only using single call
- i would add some password checking for password creation, so it could be more secure with some rule (ex: minimal length, must contain upper case, lower case, special char, etc.)