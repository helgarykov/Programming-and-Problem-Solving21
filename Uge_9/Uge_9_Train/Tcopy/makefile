cat:
	fsharpc --nologo -a readNWrite.fs 
	fsharpc -r readNWrite.dll cat.fsx
	mono cat.exe
tac:
	fsharpc --nologo -a readNWrite.fs 
	fsharpc -r readNWrite.dll tac.fsx
	mono tac.exe
cl:
	fsharpc --nologo -a readNWrite.fs 
	fsharpc -r readNWrite.dll countLinks.fsx
	mono countLinks.exe