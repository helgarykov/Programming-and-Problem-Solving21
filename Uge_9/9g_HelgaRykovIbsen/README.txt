To compile and execute the programs cat.fsx, tac.fsx and countLinks.fsx I run the following commands:

cat:
	fsharpc --doc:readNWrite.xml -a readNWrite.fs 
	fsharpc -r readNWrite.dll cat.fsx
	mono cat.exe
tac:
	fsharpc --doc:readNWrite.xml -a readNWrite.fs 
	fsharpc -r readNWrite.dll tac.fsx
	mono tac.exe
cl:
	fsharpc --doc:readNWrite.xml -a readNWrite.fs 
	fsharpc -r readNWrite.dll countLinks.fsx
	mono countLinks.exe

2. I ran <cat.fsx> on several txt-files as well as existing programs and got the contents as the output. Fx : 

- <mono cat.exe cat.fsx> returned 

[<EntryPoint>] // allows to put arguments into the function from the command line 
let main args =
    match readNWrite.cat (Array.toList (args)) with    // argumentet args er en array, for at konvertere den til en liste, bruges funktion Array.toList 
    | Some files ->
        printf "%s" files   
        0
    | None ->
        1  

On non-existing files as arguments, <mono cat.exe cat.fsx> throwed an exception. 

3. <mono tac.exe tac.fsx> returned the following output:

             1        
>- enoN |    
0        
    selif "s%" ftnirp        
>- selif emoS |    
    tsiLot.yarrA noitknuf segurb ,etsil ne lit ned eretrevnok ta rof ,yarra ne re sgra tetnemugra //   htiw ))sgra( tsiLot.yarrA( cat.etirWNdaer hctam    
= sgra niam tel
 enil dnammoc eht morf noitcnuf eht otni stnemugra tup ot swolla // ]>tnioPyrtnE<[%   


 3. I ran <countLinks.fsx> on the http://fsharp.org and got the output 82 links. Given an invalid link as input, the function returns None.