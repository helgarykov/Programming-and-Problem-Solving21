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

let htmlString = readUrl url     //teger en hjemmeside som en streng og returnerer det hele som en streng 

let rec countLinks (htmlString : string) : int= 
    let i = htmlString.IndexOf("<a")  
    match i with
    |(-1) -> 0          //når strengen ikke findes, så svarer det til -1.
    |_ -> 1 + countLinks(htmlString.[(i + 1)..]) // 1+ fordi vi tæller antallet af de kald hvor vi finder det vi leder efter (her "<a"), så countLinks leder efter en streng "<a". 


printfn "%A" (countLinks htmlString)
