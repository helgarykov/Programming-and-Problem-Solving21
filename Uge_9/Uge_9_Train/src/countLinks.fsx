open System.Net
open System
open System.IO

///<summary> Given a link, reads and returns the contents of the website. If the site doesn't exixsts, the function fails silently.</summary>
///<param name = "url" > The link to the desired website.</param> 
///<returns>Returns the content of the website.</returns>
let readUrl (url:string) : string option  =
    try
        let request = WebRequest.Create(Uri(url))
        let response = request.GetResponse()
        let stream = response.GetResponseStream()
        let reader = new StreamReader(stream)
        Some(reader.ReadToEnd())
    with
        | _ -> None


///<summary> Given a link to a website, uses readUrl and returns the amount of links the given website contains. </summary>
///<param name = "url" >The link to the desired website.</param> 
///<returns>Returns number of links the website contains.</returns>
let countLinks (url: string) : int =
    match readUrl url with 
    | Some link ->
        let regex = Text.RegularExpressions.Regex "(?s)<a [^>]*?>(?<text>.*?)</a>"
        regex.Matches(link).Count
    | None -> -1


let url = "http://fshar.org "


printfn "%A" (countLinks url)

