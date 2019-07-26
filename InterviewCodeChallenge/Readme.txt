when launching this website it should defaultly go to the /api/followers path, which will instruct you to append a username in order to get the list of followers, as an example:

/api/followers/cowilliams419

Optionally, you can add your own user account to run the query with your own authentication (login not required, it's just for github to handle attacks through abuse of the api)

/api/followers/cowilliams419/myUserName


The api will retrieve up to 5 followers for the given user, and then recursively return up to five followers for each follower, down to 3 levels deep. The return format is a simple json string containing the users id (username) and a collection of their followers, returned in the same format:

{
	"ID":"john",	"Followers":[{"ID":"dwillis","Followers":[]},{"ID":"zeke",	"Followers":[]},{"ID":"hobbeswalsh","Followers":[]},{"ID":	"hamman","Followers":[]},{"ID":"altay","Followers":[]}]
}