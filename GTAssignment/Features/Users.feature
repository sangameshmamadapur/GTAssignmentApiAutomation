Feature: Users

User should be able perform create,delete,get and update operations.


@RemoveUser
Scenario Outline: CreateValidUser-200-Statuscode
Given I set api endpoint for user
When I send post request with valid new user data
Then I should get response status code as <statuscode>
Examples:
	| statuscode |
	| 200        |


Scenario Outline: CreateUser-send-Empty-data-500-statusCode
Given I set api endpoint for user
When I send post request with no data
Then I should get response status code as <statuscode>
Examples:
	| statuscode |
	| 500        |

Scenario Outline: CreateUser-invalid-json-format-data-400-statusCode
Given I set api endpoint for user
When I send post request with invalid json data
Then I should get response status code as <statuscode>
Examples:
	| statuscode |
	| 400        |


@RemoveUser
Scenario Outline: GetUserDetail-200-Statuscode
Given I set api endpoint for user
And I already created user with user name <username>
When I send get request with user name <username>
Then I should get response status code as <statuscode>
Examples:
	| username    | statuscode |
	| testUserGet | 200        |


Scenario Outline: GetUserDetail-404-Statuscode
Given I set api endpoint for user
When I send get request with invalid user name <username>
Then I should get response status code as <statuscode>
Examples:
	| username     | statuscode |
	| notFoundUser | 404        |

@RemoveUser
Scenario Outline: DeleteUserDetail-200-Statuscode
Given I set api endpoint for user
And I already created user with user name <username>
When I send delete request with valid user name <username>
Then I should get response status code as <statuscode>
Examples:
	| username       | statuscode |
	| testUserDelete | 200        |


Scenario Outline: DeleteUserDetail-404-Statuscode
Given I set api endpoint for user
When I send delete request with invalid user name <username>
Then I should get response status code as <statuscode>
Examples:
	| username | statuscode |
	| 123      | 404        |

@RemoveUser
Scenario Outline: UpdateUserDetail-200-Statuscode
Given I set api endpoint for user
And I already created user with user name <username>
When I send put request to update user details for user <username>
Then I should get response status code as <statuscode>
Examples:
	| username       | statuscode |
	| testUserUpdate | 200        |

Scenario Outline: UpdateUserDetail-404-Statuscode
Given I set api endpoint for user
When I send put request to update user details for invalid user <username>
Then I should get response status code as <statuscode>
Examples:
	| username          | statuscode |
	| InvalidUserUpdate | 404        |


@RemoveUser
Scenario Outline: Compare Post user response with get user response
Given I set api endpoint for user
When I send post request with valid new user data
And I send get request to get newly created user
Then I compare post data with get response data