# Rubicon-Assignment
Blog Post API

There should not be any issues that I can tell of when using the code, simply clone and run the solution
it was done using an sqlite database located in the solution itself so no specific setup is needed.

I tested the solution in Postman.

Objects returned by the api(examples):

Get:
https://localhost:44393/api/blog
Returns list of all the posts in the db
https://localhost:44393/api/blog/?tag=trends
Returns a list of all the posts that contain the trends tag
https://localhost:44393/api/blog/?tag=trends,2018
Returns a list of all the posts that contain the trends or 2018 tags
https://localhost:44393/api/blog/internet-trends-2018
Returns the post whose slug is internet-trends-2018
https://localhost:44393/api/tag
Returns the list of tags in the db
Post:
https://localhost:44393/api/blog
Body:
{
  "blogPost": {
    "title": "Internet Trends 2018",
    "description": "Ever wonder how?",
    "body": "An opinionated commentary, of the most important presentation of the year",
    "tagList": ["trends", "innovation", "2018"]
  }
}
Creates a new post in the database, generates a new slug for it and returns the created result.
It varifies if all the tags exist in the db
It varifies if the slug is available else it generates a string to attach to the slug
Put:
https://localhost:44393/api/blog/
Body:
  "blogPost": {
    "description": "Ever wonder how not?",
    "tagList": ["trends", "innovation", "2018"]
  }
 If the taglist is changed then it varifies if all the tags exist in the db, if the title is changed
 If the slug is changed it creates a new one and varifies it
 Delete:
 https://localhost:44393/api/blog/internet-trends-2018
 Deletes the post with the given slug
 
 Feel free to test the code however you wish.
