// Set up a url as a stream
let url2Stream url =
    let uri = System.Uri url
    let request = System.Net.WebRequest.Create uri
    let response = request.GetResponse()
    response.GetResponseStream()

// Read all contents of a web page as a string
let readUrl url =
    let stream = url2Stream url
    let reader = new System.IO.StreamReader(stream)
    reader.ReadToEnd()

let url = "http://fsharp.org "

let htmlString = readUrl url     


///<summary> Given a website as a sting, returns the ammount of links.</summary>
///<param name="htmlString">A website as a strings.</param>
///<returns> Number of links as an integer.</returns> 
let rec countLinks (htmlString : string) : int= 
    let i = htmlString.IndexOf("<a")  
    match i with
    |(-1) -> 0          
    |_ -> 1 + countLinks(htmlString.[(i + 1)..]) 


printfn "%A" (countLinks htmlString)
