crun:
	fsharpc --doc:rotate.xml -a rotate.fsi rotate.fs
	fsharpc -r rotate.dll blackboxtest.fsx
	mono blackboxtest.exe

	fsharpc --doc:rotate.xml -a rotate.fsi rotate.fs
	fsharpc -r rotate.dll whiteboxtest.fsx
	mono whiteboxtest.exe
	
	fsharpc --doc:rotate.xml -a rotate.fsi rotate.fs
	fsharpc -r rotate.dll game.fsx
	mono game.exe