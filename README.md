# Project specification (Compare text web API)

Provide 2 HTTP endpoints that accepts base64-encoded JSON of following format
{"input":"some value to be compared"}

1) <host>/v1/diff/<ID>/left
example: curl -X POST "<host>/v1/diff/<ID>/left" -H "accept: */*" -H "Content-Type: application/custom" -d
"\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\""

2) <host>/v1/diff/<ID>/right
example: curl -X POST "<host>/v1/diff/<ID>/right" -H "accept: */*" -H "Content-Type: application/custom" -
d "\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\""

The provided JSON data needs to be diff-ed and the results shall be available on a third end point
<host>/v1/diff/<ID>

The results shall provide the following info in JSON format.
- If value of the "input" property of diffed JSONs is equal, just return that information saying “inputs were equal”. No need to return
compared values.
- If value of the "input" property of diffed JSONs is not of equal size, just return that information “inputs are of different size”. No need
to return compared values.
- If value of the "input" property of diffed JSONs has the same size, perform a simple diff - return offsets (in both values of the "input"
property) and lengths (in both values of the "input" property) of the differences.

# Technology stack
- .Net 7 Web application (controllers + openApi documentation)
- MemoryCache as a storage for left/right text


# Limitations
Memory Cache cannot be used in production, because storage is going to be lost after rebooting.
I would use database as a storage, especially I would recommend to use Redis with TTL polciy.

# Remarks
Use "application/custom" Content-Type in order to send base64-encoded left & right text values.
