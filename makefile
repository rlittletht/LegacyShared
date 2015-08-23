ODIR=ship
SRCDIR=..

CS_FLAGS=/debug:pdbonly

!ifdef DEBUG
ODIR=debug
CS_FLAGS=$(CS_FLAGS) /define:DEBUG /debug:full /debug+
!endif

target: chdir ole32.dll

full: clean target

clean: 
	-del /q $(ODIR)\*.obj $(ODIR)\*.obj $(ODIR)\*netmodule*  $(ODIR)\*.pdb  $(ODIR)\*.dll  $(ODIR)\*dll* $(ODIR)\*.exe

chdir:
	@-mkdir $(ODIR) > NUL 2>&1
	@cd $(ODIR)  
	@echo Changed directory to $(ODIR)...

AssemblyInfo.netmodule: ..\AssemblyInfo.cs
	csc $(CS_FLAGS) /target:module /out:AssemblyInfo.netmodule ..\AssemblyInfo.cs


ole32.dll: AssemblyInfo.netmodule $(SRCDIR)\ole32.cs
	csc $(CS_FLAGS) /target:library /out:ole32.dll /addmodule:AssemblyInfo.netmodule $(SRCDIR)\ole32.cs
	
        
