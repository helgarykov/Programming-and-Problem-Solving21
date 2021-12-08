# 9g - generelle kommentarer og overholdelse af afleveringsformat

Jeg kan ikke få tac applikationen til at virke.
Tag et ekstra kig på den, og sørg for at få countLinks til at håndtere exceptions.

# 9g0 - readFile

[+] readNWrite modulet indeholder en readFile funktion
[+] Funktionen returnerer en string option
[+] Funktionen returnerer en string med indholdet af en fil

# 9g1 - cat

[+] cat kalder readFile
[+] Funktionen returnerer en string option
[+] Funktionen returnerer en string med indholdet af en fil
[+] cat applikationen printer op til flere filer i terminalen
[-] cat applikationen crasher med en exception hvis man giver den en fil der ikke eksisterer.
    Man skal generelt aldrig bruge .Value når man arbejder med option typer. I stedet er det bedre at matche på værdien. Så får man aldrig sådan nogle exceptions.

# 9g2 - tac

[+] tac kalder readFile
[+] Funktionen returnerer en string option
[-] tac applikationen printer kun en enkelt fil
[-] tac applikationen printer noget hvis en af filerne ikke eksisterer.
[-] tac applikationen kaster en System.ArgumentOutOfRangeException uanset hvad jeg giver den

# 9g3 - countLinks

[-] countLinks tæller antallet af links forkert. Du tæller en for højt.
[-] Funktionen crasher ikke hvis siden ikke kan nås. (Hint: brug try ... with ... til at fange exceptions)
En smartere måde at matche på en char list hvor de første to elementer er "<a" ville være følgende\:
| '<'\:\:'a'\:\:xs -> ...